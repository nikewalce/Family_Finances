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
  
    public partial class Add_Income : Form
    {
        private const string USERS = "user.txt";//Путь к файлу user.txt
        private const string CATEGORIES_INCOME = "Categories_Income.txt";//Путь к файлу Categories_Income.txt
        private const string LIST = "list.txt";
        public Add_Income()
        {
            InitializeComponent();
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conString);

            con.Open();
            if (textBox_sum.Text.Length != 0 && comboBox1.Text.Length != 0 && comboBox2.Text.Length != 0)//проверка на наличие значений в обязательных полях
            {
                this.Hide();

                //передаем в отдельный класс Data значение из textBox_sum
                //Data.Value = textBox_sum.Text;

                Income fm = new Income();

                try
                {

                    // ------ number id of selected user----------
                    int id = 0;
                    SqlConnection myConnection = new SqlConnection(conString);
                    myConnection.Open();
                    string query = "SELECT * FROM Users ORDER BY Id_User";
                    SqlCommand command = new SqlCommand(query, myConnection);
                    SqlDataReader reader = command.ExecuteReader();
                    List<string[]> data = new List<string[]>();
                    while (reader.Read())
                    {
                        data.Add(new string[2]);

                        data[data.Count - 1][0] = reader[0].ToString();
                        data[data.Count - 1][1] = reader[1].ToString();
                    }
                    reader.Close();
                    myConnection.Close();
                    foreach (string[] s in data)
                    {
                        string name = comboBox2.Text;
                        int x1 = name.Length - 1;
                        name = name.Remove(x1);
                        string namebd = s[1];
                        if (name == namebd)
                            id += Convert.ToInt32(s[0]);
                            //MessageBox.Show(s[0]);
                    }
                    // ------ number id of selected user----------

                    // ------ number id of selected catInc----------
                    int idCat = 0;
                    SqlConnection myConnection1 = new SqlConnection(conString);
                    myConnection1.Open();
                    string query1 = "SELECT * FROM CatInc ORDER BY Id_CatInc";
                    SqlCommand command1 = new SqlCommand(query1, myConnection1);
                    SqlDataReader reader1 = command1.ExecuteReader();
                    List<string[]> data1 = new List<string[]>();
                    while (reader1.Read())
                    {
                        data1.Add(new string[2]);

                        data1[data1.Count - 1][0] = reader1[0].ToString();
                        data1[data1.Count - 1][1] = reader1[1].ToString();
                    }
                    reader1.Close();
                    myConnection1.Close();
                    foreach (string[] s in data1)
                    {
                        string name = comboBox1.Text;
                        int x1 = name.Length - 1;
                        name = name.Remove(x1);
                        string namebd = s[1];
                        if (name == namebd)
                        {
                            idCat += Convert.ToInt32(s[0]);
                        }

                        //MessageBox.Show(s[0]);
                    }
                    // ------ number id of selected catInc----------
                    
                    string sum = textBox_sum.Text;
                    var sumre = sum.Replace(",", ".");
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        DateTime dt = dateTimePicker1.Value;
                        string q = "insert into Income( Id_CatInc, Date," +
                            " Summa, Note, [ Id_User])" +
                            " values('"+idCat+"','" + dt.ToString("yyyy.MM.dd") + "','" + sumre + "',N'" + textBox1.Text.ToString() + "','"+id+"') ";
                        SqlCommand cmd = new SqlCommand(q, con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Connection was successful!");
                    }

                    con.Close();


                    using (StreamWriter sw = new StreamWriter(LIST, true, System.Text.Encoding.Default))
                    {
                        DateTime dt = dateTimePicker1.Value;
                        sw.Write(comboBox1.Text + "_");
                        sw.Write(comboBox2.Text + "_");
                        sw.Write(textBox_sum.Text + "_");
                        sw.Write(textBox1.Text + "_");
                        sw.WriteLine(dt.ToLongDateString());
                    }

                }
                catch (Exception c)
                {
                    MessageBox.Show(c.Message);
                }

               // fm.dataGridView1.Rows.Add(comboBox1.Text, comboBox2.Text, textBox_sum.Text);
                fm.ShowDialog();
                
                this.Close();
            }
            else
            {
                MessageBox.Show("Для добавления новой записи необходимо заполнить все обязательные поля!");
            }
            
        }

        private void textBox_sum_KeyPress(object sender, KeyPressEventArgs e)
        {
            ////Запрещает вводить другие символы, кроме цифр
            //if (!(Char.IsDigit(e.KeyChar)))
            //{
            //    if (e.KeyChar != (char)Keys.Back)
            //    {
            //        e.Handled = true;
            //    }
            //}
            if (e.KeyChar == '.')
                e.KeyChar = ',';
            if (e.KeyChar != 22)
                e.Handled = !Char.IsDigit(e.KeyChar) && (e.KeyChar != ',' || (((TextBox)sender).Text.Contains(",") && !((TextBox)sender).SelectedText.Contains(","))) && e.KeyChar != (char)Keys.Back && (e.KeyChar != '-' || ((TextBox)sender).SelectionStart != 0 || (((TextBox)sender).Text.Contains("-") && !((TextBox)sender).SelectedText.Contains("-")));
            else
            {
                double d;
                e.Handled = !double.TryParse(Clipboard.GetText(), out d) || (d < 0 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.Contains("-") && !((TextBox)sender).SelectedText.Contains("-"))) || ((d - (int)d) != 0 && ((TextBox)sender).Text.Contains(",") && !((TextBox)sender).SelectedText.Contains(","));
                MessageBox.Show("Не удалось вставить содержимое буфера обмена");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Income fm = new Income();
            fm.ShowDialog();
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Data.Category_Income = comboBox1.Text;
        }


        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        public string conString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\source\\repos\\Family_Finances\\Family_Finances\\FamilyDB.mdf;Initial Catalog=FamilyDB;Integrated Security=True";
        private void Add_Income_Load(object sender, EventArgs e)
        {


            if (!File.Exists(CATEGORIES_INCOME))//проверка на наличие файла
            {
                //MessageBox.Show("File loading error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return;
                var cat = File.Create("Categories_Income.txt");//создает файл
                cat.Close();//освобождает поток 
                
            }
            if(!File.Exists(USERS))
            {
                var user = File.Create("user.txt");
                user.Close();
            }
            if (!File.Exists(LIST))
            {
               var lst= File.Create("list.txt");
                lst.Close();
            }
            // --------- read users ---------
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
                            comboBox2.Items.Add(SDR[i] + " ").ToString();
                    }
                    MoreResults = SDR.NextResult();
                } while (MoreResults);               
            }
            con.Close();
            // --------- read users ---------
            // --------- read catInc ---------
            SqlConnection con1 = new SqlConnection(conString);

            con1.Open();
            if (con1.State == System.Data.ConnectionState.Open)
            {
                string q = "SELECT Name_CatInc FROM CatInc";
                SqlCommand cmd = new SqlCommand(q, con1);
                SqlDataReader SDR = cmd.ExecuteReader();
                bool MoreResults = false;
                do
                {
                    while (SDR.Read())
                    {
                        for (int i = 0; i < SDR.FieldCount; i++)
                            comboBox1.Items.Add(SDR[i] + " ").ToString();
                    }
                    MoreResults = SDR.NextResult();
                } while (MoreResults);
                MessageBox.Show("Connection was successful!");
            }
            // --------- read catInc ---------
            con1.Close();
        }


        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox_sum_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
