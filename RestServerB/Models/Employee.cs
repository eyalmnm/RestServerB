using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestServerB.Models
{
    // Relacting an item in EmployeeList table
    public class Employee
    {
        public long EmployeeListId { set; get; }
        public long DisplayId { set; get; }
        public string Name { set; get; }
        public bool Active { set; get; }
        public int Job { set; get; }
        public string CodeName { set; get; }
        public string Password { set; get; }
        public int SecurityLevel { set; get; }
        public string LastOpenedFile { set; get; }
        public bool ReminderActive { set; get; }
        public string Address { set; get; }
        public int City { set; get; }
        public int Zip { set; get; }
        public string Email { set; get; }
        public string PhoneHome { set; get; }
        public string Mobile { set; get; }
        public string Fax { set; get; }
        public string PhoneAdditional { set; get; }
        public string Degree { set; get; }
        public string Education { set; get; }
        public string JobDescription { set; get; }
        public DateTime DisableBlockUntil { set; get; }
        public int EmpFilePrecentage { set; get; }
        public string EmpNicName { set; get; }
        public int EmployeeManager { set; get; }
        public int HideReminders { set; get; }
        public string EmpIdNum { set; get; }
        public int DispalyInOpenByList { set; get; }
        public int CurrentOpenedFileId { set; get; }

        public Employee (long employeeListId, long displayId, string name, bool active, int job, 
            string codeName, string password, int securityLevel, string lastOpenedFile, 
            bool reminderActive, string address, int city, int zip, string email, string phoneHome,
            string mobile, string fax, string phoneAdditional, string degree, string education,
            string jobDescription, DateTime disableBlockUntil, int empFilePrecentage, string empNicName,
            int employeeManager, int hideReminders, string empIdNum, int dispalyInOpenByList,
            int currentOpenedFileId)
        {
            this.EmployeeListId = employeeListId;
            this.DisplayId = displayId;
            this.Name = name;
            this.Active = active;
            this.Job = job;
            this.CodeName = codeName;
            this.Password = password;
            this.SecurityLevel = securityLevel;
            this.LastOpenedFile = lastOpenedFile;
            this.ReminderActive = reminderActive;
            this.Address = address;
            this.City = city;
            this.Zip = zip;
            this.Email = email;
            this.PhoneHome = phoneHome;
            this.Mobile = mobile;
            this.Fax = fax;
            this.PhoneAdditional = phoneAdditional;
            this.Degree = degree;
            this.Education = education;
            this.JobDescription = jobDescription;
            this.DisableBlockUntil = disableBlockUntil;
            this.EmpFilePrecentage = empFilePrecentage;
            this.EmpNicName = empNicName;
            this.EmployeeManager = employeeManager;
            this.HideReminders = hideReminders;
            this.EmpIdNum = empIdNum;
            this.DispalyInOpenByList = dispalyInOpenByList;
            this.CurrentOpenedFileId = currentOpenedFileId;
        }
    }
}