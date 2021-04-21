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
using System.Windows.Shapes;
using ClassLibrary.MainClasses;
using ClassLibrary.Database;
using ClassLibrary.DataLayer;
using ClosedXML.Excel;
using System.IO;
using System.Threading;
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;

namespace SM
{
    /// <summary>
    /// Interaction logic for GenerateSalary.xaml
    /// </summary>
    public partial class GenerateSalary : System.Windows.Window
    {
        public GenerateSalary()
        {
            InitializeComponent();
            populateEmployees();
            populateMonths();
            populateYears();
            if (cmbEmployees.Items.Count > 0) cmbEmployees.SelectedIndex = 0;
                    
           //populatethedetailsofemployees();
        }
        public String getCurrentMonth() 
        {
            return (String)(cmbselectMonth.SelectedItem); 
        }
        public string getcurrentYear()
        {
            return (String)(cmbselectyear.SelectedItem);
        }

        public Employee GetSelectedEmployee() 
        {
            return (Employee)(cmbEmployees.SelectedItem);
        }
        
        /// <summary>
        /// This will Populate the details of employees for the database in the boxes.
        /// </summary>
        public void populatethedetailsofemployees() 
        {
            Datalayer dl = new Datalayer();

            //First get the employee data from here and then you can see the data from there.
            Employee employee = new Employee();
            employee = GetSelectedEmployee();
            //MessageBox.Show(employee.employeeID.ToString());
            Response res = dl.getEmployeeSalaryDetails(GetSelectedEmployee());
            if (res.success)
            {
                
                SalaryComponents salaryComponents = new SalaryComponents();
                //Logic ??????
                if (salaryComponents.optforpf == 1)
                {
                    ckboptforpf.IsChecked = true;

                }
                if (salaryComponents.optforEsi == 1)
                {
                    ckboptforesi.IsChecked = true;
                }
                salaryComponents = (SalaryComponents)res.body;
                txtbasicandDA.Text = salaryComponents.basic_plus_DA1.ToString();
                txtBonus.Text = salaryComponents.bonus1.ToString();
                txtConveyance.Text = salaryComponents.conveyance1.ToString();
                txtProductionIncentive.Text = salaryComponents.productionIncentive1.ToString();
                txtFood.Text = salaryComponents.food.ToString();
                txtcompanyrevenue_ltd_allow.Text = salaryComponents.allowance1.ToString();
                txtspecialAllowance.Text = salaryComponents.allowance2.ToString();
                txtallowance5.Text = salaryComponents.allowance5_telephone.ToString();
                txtAllowanceMobile.Text = salaryComponents.allowance4.ToString();    //
                txtCEA.Text = salaryComponents.cea.ToString();
                txtcommmitmentallowance.Text = salaryComponents.commitment_allowance.ToString();
                txtAttendanceBonus.Text = salaryComponents.attendance_bonus.ToString();
                txtGrossSalary.Text = salaryComponents.grossSalary.ToString();
                txtESIDebits.Text = salaryComponents.esi_debits.ToString();
                txtpfDebits.Text = salaryComponents.pf_debits.ToString();
                txtPtaxDebits.Text = salaryComponents.ptax_debits.ToString();
                txtTdsDebits.Text = salaryComponents.tds_debits.ToString();
                txtOtherDebits.Text = salaryComponents.otherdebits.ToString();
                //txtlateattendance
                txtTotaldebits.Text = salaryComponents.totaldebits.ToString();
                txtEsi_Employer_credits.Text = salaryComponents.esi_employer_credit.ToString();
                txtPf_employer_credits.Text = salaryComponents.pf_employer_credit.ToString();
                txtMobilePhoneCredits.Text = salaryComponents.mobile_phone_credit.ToString();
                txtEarnedLeavesCredit.Text = salaryComponents.earned_leave_credit.ToString();
                txtGratuity.Text = salaryComponents.gratuity.ToString();
                txtMedicalInsurance.Text = salaryComponents.medical_insurance.ToString();
                txtAccidentalInsurance.Text = salaryComponents.accidentql_insurance.ToString();
                txtEarlyAttendanceBonus.Text = salaryComponents.early_attendance_bonus.ToString();
                txttotalOtherCredits.Text = salaryComponents.total_other_credits.ToString();
                txtTotalAccruedDeposit.Text = salaryComponents.accrued_deposite.ToString();
                txtaccuredSavings.Text = salaryComponents.accrued_savings.ToString();
                txtCurrentMonthAccredDeposit.Text = "0";
                txtCurrentMonthAccruedSavings.Text = "0";
                txtSeverancePackage.Text = salaryComponents.severance_pakage.ToString();
                txttakehome.Text = salaryComponents.takeHome.ToString();
                txtSavingsIncome.Text = salaryComponents.savings_salary.ToString();
                txtNetsalary.Text = salaryComponents.net_salary.ToString();
                txtCTC.Text = salaryComponents.ctc.ToString();
                




            }
            else if (res.isException)
            {
                MessageBox.Show("Exception while doing certain automation" + res.exception);
            }
            
        }

        

