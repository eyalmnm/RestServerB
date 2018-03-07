using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;


namespace RestServerB.dbVirtulization
{
    public abstract class DBVirtualizationBase
    {
        public enum QueryType : int { Update = 0, Insert = 1, Delete = 2, Select = 3 };

        //struct tableCommands 
        class tableCommands        //store sql commands for tables which are stored in the database    
        {
            public string selectSql;
            public string updateSql;
            public string insertSql;
            public string deleteSql;
        }
        public DbConnection conn;
        protected bool transactionMode = false;
        protected DbTransaction dbTransaction;
        

        DataSet objDS = null;
        Dictionary<string, tableCommands> m_tablesCommands = new Dictionary<string, tableCommands>();

        public abstract DbDataAdapter GetNewAdapter();
        public abstract DbCommand GetNewCommand();
        public abstract DbParameter GetNewParameter();

        /// <summary>
        /// Return the MAX length of the field of type string.
        /// </summary>
        public  virtual uint MAX_STRING_LEN
        {
            get
            {
                return 8000;
            }
        }

        public abstract void AddParameter(string typeName,string name,object value,DbCommand dbCommand, DbParameter dbParameter);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual string ParseColType2string(ColumnVirtualization.ColumnItem item)
        {
            string retVal = string.Empty;
            switch (item.ColType)
            {
                case ColumnVirtualization.ColumnType.boolean:
                    retVal = "Bit";      
                    break;

                case ColumnVirtualization.ColumnType.DateTime:
                    retVal = "DateTime";
                    break;
            }
            return retVal;
        }

        /// <summary>
        /// Add new column to data table only if it does not exist
        /// Return true if new column was added to the data table, otherwise false.
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="t">Example typeof(int)</param>
        /// <param name="colName"></param>
        /// <remarks>
        /// #1347
        /// </remarks>
        public static bool AddColumn2DataTable(DataTable dt,Type t,string colName)
        {
            bool retVal=false;

            if (dt.Columns.Contains(colName) == false)
            {
                dt.Columns.Add(colName, t);
                retVal = true;
            }
            return retVal;
        }

        /// <summary>
        /// Each database enjine hanldes the auto increament field in different way.
        /// For example in access,the type of the field is AUTOINCREMENT and without the identity key word.
        /// in sql server , the type of the field is as requested(int,bigInt),and with the identity key word.
        /// </summary>
        protected virtual void HandleAutoIncreamentCol(ref string strIdentity, ref string parsedType)
        {
            throw new Exception("HandleIdentity function must be overrided");
        }

        protected virtual void HandleCompressedCol(ref string strCompressed)
        {
            strCompressed = string.Empty;
        }

        /// <summary>
        /// Produce a valid sql string according to the column properties.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string ProduceNewColumnQuery(ColumnVirtualization.ColumnItem item)
        {
            string sqlStr,strAllowNull=string.Empty,strPrimaryKey=string.Empty,strIdentity=string.Empty,strLen=string.Empty;
            string parsedType;  //Stores the type as a string according to database type
            string strCompressed=string.Empty;

            parsedType = ParseColType2string(item);
            //sqlStr += columnNewItem.Name + " " + columnNewItem.Type + columnNewItem.Len + " " + columnNewItem.AllowNull + " " + columnNewItem.PrimaryKey + " " + columnNewItem.Identity;
            if (item.Len != null)
            {
                strLen = "(" + item.Len.ToString() + ")";
            }

            if (item.Compressed == true)//used in access,default value false.
            {
               HandleCompressedCol(ref strCompressed);
            
            }

            if (item.AllowNull == false)
            {
                strAllowNull = "not null";
            }
            if (item.PrimaryKey == true)
            { 
                strPrimaryKey="primary key";
            }

            if (item.Identity == true)
            {
                HandleAutoIncreamentCol(ref strIdentity, ref parsedType);
            }

            sqlStr = item.Name + " " + parsedType + strLen + " " +strCompressed+" " +strAllowNull + " " + strPrimaryKey + " " + strIdentity;
            return sqlStr;
        }

