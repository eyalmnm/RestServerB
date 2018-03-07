using System;
using System.Collections.Generic;
using System.Text;

namespace RestServerB.dbVirtulization
{
    public class ParamsVirtulization
    {
        public struct ParamElement
        {
            public string name;
            public object value;
            public string typeName;
        }
        List<ParamElement> m_params;

        public ParamsVirtulization()
        {
            m_params = new List<ParamElement>();
        }

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"> parameter name</param>
        /// <param name="value">parameter value</param>
        /// <param name="typeName">use for not standart type</param>
        public void Add(string name, object value, string typeName)
        {
            ParamElement pe = new ParamElement();
            pe.name = name;
            pe.value = value;
            pe.typeName = typeName;
            m_params.Add(pe);
        }

        public void Clear()
        {
            m_params.Clear();
           
        }

        public int Count
        {
            get
            {
                return m_params.Count;
            }
        }

        public ParamElement this[int index]
        {
            get
            {
                return m_params[index];
            }
        }


    }

    public class ColumnVirtualization
    {
        public enum ColumnType
        {
            DateTime,
            String,
            Int,
            LongInteger,
            Byte,
            boolean,
            Single, // dont forget to implement at the dbSql
            Double,
            /// <summary>
            /// only in access
            /// </summary>
            Memo
            //AutoNumber//access
        }

        public struct ColumnItem
        {
            public string Name;
            public ColumnType ColType;        //example : int,varchar,datetime,AUTOINCREMENT(access).
            public object Len;
            public bool AllowNull;   //alow nulls,else write "not null".
            public bool PrimaryKey;  //for primary key write "primary key".(the nullValue must be set to "not null")
            public bool Identity;    //for identity field write "Identity".
            public bool Compressed;//unicode compression of text filed in access.
        }
        List<ColumnItem> m_columns;

        public ColumnVirtualization()
        {
            m_columns = new List<ColumnItem>();
        }

        public void Add(string name, ColumnType colType, object len, bool allowNull, bool primaryKey, bool identity)
        {
            ColumnItem item=new ColumnItem();

            item.Name = name;
            item.ColType = colType;
            item.Len = len;
            item.AllowNull = allowNull;
            item.PrimaryKey = primaryKey;
            item.Identity = identity;
            item.Compressed = false;//default value
            m_columns.Add(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="colType"></param>
        /// <param name="len"></param>
        /// <param name="allowNull"></param>
        /// <param name="primaryKey"></param>
        /// <param name="identity"></param>
        /// <param name="compressed">unicode compression of text filed in access</param>
        public void Add(string name, ColumnType colType, object len, bool allowNull, bool primaryKey, bool identity,bool compressed)
        {
            ColumnItem item = new ColumnItem();

            item.Name = name;
            item.ColType = colType;
            item.Len = len;
            item.AllowNull = allowNull;
            item.PrimaryKey = primaryKey;
            item.Identity = identity;
            item.Compressed = compressed;
            m_columns.Add(item);
        
        }



        public void Clear()
        {
            m_columns.Clear();
        }

        public int Count
        {
            get
            {
                return m_columns.Count;
            }
        }

        public ColumnItem this[int index]
        {
            get
            {
                return m_columns[index];
            }
        }


    }



    public class IndexesVirtualization
    {
       

        List<string> m_indexes;
        string m_indexName;
        bool m_unique;

        public IndexesVirtualization(string indexName,bool unique)
        {
            m_indexes = new List<string>();
            m_indexName = indexName;
            m_unique = unique;
        }

        public void Add(string field)
        {

            m_indexes.Add(field);
        }


        public void Clear()
        {
            m_indexes.Clear();
        }

        public int Count
        {
            get
            {
                return m_indexes.Count;
            }
        }

        public string this[int index]
        {
            get
            {
                return m_indexes[index];
            }
        }
        public string IndexName
        {
            get
            {
                return m_indexName;
            }

        }
        public bool Unique
        {
            get
            {
                return m_unique;
            }
        }
    }



    public class CommandVirtualization : IDisposable
    {
        bool disposed = false;

        /// <summary>
        /// Public implementation of Dispose pattern callable by consumers.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                if (NewColumns != null)
                {
                    NewColumns.Clear();
                    NewColumns = null;
                }
                if (Parameters != null)
                {
                    Parameters.Clear();
                    Parameters = null;
                }
            }
            disposed = true;
        }

