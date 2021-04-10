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
using ClassLibrary.DataLayer;
using ClassLibrary.Exception;
using ClosedXML.Excel;
using Microsoft.Office.Interop.Excel;

namespace SM
{
    /// <summary>
    /// Interaction logic for salarycomponents.xaml
    /// </summary>
    public partial class salarycomponents : System.Windows.Window
    {
        public salarycomponents()
        {
            InitializeComponent();
            populateEmployees();
            if (cmbEmployees.Items.Count > 0) cmbEmployees.SelectedIndex = 0;
            
            //populatethedetailsofemployees();

        }


        public Employee GetSelectedEmployee()
        {
            return (Employee)(cmbEmployees.SelectedItem);
        }
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
                salaryComponents = (SalaryComponents)res.body;
                txtBasicplusDA.Text = salaryComponents.basic_plus_DA1.ToString();
                txtBonus.Text = salaryComponents.bonus1.ToString();
                txtConveyance.Text = salaryComponents.conveyance1.ToString();
                txtProductionIncentive.Text = salaryComponents.productionIncentive1.ToString();
                txtFood.Text = salaryComponents.food.ToString();
                txtCompanyRevenueLtd.Text = salaryComponents.allowance1.ToString();
                txtdailyAllow.Text = salaryComponents.allowance2.ToString();
                txtMobileAllowance.Text = salaryComponents.allowance5_telephone.ToString();
                txtoutstationAllow.Text = salaryComponents.allowance4.ToString();    //
                txtCEA.Text = salaryComponents.cea.ToString();
                txtCommitmentAllowance.Text = salaryComponents.commitment_allowance.ToString();
                txtAttendanceBonus.Text = salaryComponents.attendance_bonus.ToString();
                txtGrossSalary.Text = salaryComponents.grossSalary.ToString();
                txtEsiDebits.Text = salaryComponents.esi_debits.ToString();
                txtPfDebits.Text = salaryComponents.pf_debits.ToString();
                txtPtaxDebits.Text = salaryComponents.ptax_debits.ToString();
                txtTDSDebits.Text = salaryComponents.tds_debits.ToString();
                txtOtherDebits.Text = salaryComponents.otherdebits.ToString();
                //txtlateattendance
                txtTotalDebits.Text = salaryComponents.totaldebits.ToString();
                txtESIEmployerCredit.Text = salaryComponents.esi_employer_credit.ToString();
                txtPFEmployerCredit.Text = salaryComponents.pf_employer_credit.ToString();
                txtMobileCredit.Text = salaryComponents.mobile_phone_credit.ToString();
                txtEarnedLeavecredit.Text = salaryComponents.earned_leave_credit.ToString();
                txtGratuity.Text = salaryComponents.gratuity.ToString();
                txtMedicalInsurance.Text = salaryComponents.medical_insurance.ToString();
                txtAccidentInsurance.Text = salaryComponents.accidentql_insurance.ToString();
                txtEarlyAttendanceBonus.Text = salaryComponents.early_attendance_bonus.ToString();
                txttotalOtherCredits.Text = salaryComponents.total_other_credits.ToString();
                
               // txtdepos .Text = salaryComponents.accrued_deposite.ToString();
               // txtaccuredSavings.Text = salaryComponents.accrued_savings.ToString();
                // txtCurrentMonthAccredDeposit.Text = "0";
               // txtCurrentMonthAccruedSavings.Text = "0";
                txtSeverancePackage.Text = salaryComponents.severance_pakage.ToString();
                txtTakeHome.Text = salaryComponents.takeHome.ToString();
               txtsavingsalary .Text = salaryComponents.savings_salary.ToString();
               txtnetsalary.Text = salaryComponents.net_salary.ToString();
               txtctc.Text = salaryComponents.ctc.ToString();




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
                   // cmbEmployeeName.SelectionChanged += cmbEmployeeName_SelectionChanged;  
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