        /// <summary>
        /// Add columns to a table.
        /// return the number of columns added.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="commandColumns"></param>
        /// <returns></returns>
        public int AddColoumnToTable(string tableName, CommandVirtualization commandColumns)
        {
            CommandVirtualization command;
            string sqlStr = string.Empty;
            int retVal = 0;

            //get the column of the table and check if a specefic column exists.
            //if not , add them to the table
            DataTable dtColumn = null;
            ColumnVirtualization.ColumnItem ci;
          
                dtColumn = GetTableColoumns(tableName);
                for (int i = 0; i < commandColumns.NewColumns.Count; i++)
                {
                    ci = commandColumns.NewColumns[i];
                    if (ColumnExists(dtColumn, ci.Name) == false)
                    {
                        sqlStr = "ALTER TABLE " + tableName + " ADD " + ProduceNewColumnQuery(ci);
                        command = new CommandVirtualization(sqlStr, CommandVirtualization.SqlType.nonQuery);
                        Execute(command, false);
                        retVal++;  //#1102
                    }
                }
                    
            return retVal; //#1102
        }

        public void CloseConnection()
        {
            conn.Close();
        }

        /// <summary>
        /// Alter(type\size..)existing columns from a specific table.
        /// Note:In case of type text,the function does not alter the Allow sero length property.
        /// return the number of columns altered.
        /// </summary>
        /// <remarks>
        /// issue 8046
        /// </remarks>
        /// <param name="tableName"></param>
        /// <param name="commandColumns"></param>
        /// <returns></returns>
        public int AlterColoumn(string tableName, CommandVirtualization commandColumns)
        {
            CommandVirtualization command;
            string sqlStr = string.Empty;
            int retVal = 0;

            //get the column of the table and check if a specefic column exists.
            //if not,do nothing.
            DataTable dtColumn = null;
            ColumnVirtualization.ColumnItem ci;

            dtColumn = GetTableColoumns(tableName);
            for (int i = 0; i < commandColumns.NewColumns.Count; i++)
            {
                ci = commandColumns.NewColumns[i];
                if (ColumnExists(dtColumn, ci.Name) == true)
                {
                    sqlStr = "ALTER TABLE " + tableName + " ALTER  COLUMN " + ProduceNewColumnQuery(ci);
                    command = new CommandVirtualization(sqlStr, CommandVirtualization.SqlType.nonQuery);
                    Execute(command, false);
                    retVal++;  //#1102
                }
            }

            return retVal; //#1102
        }



        //public string ProduceNewColumnQuery(ColumnNewItem item)
        //{
        //    string retVal;
        //    string sqlStr = string.Empty;
        //    string parsedType;  //Stores the type as a string according to database type

        //    parsedType = ParseColType2string(item);
        //    retVal = "ALTER TABLE " + tableName + " ADD " + ci.Name + " " + parsedType;
            
        //    //switch (item.ColType)
        //    //{
        //    //    case ColumnType.String:

        //    //        break;


        //    //}
        //    //switch ((SqlDbType)ci.type)
        //    //{
        //    //    case SqlDbType.VarChar:
        //    //        sqlStr = "ALTER TABLE " + tableName + " ADD " + ci.Name + " " + (SqlDbType)ci.type + "(" + ci.len + ")";
        //    //        break;
        //    //    case SqlDbType.DateTime:
        //    //    case SqlDbType.SmallInt:
        //    //    case SqlDbType.BigInt:
        //    //    case SqlDbType.Bit:
        //    //        sqlStr = "ALTER TABLE " + tableName + " ADD " + ci.Name + " " + (SqlDbType)ci.type;
        //    //        break;
     
        

