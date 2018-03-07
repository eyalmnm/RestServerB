using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestServerB.Models
{
    // Relacting an item in EmployeeList table
    public class Employee
    {
        public long EmployeeListId { get; }
        public long DisplayId { get; }
        public string Name { get; }
        public bool Active { get; }
        public int Job { get; }
        public string CodeName { get; }
        public string Password { get; }
        public int SecurityLevel { get; }
        public string LastOpenedFile { get; }
        public bool ReminderActive { get; }
        public string Address { get; }
        public int City { get; }
        public int Zip { get; }
        public string Email { get; }
        public string PhoneHome { get; }
        public string Mobile { get; }
        public string Fax { get; }
        public string PhoneAdditional { get; }
        public string Degree { get; }
        public string Education { get; }
        public string JobDescription { get; }
        public DateTime DisableBlockUntil { get; }
        public int EmpFilePrecentage { get; }
        public string EmpNicName { get; }
        public int EmployeeManager { get; }
        public int HideReminders { get; }
        public string EmpIdNum { get; }
        public int DispalyInOpenByList { get; }
        public int CurrentOpenedFileId { get; }

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