        //This class will save the data in the database. So we have to write the logic so that we can do the calculations in the field and have to update them
        // as well when it trigger something like a + b then C will immediate have the value when we habe defined something like a+ b = c in the field of c.


        /// <summary>
        /// This is the method which will save all the data and call the datalayer to save the salary components into the database.
        /// </summary>
        /// <param name="sender"></param>
        /// 
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = new Employee();
            employee = GetSelectedEmployee();
            SalaryComponents salaryComponents = new SalaryComponents();
            salaryComponents.employeeID = employee.employeeID;
            salaryComponents.basic_plus_DA1 = Int32.Parse(txtBasicplusDA.Text);
            salaryComponents.bonus1 = Int32.Parse(txtBonus.Text);
            salaryComponents.hra = Int32.Parse(txtHRA.Text);
            salaryComponents.conveyance1 = Int32.Parse(txtConveyance.Text);
            salaryComponents.productionIncentive1 = Int32.Parse(txtProductionIncentive.Text);
            salaryComponents.food = Int32.Parse(txtFood.Text);
            salaryComponents.allowance1 = Int32.Parse(txtCompanyRevenueLtd.Text);
            salaryComponents.allowance2 = Int32.Parse(txtoutstationAllow.Text);
            salaryComponents.cea = Int32.Parse(txtCEA.Text);
            salaryComponents.ot_hours = Int32.Parse(txtMinimumOtHours.Text);
            salaryComponents.commitment_allowance = Int32.Parse(txtCommitmentAllowance.Text);
            salaryComponents.attendance_bonus = Int32.Parse(txtAttendanceBonus.Text);
            salaryComponents.grossSalary = Int32.Parse(txtGrossSalary.Text);
            salaryComponents.allowance3_dailyrepory = Int32.Parse(txtdailyAllow.Text);
            salaryComponents.allowance4 = Int32.Parse(txtMobileAllowance.Text); ;
            salaryComponents.allowance5_telephone = Int32.Parse(txtAllowance5.Text);
            salaryComponents.allowance6 = Int32.Parse(txtspecialallow.Text);
            salaryComponents.esi_debits = Int32.Parse(txtEsiDebits.Text);
            salaryComponents.pf_debits = Int32.Parse(txtPfDebits.Text);
            salaryComponents.ptax_debits = Int32.Parse(txtPtaxDebits.Text);
            salaryComponents.tds_debits = Int32.Parse(txtTDSDebits.Text);
            salaryComponents.otherdebits = Int32.Parse(txtOtherDebits.Text);
            salaryComponents.totaldebits = Int32.Parse(txtTotalDebits.Text);
            salaryComponents.esi_employer_credit = Int32.Parse(txtESIEmployerCredit.Text);
            salaryComponents.pf_employer_credit = Int32.Parse(txtPFEmployerCredit.Text);
            salaryComponents.mobile_phone_credit = Int32.Parse(txtMobileCredit.Text);
            salaryComponents.canteen_credit = Int32.Parse(txtCanteenCredit.Text);
            salaryComponents.earned_leave_credit = Int32.Parse(txtEarnedLeavecredit.Text);
            salaryComponents.gratuity = Int32.Parse(txtGratuity.Text);
            salaryComponents.medical_insurance = Int32.Parse(txtMedicalInsurance.Text);
            salaryComponents.accidentql_insurance = Int32.Parse(txtAccidentInsurance.Text);
            salaryComponents.total_other_credits = Int32.Parse(txttotalOtherCredits.Text);
            salaryComponents.accrued_deposite = 0;
            salaryComponents.accrued_savings = 0;
            salaryComponents.severance_pakage = Int32.Parse(txtSeverancePackage.Text);
            salaryComponents.takeHome = Int32.Parse(txtTakeHome.Text);
            salaryComponents.savings_salary = Int32.Parse(txtsavingsalary.Text);
            salaryComponents.net_salary = Int32.Parse(txtnetsalary.Text);
            salaryComponents.ctc = Int32.Parse(txtctc.Text);
            if (checkoptforesi.IsChecked == true)
            {
                salaryComponents.optforEsi = 1;
            }
            else
            {
                salaryComponents.optforEsi = 0;
                salaryComponents.esi_debits = 0;
                salaryComponents.esi_employer_credit = 0;
            }
            if (checkoptforpf.IsChecked == true)
            {
                salaryComponents.optforpf = 1;
            }
            else 
            {
                salaryComponents.optforpf = 0;
                salaryComponents.pf_debits = 0;
                salaryComponents.esi_employer_credit = 0;
            }
            
