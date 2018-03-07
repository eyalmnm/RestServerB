using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestServerB.Managers
{
    public class ConnectionsManager
    {
        private static Dictionary<string, string> connections = new Dictionary<string, string>();
        //private static string uudi;

        public static string addConnection(string userPassword)
        {
            string uuid = "";
            do
            {
                uuid = System.Guid.NewGuid().ToString();
            } while (connections.ContainsKey(uuid));
            connections.Add(uuid, userPassword);
            return uuid;
        }

        public static bool Remove(string uuid, string password)
        {
            connections.Remove(uuid);
            return true;
        }
    }
}