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

        public static String FindRecordQuery()
        {
            return "SELECT Files.FileNumber, InsuredList.Name, Customers.Name, EmployeeList.Name, Files.SuitNumber, FileStatus.Name, Files.CreationDate "+
            "FROM((((Files LEFT JOIN FileCustomerLink ON Files.FilesId = FileCustomerLink.FileId) "+
            "LEFT JOIN Customers ON FileCustomerLink.CustomerId = Customers.CustomersId) "+
            "LEFT JOIN InsuredList ON Files.InsuredId = InsuredList.InsuredListId) "+
            "LEFT JOIN EmployeeList ON Files.MainApprasierId = EmployeeList.EmployeeListId) "+
            "LEFT JOIN FileStatus ON Files.FileStatus = FileStatus.FileStatusId "+
            "WHERE(Files.FileNumber = @FileNumber OR @FileNumber IS NULL) "+
            "AND(InsuredList.Name = @InsuredName OR @InsuredName IS NULL) "+
            "AND(Customers.Name = @CustomerName OR @CustomerName IS NULL) "+
            "AND(EmployeeList.Name = @EmpName OR @EmpName IS NULL) "+
            "AND(Files.SuitNumber = @SuitNumber OR @SuitNumber IS NULL) "+
            "AND(FileStatus.Name = @FileStatusName OR @FileStatusName IS NULL) "+
            "AND((Files.CreationDate >= @CreationDateFrom OR @CreationDateFrom IS NULL) AND(Files.CreationDate <= @CreationDateTo OR @CreationDateTo IS NULL))";
        }

        public static String FindRecordByCreationDate()
        {
            return "SELECT Files.FileNumber, InsuredList.Name, Customers.Name, EmployeeList.Name, Files.SuitNumber, FileStatus.Name, Files.CreationDate " +
                "FROM((((Files LEFT JOIN FileCustomerLink ON Files.FilesId = FileCustomerLink.FileId) LEFT JOIN Customers ON FileCustomerLink.CustomerId = Customers.CustomersId) " +
                "LEFT JOIN InsuredList ON Files.InsuredId = InsuredList.InsuredListId) LEFT JOIN EmployeeList ON Files.MainApprasierId = EmployeeList.EmployeeListId) " +
                "LEFT JOIN FileStatus ON Files.FileStatus = FileStatus.FileStatusId " +
                "WHERE  Files.CreationDate = @CreationDate";
        }

        public static String FindRecordBySuitNumber()
        {
            return "SELECT Files.FileNumber, InsuredList.Name, Customers.Name, EmployeeList.Name, Files.SuitNumber, FileStatus.Name, Files.CreationDate " +
                "FROM((((Files LEFT JOIN FileCustomerLink ON Files.FilesId = FileCustomerLink.FileId) LEFT JOIN Customers ON FileCustomerLink.CustomerId = Customers.CustomersId) " +
                "LEFT JOIN InsuredList ON Files.InsuredId = InsuredList.InsuredListId) LEFT JOIN EmployeeList ON Files.MainApprasierId = EmployeeList.EmployeeListId) " +
                "LEFT JOIN FileStatus ON Files.FileStatus = FileStatus.FileStatusId " +
                "WHERE  Files.SuitNumber = @SuitNumber";
        }

        public static String FindRecordByEmployeeName()
        {
            return "SELECT Files.FileNumber, InsuredList.Name, Customers.Name, EmployeeList.Name, Files.SuitNumber, FileStatus.Name, Files.CreationDate " +
                "FROM((((Files LEFT JOIN FileCustomerLink ON Files.FilesId = FileCustomerLink.FileId) LEFT JOIN Customers ON FileCustomerLink.CustomerId = Customers.CustomersId) " +
                "LEFT JOIN InsuredList ON Files.InsuredId = InsuredList.InsuredListId) LEFT JOIN EmployeeList ON Files.MainApprasierId = EmployeeList.EmployeeListId) " +
                "LEFT JOIN FileStatus ON Files.FileStatus = FileStatus.FileStatusId " +
                "WHERE  EmployeeList.Name = @Employee";
        }

        public static String FindRecordByCustomerName()
        {
            return "SELECT Files.FileNumber, InsuredList.Name, Customers.Name, EmployeeList.Name, Files.SuitNumber, FileStatus.Name, Files.CreationDate " +
                "FROM((((Files LEFT JOIN FileCustomerLink ON Files.FilesId = FileCustomerLink.FileId) LEFT JOIN Customers ON FileCustomerLink.CustomerId = Customers.CustomersId) " +
                "LEFT JOIN InsuredList ON Files.InsuredId = InsuredList.InsuredListId) LEFT JOIN EmployeeList ON Files.MainApprasierId = EmployeeList.EmployeeListId) " +
                "LEFT JOIN FileStatus ON Files.FileStatus = FileStatus.FileStatusId " +
                "WHERE Customers.Name = @Customer";
        }

        public static String FindRecordByInsuredName()
        {
            return "SELECT Files.FileNumber, InsuredList.Name, Customers.Name, EmployeeList.Name, Files.SuitNumber, FileStatus.Name, Files.CreationDate " +
                "FROM((((Files LEFT JOIN FileCustomerLink ON Files.FilesId = FileCustomerLink.FileId) LEFT JOIN Customers ON FileCustomerLink.CustomerId = Customers.CustomersId) " +
                "LEFT JOIN InsuredList ON Files.InsuredId = InsuredList.InsuredListId) LEFT JOIN EmployeeList ON Files.MainApprasierId = EmployeeList.EmployeeListId) " +
                "LEFT JOIN FileStatus ON Files.FileStatus = FileStatus.FileStatusId " +
                "WHERE InsuredList.Name = @Insured";
        }

        public static String FindRecordByFileId()
        {
            return "SELECT Files.FileNumber, InsuredList.Name, Customers.Name, EmployeeList.Name, Files.SuitNumber, FileStatus.Name, Files.CreationDate " + 
                "FROM((((Files LEFT JOIN FileCustomerLink ON Files.FilesId = FileCustomerLink.FileId) LEFT JOIN Customers ON FileCustomerLink.CustomerId = Customers.CustomersId) " +
                "LEFT JOIN InsuredList ON Files.InsuredId = InsuredList.InsuredListId) LEFT JOIN EmployeeList ON Files.MainApprasierId = EmployeeList.EmployeeListId) " +
                "LEFT JOIN FileStatus ON Files.FileStatus = FileStatus.FileStatusId " +
                "WHERE Files.FileNumber = @FileNumber";
        }

        public static String SaveRecord()
        {
            // TODO
            return null;
        }

        public virtual string getNextRecordId()
        {
            return currentRecordId++.ToString(); // TODO
        }
    }
}
