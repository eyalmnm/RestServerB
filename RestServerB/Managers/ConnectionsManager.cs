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

        // Remove items by Value (userPassword)
        // https://stackoverflow.com/questions/1636885/remove-item-in-dictionary-based-on-value
        public static string addConnection(string userPassword)
        {
            if(true == connections.ContainsValue(userPassword))
            {
                foreach (var item in connections.Where(kvp => kvp.Value == userPassword).ToList())
                {
                    connections.Remove(item.Key);
                }
            }
            string uuid = "";
            do
            {
                uuid = System.Guid.NewGuid().ToString();
            } while (connections.ContainsKey(uuid));
            connections.Add(uuid, userPassword);
            return uuid;
        }

        public static bool IsExist(String uuid)
        {
            return connections.ContainsKey(uuid);
        }

        public static bool Remove(string uuid, string password)
        {
            connections.Remove(uuid);
            return true;
        }
    }
}