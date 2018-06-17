using RestServerB.dbVirtulization;
using System;
using System.Data;

namespace RestServerB.Data_Manager
{
    public class CreateFilesPersistance
    {
        public static String password = "Tapuach27072004";
        public static String connectionString = "Provider = Microsoft.Jet.OLEDB.4.0;OLE DB Services=-1;" +
            " Data Source=" + "C:\\projects\\MyDirs\\Omdan\\DB\\Omdan.mdb;" + "Persist Security Info=False" + ";" +
            "Jet OLEDB:Database Password=" + password;

        private DBVirtualizationOleDB dBVirtualizationOleDB;
        private String tempTblName = "Records";

        public String getFileRecord()
        {
            /*Initializer();
            String sqlCommand = SqlDepot.getNextRecordId();
            using (CommandVirtualization command = new CommandVirtualization(sqlCommand))
            {
                try
                {
                    dBVirtualizationOleDB.Execute(command, tempTblName);
                    DataTable dt = dBVirtualizationOleDB.Import(tempTblName);
                    if (0 >= dt.Rows.Count)
                    {
                        Console.WriteLine("Record Not Found");
                        return null;
                    } else
                    {
                        if (DBNull.Value != dt.Rows[0]["FileNumber"])
                        {
                            return dt.Rows[0]["FileNumber"].ToString();
                        }
                    }
                } catch (Exception ex)
                {
                    Console.WriteLine($"Exception thrown{ex.Message}");
                }
                finally
                {
                    dBVirtualizationOleDB.ClearTableFromDataSet("User");
                }
                return null;
            }*/
            Random rnd = new Random();
            int recId = rnd.Next(1000, 1000000);
            return recId.ToString();
        }

        public bool addNewRecord(String fileNumber, long creationDate, String insuredName, String customer,
            String employee, String suitNumber, String fileStatus)
        {
            DataTable dt;

            Initializer();

            // Init with defualt command
            String sqlCommand = SqlDepot.SaveRecord();
            using (CommandVirtualization command = new CommandVirtualization(sqlCommand))
            {
                // Create DateTime for creationDate
                TimeSpan time = TimeSpan.FromMilliseconds(creationDate);
                DateTime startdate = new DateTime(1970, 1, 1) + time;
                DateTime now = DateTime.Now;

                // Use default command
                command.Parameters.Add("FileNumber ", fileNumber, "String");
                command.Parameters.Add("InsuredName", insuredName, "String");
                command.Parameters.Add("CustomerName", customer, "String");
                command.Parameters.Add("EmpName", employee, "String");
                command.Parameters.Add("SuitNumber ", suitNumber, "String");
                command.Parameters.Add("FileStatusName", fileStatus, "String");
                command.Parameters.Add("CreationDateFrom ", startdate, "PureDateTime");
                command.Parameters.Add("CreationDateTo ", now, "PureDateTime");

                try
                {
                    int retVal = dBVirtualizationOleDB.Execute(command, tempTblName);
                    if (0 < retVal)
                    {
                        return true;
                    } else
                    {
                        return false;
                    }
                } catch (Exception ex)
                {
                    Console.WriteLine($"Exception thrown{ex.Message}");
                }
                finally
                {
                    dBVirtualizationOleDB.ClearTableFromDataSet("User");
                }
                return false;
            }
        }

        private void Initializer()
        {
            dBVirtualizationOleDB = new DBVirtualizationOleDB();
            dBVirtualizationOleDB.SetConnection(connectionString);
        }

        ~CreateFilesPersistance()
        {
            if (null != dBVirtualizationOleDB)
            {
                dBVirtualizationOleDB.CloseConnection();
            }
        }
    }
}