            salaryComponents.numberofLeaves =0;
            salaryComponents.number_of_availableWorkingDays = 0;
            salaryComponents.number_of_days_worked = 0;
            salaryComponents.number_of_Hours_worked = 0;
            salaryComponents.overTime_inhours = 0;
            salaryComponents.salary_package_allowance1 = 0;
            salaryComponents.salary_package_allowance2_outrstation = 0;
            salaryComponents.salary_package_allowance3_dailyReport =0;
            salaryComponents.salary_package_allowance4 =0;
            salaryComponents.salary_package_allowance5 =0;
            salaryComponents.salary_package_allowance6 =0;
            salaryComponents.salary_package_attendance_bonus = 0;
            salaryComponents.overtimeRate = Int32.Parse(txtOvertimeRate.Text);
            salaryComponents.numberofOutstationDays = 0;
            salaryComponents.numberofdaysinDailyReport =0;
            salaryComponents.multriplicationFactor = 1;
            salaryComponents.basicPlusDA_salary_package =0;
            salaryComponents.allowance1_multiplicationFactor = 0;
            salaryComponents.allowance4_multiplicationFactor = 0;
            salaryComponents.allowance5_multiplicationFactor = 0;
            salaryComponents.allowance6_multiplicationFactor =0;
            salaryComponents.Bonus = 0;
            salaryComponents.pf_debits2 =0; //--------------------------------------???
            salaryComponents.ptax_debits2 = 0;
            salaryComponents.pf_employer_credit2 = 0;
            salaryComponents.earned_leave_credit2 =0;
            salaryComponents.gratuity2 = 0;
            salaryComponents.conveyance2 = 0;
            salaryComponents.production_incentive =0;
            salaryComponents.early_attandance_bonus_salarypkg = 0;
            salaryComponents.late_attendance_debitrate = Int32.Parse(txtLateAttendanceDebitRate.Text);
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
        //DO calculations here so that 
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //if (!string.IsNullOrEmpty(txtEmployeeID.Text))
            //{
                var Workbook = new XLWorkbook("SALARYCOMPONENTS.xlsx");
                IXLWorksheet worksheet = Workbook.Worksheet("Sal Temp");


