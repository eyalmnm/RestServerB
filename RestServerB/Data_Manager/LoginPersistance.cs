using System;
using RestServerB.dbVirtulization;
using System.Data;
using RestServerB.Managers;
using RestServerB.MyConfig;


namespace RestServerB.Data_Manager
{
    public class LoginPersistance
    {
        public static String password = "Tapuach27072004";
        public static String connectionString = "Provider = Microsoft.Jet.OLEDB.4.0;OLE DB Services=-1;" +
            " Data Source=" + CsConstatnts.ACCESS_DB_BASE_PATH + "Omdan.mdb;" + "Persist Security Info=False" + ";" +
            "Jet OLEDB:Database Password=" + password;
        
        private DBVirtualizationOleDB dBVirtualizationOleDB;

        public String Login(String name, String password)
        {
            DataTable dt;
            String codeName;

            Initializer();

            using (CommandVirtualization command = new CommandVirtualization(SqlDepot.LoginUser()))
            {
                command.Parameters.Add("CodeName", name, null);
                command.Parameters.Add("Password", password, null);

                try
                {
                    dBVirtualizationOleDB.Execute(command, "User");
                    dt = dBVirtualizationOleDB.Import("User");
                    if (0 >= dt.Rows.Count)
                    {
                        Console.WriteLine("User Not Found");
                        return null;
                    }
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

        private void Initializer()
        {
            dBVirtualizationOleDB = new DBVirtualizationOleDB();
            dBVirtualizationOleDB.SetConnection(connectionString);
        }

        public bool Logout(String uuid, String password)
        {
            return ConnectionsManager.Remove(uuid, password);
        }

        ~LoginPersistance()
        {
            if (null != dBVirtualizationOleDB)
            {
                dBVirtualizationOleDB.CloseConnection();
            }
        }
    }
}