        //    //}
        //    return sqlStr;
        //}




        
        /// <summary>
        /// Example:
        /// CREATE TABLE Persons
        ///(
        ///P_Id int NOT NULL PRIMARY KEY IDENTITY,
        ///LastName varchar(255) NOT NULL,
        ///FirstName varchar(255),
        ///Address varchar(255),
        ///City varchar(255)
        ///)
        /// Usage in acces to create a primary key field autoNumber:
        /// columnNewItem.Name = "efitableId";
        /// columnNewItem.Type = "AUTOINCREMENT";
        /// columnNewItem.PrimaryKey = "primary key";
        /// 
        /// Usage in sql server to create a primary key field autoNumber:
        /// columnNewItem.Name = "eftableId";
        /// columnNewItem.Type = "bigInt";
        /// columnNewItem.NullValue = "not null";
        /// columnNewItem.PrimaryKey = "primary key";
        //  columnNewItem.Identity = "identity";
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns>True if the table was added, false in case the table already exists</returns>
        ///<remarks>
        ///issue #9999
        ///</remarks>
        public bool CreateTable(string tableName, CommandVirtualization commandColumns)
        {
            CommandVirtualization command;
            string sqlStr = string.Empty;
            ColumnVirtualization.ColumnItem columnNewItem;
            Dictionary<string, byte> dicTableNames;
            bool retVal=false;

            //populate the existing table names to a dictionary 
            dicTableNames = GetTablesNamesFromDataSet();

            ////check if the table does not exist already
            if (!IsTableAlreadyExist(tableName, dicTableNames))
            {
                //prepare the sqlStr
                for (int i = 0; i < commandColumns.NewColumns.Count; i++)
                {
                    columnNewItem = commandColumns.NewColumns[i];
                    if (i > 0)
                    {
                        sqlStr += ",";
                    }
                    sqlStr += ProduceNewColumnQuery(columnNewItem);
                }

                sqlStr = "CREATE TABLE " + tableName + " (" + sqlStr + ")";
                command = new CommandVirtualization(sqlStr, CommandVirtualization.SqlType.nonQuery);
                Execute(command, false);
                retVal = true;
            }
            return retVal;

        }


        public void CreateIndex(string tableName, CommandVirtualization commandIndexes)
        {
            CommandVirtualization command;
            string sqlStr = string.Empty;
            string strUnique = string.Empty;


            if (commandIndexes.NewIndexes.Unique)
            {
                strUnique = "UNIQUE ";
            }

            for (int i = 0; i < commandIndexes.NewIndexes.Count; i++)
            {
                if (i > 0)
                {
                    sqlStr += ",";
                }
                sqlStr += commandIndexes.NewIndexes[i];
            }

            sqlStr = "CREATE " + strUnique + " " + "INDEX" + " " + commandIndexes.NewIndexes.IndexName + " " + "ON" + " " + tableName + "(" + sqlStr + ")";
            command = new CommandVirtualization(sqlStr, CommandVirtualization.SqlType.nonQuery);
            Execute(command, false);

        }

        /// <summary>
        /// Remove an index from a table.
        /// Syntax : DROP INDEX index_name ON table_name
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="indexName"></param>
        public void DeleteIndex(string tableName, string indexName)
        {
            string sqlStr = string.Empty;
            CommandVirtualization command;

            sqlStr = "DROP INDEX " +indexName+" ON "+ tableName;
            command = new CommandVirtualization(sqlStr, CommandVirtualization.SqlType.nonQuery);
            Execute(command, false);
        }


        
        public virtual Dictionary<string, byte> GetTablesNamesFromDataSet()
        {
            throw new Exception("HandleIdentity function must be overrided");
        }

        /// <summary>
        /// check if the table already exist in the data set
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public bool IsTableAlreadyExist(string tableName,Dictionary<string,byte>dicTableNames)
        {
            bool retVal = false;

            if (dicTableNames.ContainsKey(tableName))
            {
                retVal = true;
            }

            return retVal;
        }


        protected virtual string GetSqlQueryForSelectingAllTableNamesFromDataSet()
        {
            throw new Exception("GetSqlQueryForSelectingAllTableNamesFromDataSet function must be overrided");
        }


        /// <summary>
        /// Create new table and populate it the dtSource data.
        /// </summary>
        /// <param name="dtSource"></param>
        /// <returns></returns>
        public DataTable CloneDataTableIncludeData(DataTable dtSource)
        {
            DataTable retVal;
            //DataRow drNew;

            retVal = dtSource.Copy();
            //for (int i = 0; i < dtSource.Rows.Count; i++)
            //{
            //    drNew = retVal.NewRow();
            //    retVal.Rows.Add(drNew);
            //    for (int j = 0; j < dtSource.Columns.Count; j++)
            //    {
            //        retVal.Rows[i][j] = dtSource.Rows[i][j];
            //    }

            //}
            return retVal;
        }

        /// <summary>
        /// Create new table and populate it the dtSource data.
        /// </summary>
        /// <param name="dtSource"></param>
        /// <returns></returns>
        /// <remarks>
        /// The function 'CloneDataTableIncludeData' is the same except this is static.
        /// </remarks>
        public static DataTable CloneDataTableIncludeDataStat(DataTable dtSource)
        {
            DataTable retVal;

            retVal = dtSource.Copy();
            return retVal;
        }