                worksheet.Cell("B1").Value = Int32.Parse(txtBasicplusDA.Text);
                worksheet.Cell("B3").Value = Int32.Parse(txtHRA.Text);
                worksheet.Cell("B4").Value = Int32.Parse(txtConveyance.Text);
                worksheet.Cell("B5").Value = Int32.Parse(txtProductionIncentive.Text);
                worksheet.Cell("B6").Value = Int32.Parse(txtFood.Text);
                worksheet.Cell("B7").Value = Int32.Parse(txtCompanyRevenueLtd.Text);
                worksheet.Cell("B8").Value = Int32.Parse(txtoutstationAllow.Text);
                worksheet.Cell("B14").Value = Int32.Parse(txtdailyAllow.Text);
                worksheet.Cell("B15").Value = Int32.Parse(txtspecialallow.Text);
                worksheet.Cell("B16").Value = Int32.Parse(txtAllowance5.Text);
                worksheet.Cell("B17").Value = Int32.Parse(txtMobileAllowance.Text);
                worksheet.Cell("B9").Value = Int32.Parse(txtCEA.Text);
                worksheet.Cell("F15").Value = Int32.Parse(txtOvertimeRate.Text);
                worksheet.Cell("F14").Value = Int32.Parse(txtAttendanceBonus.Text);
                worksheet.Cell("F7").Value = Int32.Parse(txtMinimumOtHours.Text);
                worksheet.Cell("D4").Value = Int32.Parse(txtTDSDebits.Text);
                worksheet.Cell("D5").Value = Int32.Parse(txtOtherDebits.Text);
                worksheet.Cell("I13").Value = Int32.Parse(txtLateAttendanceDebitRate.Text);
                worksheet.Cell("D9").Value = Int32.Parse(txtMobileCredit.Text);
                worksheet.Cell("D10").Value = Int32.Parse(txtCanteenCredit.Text);
                worksheet.Cell("D13").Value = Int32.Parse(txtMedicalInsurance.Text);
                worksheet.Cell("D14").Value = Int32.Parse(txtAccidentInsurance.Text);
                worksheet.Cell("I17").Value = Int32.Parse(txtEarlyAttendanceBonus.Text);
                worksheet.Cell("D18").Value = Int32.Parse(txtSeverancePackage.Text);
                if (checkoptforpf.IsChecked == true)
                {
                    worksheet.Cell("F1").Value = 1;
                    txtPfDebits.Text = (worksheet.Cell("D2").Value).ToString();
                }
                else
                {
                    worksheet.Cell("F1").Value = 0;
                    txtPfDebits.Text = (worksheet.Cell("D2").Value).ToString();

            }

                if (checkoptforesi.IsChecked == true)
                {
                    worksheet.Cell("F2").Value = 1;
                    txtEsiDebits.Text = (worksheet.Cell("D1").Value).ToString();

                }
                else
                {
                    worksheet.Cell("F2").Value = 0;
                    txtEsiDebits.Text = (worksheet.Cell("D1").Value).ToString();
            }



                //Display the data now 
                txtBonus.Text = (worksheet.Cell("B2").Value).ToString();
                txtCommitmentAllowance.Text = (worksheet.Cell("B11").Value).ToString();
                txtGrossSalary.Text = (worksheet.Cell("B13").Value).ToString();

                txtPtaxDebits.Text = (worksheet.Cell("D3").Value).ToString();
                txtTotalDebits.Text = (worksheet.Cell("D6").Value).ToString();
                txtESIEmployerCredit.Text = (worksheet.Cell("D7").Value).ToString();
                txtPFEmployerCredit.Text = (worksheet.Cell("D8").Value).ToString();
                txtEarnedLeavecredit.Text = (worksheet.Cell("D11").Value).ToString();
                txtGratuity.Text = (worksheet.Cell("D12").Value).ToString();
                txttotalOtherCredits.Text = (worksheet.Cell("D15").Value).ToString();
                txtTakeHome.Text = (worksheet.Cell("D19").Value).ToString();
                txtsavingsalary.Text = (worksheet.Cell("D20").Value).ToString();
                txtnetsalary.Text = (worksheet.Cell("D21").Value).ToString();
                txtctc.Text = (worksheet.Cell("D22").Value).ToString();

                Workbook.SaveAs(@"C:\Users\Vikas\Desktop\Merci\file.xlsx");
            //}
            //else
            //{
            //    MessageBox.Show("Please Enter the Id of Employee whose Salary you want to add");
            //}

            //Fetch The data from here.




            //    //This will do the calculations so this would consist the all stuff.
            //    var Workbook = new XLWorkbook("SALARYCOMPONENTS.xlsx");
            //    IXLWorksheet worksheet = Workbook.Worksheet("Sal Temp");

            //


            //    //Actually we don't have to save the file because actually we need to do calculations only here.
            //    //worksheet.Cell("F1").Value = 1;
            //    //worksheet.Cell("F19").Value = 123;
            //    //worksheet.Cell("F18").Value = 121;
            //    //worksheet.Cell("B1").Value = worksheet.Cell("B1").Value;
            //    //txtbasicplusda1.Text = (worksheet.Cell("B1").Value).ToString();
            //    //save the data now.