        public void populateEmployees()
        {
            try
            {
                Datalayer dl = new Datalayer();
                Response res = dl.fetchallEmployees();
                if (res.success)
                {
                    List<Employee> employees = (List<Employee>)res.body;
                    cmbEmployees.ItemsSource = employees;
                    cmbEmployees.DisplayMemberPath = "employeeNameToshow";
                   // cmbEmployees.SelectionChanged += cmbCompaniesSelectionChanged; 
                }
                else if (res.isException)
                {
                    MessageBox.Show("Exception while populating companies : " + res.exception);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Exception in populate companies cmb : " + exception);
            }
        }

        private void cmbEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            populatethedetailsofemployees();
        }

        //Generate for all.
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //for this we have to take data from all the employees and then we have to 
           
            if (cmbselectMonth.SelectedIndex > 0 && cmbselectyear.SelectedIndex > 0)
            {
                //First get all the employees.
                try
                {
                    Datalayer dl = new Datalayer();
                    Response res = dl.GetAllEmployeesID();
                    if (res.success)
                    {
                        List<Employee> employees = (List<Employee>)res.body;
                        for (int i = 0; i < employees.Count; i++)
                        {
                            //How we have the employee ID of all the employees so now let's start the game !

                            //we can fetch the salary components according to the employeeid
                            Datalayer dla = new Datalayer();
                            Response response = dla.getEmployeeSalaryDetailsbyID(employees[i].employeeID);
                            if (response.success)
                            {

                                SalaryComponents salaryComponents = new SalaryComponents();
                                salaryComponents =(SalaryComponents) response.body;
                                //salaryComponents = (SalaryComponents)res.body;
                                ////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\/\/\/\/\/\/\/\/\/\/\/\/\/\//\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
                                var Workbook = new XLWorkbook("SALARYCOMPONENTS.xlsx");
                                IXLWorksheet worksheet = Workbook.Worksheet("Sal Temp");


                                worksheet.Cell("B1").Value = salaryComponents.basic_plus_DA1;                        
                                worksheet.Cell("B3").Value = salaryComponents.hra;     
                                worksheet.Cell("B4").Value = salaryComponents.conveyance1;                    
                                worksheet.Cell("B5").Value = salaryComponents.productionIncentive1; 
                                worksheet.Cell("B6").Value = salaryComponents.food;  
                                worksheet.Cell("B7").Value = salaryComponents.allowance1; 
                                worksheet.Cell("B8").Value = salaryComponents.allowance2; 
                                worksheet.Cell("B14").Value = salaryComponents.allowance3_dailyrepory;
                                worksheet.Cell("B15").Value = salaryComponents.allowance4;
                                worksheet.Cell("B16").Value = salaryComponents.allowance5_telephone; 
                                worksheet.Cell("B17").Value = salaryComponents.allowance6;
                                worksheet.Cell("B9").Value = salaryComponents.cea;  
                                worksheet.Cell("F15").Value = salaryComponents.overtimeRate;
                                worksheet.Cell("F14").Value = salaryComponents.salary_package_attendance_bonus;
                                worksheet.Cell("F7").Value = salaryComponents.ot_hours;
                                worksheet.Cell("D4").Value = salaryComponents.tds_debits;
                                worksheet.Cell("D5").Value = salaryComponents.otherdebits;
                                worksheet.Cell("I13").Value = salaryComponents.late_attendance_debitrate; 
                                worksheet.Cell("D9").Value = salaryComponents.mobile_phone_credit;
                                worksheet.Cell("D10").Value = salaryComponents.canteen_credit;
                                worksheet.Cell("D13").Value = salaryComponents.medical_insurance;
                                worksheet.Cell("D14").Value = salaryComponents.accidentql_insurance;
                                worksheet.Cell("I17").Value = salaryComponents.early_attendance_bonus; 
                                worksheet.Cell("D18").Value = salaryComponents.severance_pakage;

                                if (salaryComponents.optforEsi == 1)
                                {
                                    worksheet.Cell("F1").Value = 1;

                                }
                                else 
                                {
                                    worksheet.Cell("F1").Value = 0;
                                }
                                if (salaryComponents.optforpf == 1)
                                {
                                    worksheet.Cell("F2").Value = 1;
                                }
                                else 
                                {
                                    worksheet.Cell("F2").Value = 0;
                                }




                                ////Display the data now 
                                //txtBonus.Text = (worksheet.Cell("B2").Value).ToString();
                                //txtCommitmentAllowance.Text = (worksheet.Cell("B11").Value).ToString();
                                //txtGrossSalary.Text = (worksheet.Cell("B13").Value).ToString();

                                //txtPtaxDebits.Text = (worksheet.Cell("D3").Value).ToString();
                                //txtTotalDebits.Text = (worksheet.Cell("D6").Value).ToString();
                                //txtESIEmployerCredit.Text = (worksheet.Cell("D7").Value).ToString();
                                //txtPFEmployerCredit.Text = (worksheet.Cell("D8").Value).ToString();
                                //txtEarnedLeavecredit.Text = (worksheet.Cell("D11").Value).ToString();
                                //txtGratuity.Text = (worksheet.Cell("D12").Value).ToString();
                                //txttotalOtherCredits.Text = (worksheet.Cell("D15").Value).ToString();
                                //txtTakeHome.Text = (worksheet.Cell("D19").Value).ToString();
                                //txtsavingsalary.Text = (worksheet.Cell("D20").Value).ToString();
                                //txtnetsalary.Text = (worksheet.Cell("D21").Value).ToString();
                                //txtctc.Text = (worksheet.Cell("D22").Value).ToString();

                                //Now creating the particular Fields for the system

                                //Create the folder for the employee
                                string monthname = getCurrentMonth();
                                string year = getcurrentYear();
                                string nameofEMP = salaryComponents.employeeName;
                                string pathfileabs = @"C:\PayrollManagerofCTPL\SalaryData\";
                                string path = pathfileabs + nameofEMP+@"\" +year + @"\"+ monthname;
                                if (!Directory.Exists(path))
                                {
                                    Directory.CreateDirectory(path);
                                    //Thread.Sleep(100);
                                }
                                string filename = monthname + "" + year;
                                string filepath = path + @"\" + filename + ".xlsx";
                                Workbook.SaveAs(filepath);
                                GenerateSalaryOfEmployee generateSalaryOfEmployee = new ClassLibrary.MainClasses.GenerateSalaryOfEmployee();
                                generateSalaryOfEmployee.employeeID = salaryComponents.employeeID;
                                generateSalaryOfEmployee.nameofEmployee = salaryComponents.employeeName;
                                generateSalaryOfEmployee.monthName = monthname;
                                generateSalaryOfEmployee.year = year;
                                generateSalaryOfEmployee.pathFile = filepath;

                                Datalayer datalayer = new Datalayer();
                                Response response1 = dl.savegeneratesalaryinDB(generateSalaryOfEmployee);
                                if (response1.success)
                                {

                                }
                                else if (res.isException)
                                {
                                    MessageBox.Show("Error occured" + response1.exception);
                                }
                                //Workbook.SaveAs(@"C:\Users\Vikas\Desktop\Merci\file.xlsx");


                            }

                        }


                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception on the front Page" + ex);
                }
                MessageBox.Show("Generated For all");
            }
            else 
            {
                MessageBox.Show("Choose Proper Month And Year In order to genearate the salary");
            }
           
        }

