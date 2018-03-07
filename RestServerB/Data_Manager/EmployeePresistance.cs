using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestServerB.dbVirtulization;
using RestServerB.Managers;
using System.Data;

namespace RestServerB.Data_Manager
{
    public class EmployeePersistance
    {
        public static string password = "Tapuach27072004";
        public static string connectionString = "Provider = Microsoft.Jet.OLEDB.4.0;OLE DB Services=-1;" +
            " Data Source=" + "..\\..\\..\\..\\DB\\Omdan.mdb;" + "Persist Security Info=False" + ";" +
            "Jet OLEDB:Database Password=" + password;

        private DBVirtualizationOleDB dBVirtualizationOleDB;

        public EmployeePersistance()
        {
            dBVirtualizationOleDB = new DBVirtualizationOleDB();
            dBVirtualizationOleDB.SetConnection(connectionString);
        }

        public bool Logout(string uuid, string password)
        {
            return ConnectionsManager.Remove(uuid, password);
        }

        public string Login(string name, string password)
        {
            DataTable dt;
            string codeName;

            using (CommandVirtualization command = new CommandVirtualization(SqlDepot.LoginUser()))
            {
                command.Parameters.Add("CodeName", name, null);
                command.Parameters.Add("Password", password, null);

                try
                {
                    dBVirtualizationOleDB.Execute(command, "User");
                    dt = dBVirtualizationOleDB.Import("User");
                    codeName = dt.Rows[0]["CodeName"].ToString();
                    if ((null != codeName) && (0 < codeName.Trim().Length))
                    {
                        return ConnectionsManager.addConnection(password);
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

        ~EmployeePersistance()
        {
            if (null != dBVirtualizationOleDB)
            {
                dBVirtualizationOleDB.CloseConnection();
            }
        }
    }

}