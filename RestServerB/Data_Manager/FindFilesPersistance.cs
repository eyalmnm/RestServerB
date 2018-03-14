using System;
using RestServerB.dbVirtulization;
using System.Collections.Generic;
using System.Data;
using RestServerB.Managers;

namespace RestServerB.Data_Manager
{
    public class FindFilesPersistance
    {
        public static String password = "Tapuach27072004";
        public static String connectionString = "Provider = Microsoft.Jet.OLEDB.4.0;OLE DB Services=-1;" +
            " Data Source=" + "C:\\projects\\MyDirs\\Omdan\\DB\\Omdan.mdb;" + "Persist Security Info=False" + ";" +
            "Jet OLEDB:Database Password=" + password;

        private DBVirtualizationOleDB dBVirtualizationOleDB;
        private String tempTblName = "Records";

        public Dictionary<String, object> FindFile(String fileNumber)
        {
            DataTable dt;
            String retFileNumber;

            Initializer();

            using (CommandVirtualization command = new CommandVirtualization(SqlDepot.FindRecord()))
            {
                command.Parameters.Add("FileNumber", fileNumber, null);

                try
                {
                    dBVirtualizationOleDB.Execute(command, tempTblName);
                    dt = dBVirtualizationOleDB.Import(tempTblName);

                    if (0 >= dt.Rows.Count)
                    {
                        Console.WriteLine("Record Not Found");
                        return null;
                    }
                    retFileNumber = dt.Rows[0]["FileNumber"].ToString();
                    if ((null != retFileNumber) && (0 < retFileNumber.Trim().Length))
                    {
                        Dictionary<String, object> rowValues = new Dictionary<String, object>();
                        rowValues.Add("Files.FileNumber", dt.Rows[0]["Files.FileNumber"].ToString());
                        rowValues.Add("InsuredList.Name", dt.Rows[0]["InsuredList.Name"].ToString());
                        rowValues.Add("Customers.Name", dt.Rows[0]["Customers.Name"].ToString());
                        rowValues.Add("EmployeeList.Name", dt.Rows[0]["EmployeeList.Name"].ToString());
                        rowValues.Add("Files.SuitNumber", dt.Rows[0]["Files.SuitNumber"].ToString());
                        rowValues.Add("FileStatus.Name", dt.Rows[0]["FileStatus.Name"].ToString());
                        rowValues.Add("Files.CreationDate", dt.Rows[0]["Files.CreationDate"].ToString());
                        return rowValues;
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception thrown{ex.Message}");
                }
                finally
                {
                    dBVirtualizationOleDB.ClearTableFromDataSet("User");
                }
            }
            return null;
        }

        private void Initializer()
        {
            dBVirtualizationOleDB = new DBVirtualizationOleDB();
            dBVirtualizationOleDB.SetConnection(connectionString);
        }

        ~FindFilesPersistance()
        {
            if (null != dBVirtualizationOleDB)
            {
                dBVirtualizationOleDB.CloseConnection();
            }
        }
    }
}