        //Save/Update In database
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            Employee employee = new Employee();
            employee = GetSelectedEmployee();
            SalaryComponents salaryComponents = new SalaryComponents();
            salaryComponents.employeeID = employee.employeeID;
            salaryComponents.basic_plus_DA1 = Int32.Parse(txtbasicandDA.Text);
            salaryComponents.bonus1 = Int32.Parse(txtBonus.Text);
            salaryComponents.hra = Int32.Parse(txthra.Text);
            salaryComponents.conveyance1 = Int32.Parse(txtConveyance.Text);
            salaryComponents.productionIncentive1 = Int32.Parse(txtProductionIncentive.Text);
            salaryComponents.food = Int32.Parse(txtFood.Text);
            salaryComponents.allowance1 = Int32.Parse(txtcompanyrevenue_ltd_allow.Text);
            salaryComponents.allowance2 = Int32.Parse(txtoutstationallowance.Text);
            salaryComponents.cea = Int32.Parse(txtCEA.Text);
            salaryComponents.ot_hours = Int32.Parse(txtOverTimeHours.Text);
            salaryComponents.commitment_allowance = Int32.Parse(txtcommmitmentallowance.Text);
            salaryComponents.attendance_bonus = Int32.Parse(txtAttendanceBonus.Text);
            salaryComponents.grossSalary = Int32.Parse(txtGrossSalary.Text);
            salaryComponents.allowance3_dailyrepory = Int32.Parse(txtdailyReportallowance.Text);
            salaryComponents.allowance4 = Int32.Parse(txtAllowanceMobile.Text); ;
            salaryComponents.allowance5_telephone = Int32.Parse(txtallowance5.Text);
            salaryComponents.allowance6 = Int32.Parse(txtspecialAllowance.Text);
            salaryComponents.esi_debits = Int32.Parse(txtESIDebits.Text);
            salaryComponents.pf_debits = Int32.Parse(txtpfDebits.Text);
            salaryComponents.ptax_debits = Int32.Parse(txtPtaxDebits.Text);
            salaryComponents.tds_debits = Int32.Parse(txtTdsDebits.Text);
            salaryComponents.otherdebits = Int32.Parse(txtOtherDebits.Text);
            salaryComponents.totaldebits = Int32.Parse(txtTotaldebits.Text);
            salaryComponents.esi_employer_credit = Int32.Parse(txtEsi_Employer_credits.Text);
            salaryComponents.pf_employer_credit = Int32.Parse(txtPf_employer_credits.Text);
            salaryComponents.mobile_phone_credit = Int32.Parse(txtMobilePhoneCredits.Text);
            salaryComponents.canteen_credit = Int32.Parse(txtCanteenCredits.Text);
            salaryComponents.earned_leave_credit = Int32.Parse(txtEarnedLeavesCredit.Text);
            salaryComponents.gratuity = Int32.Parse(txtGratuity.Text);
            salaryComponents.medical_insurance = Int32.Parse(txtMedicalInsurance.Text);
            salaryComponents.accidentql_insurance = Int32.Parse(txtAccidentalInsurance.Text);
            salaryComponents.total_other_credits = Int32.Parse(txttotalOtherCredits.Text);
            salaryComponents.accrued_deposite = 0;
            salaryComponents.accrued_savings = 0;
            salaryComponents.severance_pakage = Int32.Parse(txtSeverancePackage.Text);
            salaryComponents.takeHome = Int32.Parse(txttakehome.Text);
            salaryComponents.savings_salary = Int32.Parse(txtSavingsIncome.Text);
            salaryComponents.net_salary = Int32.Parse(txtNetsalary.Text);
            salaryComponents.ctc = Int32.Parse(txtCTC.Text);
            if (ckboptforesi.IsChecked == true)
            {
                salaryComponents.optforEsi = 1;
            }
            else
            {
                salaryComponents.optforEsi = 0;
            }
            if (ckboptforpf.IsChecked == true)
            {
                salaryComponents.optforpf = 1;
            }
            else
            {
                salaryComponents.optforpf = 0;
            }