        /// <summary>
        /// Default value is false.
        /// Used when setting the adapter schema if needed.
        /// schema is needed when you need to get additional propertys of the datat table(such as column max length)
        /// </summary>
        public bool addSchema=false;

        public enum SqlType
        {
            query,
            nonQuery,
            scalar
        }

        public struct DisconnectedSqlsStruct
        {
            public string sqlQuery;
            public string sqlInsert;
            public string sqlUpdate;
            public string sqlDelete;
        }

        string m_sql;                               //used for connected command
        DisconnectedSqlsStruct m_disconnectedSqls;  //used for disconnected command
        public ParamsVirtulization Parameters;

        public ColumnVirtualization NewColumns;
        public IndexesVirtualization NewIndexes;

        SqlType m_sqlType;

        /// <summary>
        /// Use this constructor when you want:
        /// 1. To add new columns.
        /// 2. To create table.
        /// </summary>
        public CommandVirtualization()
        {
            NewColumns = new ColumnVirtualization();
        }

        /// <summary>
        /// Use this constructor to set indexes to table.
        /// </summary>
        /// <param name="indexName"></param>
        public CommandVirtualization(string indexName, bool unique)
        {
            NewIndexes = new IndexesVirtualization(indexName, unique);
            NewColumns = new ColumnVirtualization();
        }


        /// <summary>
        /// Use this contructor inorder to execute connected command
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="sqlType"></param>
        public CommandVirtualization(string sql,SqlType sqlType)
        {
            Parameters = new ParamsVirtulization();
            m_sql = sql;
            m_sqlType = sqlType;
            NewColumns = new ColumnVirtualization();
        }

        /// <summary>
        /// Use this contructor inorder to execute disconnected command
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <param name="sqlInsert"></param>
        /// <param name="sqlUpdate"></param>
        /// <param name="sqlDelete"></param>
        public CommandVirtualization(string sqlQuery, string sqlInsert, string sqlUpdate, string sqlDelete)
        {
            NewColumns = new ColumnVirtualization();
            Parameters = new ParamsVirtulization();
            m_disconnectedSqls.sqlQuery = sqlQuery;
            m_disconnectedSqls.sqlInsert = sqlInsert;
            m_disconnectedSqls.sqlUpdate = sqlUpdate;
            m_disconnectedSqls.sqlDelete = sqlDelete;
        }


        /// <summary>
        /// Use this contructor inorder to execute disconnected command which need only the query without insert, delete and update.
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <param name="sqlInsert"></param>
        /// <param name="sqlUpdate"></param>
        /// <param name="sqlDelete"></param>
        public CommandVirtualization(string sqlQuery)
        {
            NewColumns = new ColumnVirtualization();
            Parameters = new ParamsVirtulization();
            m_disconnectedSqls.sqlQuery = sqlQuery;
            m_disconnectedSqls.sqlInsert = null;
            m_disconnectedSqls.sqlUpdate = null;
            m_disconnectedSqls.sqlDelete = null;
        } 

        public DisconnectedSqlsStruct DisconnectedSqls
        {
            get
            {
                return m_disconnectedSqls;
            }
        }

        public string ConnectedSql
        {
            get
            {
                return m_sql;
            }
            set
            {
                m_sql = value;
            }
        }

        public SqlType Type
        {
            get
            {
                return m_sqlType;
            }
            set
            {
                m_sqlType = value;
            }
        }

    }
}
