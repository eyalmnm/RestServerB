using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestServerB.Utils;

namespace RestServerB.Models
{
    public class FindFilesData
    {
        private String FileNumber { get; set; }
        private String CreationDateStart { get; set; }
        private String CreationDateEnd { get; set; }
        private String InsuredName { get; set; }
        private String Customer { get; set; }
        private String Employee { get; set; }
        private String SuitNumber { get; set; }
        private String FileStatus { get; set; }

        public FindFilesData(String fileNumber, String creationDateStart, String creationDateEnd, String insuredName, String customer, 
            String employee, String suitNumber, String fileStatus)
        {
            Console.WriteLine($"Searching: {fileNumber} {creationDateStart} {creationDateEnd} {insuredName} {customer} {employee} {suitNumber} {fileStatus}");
            this.FileNumber = StringUtils.IsNullOrEmpty(fileNumber) ? null : fileNumber;
            this.CreationDateStart = StringUtils.IsNullOrEmpty(creationDateStart) ? null : creationDateStart;
            this.CreationDateEnd = StringUtils.IsNullOrEmpty(creationDateEnd) ? null : creationDateEnd;
            this.InsuredName = StringUtils.IsNullOrEmpty(insuredName) ? null : insuredName;
            this.Customer = StringUtils.IsNullOrEmpty(customer) ? null : customer;
            this.Employee = StringUtils.IsNullOrEmpty(employee) ? null : employee;
            this.SuitNumber = StringUtils.IsNullOrEmpty(suitNumber) ? null : suitNumber;
            this.FileStatus = StringUtils.IsNullOrEmpty(fileStatus) ? null : fileStatus;
        }

        public String getFileNumber()
        {
            return this.FileNumber;
        }

        public String getCreationDateStart()
        {
            return this.CreationDateStart;
        }

        public String getCreationDateEnd()
        {
            return this.CreationDateEnd;
        }

        public String getInsuredName()
        {
            return this.InsuredName;
        }

        public String getCustomer()
        {
            return this.Customer;
        }

        public String getEmployee()
        {
            return this.Employee;
        }

        public String getSuitNumber()
        {
            return this.SuitNumber;
        }

        public String getFileStatus()
        {
            return this.FileStatus;
        }
    }
}