            salaryComponents.numberofLeaves = 0;
            salaryComponents.number_of_availableWorkingDays = 0;
            salaryComponents.number_of_days_worked = 0;
            salaryComponents.number_of_Hours_worked = 0;
            salaryComponents.overTime_inhours = 0;
            salaryComponents.salary_package_allowance1 = 0;
            salaryComponents.salary_package_allowance2_outrstation = 0;
            salaryComponents.salary_package_allowance3_dailyReport = 0;
            salaryComponents.salary_package_allowance4 = 0;
            salaryComponents.salary_package_allowance5 = 0;
            salaryComponents.salary_package_allowance6 = 0;
            salaryComponents.salary_package_attendance_bonus = 0;
            salaryComponents.overtimeRate = Int32.Parse(txtOTRate.Text);
            salaryComponents.numberofOutstationDays = 0;
            salaryComponents.numberofdaysinDailyReport = 0;
            salaryComponents.multriplicationFactor = 1;
            salaryComponents.basicPlusDA_salary_package = 0;
            salaryComponents.allowance1_multiplicationFactor = 0;
            salaryComponents.allowance4_multiplicationFactor = 0;
            salaryComponents.allowance5_multiplicationFactor = 0;
            salaryComponents.allowance6_multiplicationFactor = 0;
            salaryComponents.Bonus = 0;
            salaryComponents.pf_debits2 = 0; //--------------------------------------???
            salaryComponents.ptax_debits2 = 0;
            salaryComponents.pf_employer_credit2 = 0;
            salaryComponents.earned_leave_credit2 = 0;
            salaryComponents.gratuity2 = 0;
            salaryComponents.conveyance2 = 0;
            salaryComponents.production_incentive = 0;
            salaryComponents.early_attandance_bonus_salarypkg = 0;
            salaryComponents.late_attendance_debitrate = Int32.Parse(txtlateattendencedebitrate.Text);
            salaryComponents.latebydays = 0;
            salaryComponents.late_attendence_relaxation = 0;
            salaryComponents.total_Late_Attendance_debit = 0;
            salaryComponents.early_attendance_bonus = Int32.Parse(txtEarlyAttendanceBonus.Text);
            salaryComponents.employeeName = employee.employeeName;

