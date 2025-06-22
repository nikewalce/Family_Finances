using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace Family_Finances
{
    public partial class List_of_Income : Form
    {
        private const string CATEGORIES_INCOME = "Categories_Income.txt";//Путь к файлу Categories_Income.txt
        public List_of_Income()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conString);

            con.Open();



            string del1 = $"DELETE FROM Income FROM Income inc LEFT JOIN CatInc ON inc.[Id_CatInc] = CatInc.Id_CatInc ";
            string del = $"DELETE FROM CatInc";
            SqlCommand cmd1 = new SqlCommand(del1, con);
            SqlCommand cmd = new SqlCommand(del, con);
            cmd1.ExecuteNonQuery();
            cmd.ExecuteNonQuery();
            string renul = "DBCC CHECKIDENT (CatInc,RESEED,0)";   //|
            SqlCommand ren = new SqlCommand(renul, con);         //|Обнуление счетчика
            ren.ExecuteNonQuery();                               //|      
            MessageBox.Show("All users have been deleted!");



            con.Close();

            listView1.Items.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Income newForm = new Income();
            newForm.ShowDialog();
            this.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void List_of_Income_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conString);

            con.Open();


            if (con.State == System.Data.ConnectionState.Open)
            {
                string q = "SELECT Name_CatInc FROM CatInc";
                SqlCommand cmd = new SqlCommand(q, con);
                SqlDataReader SDR = cmd.ExecuteReader();
                bool MoreResults = false;
                do
                {
                    while (SDR.Read())
                    {
                        for (int i = 0; i < SDR.FieldCount; i++)
                            listView1.Items.Add(SDR[i] + " ").ToString();
                    }
                    MoreResults = SDR.NextResult();
                } while (MoreResults);
                MessageBox.Show("Connection was successful!");
            }
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //---------Перезапись файла(выбранную строчку)------------------------------
            SqlConnection con = new SqlConnection(conString);

            con.Open();

            ListView.SelectedIndexCollection collection = listView1.SelectedIndices;
            if (collection.Count != 0)
            {
                int selected = Convert.ToInt32(collection[0]) + 1;//номер выбранного элемента + 1, т.к. счетчик в таблице начинается с 1
                listView1.Items.RemoveAt(collection[0]);
                string del1 = $"DELETE FROM Income FROM Income inc LEFT JOIN CatInc ON inc.[Id_CatInc] = {selected} ";
                string del = $"DELETE FROM CatInc WHERE [Id_CatInc] = {selected}";
                SqlCommand cmd1 = new SqlCommand(del1, con);
                SqlCommand cmd = new SqlCommand(del, con);
                cmd1.ExecuteNonQuery();
                cmd.ExecuteNonQuery();
                MessageBox.Show("User has been deleted!");
            }

            con.Close();
            //---------Перезапись файла(выбранную строчку)------------------------------
        }
        public string conString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\source\\repos\\Family_Finances\\Family_Finances\\FamilyDB.mdf;Initial Catalog=FamilyDB;Integrated Security=True";
        private void button1_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(conString);

            con.Open();
            // Запись в файл по нажатию на кнопку
            try
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    string q = "insert into CatInc(Name_CatInc)" +
                        " values(N'" + textBox1.Text.ToString() + "') ";
                    SqlCommand cmd = new SqlCommand(q, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Connection was successful!");
                }

                con.Close();

                
            }
            catch (Exception)
            {

            }
        }
    }
}
