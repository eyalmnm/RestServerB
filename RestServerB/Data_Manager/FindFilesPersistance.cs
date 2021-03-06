﻿using System;
using RestServerB.dbVirtulization;
using System.Collections.Generic;
using System.Data;
using RestServerB.Utils;

// Ref: https://stackoverflow.com/questions/221732/datetime-null-value

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

        public List<Dictionary<String, object>> FindFile(String fileNumber, String creationDateFrom, String creationDateTo, String insuredName, String customer,
            String employee, String suitNumber, String fileStatus)
        {
            DataTable dt;

            Initializer();

            // Init with defualt command
            String sqlCommand = SqlDepot.FindRecordQuery();
            using (CommandVirtualization command = new CommandVirtualization(sqlCommand))
            {
                // Create DateTime for creationDate
                Nullable<DateTime> startdate = null;
                Nullable<DateTime> endDate = null;
                if (false == StringUtils.IsNullOrEmpty(creationDateFrom)) {
                    long creationDateL = long.Parse(creationDateFrom);
                    TimeSpan time = TimeSpan.FromMilliseconds(creationDateL);
                    startdate = new DateTime(1970, 1, 1) + time;
                }
                if (false == StringUtils.IsNullOrEmpty(creationDateTo))
                {
                    long endDateL = long.Parse(creationDateTo);
                    TimeSpan timeEnd = TimeSpan.FromMilliseconds(endDateL);
                    endDate = new DateTime(1970, 1, 1) + timeEnd;
                }
                else
                {
                    if (false == StringUtils.IsNullOrEmpty(creationDateFrom))
                    {
                        endDate = DateTime.Now;
                    }
                }

                // Use default command
                command.Parameters.Add("FileNumber ", fileNumber, "String");
                command.Parameters.Add("InsuredName", insuredName, "String");
                command.Parameters.Add("CustomerName", customer, "String");
                command.Parameters.Add("EmpName", employee, "String");
                command.Parameters.Add("SuitNumber ", suitNumber, "String");
                command.Parameters.Add("FileStatusName", fileStatus, "String");
                if (true == startdate.HasValue)
                {
                    command.Parameters.Add("CreationDateFrom ", startdate.Value, "PureDateTime");
                }
                else
                {
                    command.Parameters.Add("CreationDateFrom ", null, "PureDateTime");
                }
                if (true == endDate.HasValue) {
                    command.Parameters.Add("CreationDateTo ", endDate.Value, "PureDateTime");
                }
                else
                {
                    command.Parameters.Add("CreationDateTo ", null, "PureDateTime");
                }
                try
                {
                    dBVirtualizationOleDB.Execute(command, tempTblName);
                    dt = dBVirtualizationOleDB.Import(tempTblName);

                    if (0 >= dt.Rows.Count)
                    {
                        Console.WriteLine("Record Not Found");
                        return null;
                    }
                    List<Dictionary<String, object>> resultsList = new List<Dictionary<String, object>>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {                      
                        Dictionary<String, object> rowValues = new Dictionary<String, object>();
                        if (DBNull.Value != dt.Rows[i]["FileNumber"]) rowValues.Add("FileNumber", dt.Rows[i]["FileNumber"].ToString());
                        if (DBNull.Value != dt.Rows[i]["InsuredList.Name"]) rowValues.Add("InsuredList.Name", dt.Rows[i]["InsuredList.Name"].ToString());
                        if (DBNull.Value != dt.Rows[i]["Customers.Name"]) rowValues.Add("Customers.Name", dt.Rows[i]["Customers.Name"].ToString());
                        if (DBNull.Value != dt.Rows[i]["EmployeeList.Name"]) rowValues.Add("EmployeeList.Name", dt.Rows[i]["EmployeeList.Name"].ToString());
                        if (DBNull.Value != dt.Rows[i]["SuitNumber"]) rowValues.Add("SuitNumber", dt.Rows[i]["SuitNumber"].ToString());
                        if (DBNull.Value != dt.Rows[i]["FileStatus.Name"]) rowValues.Add("FileStatus.Name", dt.Rows[i]["FileStatus.Name"].ToString());
                        if (DBNull.Value != dt.Rows[i]["CreationDate"]) rowValues.Add("CreationDate", dt.Rows[i]["CreationDate"].ToString());  // DBNull.Value
                        resultsList.Add(rowValues);               
                    }
                    return resultsList;
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