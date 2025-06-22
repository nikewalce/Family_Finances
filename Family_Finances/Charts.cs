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
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Wpf;

namespace Family_Finances
{
    public partial class Charts : Form
    {
        public string conString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\source\\repos\\Family_Finances\\Family_Finances\\FamilyDB.mdf;Initial Catalog=FamilyDB;Integrated Security=True";
        public Charts()
        {
            InitializeComponent();
            Chart1();
            Chart2();
        }

        private void Chart1()
        {

            //-------------График Доходы---------------
            SeriesCollection series = new SeriesCollection(); //отображение данных на график. Линии и т.д.
            ChartValues<double> zp = new ChartValues<double>(); //Значения которые будут на линии, будет создания чуть позже.
            List<string> date = new List<string>(); //здесь будут храниться значения для оси X


            SqlConnection myConnection = new SqlConnection(conString);
            myConnection.Open();
            string query = "SELECT I.Id_Operation,C.Name_CatInc,I.Date,I.Summa, I.Note, U.Name_User  FROM Income AS I, CatInc AS C, Users AS U WHERE I.Id_CatInc = C.Id_CatInc AND I.[ Id_User] = U.Id_User  ORDER BY MONTH(Date),DAY(Date);";
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
                DateTime date1 = Convert.ToDateTime(s[2]);
                sum += Convert.ToDouble(s[3]);
                //MessageBox.Show(s[0]);
                zp.Add(sum);
                date.Add(date1.ToShortDateString());
            }


            cartesianChart1.AxisX.Clear(); //Очищаем ось X от значений по умолчанию
            cartesianChart1.AxisX.Add(new Axis //Добавляем на ось X значения, через блок инициализатора.
            {
                Title = "Дата",
                Labels = date
            });

            LineSeries line = new LineSeries(); //Создаем линию, задаем ей значения из коллекции
            line.Title = "";
            line.Values = zp;

            series.Add(line); //Добавляем линию на график
            cartesianChart1.Series = series; //Отрисовываем график в интерфейсе
            //-------------График Доходы---------------
        }

        private void Chart2()
        {

            //-------------График Расходы---------------
            SeriesCollection series = new SeriesCollection(); //отображение данных на график. Линии и т.д.
            ChartValues<double> zp = new ChartValues<double>(); //Значения которые будут на линии, будет создания чуть позже.
            List<string> date = new List<string>(); //здесь будут храниться значения для оси X


            SqlConnection myConnection = new SqlConnection(conString);
            myConnection.Open();
            string query = "SELECT C.Id_Operation,CC.Name_CatCost,C.Date,C.Summa, C.Note, U.Name_User FROM Costs AS C, CatCosts AS CC, Users AS U WHERE C.Id_CatCost = CC.Id_CatCost AND C.[ Id_User] = U.Id_User  ORDER BY MONTH(Date),DAY(Date);";
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
                DateTime date1 = Convert.ToDateTime(s[2]);
                sum += Convert.ToDouble(s[3]);
                //MessageBox.Show(s[0]);
                zp.Add(sum);
                date.Add(date1.ToShortDateString());
            }


            cartesianChart2.AxisX.Clear(); //Очищаем ось X от значений по умолчанию
            cartesianChart2.AxisX.Add(new Axis //Добавляем на ось X значения, через блок инициализатора.
            {
                Title = "Дата",
                Labels = date
            });

            LineSeries line = new LineSeries(); //Создаем линию, задаем ей значения из коллекции
            line.Title = "";
            line.Values = zp;

            series.Add(line); //Добавляем линию на график
            cartesianChart2.Series = series; //Отрисовываем график в интерфейсе
            //-------------График Расходы---------------
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Income newForm = new Income();
            newForm.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
