using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClassLibrary.MainClasses;
using ClassLibrary.Database;
using ClassLibrary.Exception;
using ClassLibrary.DataLayer;
using Microsoft.WindowsAPICodePack.Dialogs;
using ClosedXML.Excel;
using Microsoft.Office.Interop.Excel;

namespace SM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow :System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            if (txtattendanceid.Text == "" && txtemployeeId.Text == "" && txtempname.Text == "" && txtattendanceid.Text == "")
            {
                MessageBox.Show("Enter Each Details");
            }
            else
            {
                Employee emp = new Employee();
                emp.employeeName = txtempname.Text;
                emp.employeeidGivenByCompany = Int32.Parse(txtemployeeId.Text);
                emp.designation = txtDesignation.Text;
                emp.attandanceID = Int32.Parse(txtattendanceid.Text);
                emp.joiningDate = (DateTime)datepicker.SelectedDate;
                Datalayer dl = new Datalayer();
                Response res = dl.addEmpToDatabase(emp);
                if (res.success)
                {
                    MessageBox.Show("Employee Created. Add more data");
                    txtattendanceid.Text = "";
                    txtDesignation.Text = "";
                    //txtemployeeId.Text = "";
                    txtempname.Text = "";
                }
                else if (res.isException)
                {
                    MessageBox.Show("Exception occured : " + res.exception);
                }
            }
        }


        //this will trigger the bank details info fill form
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            BankDetails bankDetailswindow = new BankDetails();
            bankDetailswindow.Show();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            PersonalDetails personalDetailsWindow = new PersonalDetails();
            personalDetailsWindow.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            salarycomponents salarycomponentsWindow = new salarycomponents();
            salarycomponentsWindow.Show();
        }




        /// <summary>
        /// Button Actully Helps to take Excl sheet and save the data in the db.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            //First Thing is We got the Path of the file. So What we can do is 
            //Use that path we have to store all the people in our database.
            // All the salaries and stuff.


            //Logic for First Reading the Data.

            string path = @"C:\PayrollManagerofCTPL\Input Template\DataFile.xlsx";
            var excelFile = new Microsoft.Office.Interop.Excel.Application();
            Workbook workbook = excelFile.Workbooks.Open(path);
            Worksheet worksheet = workbook.Worksheets[1];
            //for (int row = 1, col = 2; row <= 5; row++)
            //{
            //    MessageBox.Show(worksheet.Cells[row,col].Value.ToString());
            //}
            for (int col = 2; col <= 52; col++)
            {
                //for (int row = 1; row < 36; row++) // Actully Row will fetch the details so we have to have to put that in inner Loop.
                SalaryComponents salaryComponents = new SalaryComponents();


                salaryComponents.employeeID = (int)worksheet.Cells[col,1].Value;
                salaryComponents.employeeName = worksheet.Cells[col,2].Value;
                salaryComponents.basic_plus_DA1 = (int) worksheet.Cells[col,3].Value;
                salaryComponents.hra = (int) worksheet.Cells[col,4].Value;
                salaryComponents.conveyance1 = (int)  worksheet.Cells[col,5].Value;
                salaryComponents.productionIncentive1= (int) worksheet.Cells[col,6].Value;
                salaryComponents.food = (int) worksheet.Cells[col,7].Value;
                salaryComponents.allowance1= (int) worksheet.Cells[col,8].Value;
                salaryComponents.allowance2 = (int) worksheet.Cells[col,9].Value;
                salaryComponents.cea = (int) worksheet.Cells[col,10].Value;
                salaryComponents.attendance_bonus = (int)  worksheet.Cells[col,11].Value;
                salaryComponents.bonus1 = (int) worksheet.Cells[col,12].Value;
                salaryComponents.commitment_allowance = (int) worksheet.Cells[col,13].Value;
                salaryComponents.ot_hours = (int) worksheet.Cells[col,14].Value;
                salaryComponents.allowance3_dailyrepory = (int) worksheet.Cells[col,15].Value; 
                salaryComponents.grossSalary = (int)  worksheet.Cells[col,16].Value;
                salaryComponents.esi_debits = (int) worksheet.Cells[col,17].Value;
                salaryComponents.pf_debits = (int)  worksheet.Cells[col,18].Value;
                salaryComponents.ptax_debits = (int) worksheet.Cells[col,19].Value;
                salaryComponents.tds_debits = (int) worksheet.Cells[col,20].Value;
                salaryComponents.totaldebits = (int) worksheet.Cells[col,21].Value;
                salaryComponents.otherdebits = (int) worksheet.Cells[col,22].Value;
                salaryComponents.net_salary = (int) worksheet.Cells[col,23].Value;
                salaryComponents.earned_leave_credit = (int) worksheet.Cells[col,24].Value;
                salaryComponents.takeHome = (int) worksheet.Cells[col,25].Value;
                salaryComponents.severance_pakage= (int)  worksheet.Cells[col,26].Value;
                salaryComponents.esi_employer_credit = (int) worksheet.Cells[col,27].Value;
                salaryComponents.pf_employer_credit = (int) worksheet.Cells[col,28].Value;
                salaryComponents.mobile_phone_credit = (int) worksheet.Cells[col,29].Value;
                salaryComponents.canteen_credit = (int) worksheet.Cells[col,30].Value;
                salaryComponents.gratuity = (int) worksheet.Cells[col,31].Value;
                salaryComponents.medical_insurance = (int) worksheet.Cells[col,32].Value;
                salaryComponents.accidentql_insurance = (int) worksheet.Cells[col,33].Value;
                salaryComponents.total_other_credits = (int)  worksheet.Cells[col,34].Value;
                salaryComponents.savings_salary = (int) worksheet.Cells[col,35].Value;
                salaryComponents.ctc = (int) worksheet.Cells[col,36].Value;
                salaryComponents.numberofLeaves = (int) worksheet.Cells[col,37].Value;
                salaryComponents.number_of_Available_Working_hours = (int)worksheet.Cells[col,38].Value;
                salaryComponents.number_of_availableWorkingDays= (int) worksheet.Cells[col,39].Value;
                salaryComponents.optforEsi = (int)worksheet.Cells[col,40].Value;
                salaryComponents.optforpf = (int)worksheet.Cells[col,41].Value;
                salaryComponents.overtimeRate = (int)worksheet.Cells[col,42].Value;
                salaryComponents.allowance4 = 0;
                salaryComponents.allowance6 = 0;
                salaryComponents.allowance5_telephone = 0;
                salaryComponents.accrued_savings = 0;
                salaryComponents.accrued_deposite = 0;
                salaryComponents.number_of_days_worked = 0;
                salaryComponents.number_of_Hours_worked = 0;
                salaryComponents.salary_package_allowance2_outrstation = 0;
                salaryComponents.salary_package_allowance3_dailyReport = 0;
                salaryComponents.salary_package_allowance4 = 0;
                salaryComponents.salary_package_allowance5 = 0;
                salaryComponents.salary_package_attendance_bonus = 0;
                salaryComponents.numberofOutstationDays = 0;
                salaryComponents.numberofdaysinDailyReport = 0;
                salaryComponents.Bonus = 0;
                salaryComponents.basicPlusDA_salary_package = 0;
                salaryComponents.pf_debits2 = 0;
                salaryComponents.allowance1_multiplicationFactor = 0;
                salaryComponents.allowance4_multiplicationFactor = 0;
                salaryComponents.allowance5_multiplicationFactor = 0;
                salaryComponents.allowance6_multiplicationFactor = 0;
                salaryComponents.ptax_debits2 = 0;
                salaryComponents.pf_employer_credit2 = 0;
                salaryComponents.production_incentive = 0;
                salaryComponents.early_attandance_bonus_salarypkg = 0;
                salaryComponents.late_attendance_debitrate = 0;
                salaryComponents.latebydays = 0;
                salaryComponents.early_attendance_bonus = 0;
                salaryComponents.earned_leave_credit2 = 0;
                Datalayer dl = new Datalayer();
                Response res = dl.saveSalaryComponentofEmployee(salaryComponents);
                if (res.success)
                {
                    MessageBox.Show("Salary Components saved in the Database");
                }
                else if (res.isException)
                {
                    MessageBox.Show("Exception occured : " + res.exception);
                }



            }



            if (!string.IsNullOrEmpty(txtpathofemployee.Text))
            {
                    SalaryComponents salaryComponents = new SalaryComponents();
            }
            excelFile.Workbooks.Close();


            

          
        }
        /// <summary>
        /// This is actually to get the file path
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectthepath_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.Multiselect = false;
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                txtpathofemployee.Text = dialog.FileName;
            }
            
                

        }
    }
}
