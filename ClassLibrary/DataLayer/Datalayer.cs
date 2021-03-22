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
    }

           
        
    
}
