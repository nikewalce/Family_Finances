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
    public partial class List_of_names : Form
    {
        private const string USERS = "user.txt";//Путь к файлу user.txt
        public List_of_names()
        {
            InitializeComponent();
        }

        private void List_of_names_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conString);

            con.Open();
            

                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        string q = "SELECT Name_User FROM Users";
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
                    string q = "insert into Users(Name_User)" +
                        " values(N'"+textBox1.Text.ToString()+"') ";
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

        private void button2_Click(object sender, EventArgs e)//Удаляет выбраную строчку в файле 
        {
            //---------Перезапись файла(выбранную строчку)------------------------------
            SqlConnection con = new SqlConnection(conString);

            con.Open();

            ListView.SelectedIndexCollection collection = listView1.SelectedIndices;
            if (collection.Count != 0)
            {
                int selected = Convert.ToInt32(collection[0]) + 1;//номер выбранного элемента + 1, т.к. счетчик в таблице начинается с 1
                listView1.Items.RemoveAt(collection[0]);
                string del1 = $"DELETE FROM Income FROM Income inc LEFT JOIN Users ON inc.[ Id_User] = {selected} ";
                string del2 = $"DELETE FROM Costs FROM Costs cost LEFT JOIN Users ON cost.[ Id_User] = {selected} ";
                string del = $"DELETE FROM Users WHERE [Id_User] = {selected}";
                SqlCommand cmd1 = new SqlCommand(del1, con);
                SqlCommand cmd2 = new SqlCommand(del2, con);
                SqlCommand cmd = new SqlCommand(del, con);
                cmd1.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                cmd.ExecuteNonQuery();
                MessageBox.Show("User has been deleted!");
            }

            con.Close();


            
            //---------Перезапись файла(выбранную строчку)------------------------------

        }

        private void button3_Click(object sender, EventArgs e)//очищает файл и listView
        {
            SqlConnection con = new SqlConnection(conString);

            con.Open();



            string del1 = $"DELETE FROM Income FROM Income inc LEFT JOIN Users ON inc.[ Id_User] = Users.Id_User ";
            string del2 = $"DELETE FROM Costs FROM Costs cost LEFT JOIN Users ON cost.[ Id_User] = Users.Id_User ";
            string del = $"DELETE FROM Users";
            SqlCommand cmd1 = new SqlCommand(del1, con);
            SqlCommand cmd2 = new SqlCommand(del2, con);
            SqlCommand cmd = new SqlCommand(del, con);
            cmd1.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();
            cmd.ExecuteNonQuery();
            string renul = "DBCC CHECKIDENT (Users,RESEED,0)";   //|
            SqlCommand ren = new SqlCommand(renul, con);         //|Обнуление счетчика
            ren.ExecuteNonQuery();                               //|      
            MessageBox.Show("All users have been deleted!");

                

            con.Close();
            
            listView1.Items.Clear();
            //File.WriteAllText(USERS, "");

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Income newForm = new Income();
            newForm.ShowDialog();
            this.Close();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