            Datalayer dl = new Datalayer();
            MessageBox.Show(salaryComponents.employeeID.ToString());
            Response res = dl.UpdateSalaryComponentsofEmployees(salaryComponents);
            if (res.success)
            {
                MessageBox.Show("Salary Components saved in the Database");
            }
            else if (res.isException)
            {
                MessageBox.Show("Exception occured : " + res.exception);
            }
        }
        private void populateYears()
        {
            try
            {
                Datalayer dl = new Datalayer();
                Response res = dl.getallYears();
                if (res.success)
                {
                    String[] YearName = (String[])res.body;
                    cmbselectyear.ItemsSource = YearName;
                    
                }
                else if (res.isException)
                {
                    MessageBox.Show("Exception in populationg the Month names " + res.exception);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception on the front Page" + ex);
            }

        }
        private void populateMonths() 
        {
            try
            {
                Datalayer dl = new Datalayer();
                Response res = dl.getallMonths();
                if (res.success)
                {
                    String[] Monthnames = (String[])res.body;
                    cmbselectMonth.ItemsSource = Monthnames;
                    //cmbselectMonth.DisplayMemberPath = "Monthnames";
                }
                else if (res.isException)
                {
                    MessageBox.Show("Exception in populationg the Month names " + res.exception);
                }

            }
            catch (Exception ex) 
            {
                MessageBox.Show("Exception on the front Page" + ex);
            }
        }

        //Recalculate
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var Workbook = new XLWorkbook("SALARYCOMPONENTS.xlsx");
            IXLWorksheet worksheet = Workbook.Worksheet("Sal Temp");


            worksheet.Cell("B1").Value = Int32.Parse(txtbasicandDA.Text);
            worksheet.Cell("B3").Value = Int32.Parse(txthra.Text);
            worksheet.Cell("B4").Value = Int32.Parse(txtConveyance.Text);
            worksheet.Cell("B5").Value = Int32.Parse(txtProductionIncentive.Text);
            worksheet.Cell("B6").Value = Int32.Parse(txtFood.Text);
            worksheet.Cell("B7").Value = Int32.Parse(txtcompanyrevenue_ltd_allow.Text);
            worksheet.Cell("B8").Value = Int32.Parse(txtoutstationallowance.Text);
            worksheet.Cell("B14").Value = Int32.Parse(txtdailyReportallowance.Text);
            worksheet.Cell("B15").Value = Int32.Parse(txtspecialAllowance.Text);
            worksheet.Cell("B16").Value = Int32.Parse(txtallowance5.Text);
            worksheet.Cell("B17").Value = Int32.Parse(txtAllowanceMobile.Text);
            worksheet.Cell("B9").Value = Int32.Parse(txtCEA.Text);
            worksheet.Cell("F15").Value = Int32.Parse(txtOTRate.Text);
            worksheet.Cell("F14").Value = Int32.Parse(txtAttendanceBonus.Text);
            worksheet.Cell("F7").Value = Int32.Parse(txtOverTimeHours.Text);
            worksheet.Cell("D4").Value = Int32.Parse(txtTdsDebits.Text);
            worksheet.Cell("D5").Value = Int32.Parse(txtOtherDebits.Text);
            worksheet.Cell("I13").Value = Int32.Parse(txtlateattendencedebitrate.Text);
            worksheet.Cell("D9").Value = Int32.Parse(txtMobilePhoneCredits.Text);
            worksheet.Cell("D10").Value = Int32.Parse(txtCanteenCredits.Text);
            worksheet.Cell("D13").Value = Int32.Parse(txtMedicalInsurance.Text);
            worksheet.Cell("D14").Value = Int32.Parse(txtAccidentalInsurance.Text);
            worksheet.Cell("I17").Value = Int32.Parse(txtEarlyAttendanceBonus.Text);
            worksheet.Cell("D18").Value = Int32.Parse(txtSeverancePackage.Text);
            if (ckboptforpf.IsChecked == true)
            {
                worksheet.Cell("F1").Value = 1;
                txtpfDebits.Text = (worksheet.Cell("D2").Value).ToString();
            }
            else
            {
                worksheet.Cell("F1").Value = 0;
            }

            if (ckboptforesi.IsChecked == true)
            {
                worksheet.Cell("F2").Value = 1;
                txtESIDebits.Text = (worksheet.Cell("D1").Value).ToString();

            }
            else
            {
                worksheet.Cell("F2").Value = 0;
            }



            //Display the data now 
            txtBonus.Text = (worksheet.Cell("B2").Value).ToString();
            txtcommmitmentallowance.Text = (worksheet.Cell("B11").Value).ToString();
            txtGrossSalary.Text = (worksheet.Cell("B13").Value).ToString();
            txtPtaxDebits.Text = (worksheet.Cell("D3").Value).ToString();
            txtTotaldebits.Text = (worksheet.Cell("D6").Value).ToString();
            txtEsi_Employer_credits.Text = (worksheet.Cell("D7").Value).ToString();
            txtPf_employer_credits.Text = (worksheet.Cell("D8").Value).ToString();
            txtEarnedLeavesCredit.Text = (worksheet.Cell("D11").Value).ToString();
            txtGratuity.Text = (worksheet.Cell("D12").Value).ToString();
            txttotalOtherCredits.Text = (worksheet.Cell("D15").Value).ToString();
            txttakehome.Text = (worksheet.Cell("D19").Value).ToString();
            txtSavingsIncome.Text = (worksheet.Cell("D20").Value).ToString();
            txtNetsalary.Text = (worksheet.Cell("D21").Value).ToString();
            txtCTC.Text = (worksheet.Cell("D22").Value).ToString();

            Workbook.SaveAs(@"C:\Users\Vikas\Desktop\Merci\file.xlsx");
        }

