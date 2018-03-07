using System;
using System.Collections.Generic;
using System.Text;

namespace RestServerB.dbVirtulization
{
    class SqlDepot
    {
        private static int currentRecordId = 100;

        public static string LoginUser()
        {
            return "SELECT * FROM EmployeeList WHERE CodeName=@CodeName AND Password=@Password And Active=TRUE";
        }

        public static List<KeyValuePair<string, string>> FindRecord()
        {
            return new List<KeyValuePair<string, string>>(); // TODO
        }

        public static void SaveRecord(List<KeyValuePair<string, string>> recData)
        {
            // TODO
        }

        public virtual string getNextRecordId()
        {
            return currentRecordId++.ToString(); // TODO
        }
    }
}
