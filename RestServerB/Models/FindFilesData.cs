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
        private long CreationDate { get; set; }
        private String InsuredName { get; set; }
        private String Customer { get; set; }
        private String Employee { get; set; }
        private String SuitNumber { get; set; }
        private String FileStatus { get; set; }

        public FindFilesData(String fileNumber, String creationDate, String insuredName, String customer, 
            String employee, String suitNumber, String fileStatus)
        {
            Console.WriteLine($"Searching: {fileNumber} {creationDate} {insuredName} {customer} {employee} {suitNumber} {fileStatus}");
            this.FileNumber = StringUtils.IsNullOrEmpty(fileNumber) ? null : fileNumber;
            this.CreationDate = long.Parse(creationDate);
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

        public long getCreationDate()
        {
            return this.CreationDate;
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