        ///// <summary>
        ///// Add new columns as specified in the list to the 'tableName'.
        ///// </summary>
        ///// <param name="tableName"></param>
        ///// <param name="listColumns"></param>
        ///// <returns>Return true if nw columns was added to table</returns>
        //public bool AddColoumnToTable(string tableName, List<DBVirtualizationBase.ColumnItemOld> listColumns)
        //{
        //    CommandVirtualization command;
        //    string sqlStr = string.Empty;
        //    bool retVal=false;

        //    //get the column of the table and check if a specefic column exists.
        //    //if not , add them to the table
        //    DataTable dtColumn = null;
        //    dtColumn = GetTableColoumns(tableName);
        //    if (dtColumn != null)
        //    {
        //        foreach (ColumnItemOld ci in listColumns)
        //        {
        //            if (ColumnExists(dtColumn, ci.Name) == false)
        //            {
        //                sqlStr = GetSqlQueryByDbType(ci, tableName);
        //                command = new CommandVirtualization(sqlStr, CommandVirtualization.SqlType.nonQuery);
        //                Execute(command, false);
        //                retVal = true;  //#1102
        //            }
        //        }
        //    }
        //    return retVal; //#1102
        //}

        /// <summary>
        /// used in altering a table.
        /// return an alter table sql statament according to the db type(ole,sql)
        /// default=oleDb
        /// </summary>
        /// <returns></returns>
        //public virtual string GetSqlQueryByDbType(ColumnVirtualization.ColumnItem ci, string tableName)
        //{
        //    string sqlStr = string.Empty;
        //    switch ((OleDbType)ci.type)
        //    {
        //        case OleDbType.VarChar:
        //            sqlStr = "ALTER TABLE " + tableName + " ADD " + ci.Name + " " + (OleDbType)ci.type + "(" + ci.len + ")";
        //            break;
        //    }

        //    return sqlStr;
        //}


        /// <summary>
        /// chek if a column exist in a data table.
        /// if exist return true.
        /// </summary>
        /// <param name="Table"></param>
        /// <param name="ColumnName"></param>
        /// <returns></returns>
        public  bool ColumnExists(DataTable Table, string name)
        {
            int count = Table.Rows.Count;
            bool bRet = false;
            foreach (DataColumn col in Table.Columns)
            {
                if (col.ColumnName == name)
                {
                    bRet = true;
                    break;
                }
            }
            return bRet;
           
        }


        /// <summary>
        /// get all the columns of the specified table
        /// the data table contains 0 rows.
        /// </summary>
        /// <param name="tableName"></param>
        public  DataTable GetTableColoumns(string tableName)
        {
            CommandVirtualization command;
            DataTable dtColumns = null;
            string sqlStr = "SELECT * from " + tableName + " WHERE 1=2";

            command = new CommandVirtualization(sqlStr, null, null, null);

            Execute(command, "Columns");
            dtColumns = Import("Columns");
            return dtColumns;
        }

        //public int GetColSize(string tableName, string columnName)
        //{
        //    CommandVirtualization command;
        //    DataTable dtColumns = null;
        //    string sqlStr = "SELECT "+columnName+ " from " + tableName + " WHERE 1=2";

        //    command = new CommandVirtualization(sqlStr, null, null, null);

        //    Execute(command, "Column");
        //    dtColumns = Import("Column");
        //    return dtColumns.Columns[columnName].MaxLength
        //}

        protected void HandleSpecialParameter(string typeName, string name, object value, ref DbCommand dbCommand)
        {
            string query = dbCommand.CommandText;
            string tmp=null;
            
            switch (typeName)
            {
                case "ArrayIds":
                    uint[] array = (uint[])value;
                    for (int i = 0; i < array.Length; i++)
                    {
                        if (i > 0)
                        {
                            tmp += ",";
                        }
                        tmp += array[i].ToString();
                    }
                    query = query.Replace("@" + name, tmp);
                    break;
                case "ArrayStr":
                    string[] arrayStr = (string[])value;
                    for (int i = 0; i < arrayStr.Length; i++)
                    {
                        if (i > 0)
                        {
                            tmp += ",";
                        }
                        tmp += "'" + arrayStr[i].ToString() + "'";
                    }
                    query = query.Replace("@" + name, tmp);
                    break;
            }
            dbCommand.CommandText = query;
        }

        /// <summary>
        /// initiaize the conn prtotected variable
        /// </summary>
        public abstract void SetConnection(string connectionString);