            //    //-------------------------Now all the parameters which you have to fill. [Bluething]
            //    worksheet.Cell("F1").Value = Int32.Parse(txtzoptforpf.Text);
            //    worksheet.Cell("F2").Value = Int32.Parse(txtzoptforEsi.Text);
            //    worksheet.Cell("F3").Value = Int32.Parse(txtzNumberofLeaves.Text);
            //    worksheet.Cell("F4").Value = Int32.Parse(txtznumberofavailableworkingdays.Text);
            //    worksheet.Cell("F5").Value = Int32.Parse(txtznumberofdaysWorked.Text);
            //    worksheet.Cell("F6").Value = Int32.Parse(txtzNumberofhoursWorked.Text);
            //    worksheet.Cell("F7").Value = Int32.Parse(txtzOvertime2.Text);  // This is the data of overtime which might be calculated via overtime rate.
            //    worksheet.Cell("F8").Value = Int32.Parse(txtzSalaryPackageAllowance1.Text);
            //    worksheet.Cell("F9").Value = Int32.Parse(txtzsalarypackageallowancw2.Text);
            //    worksheet.Cell("F10").Value = Int32.Parse(txtzsalaryPackageallowance3.Text);
            //    worksheet.Cell("F11").Value = Int32.Parse(txtzsalarypackageallowance4.Text);
            //    worksheet.Cell("F12").Value = Int32.Parse(txtzsalarypackageallowance5.Text);
            //    worksheet.Cell("F13").Value = Int32.Parse(txtzsalarypackageallowance6.Text);
            //    //worksheet.Cell("F14").Value = Int32.Parse(txtzs);  //SALARY_PKG_ATTENDANCE_BONUS
            //    worksheet.Cell("F15").Value = Int32.Parse(txtzovertimerate.Text);
            //    worksheet.Cell("F16").Value = Int32.Parse(txtzNumberofoutstationdays.Text);
            //    worksheet.Cell("F17").Value = Int32.Parse(txtzNumberofdaysindailyreport.Text);
            //    worksheet.Cell("F18").Value = Int32.Parse(txtzMultiplicationFactor.Text);
            //    worksheet.Cell("F19").Value = Int32.Parse(txtzbasicplusdasalarypackage.Text);
            //    worksheet.Cell("F20").Value = Int32.Parse(txtzallowance1multiplication.Text);
            //    worksheet.Cell("F21").Value = Int32.Parse(txtzallowance4Multiplication.Text);
            //    worksheet.Cell("F22").Value = Int32.Parse(txtzallowance5Multiplication.Text);
            //    worksheet.Cell("F23").Value = Int32.Parse(txtzallowance6Multiplication.Text);
            //    worksheet.Cell("F24").Value = Int32.Parse(txtzbonus2.Text);
            //    worksheet.Cell("F25").Value = Int32.Parse(txtzPFDebits.Text);
            //    worksheet.Cell("F26").Value = Int32.Parse(txtzptaxDebits.Text);
            //    worksheet.Cell("F27").Value = Int32.Parse(txtzpfEmployerCredit.Text);
            //    worksheet.Cell("F28").Value = Int32.Parse(txtzEarnedleavecredit.Text);
            //    worksheet.Cell("F29").Value = Int32.Parse(txtzGratuity.Text);
            //    worksheet.Cell("F30").Value = Int32.Parse(txtzConveyance1.Text);
            //    worksheet.Cell("F31").Value = Int32.Parse(txtzproductionIncentive1.Text);

