using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

        public FindFilesData(String fileNumber, long creationDate, String insuredName, String customer, 
            String employee, String suitNumber, String fileStatus)
        {
            Console.WriteLine($"Searching: {fileNumber} {creationDate} {insuredName} {customer} {employee} {suitNumber} {fileStatus}");
            this.FileNumber = fileNumber;
            this.CreationDate = creationDate;
            this.InsuredName = insuredName;
            this.Customer = customer;
            this.Employee = employee;
            this.SuitNumber = suitNumber;
            this.FileStatus = fileStatus;
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