        /// <summary>
        /// Set parameters for connectionless mode.
        /// </summary>
        /// <remarks></remarks>
        public abstract void SetParametersForUpdate(ref DbCommand objCmd, DataTable dataTableStructure, QueryType queryType);

        /// <summary>
        /// Set transaction type oleDB,Sql...
        /// </summary>
        public abstract void SetTransactionType();
       
        public DBVirtualizationBase()
        {
            objDS = new DataSet();
            SetTransactionType();
        }

        public void Dispose()
        {
            ClearPool();
            conn.Dispose();
            conn = null;
            objDS.Clear();
            objDS.Dispose();
            objDS = null;
        }

        public void ClearTableFromDataSet(DataTable dt)
        {
            if(dt!=null)
            {
                ClearTableFromDataSet(dt.TableName);
            }
        }

        /// <summary>
        /// Remove the data table from the DataSet and clear the resource.
        /// </summary>
        /// <param name="tblName">Table name to remove</param>
        public void ClearTableFromDataSet(string tblName)
        {
            DataTable dt;

            if (objDS.Tables.Contains(tblName))
            {
                dt = objDS.Tables[tblName];
                objDS.Tables.Remove(tblName);
                dt.Clear();
                dt.Rows.Clear();
                dt.Dispose();
                dt = null;

                //#1380
                if (m_tablesCommands.ContainsKey(tblName))  
                {
                    m_tablesCommands[tblName] = null;
                    m_tablesCommands.Remove(tblName);
                }

                //#1380
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        /// <summary>
        /// Clear all the data tables from the data set and clear the resource.
        /// </summary>
        public void ClearAllTablesFromDataSet()
        {
            List<string> lstTblNames = new List<string>();

            for (int i = 0; i < objDS.Tables.Count; i++)
            {
                lstTblNames.Add(objDS.Tables[i].TableName);
            }

            for (int i = 0; i < lstTblNames.Count; i++)
            { 
                ClearTableFromDataSet(lstTblNames[i]);
            }
        }


        /// <summary>
        /// Clone data rows schema and data.
        /// </summary>
        /// <param name="drSrc"></param>
        /// <returns></returns>
        public DataRow CloneDataRow(DataRow drSrc)
        {
            DataTable dt;
            DataRow drNew;

            dt= drSrc.Table.Clone();
            drNew=dt.NewRow();
            CopyDataRows(drSrc, drNew);
            return drNew;
        }

        /// <summary>
        /// Copy two data rows which have same structure
        /// </summary>
        /// <param name="src">source data row</param>
        /// <param name="dst">destination data row</param>
        public void CopyDataRows(DataRow src, DataRow dst)
        {
            for (int i = 0; i < src.Table.Columns.Count; i++)
            {
                dst[i] = src[i];
            }
        }

        /// <summary>
        /// Copy two data rows which have same structure
        /// </summary>
        /// <param name="src">source data row</param>
        /// <param name="dst">destination data row</param>
        public void CopyDataRowsQuick(DataRow src, DataRow dst)
        {
            dst.ItemArray = src.ItemArray;
        }

        /// <summary>
        /// Copy two data rows which have same structure
        /// </summary>
        /// <param name="src">source data row</param>
        /// <param name="dst">destination data row</param>
        public static void CopyDataRowsStat(DataRow src, DataRow dst)
        {
            for (int i = 0; i < src.Table.Columns.Count; i++)
            {
                dst[i] = src[i];
            }
        }

        public static DataTable MergeDataTables(List<DataTable> dataTableList)
        {
            DataTable retVal=null;

            if (dataTableList.Count > 0)
            {
                retVal = dataTableList[0];
                for (int tableIndex = 1; tableIndex < dataTableList.Count; tableIndex++)
                {
                    DBVirtualizationBase.MergeDataTables(retVal, dataTableList[tableIndex]);
                }
            }
            return retVal;
        }

        /// <summary>
        /// Add data row of dtNew to the end of the dtOriginal data table.
        /// </summary>
        /// <param name="dtOrginal">Data table which data rows should be added to.</param>
        /// <param name="dtNew">Data table which its data rows should be added from</param>
        public static void MergeDataTables(DataTable dtOriginal, DataTable dtNew)
        {
            DataRow[] drArray=new DataRow[dtNew.Rows.Count];
            DataRow drNew;

            dtNew.Rows.CopyTo(drArray, 0);
            for (int i = 0; i < dtNew.Rows.Count; i++)
            {
                drNew = dtOriginal.NewRow();
                dtOriginal.Rows.Add(drNew);
                CopyDataRowsStat(dtNew.Rows[i], drNew);
            }
        }

        /// <summary>
        /// Scan the destination data row and for each column copy the column's value from the 
        /// destination data row
        /// </summary>
        /// <param name="src">source data row</param>
        /// <param name="dst">destination data row</param>
        public void CopyDataRowsByName(DataRow src, DataRow dst)
        {
            string colName;

            for (int i = 0; i < dst.Table.Columns.Count; i++) //Start from 1 - skip the id column
            {
                colName = dst.Table.Columns[i].ColumnName;
                if(src.Table.Columns.Contains(colName))
                {
                    dst[colName]=src[colName];
                }
            }
        }


        public DataRow[] CopyFromDataTableToDataRowArray(DataTable dt)
        {
            List<DataRow> retVal = new List<DataRow>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                retVal.Add(dt.Rows[i]);
            }
            return retVal.ToArray();
        }

        /// <summary>
        /// Get the current time from the server
        /// </summary>
        /// <returns></returns>
        public virtual DateTime GetServerCurrentTime()
        {
            return DateTime.Now;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable GetTableStructure(string tableName)
        {
            string query;
            CommandVirtualization command;
            string tblAutoName;
            DataTable retVal;

            query = "SELECT * FROM " + tableName + " WHERE 1=2";

            command = new CommandVirtualization(query);
            command.addSchema = true;
            ExecuteAutoDtName(command, out tblAutoName);
            retVal = Import(tblAutoName);
            return retVal;
        }

        private bool m_usedStroedProcedures = false;

        /// <summary>
        /// Indicates if we should use stroed procedure during execution.
        /// </summary>
        public bool UseStoredProcedures
        {
            get
            {
                return m_usedStroedProcedures;
            }
            set
            {
                m_usedStroedProcedures = value;
            }
        }

        /// <summary>
        /// Use this function for disconnected commands. The result will be stored as a table in the data set
        /// </summary>
        /// <param name="command"></param>
        /// <param name="tblName">name of the table, which will store the result in the dataSet </param>
        /// <returns> The number of records the new table has.</returns>
        public int Execute(CommandVirtualization command, string tblName)
        {
            int retVal = 0;
            DbDataAdapter adapter=null; 
            DbCommand dbCommand=null;
            tableCommands currTableCommands;

            if (objDS.Tables.Contains(tblName))
            {
                objDS.Tables[tblName].Clear();
                objDS.Tables[tblName].Dispose();
                objDS.Tables.Remove(tblName);
            }

            try
            {
                adapter = GetNewAdapter();
                if(command.addSchema)
                {
                    adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                }

                dbCommand = GetNewCommand();
                currTableCommands = new tableCommands();


                currTableCommands.selectSql = command.DisconnectedSqls.sqlQuery;
                currTableCommands.insertSql = command.DisconnectedSqls.sqlInsert;
                currTableCommands.updateSql = command.DisconnectedSqls.sqlUpdate;
                currTableCommands.deleteSql = command.DisconnectedSqls.sqlDelete;

                if (transactionMode)
                {
                    dbCommand.Transaction = dbTransaction;
                }
                else
                {
                    conn.Open();
                }

                dbCommand.Connection = conn;

                if (m_usedStroedProcedures)
                {
                    dbCommand.CommandType = CommandType.StoredProcedure;
                }

                dbCommand.CommandText = command.DisconnectedSqls.sqlQuery;
                SetParameters(command.Parameters, dbCommand);
                adapter.SelectCommand = dbCommand;
                
                //join query on adama project throw an exception. We omit this row until bug detection
                //adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                 retVal = adapter.Fill(objDS, tblName); //create new table in the data set
            
 
                if (m_tablesCommands.ContainsKey(tblName))
                {
                    m_tablesCommands.Remove(tblName);
                }
      
                m_tablesCommands.Add(tblName, currTableCommands); //add table properties to dictionary
                return retVal;
            }
            catch (System.OutOfMemoryException outOfMemEx)
            {
                throw outOfMemEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
				//#1380
                if (adapter != null)
                {
                    adapter.Dispose();
                }
                if (dbCommand != null)
                {
                    dbCommand.Parameters.Clear();
                    dbCommand.Dispose();
                }
				//End #1380
                if (!transactionMode)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
        }

        /// <summary>
        /// Use this function in case you want the system to create automatic name for the new data table.
        /// tblName will store the new name of the data table which was created in the Data Set.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="tblName"></param>
        /// <returns></returns>
        /// <remarks>
        /// Issue #1185
        /// </remarks>
        public int ExecuteAutoDtName(CommandVirtualization command, out string tblName)
        {
            DateTime dt=DateTime.Now;

            tblName = dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString() + dt.Hour.ToString() + dt.Minute.ToString() + dt.Second.ToString() + dt.Millisecond.ToString();
            System.Threading.Thread.Sleep(20);
            return Execute(command, tblName);
        }

        /// <summary>
        /// Get size of coumns as defined in the database. Use this function for text columns.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="colName"></param>
        /// <returns></returns>
        public int GetColumnSize(string tableName, string colName)
        {
            DataTable dt;

            dt = GetSchema(tableName, colName);
            return Convert.ToInt32(dt.Rows[0]["ColumnSize"]);
        }

        /// <summary>
        /// Get table schema. In case you want to get specific column name schema then set the 'colName' parameter with column name,
        /// otherwise set this paramete with null.  
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="colName"></param>
        /// <returns></returns>
        public DataTable GetSchema(string tableName,string colName)
        {
            DbCommand dbCommand = null;
            string query;

            query="SELECT ";
            if(colName!=null)
            {
                query=query+ colName; 
            }
            else
            {
                query=query+"*";
            }
            query=query+" FROM "+tableName;

            try
            {
                conn.Open();
                dbCommand = GetNewCommand();
                dbCommand.Connection = conn;
                if (m_usedStroedProcedures)
                {
                    dbCommand.CommandType = CommandType.StoredProcedure;
                }
                dbCommand.CommandText = query;
                return dbCommand.ExecuteReader().GetSchemaTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (!transactionMode)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
        }

        public object Execute(CommandVirtualization command, bool Identity)
        {
            object retVal = 0;
            DbCommand dbCommand=null;

            try
            {
                dbCommand = GetNewCommand();
                if (transactionMode)
                {
                    dbCommand.Transaction = dbTransaction;
                }
                else
                {
                    conn.Open();
                }

                dbCommand.Connection = conn;
                if (m_usedStroedProcedures)
                {
                    dbCommand.CommandType = CommandType.StoredProcedure;
                }
                dbCommand.CommandText = command.ConnectedSql;
                SetParameters(command.Parameters, dbCommand);
                switch (command.Type)
                {
                    case CommandVirtualization.SqlType.query:
                        //dbCommand.ExecuteReader().GetSchemaTable();
                        break;

                    case CommandVirtualization.SqlType.nonQuery:
                          retVal = dbCommand.ExecuteNonQuery();
                        if (Identity)
                        {
                            dbCommand.CommandType = CommandType.Text;    //In case we are using strored procedures..
                            dbCommand.CommandText = "SELECT @@Identity";
                            retVal = dbCommand.ExecuteScalar();
                        }
                        break;

                    case CommandVirtualization.SqlType.scalar:
                        retVal = dbCommand.ExecuteScalar();
                        break;
                }
                return retVal;
            }
            catch (System.OutOfMemoryException outOfMemEx)
            {
                throw outOfMemEx;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
				//#1380
                if (dbCommand != null)
                {
                    dbCommand.Parameters.Clear();
                    dbCommand.Dispose();
                    dbCommand = null;
                }
				//End #1380
                if (!transactionMode)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
        }

        public void SetParameters(ParamsVirtulization parameters, DbCommand sqlCommand)
        {
            int i;
            string typeName=null;
            DbParameter dbParam = GetNewParameter(); ;
            
            for (i = 0; i < parameters.Count; i++)
            {
                if (parameters[i].typeName != null)
                {
                    typeName = parameters[i].typeName;
                }
                else
                {
                   Type t = parameters[i].value.GetType();
                   typeName = t.Name;
                }
                AddParameter(typeName, parameters[i].name, parameters[i].value,sqlCommand,dbParam);
            }
        }


        //public void ClearDataTable(string tableName)
        //{
        //    if (objDS.Tables.Contains(tableName))
        //    {
        //        objDS.Tables[tableName].Dispose();
        //        GC.Collect();
        //        GC.WaitForFullGCApproach
        //        objDS.Clear();
        //    }
        //}

        public DataTable Import(string tblName)
        {
            return objDS.Tables[tblName];
        }

        public void BeginTransaction()
        {
            try
            {
                conn.Open();
                dbTransaction = conn.BeginTransaction();
                transactionMode = true;
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                throw ex;
            }
        }

        public bool TransactionMode
        {
            get
            {
                return transactionMode;
            }

        }

        /// <summary>
        /// Ending transaction after commit 
        /// </summary>
        public void EndTransaction()
        {
            transactionMode = false;
            conn.Close();
            dbTransaction = null;
        }

        /// <summary>
        /// commit transaction to the database
        /// </summary>
        public void CommitTransaction()
        {
            try
            {
                dbTransaction.Commit();
            }
            catch (Exception ex1)
            {
                try
                {
                    if (dbTransaction != null)
                    {
                        dbTransaction.Rollback();
                    }
                }
                catch(Exception ex2)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    throw ex2;
                }
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                throw ex1;
            }
        }

        /// <summary>
        /// Rollback last transaction execution
        /// </summary>
        public void RollBackTransaction()
        {
            try
            {
                if (dbTransaction != null)
                {
                    dbTransaction.Rollback();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual void ClearPool()
        {
        }

        //public virtual void ClearConnection()
        //{
        //    objDS.Clear();
        //    objDS.Dispose();
        //    objDS = null;
        //    GC.Collect();
        //}

        /// <summary>
        /// Update specific table in the data set.
        /// all fields must be wrriten in the sql statment for insert and update.
        /// the order of the wrriten fields is according to order they were selected by the select statment.
        /// i.e ,select * : the order is according to the fields order in the table.
        /// 
        /// Notes:
        /// 1. You must not use NOW() in the queries, you should set the DateTime fields directly in the data table.
        ///    If you use NOW() concurreny exception is thrown.
        /// 2. an integer field in the db is refered to int16.
        ///    if you pass as a parameter integer(the default is int32) you will get overflow error.
        /// </summary>
        /// <param name="tblName"></param>
        /// <returns></returns>
        public int Update(string tblName)
        {
            int result = 0;
            tableCommands currentTableCommands;
            DbCommand updateCommand;
            DbCommand deleteCommand;
            DbCommand insertCommand;
            DbDataAdapter dataAdapter;

            try
            {
                updateCommand = GetNewCommand();
                deleteCommand = GetNewCommand();
                insertCommand = GetNewCommand();
                if (m_usedStroedProcedures)
                {
                    updateCommand.CommandType = CommandType.StoredProcedure;
                    deleteCommand.CommandType = CommandType.StoredProcedure;
                    insertCommand.CommandType = CommandType.StoredProcedure;
                }
                dataAdapter = GetNewAdapter();
                currentTableCommands = new tableCommands();

                dataAdapter.UpdateCommand = updateCommand;
                dataAdapter.DeleteCommand = deleteCommand;  
                dataAdapter.InsertCommand = insertCommand;  

                if (transactionMode)
                {
                    dataAdapter.UpdateCommand.Transaction = dbTransaction;
                    dataAdapter.DeleteCommand.Transaction = dbTransaction;
                    dataAdapter.InsertCommand.Transaction = dbTransaction;
                }
                else
                {
                    conn.Open();
                }

                updateCommand.Connection = conn;
                deleteCommand.Connection = conn; 
                insertCommand.Connection = conn; 
                m_tablesCommands.TryGetValue(tblName, out currentTableCommands);
                updateCommand.CommandText = currentTableCommands.updateSql;
                deleteCommand.CommandText = currentTableCommands.deleteSql;  
                insertCommand.CommandText = currentTableCommands.insertSql;      
                SetParametersForUpdate(ref updateCommand, objDS.Tables[tblName], QueryType.Update);
                SetParametersForUpdate(ref deleteCommand, objDS.Tables[tblName], QueryType.Delete);    
                SetParametersForUpdate(ref insertCommand, objDS.Tables[tblName], QueryType.Insert);
                result = dataAdapter.Update(objDS, tblName);
                return result;
            }
            catch (OutOfMemoryException outOfMemE)
            {
                throw outOfMemE;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (!(transactionMode)) 
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
            //return result;
        }
    }
}
