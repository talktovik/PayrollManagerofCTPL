using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.MainClasses
{
    public class Employee
    {
        public int employeeID { get; set; }
        public string employeeName { get; set; }
        public int attandanceID { get; set; }
        public int employeeidGivenByCompany { get; set; }
        public string designation { get; set; }
        public DateTime joiningDate { get; set; } 
        public PersonalDetails personalDetails { get; set; }
        public EmployeeBankDetails employeeBankDetails { get; set; }
        public AttendanceThing attendanceThing { get; set; }
        public SalaryComponents salaryComponents { get; set; }


    }
}
