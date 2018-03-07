using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data.Common;
using System.Data;

namespace RestServerB.dbVirtulization
{
    public class DBVirtualizationOleDB : DBVirtualizationBase
    {
        public override void SetConnection(string connectionString)
        {
            try
            {
                conn = new OleDbConnection(connectionString);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override DbDataAdapter GetNewAdapter()
        {
            return new OleDbDataAdapter();
        }

        public override DbCommand GetNewCommand()
        {
            return new OleDbCommand();
        }

        public override void SetTransactionType()
        {
            OleDbTransaction oleTransaction = null;
            dbTransaction = oleTransaction;
        }


        /// <summary>
        /// When we close the connection and the application is still open the the MDB file is closed only after ~60 seconds (becuase the connection is still in the pool).
        /// If you want  to force the immediate close then call to this function after calling the 'Close' and 'Dispose' functions.
        /// </summary>
        public override void ClearPool()
        {
            OleDbConnection.ReleaseObjectPool();
            GC.Collect();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="dbCommand"></param>
        /// <param name="dbParameter"></param>
        /// <remarks>
        /// Adding ArrayIds parameter which is array of unsigned integer that should be insert into sql query
        /// used for into statement
        /// </remarks>
        public override void AddParameter(string typeName, string name, object value, DbCommand dbCommand, DbParameter dbParameter)
        {
            OleDbType odt=OleDbType.VarChar;
            OleDbParameter oleParam = (OleDbParameter)dbParameter;
            OleDbCommand oleDbCommand = (OleDbCommand)dbCommand;
            bool ignoreParam = false;

            switch (typeName)
            {
                case "ArrayIds":
                case "ArrayStr":
                    ignoreParam = true;
                    HandleSpecialParameter(typeName,name, value, ref dbCommand);
                    break;

                case "String":
                    odt = OleDbType.VarChar;
                    break;

                case "Byte":
                    odt = OleDbType.TinyInt;
                    break;

                case "Int32":
                    odt = OleDbType.Integer;
                    break;

                case "UInt32":
                    odt = OleDbType.BigInt;
                    break;

                case "UInt64":
                    odt = OleDbType.UnsignedBigInt;
                    break;

                case "DateTime":
                    odt = OleDbType.DBDate;
                    break;
                case "PureDateTime":
                    odt = OleDbType.Date;
                    break;

                case "Boolean":
                    odt = OleDbType.Boolean;
                    break;

                case "Decimal":
                case "Single":
                    odt = OleDbType.Decimal;
                    break;

                default:
                    odt = OleDbType.VarChar;
                    break;
            }

            if (ignoreParam == false)  //parameter is of special type - ignore
            {
                oleParam = oleDbCommand.Parameters.Add("@" + name, odt);
                if (value == null)
                {
                    oleParam.Value = DBNull.Value;
                }
                else
                {
                    oleParam.Value = value;
                }
            }
        }

        public override DbParameter GetNewParameter()
        {
            return new OleDbParameter();
        }

        /// <summary>
        /// set parameters for connectionless mode.
        /// </summary>
        /// <remarks></remarks>
        public override void SetParametersForUpdate(ref DbCommand dbCommand, DataTable dataTableStructure, QueryType queryType)
        {
            string idFieldName = "";
            string fieldName = "";
            OleDbType odt;
            OleDbParameter objParam;
            OleDbCommand oleDbCommand = (OleDbCommand)dbCommand;

            switch (queryType)
            {
                //Delete query only needs the id field name to commit
                // the delete operation.
                case QueryType.Delete:
                    idFieldName = dataTableStructure.Columns[0].ColumnName;
                    odt = GetParameterType(dataTableStructure.Columns[0].DataType.Name);
                    objParam = oleDbCommand.Parameters.Add("@" + idFieldName, odt);
                    objParam.SourceColumn = idFieldName;
                    objParam.SourceVersion = DataRowVersion.Current;
                    break;

                //Insert query sets all paramters and ignors the id column
                // (first column).
                case QueryType.Insert:
                    for (int i = 1; i < dataTableStructure.Columns.Count; i++)
                    {
                        fieldName = dataTableStructure.Columns[i].ColumnName;
                        odt = GetParameterType(dataTableStructure.Columns[i].DataType.Name);
                        objParam = oleDbCommand.Parameters.Add("@" + fieldName, odt);
                        objParam.SourceColumn = fieldName;
                        objParam.SourceVersion = DataRowVersion.Current;
                    }
                    break;

                //Update query sets all columns as parameters and the last
                // parameter is the id (first column).
                case QueryType.Update:
                    for (int i = 1; i < dataTableStructure.Columns.Count; i++)
                    {
                        fieldName = dataTableStructure.Columns[i].ColumnName;
                        odt =GetParameterType(dataTableStructure.Columns[i].DataType.Name);
                        objParam = oleDbCommand.Parameters.Add("@" + fieldName, odt);
                        objParam.SourceColumn = fieldName;
                        objParam.SourceVersion = DataRowVersion.Current;
                    }
                    idFieldName = dataTableStructure.Columns[0].ColumnName;
                    odt = GetParameterType(dataTableStructure.Columns[0].DataType.Name);
                    objParam = oleDbCommand.Parameters.Add("@" + idFieldName, odt);
                    objParam.SourceColumn = idFieldName;
                    objParam.SourceVersion = DataRowVersion.Current;
                    break;
            }
        }


        public override string ParseColType2string(ColumnVirtualization.ColumnItem item)
        {
            string retVal = string.Empty;

            switch (item.ColType)
            {
                case ColumnVirtualization.ColumnType.String:
                    retVal = "Text";
                    break;
                case ColumnVirtualization.ColumnType.Byte:
                    retVal = "Byte";
                    break;
                case ColumnVirtualization.ColumnType.Int:
                    retVal = "smallint";
                    break;
                case ColumnVirtualization.ColumnType.LongInteger:
                    retVal = "integer";
                    break;
                case ColumnVirtualization.ColumnType.Single:
                    retVal = "single";
                    break;
                case ColumnVirtualization.ColumnType.Double:
                    retVal = "double";
                    break;
                case ColumnVirtualization.ColumnType.Memo://#9366 - added new type
                    retVal = "MEMO";
                    break;


                //case ColumnVirtualization.ColumnType.AutoNumber:
                //    retVal = "AUTOINCREMENT";
                //    break;
                default:
                    retVal=base.ParseColType2string(item);
                    break;
            }
            return retVal;
        }

        /// <summary>
        /// Each database enjine hanldes the auto increament field in different way.
        /// For example in access,the type of the field is AUTOINCREMENT and without the identity key word.
        /// in sql server , the type of the field is as requested(int,bigInt),and with the identity key word.
        /// </summary>
        protected override void HandleAutoIncreamentCol(ref string strIdentity, ref string parsedType)
        {
            parsedType = "AUTOINCREMENT";
        }

        protected override void HandleCompressedCol(ref string strCompressed)
        {
            strCompressed = "WITH COMPRESSION";
        }

        public override Dictionary<string, byte> GetTablesNamesFromDataSet()
        {
            Dictionary<string, byte> dicTablesNames = new Dictionary<string, byte>();
            DataTable dtTableNames;

            //indicates if the connection was opened by this function.
            //if so,then close the connection at the end. 
            bool flgConOpenedByFunction = false;

            // We only want user tables, not system tables
            string[] restrictions = new string[4];
            restrictions[3] = "Table";

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                flgConOpenedByFunction = true;
            }
            dtTableNames=conn.GetSchema("Tables", restrictions);

            for (int i = 0; i < dtTableNames.Rows.Count; i++)
            {
                dicTablesNames.Add(dtTableNames.Rows[i][2].ToString(), 0);
            }

            if (flgConOpenedByFunction)
            {
                conn.Close();
            }
            return dicTablesNames;
        }



        /// <summary>
        /// gets type name (string) and returns the
        /// OleDBType referse to hte given type.
        /// </summary>
        /// <param name="paramName"></param>
        /// <returns></returns>
        /// <remarks>
        /// Efi issue #8063 - We parse "DateTime" to  OleDbType.Date instead of  OleDbType.DbDate
        ///                   becuase we are using this function only whe updating table in disconnected mode.
        ///                   In this mode we want to date to be in long format which contain date+time.
        /// </remarks>
        private OleDbType GetParameterType(string paramTypeName)
        {
            OleDbType odt;

            switch (paramTypeName)
            {
                case "String":
                    odt = OleDbType.VarChar;
                    break;
                case "Byte":
                    odt = OleDbType.TinyInt;
                    break;
                case "Int16":
                    odt = OleDbType.Integer;
                    break;
                case "Int32":
                    odt = OleDbType.Integer;
                    break;
                case "Int64":
                    odt = OleDbType.BigInt;
                    break;
                case "UInt32":
                    odt = OleDbType.UnsignedInt;
                    break;
                case "UINT32":
                    odt = OleDbType.UnsignedInt;
                    break;
                case "UInt64":
                    odt = OleDbType.UnsignedBigInt;
                    break;
                case "DateTime": //#8063
                    //odt = OleDbType.DbDate;
                    odt = OleDbType.Date;
                    break;
                case "PureDate":
                    odt = OleDbType.DBDate;
                    break;
                case "Boolean":
                    odt = OleDbType.Boolean;
                    break;
                case "Single":
                    odt = OleDbType.Single;
                    break;
                case "Double":
                    odt = OleDbType.Double;
                    break;
                case  "Decimal":
                    odt = OleDbType.Decimal;
                    break;

                default:
                    odt = OleDbType.IUnknown; //object
                    break;
            }

            return odt;
        }
    }
}

