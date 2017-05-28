using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Population
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string Cities;
        string Type;
        string State;
        double Females;
        double Males;

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtPath.Text == "")
                {
                    MessageBox.Show("Please upload the txt file");
                }
                
                List<Form1> Populations = new List<Form1>();

                StreamReader sr = new StreamReader(txtPath.Text);
                while (sr.Peek() >= 0)
                {
                    string str;
                    string[] lines = File.ReadAllLines(txtPath.Text.Trim());

                    str = sr.ReadLine();

                    lines = str.Split(',');

                    Form1 CurrentPopu = new Form1();

                    CityList.Items.Add(CurrentPopu.Cities = lines[0]);
                    TypeList.Items.Add(CurrentPopu.Type = lines[1]);
                    StateList.Items.Add(CurrentPopu.State = lines[2]);
                    FemaleList.Items.Add(CurrentPopu.Females = Convert.ToDouble(lines[5]));
                    MaleList.Items.Add(CurrentPopu.Males = Convert.ToDouble(lines[6]));
                }
            }
            catch
            {

            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();

                if(op.ShowDialog() == DialogResult.OK)
                {
                    txtPath.Text = op.FileName;
                }
            }
            catch
            {

            }
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            txtTotalMale.ReadOnly = true;
            txtTotalFemale.ReadOnly = true;

            try
            {
                if(Convert.ToString(FemaleList.Items.Count) != "")
                {
                    double[] _femaleList = new double[FemaleList.Items.Count];
                    double[] _maleList = new double[MaleList.Items.Count];

                    decimal _singleMaleList = 0;
                    decimal _singleFemaleList = 0;
                    decimal _totalMaleFemaleList = 0;

                    decimal _percentageFemaleList = 0;
                    decimal _percentageMaleList = 0;

                    decimal FemaleSum = 0;
                    decimal MaleSum = 0;
                    

                    for (int i = 0; i <= FemaleList.Items.Count - 1; i++)
                    {
                        //TotalList.Items.Add(FemaleList.Items[i]);                        
                        
                        // Start : To get the sum of male and female (male +  female)
                        _femaleList[i] = Convert.ToDouble(FemaleList.Items[i]);
                        _maleList[i] = Convert.ToDouble(MaleList.Items[i]);

                        _singleFemaleList = Convert.ToDecimal(FemaleList.Items[i]);
                        _singleMaleList = Convert.ToDecimal(MaleList.Items[i]);
                        _totalMaleFemaleList = _singleFemaleList + _singleMaleList;

                        TotalList.Items.Add(_totalMaleFemaleList);
                        // End : To get the sum of male and female (male +  female)

                        // Start : To get the percentage of female i.e, female/totalMaleFemale*100%
                        _percentageFemaleList = _singleFemaleList / _totalMaleFemaleList*100;
                        FmPer.Items.Add(Math.Ceiling((_percentageFemaleList)));
                        // End : To get the percentage of female i.e, female/totalMaleFemale*100%


                        // Start : To get the percentage of male i.e, male/totalMaleFemale*100%
                        _percentageMaleList = _singleMaleList / _totalMaleFemaleList * 100;
                        MlPer.Items.Add(Math.Ceiling((_percentageMaleList)));
                        // End : To get the percentage of male i.e, male/totalMaleFemale*100%



                        FemaleSum += Convert.ToDecimal(FemaleList.Items[i]); // Total sum female number
                        MaleSum += Convert.ToDecimal(MaleList.Items[i]); // Total sum male number
                    }

                    txtTotalFemale.Text = Convert.ToString(FemaleSum);
                    txtTotalMale.Text = Convert.ToString(MaleSum);                    
                }

                /*
                if (Convert.ToString(MaleList.Items.Count) != "")
                {
                    double[] _maleList = new double[MaleList.Items.Count];

                    decimal sum = 0;

                    for (int i = 0; i <= MaleList.Items.Count - 1; i++)
                    {
                        //FmPer.Items.Add(MaleList.Items[i]);

                        _maleList[i] = Convert.ToDouble(MaleList.Items[i]);

                        sum += Convert.ToDecimal(MaleList.Items[i]);
                    }

                    txtTotalMale.Text = Convert.ToString(sum);
                }
                

                var a = new double[];
                var b = new double[] { 4, 5, 6 };
                a.Zip(b, (x, y) => x + y);
*/

            }
            catch
            {

            }            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                if(CityList.Text != "" || StateList.Text != "" || TypeList.Text != "" || MaleList.Text != "" || FemaleList.Text != "" || FmPer.Text != "" || MlPer.Text != "" || Convert.ToString(txtTotalFemale) != "" || Convert.ToString(txtTotalMale) != "" || TotalList.Text != "" || txtPath.Text != "")
                {
                    CityList.Items.Clear();
                    StateList.Items.Clear();
                    TypeList.Items.Clear();
                    MaleList.Items.Clear();
                    FemaleList.Items.Clear();
                    FmPer.Items.Clear();
                    MlPer.Items.Clear();
                    txtTotalMale.Clear();
                    txtTotalFemale.Clear();
                    TotalList.Items.Clear();
                    txtPath.Clear();

                    MessageBox.Show("Clear all");
                }
                else
                {
                    MessageBox.Show("Sorry cannot empty");
                }
            }
            catch
            {

            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            /*
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
            {
                MessageBox.Show("Excel is not properly installed!!");
                return;
            }
            else
            {
                Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;

                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                xlWorkSheet.Cells[1, 1] = "ID";
                xlWorkSheet.Cells[1, 2] = "Name";
                xlWorkSheet.Cells[2, 1] = "1";
                xlWorkSheet.Cells[2, 2] = "One";
                xlWorkSheet.Cells[3, 1] = "2";
                xlWorkSheet.Cells[3, 2] = "Two";



                xlWorkBook.SaveAs("d:\\csharp-Excel.xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();

                Marshal.ReleaseComObject(xlWorkSheet);
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(xlApp);

                MessageBox.Show("Excel file created , you can find the file d:\\csharp-Excel.xls");
            }
            */

            
            StreamWriter myOutputStream = new StreamWriter("d:\\Myfile.csv");

            /*
            StringBuilder sb = new StringBuilder();

            sb.Append(CityList.Items);
            sb.Append(TypeList.Items);
            sb.Append(StateList.Items);
            sb.Append(FemaleList.Items);
            sb.Append(MaleList.Items);
            sb.Append(FmPer.Items);
            sb.Append(MlPer.Items);
            sb.Append(TotalList.Items);

            myOutputStream.WriteLine(sb);
            */

            
            foreach (string item in CityList.Items)
            {
                myOutputStream.WriteLine(item);
            }
            
            myOutputStream.Close();
            Process.Start("d:\\Myfile.csv");
            MessageBox.Show("Exported in CSV !!");
            

        }

    }
}
