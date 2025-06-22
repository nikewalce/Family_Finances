using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;

namespace Family_Finances
{
    public partial class Diagrams : Form
    {
        public string conString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\source\\repos\\Family_Finances\\Family_Finances\\FamilyDB.mdf;Initial Catalog=FamilyDB;Integrated Security=True";
        public Diagrams()
        {
            InitializeComponent();
            Diagram2();
            Diagram1();
            
        }
        private void Diagram1()
        {
            //-------------Диаграмма Доходы---------------
            Func<ChartPoint, string> labelPoint = chartPoint =>
                string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            SqlConnection myConnection = new SqlConnection(conString);
            myConnection.Open();
            string query = "SELECT I.Id_Operation,C.Name_CatInc,I.Date,I.Summa, I.Note, U.Name_User FROM Income AS I, CatInc AS C, Users AS U WHERE I.Id_CatInc = C.Id_CatInc AND I.[ Id_User] = U.Id_User";
            SqlCommand command = new SqlCommand(query, myConnection);
            SqlDataReader reader = command.ExecuteReader();
            List<string[]> data = new List<string[]>();
            while (reader.Read())
            {
                data.Add(new string[6]);

                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
                data[data.Count - 1][3] = reader[3].ToString();
                data[data.Count - 1][4] = reader[4].ToString();
                data[data.Count - 1][5] = reader[5].ToString();
            }
            reader.Close();
            myConnection.Close();
            foreach (string[] s in data)
            {
                double sum = 0;
                string title = s[1];
                sum += Convert.ToDouble(s[3]);


                pieChart1.Series.Add(new PieSeries
                {
                    Title = title,
                    Values = new ChartValues<double> { sum },
                    PushOut = 15,
                    DataLabels = true,
                    LabelPoint = labelPoint
                }
                );
                //MessageBox.Show(s[0]);
            }
            pieChart1.LegendLocation = LegendLocation.Bottom;
            //-------------Диаграмма Доходы---------------
        }
        private void Diagram2()
        {
            //-------------Диаграмма Расходы---------------
            Func<ChartPoint, string> labelPoint1 = chartPoint =>
                string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            SqlConnection myConnection = new SqlConnection(conString);
            myConnection.Open();
            string query = "SELECT C.Id_Operation,CC.Name_CatCost,C.Date,C.Summa, C.Note, U.Name_User FROM Costs AS C, CatCosts AS CC, Users AS U WHERE C.Id_CatCost = CC.Id_CatCost AND C.[ Id_User] = U.Id_User";
            SqlCommand command = new SqlCommand(query, myConnection);
            SqlDataReader reader = command.ExecuteReader();
            List<string[]> data1 = new List<string[]>();
            while (reader.Read())
            {
                data1.Add(new string[6]);

                data1[data1.Count - 1][0] = reader[0].ToString();
                data1[data1.Count - 1][1] = reader[1].ToString();
                data1[data1.Count - 1][2] = reader[2].ToString();
                data1[data1.Count - 1][3] = reader[3].ToString();
                data1[data1.Count - 1][4] = reader[4].ToString();
                data1[data1.Count - 1][5] = reader[5].ToString();
            }
            reader.Close();
            myConnection.Close();
            foreach (string[] s in data1)
            {
                double sum = 0;
                string title = s[1];
                sum += Convert.ToDouble(s[3]);


                pieChart2.Series.Add(new PieSeries
                {
                    Title = title,
                    Values = new ChartValues<double> { sum },
                    PushOut = 15,
                    DataLabels = true,
                    LabelPoint = labelPoint1
                }
                );
                //MessageBox.Show(s[0]);
            }
            pieChart2.LegendLocation = LegendLocation.Bottom;
            //-------------Диаграмма Расходы---------------
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Income newForm = new Income();
            newForm.ShowDialog();
            this.Close();
        }


        private void pieChart1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }


        private void Diagrams_Load(object sender, EventArgs e)
        {
            
        }

        private void pieChart2_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
