using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using ClassLibrary.MainClasses;
using ClassLibrary.Database;
using ClassLibrary.Exception;

namespace ClassLibrary.DataLayer
{
    public class Datalayer
    {
        public OleDbConnection connection = null;
        public OleDbCommand command = null;
        public OleDbDataReader reader = null;


        public Response addEmpToDatabase(Employee employee)
        {
            Response res = new Response();
            string sqlString = "";
            try
            {
                //This First to check for duplicasy
                connection = DatabaseConnection.GetConnection();
                connection.Open();
                sqlString = "select EMPLOYEE_NAME from EMPLOYEE where EMPLOYEE_ID = @1";
                command = new OleDbCommand(sqlString, connection);
                command.Parameters.AddWithValue("@1", employee.employeeidGivenByCompany);
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    throw new DAOException("Employee Already Exists.");
                }
                //This is for inserting the values in the database.
                sqlString = "Insert into EMPLOYEE(EMPLOYEE_NAME,EMPLOYEE_ID,EMPLOYEE_ATTENDANCE_ID,EMPLOYEE_DESIGNATION,EMPLOYEE_JOINING_DATE) values(@1,@2,@3,@4,@5)";
                command = new OleDbCommand(sqlString, connection);
                command.Parameters.AddWithValue("@1",employee.employeeName);
                command.Parameters.AddWithValue("@2", employee.employeeidGivenByCompany);
                command.Parameters.AddWithValue("@3", employee.attandanceID);
                command.Parameters.AddWithValue("@4", employee.designation);
                command.Parameters.AddWithValue("@5", employee.joiningDate);
                reader = command.ExecuteReader();
                if (reader.RecordsAffected == 1)
                {
                    res.success = true;
                }
                else
                {
                    res.success = false;
                    res.isException = true;
                    res.exception = "Something Wrong in saving employee Name| function addEmpToDatanase";
                }
            }
            catch (DAOException ex)
            {
                res.success = false;
                res.isException = true;
                res.exception = ex.Message;

            }
            catch (System.Exception ex) {
                res.success = false;
                res.isException = true;
                res.exception = ex.Message;
            }
            return res;
        
        }

        /// <summary>
        /// This will actually get the employees details.
        /// </summary>
        /// <returns></returns>
        /// 
        public Response savegeneratesalaryinDB(GenerateSalaryOfEmployee generateSalaryOfEmployee) 
        {
            Response res = new Response();
            string sqlString = "";
            try
            {
                connection = DatabaseConnection.GetConnection();
                connection.Open();
                sqlString = "Insert into GENERATE_SALARY(EMPLOYEE_ID,EMPLOYEE_NAME,MONTH_NAME, YEAR_NAME, PATHFILE) values(@1,@2,@3,@4,@5)";
                command = new OleDbCommand(sqlString, connection);
                command.Parameters.AddWithValue("@1", generateSalaryOfEmployee.employeeID);
                command.Parameters.AddWithValue("@2", generateSalaryOfEmployee.nameofEmployee);
                command.Parameters.AddWithValue("@3", generateSalaryOfEmployee.monthName);
                command.Parameters.AddWithValue("@4", generateSalaryOfEmployee.year);
                command.Parameters.AddWithValue("@5", generateSalaryOfEmployee.pathFile);
                reader = command.ExecuteReader();
                if (reader.RecordsAffected == 1)
                {
                    res.success = true;
                    res.isException = false;

                }
                //else
                //{
                //    res.success = false;
                //    res.isException = true;
                //    res.exception = "Something Wrong in saving the generated salary to the database.";
                //}
            }
            catch (System.Exception ex)
            {
                res.success = false;
                res.isException = true;
                res.exception = ex.Message;
            }
            return res;
        }

        public Response getallMonths()
        {
            Response res = new Response();
            //string sqlString = "";
            try 
            {
                string[] Montharray = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
                res.success = true;
                res.body = Montharray;
                res.isException = false;
            }
            catch (System.Exception ex)
            {
                res.success = false;
                res.isException = true;
                res.exception = ex.Message;
            }
            return res;

        }
        public Response GetAllEmployeesID()
        {
            Response res = new Response();
            String sqlString = "";
            try 
            {
                List<Employee> employees = new List<Employee>();
                connection = DatabaseConnection.GetConnection();
                connection.Open();
                sqlString = "Select EMPLOYEE_ID from SALARYCOMPONENTS";
                command = new OleDbCommand(sqlString, connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Employee employee = new Employee();
                    employee.employeeID = Int32.Parse(reader["EMPLOYEE_ID"].ToString());
                    employees.Add(employee);
                }
                res.success = true;
                res.isException = false;
                res.body = employees;
            }
            catch (System.Exception ex)
            {
                res.success = false;
                res.isException = true;
                res.exception = ex.Message;
            }
            return res;
        }

        public Response GenerateThesalary(int EmployeeID) 
        {
            Response res = new Response();
            return res;
        }

