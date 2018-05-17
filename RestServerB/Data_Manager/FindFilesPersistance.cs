﻿using System;
using RestServerB.dbVirtulization;
using System.Collections.Generic;
using System.Data;
using RestServerB.Utils;
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

        public List<Dictionary<String, object>> FindFile(String fileNumber, long creationDate, String insuredName, String customer,
            String employee, String suitNumber, String fileStatus)
        {
            DataTable dt;
            String retFileNumber;

            Initializer();

            // Init with defualt command
            String sqlCommand = SqlDepot.FindRecordQuery();
            //if (false == StringUtils.IsNullOrEmpty(fileNumber)) sqlCommand = SqlDepot.FindRecordByFileId();
            //else if (false == StringUtils.IsNullOrEmpty(insuredName)) sqlCommand = SqlDepot.FindRecordByInsuredName();
            //else if (false == StringUtils.IsNullOrEmpty(customer)) sqlCommand = SqlDepot.FindRecordByCustomerName();
            //else if (false == StringUtils.IsNullOrEmpty(employee)) sqlCommand = SqlDepot.FindRecordByEmployeeName();
            //else if (false == StringUtils.IsNullOrEmpty(suitNumber)) sqlCommand = SqlDepot.FindRecordBySuitNumber();
            //else if (false == (creationDate <= 0)) sqlCommand = SqlDepot.FindRecordByCreationDate();

                using (CommandVirtualization command = new CommandVirtualization(sqlCommand))
            {
                // Use default command
                //if (false == StringUtils.IsNullOrEmpty(fileNumber)) command.Parameters.Add("FileNumber", fileNumber, null);
                //else if (false == StringUtils.IsNullOrEmpty(insuredName)) command.Parameters.Add("Insured", insuredName, null);
                //else if (false == StringUtils.IsNullOrEmpty(customer)) command.Parameters.Add("Customer", customer, null);
                //else if (false == StringUtils.IsNullOrEmpty(employee)) command.Parameters.Add("Employee", employee, null);
                //else if (false == StringUtils.IsNullOrEmpty(suitNumber)) command.Parameters.Add("SuitNumber", suitNumber, null);
                //else if (false == (creationDate <= 0)) command.Parameters.Add("CreationDate", new DateTime(creationDate), null);
                command.Parameters.Add("FileNumber ", fileNumber, "String");
                command.Parameters.Add("InsuredName", insuredName, "String");
                command.Parameters.Add("CustomerName", customer, "String");
                command.Parameters.Add("EmpName", employee, "String");
                command.Parameters.Add("SuitNumber ", suitNumber, "String");
                command.Parameters.Add("FileStatusName", fileStatus, "String");
                command.Parameters.Add("CreationDateFrom ", creationDate, "PureDateTime");
                //command.Parameters.Add("CreationDateTo ", creationDateTo, "PureDateTime");
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
                        retFileNumber = dt.Rows[0]["FileNumber"].ToString();
                        if ((null != retFileNumber) && (0 < retFileNumber.Trim().Length))
                        {
                            Dictionary<String, object> rowValues = new Dictionary<String, object>();
                            if (DBNull.Value != dt.Rows[0]["FileNumber"]) rowValues.Add("FileNumber", dt.Rows[0]["FileNumber"].ToString());
                            if (DBNull.Value != dt.Rows[0]["InsuredList.Name"]) rowValues.Add("InsuredList.Name", dt.Rows[0]["InsuredList.Name"].ToString());
                            if (DBNull.Value != dt.Rows[0]["Customers.Name"]) rowValues.Add("Customers.Name", dt.Rows[0]["Customers.Name"].ToString());
                            if (DBNull.Value != dt.Rows[0]["EmployeeList.Name"]) rowValues.Add("EmployeeList.Name", dt.Rows[0]["EmployeeList.Name"].ToString());
                            if (DBNull.Value != dt.Rows[0]["SuitNumber"]) rowValues.Add("SuitNumber", dt.Rows[0]["SuitNumber"].ToString());
                            if (DBNull.Value != dt.Rows[0]["FileStatus.Name"]) rowValues.Add("FileStatus.Name", dt.Rows[0]["FileStatus.Name"].ToString());
                            if (DBNull.Value != dt.Rows[0]["CreationDate"]) rowValues.Add("CreationDate", dt.Rows[0]["CreationDate"].ToString());  // DBNull.Value
                            resultsList.Add(rowValues);
                        }
                        else
                        {
                            continue;
                        }
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