            //    //--------------------------------2nd stop
            //    worksheet.Cell("D4").Value = Int32.Parse(txtztdsDebits.Text);
            //    worksheet.Cell("D5").Value = Int32.Parse(txtzOtherDebits.Text);
            //    worksheet.Cell("D9").Value = Int32.Parse(txtzmobilephoneCredit.Text);
            //    worksheet.Cell("D10").Value = Int32.Parse(txtzCanteenCredit.Text);
            //    worksheet.Cell("D13").Value = Int32.Parse(txtzMedicalInsurance.Text);
            //    worksheet.Cell("D14").Value = Int32.Parse(txtzAccidentalInsurance.Text);
            //    worksheet.Cell("D16").Value = Int32.Parse(txtzAccruedDeposit.Text);
            //    worksheet.Cell("D17").Value = Int32.Parse(txtzaccruedSaving.Text);
            //    worksheet.Cell("D18").Value = Int32.Parse(txtzserverancePackage.Text);

            //    //------------------------------------1st Stop 
            //    worksheet.Cell("B3").Value = Int32.Parse(txtzhra.Text);
            //    worksheet.Cell("B6").Value = Int32.Parse(txtzfood.Text);
            //    worksheet.Cell("B9").Value = Int32.Parse(txtzCEA.Text);
            //    // Now Intake calculated........


            //    //Portion 1st....
            //    txtbasicplusda1.Text = (worksheet.Cell("B1").Value).ToString();
            //    txtBonus1.Text = (worksheet.Cell("B2").Value).ToString();
            //    txtConveyance2.Text = (worksheet.Cell("B4").Value).ToString();
            //    txtProductionIncentiveAutoCalulate.Text = (worksheet.Cell("B5").Value).ToString();
            //    txtAllowance1.Text = (worksheet.Cell("B7").Value).ToString();
            //    txtAllowance2.Text = (worksheet.Cell("B8").Value).ToString();
            //    txtOvertime.Text = (worksheet.Cell("B10").Value).ToString();
            //    txtcommitmentAllowance.Text = (worksheet.Cell("B11").Value).ToString();
            //    txtattendanceBonus.Text = (worksheet.Cell("B12").Value).ToString();
            //    txtgrossSalary.Text = (worksheet.Cell("B13").Value).ToString();
            //    txtAllowance3dailyreport.Text = (worksheet.Cell("B14").Value).ToString();
            //    txtallowance4.Text = (worksheet.Cell("B15").Value).ToString();
            //    txtallowance55.Text = (worksheet.Cell("B16").Value).ToString();
            //    txtallowance6.Text = (worksheet.Cell("B17").Value).ToString();

            //    //Portion 2nd
            //    txtESIdebits.Text = (worksheet.Cell("D1").Value).ToString();
            //    txtpfDebits.Text = (worksheet.Cell("D2").Value).ToString();
            //    txtptaxdebits1.Text = (worksheet.Cell("D3").Value).ToString();
            //    txtTotaldebits.Text = (worksheet.Cell("D6").Value).ToString();
            //    txtESIemployerCredit.Text = (worksheet.Cell("D7").Value).ToString();
            //    txtpfemployerCredit1.Text = (worksheet.Cell("D8").Value).ToString();
            //    txtEarnedLeaveCredit1.Text = (worksheet.Cell("D11").Value).ToString();
            //    txtgratuity1.Text = (worksheet.Cell("D12").Value).ToString();
            //    txtTotalOtherCredits.Text = (worksheet.Cell("D15").Value).ToString(); 
            //    txttakehome.Text = (worksheet.Cell("D19").Value).ToString();
            //    txtsavingIncome.Text = (worksheet.Cell("D20").Value).ToString();
            //    txtNetsalary.Text = (worksheet.Cell("D21").Value).ToString();
            //    txtCTC.Text = (worksheet.Cell("D22").Value).ToString();

            //    //portion 3 {Not Required Right Now}



            //    //  MessageBox.Show((string)Result);
            //    string employeeName = txtEmployeeID.Text;
            //    //This would help us further to find out the name of person so We would save that thing in the data.
            //    Workbook.SaveAs(@"C:\Users\Vikas\Desktop\Merci\file.xlsx");
            //
        }

        private void cmbEmployeeName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            populatethedetailsofemployees();
        }
    }
}