        private void cmbselectMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        /// <summary>
        ///                 Re-Generate the entire Salary   [ for all]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        ///         Re-Generate for the Single employee
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        ///             Show the previous data [Salary what is the salary of that particular employee after this all time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        ///                 EDIT the employee data which can be driven along with that
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            
        }
        //Import the attendance from the excel file.
        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            
            if (!(cmbselectMonth.SelectedIndex == -1 && cmbselectyear.SelectedIndex == -1))
            {
                string filepath = "";
                //Attendance Logic
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Title = "Excel File Dialog";
                dialog.InitialDirectory = @"c:\";
                dialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                //dialog.IsFolderPicker = true;

                if (dialog.ShowDialog() == DialogResult)
                {
                    filepath = dialog.FileName;
                }
                MessageBox.Show(filepath);

                //At this moment we have the path of the excel file.
                var excelFile = new Microsoft.Office.Interop.Excel.Application();
                filepath = @"C:\Users\Vikas\Desktop\ChromeDownload\file.xlsx";
                Workbook workbook = excelFile.Workbooks.Open(filepath);
                Worksheet worksheet = workbook.Worksheets[1];
                for (int col = 9; col < 1319; col = col + 14)
                {
                    string value = worksheet.Cells[col,2].Value;
                    double totalWorkDuration = worksheet.Cells[col,12 ].Value;
                    double present = worksheet.Cells[col,16 ].Value;
                    double absent = worksheet.Cells[col,19 ].Value;
                    double Latebydays = worksheet.Cells[col,23 ].Value;
                    // double earlygoingbydays = worksheet.Cells[col, 29];
                    // double LeavesTaken = worksheet.Cells[col, 32];

                    // MessageBox.Show(earlygoingbydays.ToString());

                    //Now at this point we have the Name of Employees
                    Datalayer dl = new Datalayer();
                    Response res = dl.getSalarycomponentsvianame(value);
                    //this will show that our query is fine and working
                    //and we have the data
                    if (res.success)
                    {
                        SalaryComponents salarycomponents = (SalaryComponents)res.body;
                        salarycomponents.number_of_Hours_worked =  (int)totalWorkDuration;
                        salarycomponents.number_of_days_worked = (int)present;
                        salarycomponents.numberofLeaves = (int)absent  ;
                        salarycomponents.latebydays = (int)Latebydays;
                        Datalayer dla = new Datalayer();
                        Response response = dla.UpdateSalaryComponentsofEmployees(salarycomponents);
                        if (response.success)
                        {
                            MessageBox.Show("Salary Components saved in the Database");
                        }
                        else if (response.isException)
                        {
                            MessageBox.Show("Exception occured : " + res.exception);
                        }
                    }
                    if (res.isException)
                    {
                        MessageBox.Show("This name is not in the database" + value);
                    }

                }
            }
            else
            {
                MessageBox.Show("Please Select the Month and Year for the Attendance");
            }
        }
    }
}