        public Response getallYears()
        {
            Response res = new Response();
            //string sqlString = "";
            try
            {
                string[] Montharray = {"2010", "2011", "2012", "2013", "2010", "2010", "2010", "2010", "2010", "2010", "2010", "2021", "2022", "2023", "2024", "2025", "2026", "2027", "2028", "2029", "2030" };
                res.success = true;
                res.body = Montharray;
                res.isException = false;
            }
            catch (System.Exception ex)
            {
                res.success = false;
                res.isException = true;
                res.exception = ex.Message;
            }
            return res;
        }
        public Response getEmployeeSalaryDetails(Employee employee)
        {
            Response res = new Response();
            string sqlString = "";
            try
            {
                SalaryComponents salaryComponents = new SalaryComponents();
                connection = DatabaseConnection.GetConnection();
                connection.Open();
                sqlString = "select * from SALARYCOMPONENTS where EMPLOYEE_ID = @1";
                command = new OleDbCommand(sqlString, connection);
                command.Parameters.AddWithValue("@1", employee.employeeID);
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    salaryComponents.employeeID = Int32.Parse(reader["EMPLOYEE_ID"].ToString());
                    salaryComponents.basic_plus_DA1 = Int32.Parse(reader["BASIC_AND_DA"].ToString());
                    salaryComponents.bonus1 = Int32.Parse(reader["BONUS"].ToString());
                    salaryComponents.hra = Int32.Parse(reader["HRA"].ToString());
                    salaryComponents.conveyance1 = Int32.Parse(reader["CONVEYANCE"].ToString());
                    salaryComponents.productionIncentive1 = Int32.Parse(reader["PRODUCTION_INCENTIVE"].ToString());
                    salaryComponents.food = Int32.Parse(reader["FOOD"].ToString());
                    salaryComponents.allowance1 = Int32.Parse(reader["ALLOWANCE1"].ToString());
                    salaryComponents.allowance2 = Int32.Parse(reader["ALLOWANCE2"].ToString());
                    salaryComponents.allowance3_dailyrepory = Int32.Parse(reader["ALLOWANCE3"].ToString());
                    salaryComponents.allowance4 = Int32.Parse(reader["ALLOWANCE4"].ToString());
                    salaryComponents.allowance5_telephone = Int32.Parse(reader["ALLOWANCE5"].ToString());
                    salaryComponents.allowance6 = Int32.Parse(reader["ALLOWANCE6"].ToString());
                    salaryComponents.esi_debits = Int32.Parse(reader["ESI_DEBITS"].ToString());
                    salaryComponents.pf_debits = Int32.Parse(reader["PF_DEBITS"].ToString());
                    salaryComponents.ptax_debits = Int32.Parse(reader["PTAX_DEBITS"].ToString());
                    salaryComponents.tds_debits = Int32.Parse(reader["TDS_DEBITS"].ToString());
                    salaryComponents.otherdebits = Int32.Parse(reader["OTHER_DEBITS"].ToString());
                    salaryComponents.totaldebits = Int32.Parse(reader["TOTALDEBITS"].ToString());
                    salaryComponents.esi_employer_credit = Int32.Parse(reader["ESI_EMPLOYER_CREDIT"].ToString());
                    salaryComponents.pf_employer_credit = Int32.Parse(reader["PF_EMPLOYER_CREDIT"].ToString());
                    salaryComponents.mobile_phone_credit = Int32.Parse(reader["MOBILE_PHONE_CREDIT"].ToString());
                    salaryComponents.canteen_credit = Int32.Parse(reader["CANTEEN_CREDITS"].ToString()); //-----------------------------------------
                    salaryComponents.earned_leave_credit = Int32.Parse(reader["EARNED_LEAVE_CREDITS"].ToString());
                    salaryComponents.gratuity = Int32.Parse(reader["GRATUITY"].ToString());
                    salaryComponents.medical_insurance = Int32.Parse(reader["MEDICAL_INSURANCE"].ToString());
                    salaryComponents.accidentql_insurance = Int32.Parse(reader["ACCIDENTAL_INSURANCE"].ToString());
                    salaryComponents.total_other_credits = Int32.Parse(reader["TOTAL_OTHER_CREDITS"].ToString());
                    salaryComponents.accrued_deposite = Int32.Parse(reader["ACCRUED_DEPOSIT"].ToString());
                    salaryComponents.accrued_savings = Int32.Parse(reader["ACCRUED_SAVINGS"].ToString());
                    salaryComponents.severance_pakage = Int32.Parse(reader["SEVERANCE_PACKAGE"].ToString());
                    salaryComponents.takeHome = Int32.Parse(reader["TAKE_HOME"].ToString());
                    salaryComponents.savings_salary = Int32.Parse(reader["SAVINGS_INCOME"].ToString());
                    salaryComponents.net_salary = Int32.Parse(reader["NET_SALARY"].ToString());
                    salaryComponents.ctc = Int32.Parse(reader["CTC"].ToString());
                    salaryComponents.optforEsi = Int32.Parse(reader["OPT_FOR_ESI"].ToString());
                    salaryComponents.numberofLeaves = Int32.Parse(reader["NUMBER_OF_LEAVES"].ToString());
                    salaryComponents.number_of_availableWorkingDays = Int32.Parse(reader["NUMBER_OF_AVAILABLE_WORKING_DAYS"].ToString());
                    salaryComponents.number_of_days_worked = Int32.Parse(reader["NUMBER_OF_DAYS_WORKED"].ToString());
                    salaryComponents.number_of_Hours_worked = Int32.Parse(reader["NUMBER_OF_HOURS_WORKED"].ToString());
                    salaryComponents.overTime_inhours = Int32.Parse(reader["OVERTIME_IN_HOURS"].ToString());
                    salaryComponents.salary_package_allowance1 = Int32.Parse(reader["SALARYPACKAGE_ALLOWANCE1"].ToString());
                    salaryComponents.salary_package_allowance2_outrstation = Int32.Parse(reader["SALARYPACKAGE_ALLOWANCE2"].ToString());
                    salaryComponents.salary_package_allowance3_dailyReport = Int32.Parse(reader["SALARYPACKAGE_ALLOWANCE3"].ToString());
                    salaryComponents.salary_package_allowance4 = Int32.Parse(reader["SALARYPACKAGE_ALLOWANCE4"].ToString());
                    salaryComponents.salary_package_allowance5 = Int32.Parse(reader["SALARYPACKAGE_ALLOWANCE5"].ToString());
                    salaryComponents.salary_package_allowance6 = Int32.Parse(reader["SALARYPACKAGE_ALLOWANCE6"].ToString());
                    salaryComponents.salary_package_attendance_bonus = Int32.Parse(reader["SALARYPACKAGE_ATTENDANCE_BONUS"].ToString());
                    salaryComponents.overtimeRate = Int32.Parse(reader["OVERTIME_RATE_INRSPERHOURS"].ToString());  /// ??????????????????????????????????????
                    salaryComponents.numberofOutstationDays = Int32.Parse(reader["NUMBER_OF_OUTSTATION_DAYS"].ToString());
                    salaryComponents.numberofdaysinDailyReport = Int32.Parse(reader["NUMBER_OF_DAYS_IN_DAILY_REPORT0"].ToString());
                    salaryComponents.multriplicationFactor = Int32.Parse(reader["MULTIPLICATION_FACTOR"].ToString());
                    salaryComponents.basicPlusDA_salary_package = Int32.Parse(reader["BASIC_PLUS_DA"].ToString());
                    salaryComponents.allowance1_multiplicationFactor = Int32.Parse(reader["ALLOWANCE1_MULTIPLICATIONVALUE"].ToString());
                    salaryComponents.allowance4_multiplicationFactor = Int32.Parse(reader["ALLOWANCE4_MULTIPLICATIONVALUE"].ToString());
                    salaryComponents.allowance5_multiplicationFactor = Int32.Parse(reader["ALLOWANCE5_MULTIPLICATIONVALUE"].ToString());
                    salaryComponents.allowance6_multiplicationFactor = Int32.Parse(reader["ALLOWANCE6_MULTIPLICATIONVALUE"].ToString());
                    salaryComponents.Bonus = Int32.Parse(reader["BONUS2"].ToString());
                    salaryComponents.pf_debits2 = Int32.Parse(reader["PF_DEBITS2"].ToString());
                    salaryComponents.ptax_debits2 = Int32.Parse(reader["PTAX_DEBITS2"].ToString());
                    salaryComponents.pf_employer_credit2 = Int32.Parse(reader["PF_EMPLOYER_CREDIT2"].ToString());
                    salaryComponents.earned_leave_credit2 = Int32.Parse(reader["EARNED_LEAVE_CREDIT"].ToString());
                    salaryComponents.gratuity2 = Int32.Parse(reader["GRATUITY2"].ToString());
                    salaryComponents.conveyance2 = Int32.Parse(reader["CONVEYANCE2"].ToString());
                    salaryComponents.production_incentive = Int32.Parse(reader["PRODUCTION_INCENTIVE2"].ToString());
                    salaryComponents.early_attandance_bonus_salarypkg = Int32.Parse(reader["EARLY_ATTENDANCE_BONUS_SALARYPACKAGE"].ToString());
                    salaryComponents.late_attendance_debitrate = Int32.Parse(reader["LATE_ATTENDANCE_DEBITRATE"].ToString());
                    salaryComponents.latebydays = Int32.Parse(reader["LATEBYDAYS"].ToString());
                    salaryComponents.late_attendence_relaxation = Int32.Parse(reader["LATE_ATTENDANCE_RELAXATION"].ToString());
                    salaryComponents.total_Late_Attendance_debit = Int32.Parse(reader["TOTAL_LATE_ATTENDANCE_DEBIT"].ToString());
                    salaryComponents.early_attendance_bonus = Int32.Parse(reader["EARLY_ATTENDANCE_BONUS"].ToString());
                    salaryComponents.optforpf = Int32.Parse(reader["OPT_FOR_PF"].ToString());
                    salaryComponents.cea = Int32.Parse(reader["CEA"].ToString());
                    salaryComponents.ot_hours = Int32.Parse(reader["OT"].ToString());
                    salaryComponents.commitment_allowance = Int32.Parse(reader["COMMITMENT_ALLOWANCE"].ToString());
                    salaryComponents.attendance_bonus = Int32.Parse(reader["ATTENDANCE_BONUS"].ToString());
                    salaryComponents.grossSalary = Int32.Parse(reader["GROSS_SALARY"].ToString());

                }
                res.success = true;
                res.isException = false;
                res.body = salaryComponents;

            }
            catch (System.Exception ex)
            {
                res.success = false;
                res.isException = true;
                res.exception = ex.Message;
            }
            return res;
        }


        //_______________________________________________________________________________________________________________________
        public Response getEmployeeSalaryDetailsbyID(int i)
        {
            Response res = new Response();
            string sqlString = "";
            try
            {
                SalaryComponents salaryComponents = new SalaryComponents();
                connection = DatabaseConnection.GetConnection();
                connection.Open();
                sqlString = "select * from SALARYCOMPONENTS where EMPLOYEE_ID = @1";
                command = new OleDbCommand(sqlString, connection);
                command.Parameters.AddWithValue("@1", i);
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    salaryComponents.employeeID = Int32.Parse(reader["EMPLOYEE_ID"].ToString());
                    salaryComponents.basic_plus_DA1 = Int32.Parse(reader["BASIC_AND_DA"].ToString());
                    salaryComponents.bonus1 = Int32.Parse(reader["BONUS"].ToString());
                    salaryComponents.hra = Int32.Parse(reader["HRA"].ToString());
                    salaryComponents.conveyance1 = Int32.Parse(reader["CONVEYANCE"].ToString());
                    salaryComponents.productionIncentive1 = Int32.Parse(reader["PRODUCTION_INCENTIVE"].ToString());
                    salaryComponents.food = Int32.Parse(reader["FOOD"].ToString());
                    salaryComponents.allowance1 = Int32.Parse(reader["ALLOWANCE1"].ToString());
                    salaryComponents.allowance2 = Int32.Parse(reader["ALLOWANCE2"].ToString());
                    salaryComponents.allowance3_dailyrepory = Int32.Parse(reader["ALLOWANCE3"].ToString());
                    salaryComponents.allowance4 = Int32.Parse(reader["ALLOWANCE4"].ToString());
                    salaryComponents.allowance5_telephone = Int32.Parse(reader["ALLOWANCE5"].ToString());
                    salaryComponents.allowance6 = Int32.Parse(reader["ALLOWANCE6"].ToString());
                    salaryComponents.esi_debits = Int32.Parse(reader["ESI_DEBITS"].ToString());
                    salaryComponents.pf_debits = Int32.Parse(reader["PF_DEBITS"].ToString());
                    salaryComponents.ptax_debits = Int32.Parse(reader["PTAX_DEBITS"].ToString());
                    salaryComponents.tds_debits = Int32.Parse(reader["TDS_DEBITS"].ToString());
                    salaryComponents.otherdebits = Int32.Parse(reader["OTHER_DEBITS"].ToString());
                    salaryComponents.totaldebits = Int32.Parse(reader["TOTALDEBITS"].ToString());
                    salaryComponents.esi_employer_credit = Int32.Parse(reader["ESI_EMPLOYER_CREDIT"].ToString());
                    salaryComponents.pf_employer_credit = Int32.Parse(reader["PF_EMPLOYER_CREDIT"].ToString());
                    salaryComponents.mobile_phone_credit = Int32.Parse(reader["MOBILE_PHONE_CREDIT"].ToString());
                    salaryComponents.canteen_credit = Int32.Parse(reader["CANTEEN_CREDITS"].ToString()); //-----------------------------------------
                    salaryComponents.earned_leave_credit = Int32.Parse(reader["EARNED_LEAVE_CREDITS"].ToString());
                    salaryComponents.gratuity = Int32.Parse(reader["GRATUITY"].ToString());
                    salaryComponents.medical_insurance = Int32.Parse(reader["MEDICAL_INSURANCE"].ToString());
                    salaryComponents.accidentql_insurance = Int32.Parse(reader["ACCIDENTAL_INSURANCE"].ToString());
                    salaryComponents.total_other_credits = Int32.Parse(reader["TOTAL_OTHER_CREDITS"].ToString());
                    salaryComponents.accrued_deposite = Int32.Parse(reader["ACCRUED_DEPOSIT"].ToString());
                    salaryComponents.accrued_savings = Int32.Parse(reader["ACCRUED_SAVINGS"].ToString());
                    salaryComponents.severance_pakage = Int32.Parse(reader["SEVERANCE_PACKAGE"].ToString());
                    salaryComponents.takeHome = Int32.Parse(reader["TAKE_HOME"].ToString());
                    salaryComponents.savings_salary = Int32.Parse(reader["SAVINGS_INCOME"].ToString());
                    salaryComponents.net_salary = Int32.Parse(reader["NET_SALARY"].ToString());
                    salaryComponents.ctc = Int32.Parse(reader["CTC"].ToString());
                    salaryComponents.optforEsi = Int32.Parse(reader["OPT_FOR_ESI"].ToString());
                    salaryComponents.numberofLeaves = Int32.Parse(reader["NUMBER_OF_LEAVES"].ToString());
                    salaryComponents.number_of_availableWorkingDays = Int32.Parse(reader["NUMBER_OF_AVAILABLE_WORKING_DAYS"].ToString());
                    salaryComponents.number_of_days_worked = Int32.Parse(reader["NUMBER_OF_DAYS_WORKED"].ToString());
                    salaryComponents.number_of_Hours_worked = Int32.Parse(reader["NUMBER_OF_HOURS_WORKED"].ToString());
                    salaryComponents.overTime_inhours = Int32.Parse(reader["OVERTIME_IN_HOURS"].ToString());
                    salaryComponents.salary_package_allowance1 = Int32.Parse(reader["SALARYPACKAGE_ALLOWANCE1"].ToString());
                    salaryComponents.salary_package_allowance2_outrstation = Int32.Parse(reader["SALARYPACKAGE_ALLOWANCE2"].ToString());
                    salaryComponents.salary_package_allowance3_dailyReport = Int32.Parse(reader["SALARYPACKAGE_ALLOWANCE3"].ToString());
                    salaryComponents.salary_package_allowance4 = Int32.Parse(reader["SALARYPACKAGE_ALLOWANCE4"].ToString());
                    salaryComponents.salary_package_allowance5 = Int32.Parse(reader["SALARYPACKAGE_ALLOWANCE5"].ToString());
                    salaryComponents.salary_package_allowance6 = Int32.Parse(reader["SALARYPACKAGE_ALLOWANCE6"].ToString());
                    salaryComponents.salary_package_attendance_bonus = Int32.Parse(reader["SALARYPACKAGE_ATTENDANCE_BONUS"].ToString());
                    salaryComponents.overtimeRate = Int32.Parse(reader["OVERTIME_RATE_INRSPERHOURS"].ToString());  /// ??????????????????????????????????????
                    salaryComponents.numberofOutstationDays = Int32.Parse(reader["NUMBER_OF_OUTSTATION_DAYS"].ToString());
                    salaryComponents.numberofdaysinDailyReport = Int32.Parse(reader["NUMBER_OF_DAYS_IN_DAILY_REPORT0"].ToString());
                    salaryComponents.multriplicationFactor = Int32.Parse(reader["MULTIPLICATION_FACTOR"].ToString());
                    salaryComponents.basicPlusDA_salary_package = Int32.Parse(reader["BASIC_PLUS_DA"].ToString());
                    salaryComponents.allowance1_multiplicationFactor = Int32.Parse(reader["ALLOWANCE1_MULTIPLICATIONVALUE"].ToString());
                    salaryComponents.allowance4_multiplicationFactor = Int32.Parse(reader["ALLOWANCE4_MULTIPLICATIONVALUE"].ToString());
                    salaryComponents.allowance5_multiplicationFactor = Int32.Parse(reader["ALLOWANCE5_MULTIPLICATIONVALUE"].ToString());
                    salaryComponents.allowance6_multiplicationFactor = Int32.Parse(reader["ALLOWANCE6_MULTIPLICATIONVALUE"].ToString());
                    salaryComponents.Bonus = Int32.Parse(reader["BONUS2"].ToString());
                    salaryComponents.pf_debits2 = Int32.Parse(reader["PF_DEBITS2"].ToString());
                    salaryComponents.ptax_debits2 = Int32.Parse(reader["PTAX_DEBITS2"].ToString());
                    salaryComponents.pf_employer_credit2 = Int32.Parse(reader["PF_EMPLOYER_CREDIT2"].ToString());
                    salaryComponents.earned_leave_credit2 = Int32.Parse(reader["EARNED_LEAVE_CREDIT"].ToString());
                    salaryComponents.gratuity2 = Int32.Parse(reader["GRATUITY2"].ToString());
                    salaryComponents.conveyance2 = Int32.Parse(reader["CONVEYANCE2"].ToString());
                    salaryComponents.production_incentive = Int32.Parse(reader["PRODUCTION_INCENTIVE2"].ToString());
                    salaryComponents.early_attandance_bonus_salarypkg = Int32.Parse(reader["EARLY_ATTENDANCE_BONUS_SALARYPACKAGE"].ToString());
                    salaryComponents.late_attendance_debitrate = Int32.Parse(reader["LATE_ATTENDANCE_DEBITRATE"].ToString());
                    salaryComponents.latebydays = Int32.Parse(reader["LATEBYDAYS"].ToString());
                    salaryComponents.late_attendence_relaxation = Int32.Parse(reader["LATE_ATTENDANCE_RELAXATION"].ToString());
                    salaryComponents.total_Late_Attendance_debit = Int32.Parse(reader["TOTAL_LATE_ATTENDANCE_DEBIT"].ToString());
                    salaryComponents.early_attendance_bonus = Int32.Parse(reader["EARLY_ATTENDANCE_BONUS"].ToString());
                    salaryComponents.optforpf = Int32.Parse(reader["OPT_FOR_PF"].ToString());
                    salaryComponents.cea = Int32.Parse(reader["CEA"].ToString());
                    salaryComponents.ot_hours = Int32.Parse(reader["OT"].ToString());
                    salaryComponents.commitment_allowance = Int32.Parse(reader["COMMITMENT_ALLOWANCE"].ToString());
                    salaryComponents.attendance_bonus = Int32.Parse(reader["ATTENDANCE_BONUS"].ToString());
                    salaryComponents.grossSalary = Int32.Parse(reader["GROSS_SALARY"].ToString());
                    salaryComponents.employeeName = reader["EMPLOYEE_NAME"].ToString();

                }
                res.success = true;
                res.isException = false;
                res.body = salaryComponents;

            }
            catch (System.Exception ex)
            {
                res.success = false;
                res.isException = true;
                res.exception = ex.Message;
            }
            return res;
        }

        //\/\/\/\/\/\/\/\/\/\/\/\\/\/\/\\//\/\/\/\/\/\\/\/\/\/\\\/\/\\/\/\/\/\/\/\/\\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\\/\/\/\/\/\/\\/\/\/\/\/\/\/


        public Response getSalarycomponentsvianame(string name)
        {
            Response res = new Response();
            string sqlString = "";
            try
            {
                SalaryComponents salaryComponents = new SalaryComponents();
                connection = DatabaseConnection.GetConnection();
                connection.Open();
                sqlString = "select * from SALARYCOMPONENTS where EMPLOYEE_NAME = @1";
                command = new OleDbCommand(sqlString, connection);
                command.Parameters.AddWithValue("@1", name);
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    salaryComponents.employeeID = Int32.Parse(reader["EMPLOYEE_ID"].ToString());
                    salaryComponents.basic_plus_DA1 = Int32.Parse(reader["BASIC_AND_DA"].ToString());
                    salaryComponents.bonus1 = Int32.Parse(reader["BONUS"].ToString());
                    salaryComponents.hra = Int32.Parse(reader["HRA"].ToString());
                    salaryComponents.conveyance1 = Int32.Parse(reader["CONVEYANCE"].ToString());
                    salaryComponents.productionIncentive1 = Int32.Parse(reader["PRODUCTION_INCENTIVE"].ToString());
                    salaryComponents.food = Int32.Parse(reader["FOOD"].ToString());
                    salaryComponents.allowance1 = Int32.Parse(reader["ALLOWANCE1"].ToString());
                    salaryComponents.allowance2 = Int32.Parse(reader["ALLOWANCE2"].ToString());
                    salaryComponents.allowance3_dailyrepory = Int32.Parse(reader["ALLOWANCE3"].ToString());
                    salaryComponents.allowance4 = Int32.Parse(reader["ALLOWANCE4"].ToString());
                    salaryComponents.allowance5_telephone = Int32.Parse(reader["ALLOWANCE5"].ToString());
                    salaryComponents.allowance6 = Int32.Parse(reader["ALLOWANCE6"].ToString());
                    salaryComponents.esi_debits = Int32.Parse(reader["ESI_DEBITS"].ToString());
                    salaryComponents.pf_debits = Int32.Parse(reader["PF_DEBITS"].ToString());
                    salaryComponents.ptax_debits = Int32.Parse(reader["PTAX_DEBITS"].ToString());
                    salaryComponents.tds_debits = Int32.Parse(reader["TDS_DEBITS"].ToString());
                    salaryComponents.otherdebits = Int32.Parse(reader["OTHER_DEBITS"].ToString());
                    salaryComponents.totaldebits = Int32.Parse(reader["TOTALDEBITS"].ToString());
                    salaryComponents.esi_employer_credit = Int32.Parse(reader["ESI_EMPLOYER_CREDIT"].ToString());
                    salaryComponents.pf_employer_credit = Int32.Parse(reader["PF_EMPLOYER_CREDIT"].ToString());
                    salaryComponents.mobile_phone_credit = Int32.Parse(reader["MOBILE_PHONE_CREDIT"].ToString());
                    salaryComponents.canteen_credit = Int32.Parse(reader["CANTEEN_CREDITS"].ToString()); //-----------------------------------------
                    salaryComponents.earned_leave_credit = Int32.Parse(reader["EARNED_LEAVE_CREDITS"].ToString());
                    salaryComponents.gratuity = Int32.Parse(reader["GRATUITY"].ToString());
                    salaryComponents.medical_insurance = Int32.Parse(reader["MEDICAL_INSURANCE"].ToString());
                    salaryComponents.accidentql_insurance = Int32.Parse(reader["ACCIDENTAL_INSURANCE"].ToString());
                    salaryComponents.total_other_credits = Int32.Parse(reader["TOTAL_OTHER_CREDITS"].ToString());
                    salaryComponents.accrued_deposite = Int32.Parse(reader["ACCRUED_DEPOSIT"].ToString());
                    salaryComponents.accrued_savings = Int32.Parse(reader["ACCRUED_SAVINGS"].ToString());
                    salaryComponents.severance_pakage = Int32.Parse(reader["SEVERANCE_PACKAGE"].ToString());
                    salaryComponents.takeHome = Int32.Parse(reader["TAKE_HOME"].ToString());
                    salaryComponents.savings_salary = Int32.Parse(reader["SAVINGS_INCOME"].ToString());
                    salaryComponents.net_salary = Int32.Parse(reader["NET_SALARY"].ToString());
                    salaryComponents.ctc = Int32.Parse(reader["CTC"].ToString());
                    salaryComponents.optforEsi = Int32.Parse(reader["OPT_FOR_ESI"].ToString());
                    salaryComponents.numberofLeaves = Int32.Parse(reader["NUMBER_OF_LEAVES"].ToString());
                    salaryComponents.number_of_availableWorkingDays = Int32.Parse(reader["NUMBER_OF_AVAILABLE_WORKING_DAYS"].ToString());
                    salaryComponents.number_of_days_worked = Int32.Parse(reader["NUMBER_OF_DAYS_WORKED"].ToString());
                    salaryComponents.number_of_Hours_worked = Int32.Parse(reader["NUMBER_OF_HOURS_WORKED"].ToString());
                    salaryComponents.overTime_inhours = Int32.Parse(reader["OVERTIME_IN_HOURS"].ToString());
                    salaryComponents.salary_package_allowance1 = Int32.Parse(reader["SALARYPACKAGE_ALLOWANCE1"].ToString());
                    salaryComponents.salary_package_allowance2_outrstation = Int32.Parse(reader["SALARYPACKAGE_ALLOWANCE2"].ToString());
                    salaryComponents.salary_package_allowance3_dailyReport = Int32.Parse(reader["SALARYPACKAGE_ALLOWANCE3"].ToString());
                    salaryComponents.salary_package_allowance4 = Int32.Parse(reader["SALARYPACKAGE_ALLOWANCE4"].ToString());
                    salaryComponents.salary_package_allowance5 = Int32.Parse(reader["SALARYPACKAGE_ALLOWANCE5"].ToString());
                    salaryComponents.salary_package_allowance6 = Int32.Parse(reader["SALARYPACKAGE_ALLOWANCE6"].ToString());
                    salaryComponents.salary_package_attendance_bonus = Int32.Parse(reader["SALARYPACKAGE_ATTENDANCE_BONUS"].ToString());
                    salaryComponents.overtimeRate = Int32.Parse(reader["OVERTIME_RATE_INRSPERHOURS"].ToString());  /// ??????????????????????????????????????
                    salaryComponents.numberofOutstationDays = Int32.Parse(reader["NUMBER_OF_OUTSTATION_DAYS"].ToString());
                    salaryComponents.numberofdaysinDailyReport = Int32.Parse(reader["NUMBER_OF_DAYS_IN_DAILY_REPORT0"].ToString());
                    salaryComponents.multriplicationFactor = Int32.Parse(reader["MULTIPLICATION_FACTOR"].ToString());
                    salaryComponents.basicPlusDA_salary_package = Int32.Parse(reader["BASIC_PLUS_DA"].ToString());
                    salaryComponents.allowance1_multiplicationFactor = Int32.Parse(reader["ALLOWANCE1_MULTIPLICATIONVALUE"].ToString());
                    salaryComponents.allowance4_multiplicationFactor = Int32.Parse(reader["ALLOWANCE4_MULTIPLICATIONVALUE"].ToString());
                    salaryComponents.allowance5_multiplicationFactor = Int32.Parse(reader["ALLOWANCE5_MULTIPLICATIONVALUE"].ToString());
                    salaryComponents.allowance6_multiplicationFactor = Int32.Parse(reader["ALLOWANCE6_MULTIPLICATIONVALUE"].ToString());
                    salaryComponents.Bonus = Int32.Parse(reader["BONUS2"].ToString());
                    salaryComponents.pf_debits2 = Int32.Parse(reader["PF_DEBITS2"].ToString());
                    salaryComponents.ptax_debits2 = Int32.Parse(reader["PTAX_DEBITS2"].ToString());
                    salaryComponents.pf_employer_credit2 = Int32.Parse(reader["PF_EMPLOYER_CREDIT2"].ToString());
                    salaryComponents.earned_leave_credit2 = Int32.Parse(reader["EARNED_LEAVE_CREDIT"].ToString());
                    salaryComponents.gratuity2 = Int32.Parse(reader["GRATUITY2"].ToString());
                    salaryComponents.conveyance2 = Int32.Parse(reader["CONVEYANCE2"].ToString());
                    salaryComponents.production_incentive = Int32.Parse(reader["PRODUCTION_INCENTIVE2"].ToString());
                    salaryComponents.early_attandance_bonus_salarypkg = Int32.Parse(reader["EARLY_ATTENDANCE_BONUS_SALARYPACKAGE"].ToString());
                    salaryComponents.late_attendance_debitrate = Int32.Parse(reader["LATE_ATTENDANCE_DEBITRATE"].ToString());
                    salaryComponents.latebydays = Int32.Parse(reader["LATEBYDAYS"].ToString());
                    salaryComponents.late_attendence_relaxation = Int32.Parse(reader["LATE_ATTENDANCE_RELAXATION"].ToString());
                    salaryComponents.total_Late_Attendance_debit = Int32.Parse(reader["TOTAL_LATE_ATTENDANCE_DEBIT"].ToString());
                    salaryComponents.early_attendance_bonus = Int32.Parse(reader["EARLY_ATTENDANCE_BONUS"].ToString());
                    salaryComponents.optforpf = Int32.Parse(reader["OPT_FOR_PF"].ToString());
                    salaryComponents.cea = Int32.Parse(reader["CEA"].ToString());
                    salaryComponents.ot_hours = Int32.Parse(reader["OT"].ToString());
                    salaryComponents.commitment_allowance = Int32.Parse(reader["COMMITMENT_ALLOWANCE"].ToString());
                    salaryComponents.attendance_bonus = Int32.Parse(reader["ATTENDANCE_BONUS"].ToString());
                    salaryComponents.grossSalary = Int32.Parse(reader["GROSS_SALARY"].ToString());
                    salaryComponents.employeeName = reader["EMPLOYEE_NAME"].ToString();
                    res.success = true;
                    res.isException = false;
                    res.body = salaryComponents;
                }
                //This is for those employees who are not currently in the database.
                else 
                {
                    res.success = false;
                    res.isException = true;
                    
                }
                

            }
            catch (System.Exception ex)
            {
                res.success = false;
                
                res.exception = ex.Message;
            }
            return res;
        }

        //_____________________________------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// This would save the details of Employee bank in the database
        /// </summary>
        /// <param name="bankDetails"></param>
        /// <returns>
        /// bank detail is the class so that we can pass the details knowing that we have the employee ID
        /// </returns>
        public Response addbankdetailstodatabase(EmployeeBankDetails bankDetails) 
        {
            Response res = new Response();
            string sqlString = "";
            try
            {
                connection = DatabaseConnection.GetConnection();
                connection.Open();
                sqlString = "select * from EMPLOYEE where EMPLOYEE_ID = @1";
                command = new OleDbCommand(sqlString, connection);
                command.Parameters.AddWithValue("@1",bankDetails.employeeID);
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {

                    connection.Close();
                    connection.Open();
                    sqlString = "Insert into BANKDETAILS(EMPLOYEE_ID,BANKNAME,ACCOUNTNUMBER,IFSCCODE,NAMEINBANK) values(@1,@2,@3,@4,@5)";
                    command = new OleDbCommand(sqlString, connection);
                    command.Parameters.AddWithValue("@1", bankDetails.employeeID);
                    command.Parameters.AddWithValue("@2", bankDetails.bankName);
                    command.Parameters.AddWithValue("@3", bankDetails.accountNumber);
                    command.Parameters.AddWithValue("@4", bankDetails.ifscCode);
                    command.Parameters.AddWithValue("@5", bankDetails.nameInBank);
                    reader = command.ExecuteReader();
                    if (reader.RecordsAffected == 1)
                    {
                        res.success = true;
                    }
                    else
                    {
                        res.success = false;
                        res.isException = true;
                        res.exception = "Something Wrong in saving employee Name| function bankDetailsadding";
                    }
                }
                else 
                {
                    throw new DAOException("This User DoesNot Exixts so Consider Creating the user.");      
                }
            }
            catch (DAOException ex)
            {
                res.success = false;
                res.isException = true;
                res.exception = ex.Message;

            }
            catch (System.Exception ex)
            {
                res.success = false;
                res.isException = true;
                res.exception = ex.Message;
            }
            return res;
        }
        public Response PersonalDetailsInTheDataBAse(PersonalDetails personalDetails) 
        {
            Response res = new Response();
            string sqlString = "";
            try 
            {
                connection = DatabaseConnection.GetConnection();
                connection.Open();
                sqlString = "select * from EMPLOYEE where EMPLOYEE_ID = @1";
                command = new OleDbCommand(sqlString, connection);
                command.Parameters.AddWithValue("@1",personalDetails.employeeID);
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    connection.Close();
                    connection.Open();
                    sqlString = "Insert into PERSONALDETAILS(EMPLOYEENAME,EMPLOYEEID,ATTENDANCEID,AGE,EMAILADDRESS,ADDRESS1,ADDRESS2,ADDRESS3,ADHAARNUMBER,PANNUMBER,PASSPORTNUMBER,PHONE1,PHONE2,PHONE3,FATHERNAME) values(@1,@2,@3,@4,@5,@6,@7,@8,@9,@10,@11,@12,@13,@14,@15)";
                    command = new OleDbCommand(sqlString, connection);
                    command.Parameters.AddWithValue("@1",personalDetails.EmployeeName );
                    command.Parameters.AddWithValue("@2",personalDetails.employeeID );
                    command.Parameters.AddWithValue("@3", personalDetails.attendanceId);
                    command.Parameters.AddWithValue("@4", personalDetails.age);
                    command.Parameters.AddWithValue("@5", personalDetails.emialaddress);
                    command.Parameters.AddWithValue("@6", personalDetails.address1);
                    command.Parameters.AddWithValue("@7", personalDetails.address2);
                    command.Parameters.AddWithValue("@8", personalDetails.address3);
                    command.Parameters.AddWithValue("@9", personalDetails.adhaarNumber);
                    command.Parameters.AddWithValue("@10", personalDetails.panNumber);
                    command.Parameters.AddWithValue("@11", personalDetails.passportNumer);
                    command.Parameters.AddWithValue("@12", personalDetails.phone1);
                    command.Parameters.AddWithValue("@13", personalDetails.phone2);
                    command.Parameters.AddWithValue("@14", personalDetails.phone3);
                    command.Parameters.AddWithValue("@15", personalDetails.fatherName);
                    reader = command.ExecuteReader();
                    
               
                    if (reader.RecordsAffected == 1)
                    {
                        res.success = true;
                    }
                    else
                    {
                        res.success = false;
                        res.isException = true;
                        res.exception = "Something Wrong in saving employee Name| function saving personal details to the database";
                    }
                }
                else
                {
                    throw new DAOException("This User DoesNot Exixts so Consider Creating the user.");
                }
            }
            catch (DAOException ex)
            {
                res.success = false;
                res.isException = true;
                res.exception = ex.Message;

            }
            catch (System.Exception ex)
            {
                res.success = false;
                res.isException = true;
                res.exception = ex.Message;
            }
            return res;
        }
        public Response UpdateSalaryComponentsofEmployees(SalaryComponents salaryComponents)
        {
            Response res = new Response();
            string sqlString = "";
            try
            {
                connection = DatabaseConnection.GetConnection();
                connection.Open();
                
                sqlString = "select* from SALARYCOMPONENTS where EMPLOYEE_ID = @1";
                command = new OleDbCommand(sqlString, connection);
                command.Parameters.AddWithValue("@1", salaryComponents.employeeID);
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {


                    sqlString = "Update SALARYCOMPONENTS set BASIC_AND_DA = @2,BONUS=@3,HRA=@4,CONVEYANCE=@5,PRODUCTION_INCENTIVE=@6,FOOD=@7,ALLOWANCE1=@8 ,ALLOWANCE2=@9,ALLOWANCE3=@10,ALLOWANCE4=@11  ,ALLOWANCE5=@12  ,CEA=@13  ,OT=@14  ,COMMITMENT_ALLOWANCE=@15  ,ATTENDANCE_BONUS=@16  ,GROSS_SALARY=@17  ,ALLOWANCE6=@18  ,ESI_DEBITS=@19  ,PF_DEBITS=@20  ,PTAX_DEBITS=@21  ,TDS_DEBITS=@22  ,OTHER_DEBITS=@23  ,TOTALDEBITS=@24  ,ESI_EMPLOYER_CREDIT=@25  ,PF_EMPLOYER_CREDIT=@26  ,MOBILE_PHONE_CREDIT=@27  ,EARNED_LEAVE_CREDITS=@28  ,GRATUITY=@29  ,MEDICAL_INSURANCE=@30  ,ACCIDENTAL_INSURANCE=@31  ,TOTAL_OTHER_CREDITS=@32  ,ACCRUED_DEPOSIT=@33  ,ACCRUED_SAVINGS=@34  ,SEVERANCE_PACKAGE=@35  ,TAKE_HOME=@36  ,SAVINGS_INCOME=@37  ,NET_SALARY=@38  ,CTC=@39  ,OPT_FOR_ESI=@40  ,NUMBER_OF_LEAVES=@41  ,NUMBER_OF_AVAILABLE_WORKING_DAYS=@42  ,NUMBER_OF_DAYS_WORKED=@43  ,NUMBER_OF_HOURS_WORKED=@44  ,OVERTIME_IN_HOURS=@45  ,SALARYPACKAGE_ALLOWANCE1=@46  ,SALARYPACKAGE_ALLOWANCE2=@47  ,SALARYPACKAGE_ALLOWANCE3=@48  ,SALARYPACKAGE_ALLOWANCE4=@49  ,SALARYPACKAGE_ALLOWANCE5=@50  ,SALARYPACKAGE_ALLOWANCE6=@51  ,SALARYPACKAGE_ATTENDANCE_BONUS=@52  ,OVERTIME_RATE_INRSPERHOURS=@53  ,NUMBER_OF_OUTSTATION_DAYS=@54  ,NUMBER_OF_DAYS_IN_DAILY_REPORT0=@55  ,MULTIPLICATION_FACTOR=@56  ,BASIC_PLUS_DA=@57  ,ALLOWANCE1_MULTIPLICATIONVALUE=@58  ,ALLOWANCE4_MULTIPLICATIONVALUE=@59  ,ALLOWANCE5_MULTIPLICATIONVALUE=@60  ,ALLOWANCE6_MULTIPLICATIONVALUE=@61  ,BONUS2=@62  ,PF_DEBITS2=@63  ,PTAX_DEBITS2=@64  ,PF_EMPLOYER_CREDIT2=@65  ,EARNED_LEAVE_CREDIT=@66  ,GRATUITY2=@67  ,CONVEYANCE2=@68  ,PRODUCTION_INCENTIVE2=@69  ,EARLY_ATTENDANCE_BONUS_SALARYPACKAGE=@70  ,LATE_ATTENDANCE_DEBITRATE=@71  ,LATEBYDAYS=@72  ,LATE_ATTENDANCE_RELAXATION=@73  ,TOTAL_LATE_ATTENDANCE_DEBIT=@74  ,EARLY_ATTENDANCE_BONUS=@75  ,OPT_FOR_PF=@76  ,CANTEEN_CREDITS=@77  , EMPLOYEE_NAME =@78 where EMPLOYEE_ID = @1  "; //values (@2 ,@3  ,@4  ,@5  ,@6  ,@7  ,@8  ,@9  ,@10  ,@11  ,@12  ,@13  ,@14  ,@15  ,@16  ,@17  ,@18  ,@19  ,@20  ,@21  ,@22  ,@23  ,@24  ,@25  ,@26  ,@27  ,@28  ,@29  ,@30  ,@31  ,@32  ,@33  ,@34  ,@35  ,@36  ,@37  ,@38  ,@39=@  ,@40=@  ,@41=@  ,@42=@  ,@43=@  ,@44=@  ,@45=@  ,@46=@  ,@47=@  ,@48=@  ,@49=@  ,@50=@  ,@51=@  ,@52=@  ,@53=@  ,@54=@  ,@55=@  ,@56=@  ,@57=@  ,@58=@  ,@59=@  ,@60=@  ,@61=@  ,@62=@  ,@63=@  ,@64=@  ,@65=@  ,@66=@  ,@67=@  ,@68=@  ,@69=@  ,@70=@  ,@71=@  ,@72=@  ,@73=@  ,@74=@  ,@75=@  ,@76=@  ,@77=@  ,@78) where EMPLOYEE_ID = @1 ";
                    command = new OleDbCommand(sqlString, connection);
                   
                    command.Parameters.AddWithValue("@2", salaryComponents.basic_plus_DA1);
                    command.Parameters.AddWithValue("@3", salaryComponents.bonus1);
                    command.Parameters.AddWithValue("@4", salaryComponents.hra);
                    command.Parameters.AddWithValue("@5", salaryComponents.conveyance1);
                    command.Parameters.AddWithValue("@6", salaryComponents.productionIncentive1);
                    command.Parameters.AddWithValue("@7", salaryComponents.food);
                    command.Parameters.AddWithValue("@8", salaryComponents.allowance1);
                    command.Parameters.AddWithValue("@9", salaryComponents.allowance2);
                    command.Parameters.AddWithValue("@10", salaryComponents.allowance3_dailyrepory);
                    command.Parameters.AddWithValue("@11", salaryComponents.allowance4);
                    command.Parameters.AddWithValue("@12", salaryComponents.allowance5_telephone);
                    command.Parameters.AddWithValue("@13", salaryComponents.cea);
                    command.Parameters.AddWithValue("@14", salaryComponents.ot_hours);
                    command.Parameters.AddWithValue("@15", salaryComponents.commitment_allowance);
                    command.Parameters.AddWithValue("@16", salaryComponents.attendance_bonus);
                    command.Parameters.AddWithValue("@17", salaryComponents.grossSalary);
                    command.Parameters.AddWithValue("@18", salaryComponents.allowance6);
                    command.Parameters.AddWithValue("@19", salaryComponents.esi_debits);
                    command.Parameters.AddWithValue("@20", salaryComponents.pf_debits);
                    command.Parameters.AddWithValue("@21", salaryComponents.ptax_debits);
                    command.Parameters.AddWithValue("@22", salaryComponents.tds_debits);
                    command.Parameters.AddWithValue("@23", salaryComponents.otherdebits);
                    command.Parameters.AddWithValue("@24", salaryComponents.totaldebits);
                    command.Parameters.AddWithValue("@25", salaryComponents.esi_employer_credit);
                    command.Parameters.AddWithValue("@26", salaryComponents.pf_employer_credit);
                    command.Parameters.AddWithValue("@27", salaryComponents.mobile_phone_credit);
                    command.Parameters.AddWithValue("@28", salaryComponents.earned_leave_credit);
                    command.Parameters.AddWithValue("@29", salaryComponents.gratuity);
                    command.Parameters.AddWithValue("@30", salaryComponents.medical_insurance);
                    command.Parameters.AddWithValue("@31", salaryComponents.accidentql_insurance);
                    command.Parameters.AddWithValue("@32", salaryComponents.total_other_credits);
                    command.Parameters.AddWithValue("@33", salaryComponents.accrued_deposite);
                    command.Parameters.AddWithValue("@34", salaryComponents.accrued_savings);
                    command.Parameters.AddWithValue("@35", salaryComponents.severance_pakage);
                    command.Parameters.AddWithValue("@36", salaryComponents.takeHome);
                    command.Parameters.AddWithValue("@37", salaryComponents.savings_salary);
                    command.Parameters.AddWithValue("@38", salaryComponents.net_salary);
                    command.Parameters.AddWithValue("@39", salaryComponents.ctc);
                    command.Parameters.AddWithValue("@40", salaryComponents.optforEsi);          //40
                    command.Parameters.AddWithValue("@41", salaryComponents.numberofLeaves);    //41
                    command.Parameters.AddWithValue("@42", salaryComponents.number_of_availableWorkingDays);
                    command.Parameters.AddWithValue("@43", salaryComponents.number_of_days_worked);
                    command.Parameters.AddWithValue("@44", salaryComponents.number_of_Hours_worked);
                    command.Parameters.AddWithValue("@45", salaryComponents.overTime_inhours);
                    command.Parameters.AddWithValue("@46", salaryComponents.salary_package_allowance1);
                    command.Parameters.AddWithValue("@47", salaryComponents.salary_package_allowance2_outrstation);
                    command.Parameters.AddWithValue("@48", salaryComponents.salary_package_allowance3_dailyReport);
                    command.Parameters.AddWithValue("@49", salaryComponents.salary_package_allowance4);
                    command.Parameters.AddWithValue("@50", salaryComponents.salary_package_allowance5);
                    command.Parameters.AddWithValue("@51", salaryComponents.salary_package_allowance6);
                    command.Parameters.AddWithValue("@52", salaryComponents.salary_package_attendance_bonus);
                    command.Parameters.AddWithValue("@53", salaryComponents.overtimeRate);
                    command.Parameters.AddWithValue("@54", salaryComponents.numberofOutstationDays);
                    command.Parameters.AddWithValue("@55", salaryComponents.numberofdaysinDailyReport);
                    command.Parameters.AddWithValue("@56", salaryComponents.multriplicationFactor);
                    command.Parameters.AddWithValue("@57", salaryComponents.basicPlusDA_salary_package);
                    command.Parameters.AddWithValue("@58", salaryComponents.allowance1_multiplicationFactor);
                    command.Parameters.AddWithValue("@59", salaryComponents.allowance4_multiplicationFactor);
                    command.Parameters.AddWithValue("@60", salaryComponents.allowance5_multiplicationFactor);
                    command.Parameters.AddWithValue("@61", salaryComponents.allowance6_multiplicationFactor);
                    command.Parameters.AddWithValue("@62", salaryComponents.Bonus);
                    command.Parameters.AddWithValue("@63", salaryComponents.pf_debits2);
                    command.Parameters.AddWithValue("@64", salaryComponents.ptax_debits2);
                    command.Parameters.AddWithValue("@65", salaryComponents.pf_employer_credit2);
                    command.Parameters.AddWithValue("@66", salaryComponents.earned_leave_credit2);
                    command.Parameters.AddWithValue("@67", salaryComponents.gratuity2);
                    command.Parameters.AddWithValue("@68", salaryComponents.conveyance2);
                    command.Parameters.AddWithValue("@69", salaryComponents.production_incentive);
                    command.Parameters.AddWithValue("@70", salaryComponents.early_attandance_bonus_salarypkg);
                    command.Parameters.AddWithValue("@71", salaryComponents.late_attendance_debitrate);
                    command.Parameters.AddWithValue("@72", salaryComponents.latebydays);
                    command.Parameters.AddWithValue("@73", salaryComponents.late_attendence_relaxation);
                    command.Parameters.AddWithValue("@74", salaryComponents.total_Late_Attendance_debit);
                    command.Parameters.AddWithValue("@75", salaryComponents.early_attendance_bonus);
                    command.Parameters.AddWithValue("@76", salaryComponents.optforpf);
                    command.Parameters.AddWithValue("@77", salaryComponents.canteen_credit);
                    command.Parameters.AddWithValue("@78", salaryComponents.employeeName);
                    command.Parameters.AddWithValue("@1", salaryComponents.employeeID);
                    int recordUpdated = command.ExecuteNonQuery();
                    if (recordUpdated > 0)
                    {
                        res.success = true;
                        res.isException = false;
                    }
                    else 
                    {
                        res.success = false;
                        res.isException =  true;
                        res.exception = "Some Error Occured Contact Vikas";
                    }
                }
                else
                {
                    throw new DAOException("The Employee Does NOt Exist. DO make this employee First!");
                }


            }
            catch (DAOException ex)
            {
                res.success = false;
                res.isException = true;
                res.exception = ex.Message;

            }
            catch (System.Exception ex)
            {
                res.success = false;
                res.isException = true;
                res.exception = ex.Message;
            }
            return res;
        }
        public Response saveSalaryComponentofEmployee(SalaryComponents salaryComponents) 
        {
            Response res = new Response();
            string sqlString = "";
            try
            {
                connection = DatabaseConnection.GetConnection();
                //connection.Open();
                //sqlString = "select* from EMPLOYEE where EMPLOYEE_ID = @1";
                //command = new OleDbCommand(sqlString, connection);
                //command.Parameters.AddWithValue("@1", salaryComponents.employeeID);
                //reader = command.ExecuteReader();
                //if (reader.HasRows)
                //{
                    
                    //connection.Close();
                    connection.Open();
                    sqlString = "Insert into SALARYCOMPONENTS(EMPLOYEE_ID,BASIC_AND_DA,BONUS,HRA,CONVEYANCE,PRODUCTION_INCENTIVE,FOOD,ALLOWANCE1,ALLOWANCE2,ALLOWANCE3,ALLOWANCE4,ALLOWANCE5,CEA,OT,COMMITMENT_ALLOWANCE,ATTENDANCE_BONUS,GROSS_SALARY,ALLOWANCE6,ESI_DEBITS,PF_DEBITS,PTAX_DEBITS,TDS_DEBITS,OTHER_DEBITS,TOTALDEBITS,ESI_EMPLOYER_CREDIT,PF_EMPLOYER_CREDIT,MOBILE_PHONE_CREDIT,EARNED_LEAVE_CREDITS,GRATUITY,MEDICAL_INSURANCE,ACCIDENTAL_INSURANCE,TOTAL_OTHER_CREDITS,ACCRUED_DEPOSIT,ACCRUED_SAVINGS,SEVERANCE_PACKAGE,TAKE_HOME,SAVINGS_INCOME,NET_SALARY,CTC,OPT_FOR_ESI,NUMBER_OF_LEAVES,NUMBER_OF_AVAILABLE_WORKING_DAYS,NUMBER_OF_DAYS_WORKED,NUMBER_OF_HOURS_WORKED,OVERTIME_IN_HOURS,SALARYPACKAGE_ALLOWANCE1,SALARYPACKAGE_ALLOWANCE2,SALARYPACKAGE_ALLOWANCE3,SALARYPACKAGE_ALLOWANCE4,SALARYPACKAGE_ALLOWANCE5,SALARYPACKAGE_ALLOWANCE6,SALARYPACKAGE_ATTENDANCE_BONUS,OVERTIME_RATE_INRSPERHOURS,NUMBER_OF_OUTSTATION_DAYS,NUMBER_OF_DAYS_IN_DAILY_REPORT0,MULTIPLICATION_FACTOR,BASIC_PLUS_DA,ALLOWANCE1_MULTIPLICATIONVALUE,ALLOWANCE4_MULTIPLICATIONVALUE,ALLOWANCE5_MULTIPLICATIONVALUE,ALLOWANCE6_MULTIPLICATIONVALUE,BONUS2,PF_DEBITS2,PTAX_DEBITS2,PF_EMPLOYER_CREDIT2,EARNED_LEAVE_CREDIT,GRATUITY2,CONVEYANCE2,PRODUCTION_INCENTIVE2,EARLY_ATTENDANCE_BONUS_SALARYPACKAGE,LATE_ATTENDANCE_DEBITRATE,LATEBYDAYS,LATE_ATTENDANCE_RELAXATION,TOTAL_LATE_ATTENDANCE_DEBIT,EARLY_ATTENDANCE_BONUS,OPT_FOR_PF,CANTEEN_CREDITS, EMPLOYEE_NAME) values (@1,@2,@3,@4,@5,@6,@7,@8,@9,@10,@11,@12,@13,@14,@15,@16,@17,@18,@19,@20,@21,@22,@23,@24,@25,@26,@27,@28,@29,@30,@31,@32,@33,@34,@35,@36,@37,@38,@39,@40,@41,@42,@43,@44,@45,@46,@47,@48,@49,@50,@51,@52,@53,@54,@55,@56,@57,@58,@59,@60,@61,@62,@63,@64,@65,@66,@67,@68,@69,@70,@71,@72,@73,@74,@75,@76,@77,@78) ";
                    command = new OleDbCommand(sqlString, connection);
                    command.Parameters.AddWithValue("@1", salaryComponents.employeeID);
                    command.Parameters.AddWithValue("@2",salaryComponents.basic_plus_DA1);
                    command.Parameters.AddWithValue("@3", salaryComponents.bonus1);
                    command.Parameters.AddWithValue("@4", salaryComponents.hra);
                    command.Parameters.AddWithValue("@5", salaryComponents.conveyance1);
                    command.Parameters.AddWithValue("@6", salaryComponents.productionIncentive1);
                    command.Parameters.AddWithValue("@7", salaryComponents.food);
                    command.Parameters.AddWithValue("@8", salaryComponents.allowance1);
                    command.Parameters.AddWithValue("@9", salaryComponents.allowance2);
                    command.Parameters.AddWithValue("@10", salaryComponents.allowance3_dailyrepory);
                    command.Parameters.AddWithValue("@11", salaryComponents.allowance4);
                    command.Parameters.AddWithValue("@12", salaryComponents.allowance5_telephone);
                    command.Parameters.AddWithValue("@13", salaryComponents.cea);
                    command.Parameters.AddWithValue("@14", salaryComponents.ot_hours);
                    command.Parameters.AddWithValue("@15", salaryComponents.commitment_allowance);
                    command.Parameters.AddWithValue("@16", salaryComponents.attendance_bonus);
                    command.Parameters.AddWithValue("@17", salaryComponents.grossSalary);
                    command.Parameters.AddWithValue("@18", salaryComponents.allowance6);
                    command.Parameters.AddWithValue("@19", salaryComponents.esi_debits);
                    command.Parameters.AddWithValue("@20", salaryComponents.pf_debits);
                    command.Parameters.AddWithValue("@21", salaryComponents.ptax_debits);
                    command.Parameters.AddWithValue("@22", salaryComponents.tds_debits);
                    command.Parameters.AddWithValue("@23", salaryComponents.otherdebits);
                    command.Parameters.AddWithValue("@24", salaryComponents.totaldebits);
                    command.Parameters.AddWithValue("@25", salaryComponents.esi_employer_credit);
                    command.Parameters.AddWithValue("@26", salaryComponents.pf_employer_credit);
                    command.Parameters.AddWithValue("@27", salaryComponents.mobile_phone_credit);
                    command.Parameters.AddWithValue("@28", salaryComponents.earned_leave_credit);
                    command.Parameters.AddWithValue("@29", salaryComponents.gratuity);
                    command.Parameters.AddWithValue("@30", salaryComponents.medical_insurance);
                    command.Parameters.AddWithValue("@31", salaryComponents.accidentql_insurance);
                    command.Parameters.AddWithValue("@32", salaryComponents.total_other_credits);
                    command.Parameters.AddWithValue("@33", salaryComponents.accrued_deposite);
                    command.Parameters.AddWithValue("@34", salaryComponents.accrued_savings);
                    command.Parameters.AddWithValue("@35", salaryComponents.severance_pakage);
                    command.Parameters.AddWithValue("@36", salaryComponents.takeHome);
                    command.Parameters.AddWithValue("@37", salaryComponents.savings_salary);
                    command.Parameters.AddWithValue("@38", salaryComponents.net_salary);
                    command.Parameters.AddWithValue("@39", salaryComponents.ctc);
                    command.Parameters.AddWithValue("@40", salaryComponents.optforEsi);          //40
                    command.Parameters.AddWithValue("@41", salaryComponents.numberofLeaves);    //41
                    command.Parameters.AddWithValue("@42", salaryComponents.number_of_availableWorkingDays);
                    command.Parameters.AddWithValue("@43", salaryComponents.number_of_days_worked);
                    command.Parameters.AddWithValue("@44", salaryComponents.number_of_Hours_worked);
                    command.Parameters.AddWithValue("@45", salaryComponents.overTime_inhours);
                    command.Parameters.AddWithValue("@46", salaryComponents.salary_package_allowance1);
                    command.Parameters.AddWithValue("@47", salaryComponents.salary_package_allowance2_outrstation);
                    command.Parameters.AddWithValue("@48", salaryComponents.salary_package_allowance3_dailyReport);
                    command.Parameters.AddWithValue("@49", salaryComponents.salary_package_allowance4);
                    command.Parameters.AddWithValue("@50", salaryComponents.salary_package_allowance5);
                    command.Parameters.AddWithValue("@51", salaryComponents.salary_package_allowance6);
                    command.Parameters.AddWithValue("@52", salaryComponents.salary_package_attendance_bonus);
                    command.Parameters.AddWithValue("@53", salaryComponents.overtimeRate);
                    command.Parameters.AddWithValue("@54", salaryComponents.numberofOutstationDays);
                    command.Parameters.AddWithValue("@55", salaryComponents.numberofdaysinDailyReport);
                    command.Parameters.AddWithValue("@56", salaryComponents.multriplicationFactor);
                    command.Parameters.AddWithValue("@57", salaryComponents.basicPlusDA_salary_package);
                    command.Parameters.AddWithValue("@58", salaryComponents.allowance1_multiplicationFactor);
                    command.Parameters.AddWithValue("@59", salaryComponents.allowance4_multiplicationFactor);
                    command.Parameters.AddWithValue("@60", salaryComponents.allowance5_multiplicationFactor);
                    command.Parameters.AddWithValue("@61", salaryComponents.allowance6_multiplicationFactor);
                    command.Parameters.AddWithValue("@62", salaryComponents.Bonus);
                    command.Parameters.AddWithValue("@63", salaryComponents.pf_debits2);
                    command.Parameters.AddWithValue("@64", salaryComponents.ptax_debits2);
                    command.Parameters.AddWithValue("@65", salaryComponents.pf_employer_credit2);
                    command.Parameters.AddWithValue("@66", salaryComponents.earned_leave_credit2);
                    command.Parameters.AddWithValue("@67", salaryComponents.gratuity2);
                    command.Parameters.AddWithValue("@68", salaryComponents.conveyance2);
                    command.Parameters.AddWithValue("@69", salaryComponents.production_incentive);
                    command.Parameters.AddWithValue("@70", salaryComponents.early_attandance_bonus_salarypkg);
                    command.Parameters.AddWithValue("@71", salaryComponents.late_attendance_debitrate);
                    command.Parameters.AddWithValue("@72", salaryComponents.latebydays);
                    command.Parameters.AddWithValue("@73", salaryComponents.late_attendence_relaxation);
                    command.Parameters.AddWithValue("@74", salaryComponents.total_Late_Attendance_debit);
                    command.Parameters.AddWithValue("@75", salaryComponents.early_attendance_bonus);
                    command.Parameters.AddWithValue("@76", salaryComponents.optforpf);
                    command.Parameters.AddWithValue("@77", salaryComponents.canteen_credit);
                    command.Parameters.AddWithValue("@78", salaryComponents.employeeName);


                    reader = command.ExecuteReader();
                    if (reader.RecordsAffected == 1)
                    {
                        res.success = true;

                    }
                    else 
                    {
                        res.success = true;
                        res.isException = false;
                        res.exception = "Something bad in saving the components of salary in the database contact Vikas";
                    }
                //}
                //else {
                //    throw new DAOException("The Employee Does NOt Exist. DO make this employee First!");
                //}
            }
            catch (DAOException ex)
            {
                res.success = false;
                res.isException = true;
                res.exception = ex.Message;

            }
            catch (System.Exception ex) 
            {
                res.success = false;
                res.isException = true;
                res.exception = ex.Message;
            }
            return res; 
         }
        public Response GetAllEmployees() 
        {
            Response res = new Response();
            string sqlString = "";
            try 
            {
                List<Employee> employees = new List<Employee>();
                connection = DatabaseConnection.GetConnection();
                connection.Open();
                //sqlString = "Select e.EMPLOYEE_ID, e.EMPLOYEE_NAME,e.EMPLOYEE_ATTENDANCE_ID,p.EMAILADDRESS,p.PANNUMBER,a.BANKNAME,a.ACCOUNTNUMBER,s.TAKE_HOME from EMPLOYEE as e inner join  PERSONALDETAILS as p on e.EMPLOYEE_ID = p.EMPLOYEEID inner join BANKDETAILS as a on p.EMPLOYEEID = a.EMPLOYEE_ID inner join SALARYCOMPONENTS as s on a.EMPLOYEE_ID = s.EMPLOYEE_ID ";
                sqlString = "Select e.EMPLOYEE_ID, e.EMPLOYEE_NAME,e.EMPLOYEE_ATTENDANCE_ID,p.EMAILADDRESS,p.PANNUMBER,a.BANKNAME,a.ACCOUNTNUMBER,s.TAKE_HOME from((EMPLOYEE as e inner join  PERSONALDETAILS as p on e.EMPLOYEE_ID = p.EMPLOYEEID) inner join BANKDETAILS as a on p.EMPLOYEEID = a.EMPLOYEE_ID) inner join SALARYCOMPONENTS as s on a.EMPLOYEE_ID = s.EMPLOYEE_ID";
                command = new OleDbCommand(sqlString, connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Employee employee = new Employee();
                    //employeename
                    
                    employee.employeeName = reader["EMPLOYEE_NAME"].ToString();
                    //employeeId
                    employee.employeeID = Int32.Parse(reader["EMPLOYEE_ID"].ToString());
                    //attendance 
                    employee.attandanceID = Int32.Parse(reader["EMPLOYEE_ATTENDANCE_ID"].ToString());
                    //email address 
                    employee.personalDetails.emialaddress = reader["EMAILADDRESS"].ToString();
                    //panNumber 
                    employee.personalDetails.panNumber = reader["PANNUMBER"].ToString();
                    //bank name
                    employee.employeeBankDetails.bankName = reader["BANKNAME"].ToString();
                    //accountnumber
                    employee.employeeBankDetails.accountNumber = reader["ACCOUNTNUMBER"].ToString();

                    //takehome 
                    employee.salaryComponents.takeHome =Int32.Parse(reader["TAKE_HOME"].ToString());
                    employees.Add(employee);
                }
                res.success = true;
                res.isException = false;
                res.body = employees;
            
            }
            catch(System.Exception ex) 
            {
                res.success = false;
                res.isException = true;
                res.exception = ex.Message;
            }
            return res;
        }

        //Here we get all the employees here
        public Response fetchallEmployees()
        {
            Response res = new Response();
            string sqlString = "";
            try
            {
                List<Employee> employees = new List<Employee>(); 
                connection = DatabaseConnection.GetConnection();
                connection.Open();
                sqlString = "select *  from SALARYCOMPONENTS";
                command = new OleDbCommand(sqlString,connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Employee employee = new Employee();
                    employee.employeeID = Int32.Parse(reader["EMPLOYEE_ID"].ToString());
                    employee.employeeName = reader["EMPLOYEE_NAME"].ToString();
                    employee.employeeNameToshow = reader["EMPLOYEE_NAME"].ToString() + " ("+  reader["EMPLOYEE_ID"].ToString() + ")";
                    employees.Add(employee);
                }
                res.success = true;
                res.isException = false;
                res.body = employees;
            }
            catch (System.Exception ex)
            {
                res.success = false;
                res.isException = true;
                res.exception = ex.Message;
            }
            return res;
        }
        


    }

           
        
    
}
