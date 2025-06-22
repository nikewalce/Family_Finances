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
    public partial class Income : Form
    {
        Add_Income newForm1 = new Add_Income();
        //public static int sum { get; set; }//свойство, в котором хранится сумма
        public string conString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\source\\repos\\Family_Finances\\Family_Finances\\FamilyDB.mdf;Initial Catalog=FamilyDB;Integrated Security=True";
        public Income()
        {
            InitializeComponent();
            
        }
        public void GetFont()
        {
            FontDialog fd = new FontDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                dataGridView1.Font = fd.Font;
            }
        }
        private void MakeReadOnly()
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;
        }
        private void Income_Load(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(conString);

                con.Open();
                MakeReadOnly();

                //string query = "SELECT * FROM Income ORDER BY Id_Operation";
                string query = "SELECT I.Id_Operation,C.Name_CatInc,I.Date,I.Summa, I.Note, U.Name_User FROM Income AS I, CatInc AS C, Users AS U WHERE I.Id_CatInc = C.Id_CatInc AND I.[ Id_User] = U.Id_User";
                SqlCommand command = new SqlCommand(query, con);

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

                con.Close();

                foreach (string[] s in data)
                    dataGridView1.Rows.Add(s);

                //-------------Подсчет суммы------------------
                Data.sum = 0;
                Data.sum_add = 0;
                //Data.count = dataGridView1.Rows.Count;//-------------------------
                for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                {
                    Data.sum += Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                }
                label5.Text = Data.sum.ToString();//Всего
                label3.Text = Data.sub.ToString();//Баланс


                //-------------Подсчет суммы------------------


            }
            catch (Exception c)
            {
                MessageBox.Show(c.Message);
            }


        }

        private void Costs_Click(object sender, EventArgs e)
        {
            //Запуск модального окна Costs и закрытие Income
            this.Hide();
            Family_Finances newForm = new Family_Finances();
            newForm.ShowDialog();
            this.Close();
            //Запуск модального окна Costs
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            newForm1.ShowDialog();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //dataGridView1.Sort(dataGridView1.Columns[comboBox1.Text], ListSortDirection.Ascending);
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Если нет элементов и нажать на кнопку, вызывается ошибка. Нужно доделать, чтобы ловилось в исключении!
            try
            {
                if (dataGridView1.Rows != null && dataGridView1.Rows.Count != 0 && dataGridView1.Rows.Count != 1)
                {

                        SqlConnection con = new SqlConnection(conString);
                        con.Open();
                        //------------перезапись файла(удаляет последнюю строку из файла)-------------------
                        int a = dataGridView1.CurrentRow.Index;
                        int b = dataGridView1.SelectedCells[0].RowIndex; 
                        int selected = Convert.ToInt32(dataGridView1[0, a].Value);
                        string del = $"DELETE FROM Income WHERE Id_Operation = {selected}";
                        SqlCommand cmd = new SqlCommand(del, con);
                        cmd.ExecuteNonQuery();
                        con.Close();

                }
                else
                {
                    SqlConnection con = new SqlConnection(conString);
                    con.Open();
                    int a = dataGridView1.CurrentRow.Index;
                    //dataGridView1.Rows.Remove(dataGridView1.Rows[a]);//удаляет последнюю строку из dataGridView 
                    int selected = Convert.ToInt32(dataGridView1[0, a].Value);
                    string del = $"DELETE FROM Income WHERE Id_Operation = {selected}";
                    SqlCommand cmd = new SqlCommand(del, con);
                    cmd.ExecuteNonQuery();
                    string renul = "DBCC CHECKIDENT (Income,RESEED,0)";  //|
                    SqlCommand ren = new SqlCommand(renul, con);         //|Обнуление счетчика
                    ren.ExecuteNonQuery();                               //|      
                    con.Close();
                    MessageBox.Show("RESEED 0");
                }
                
                
                //------------перезапись файла(удаляет последнюю строку из файла)-------------------
                //-------------Вычитает из суммы значение ячейки, которая была удалена--------------
                Data.sum = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                {
                    Data.sum += Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                }
                label5.Text = Data.sum.ToString();//Всего
                label3.Text = Data.sub.ToString();//Баланс
                //-------------Вычитает из суммы значение ячейки, которая была удалена--------------

            }
            catch (Exception c)
            {
                MessageBox.Show(c.Message);
            }

        }

        private void пользователиToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void списокИменToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List_of_names fm = new List_of_names();
            fm.ShowDialog();
        }


        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {
            GetFont();
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Income_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void помощьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Напишите на почту: danil123461@yandex.ua, и я вам всё подскажу :)");
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button_text_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)//Читает все примечания
            {
                string s1 = Convert.ToString(dataGridView1.Rows[i].Cells[4].Value);
                MessageBox.Show(s1);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void списокДоходовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.Hide();
            List_of_Income newForm = new List_of_Income();
            newForm.ShowDialog();
            //this.Close();
        }

        private void списокРасходовToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // this.Hide();
            List_of_Costs newForm = new List_of_Costs();
            newForm.ShowDialog();
          //  this.Close();
        }

        private void нетToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Opacity = 100;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.Opacity = .20;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            this.Opacity = .30;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            this.Opacity = .40;
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            this.Opacity = .50;
        }

        private void button_Erase_MouseHover(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(button_Erase, "Удаляет выбранную строку таблицы");
        }

        private void button_text_MouseHover(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(button_text, "Показывает все примечания таблицы");
        }

        private void button_Add_MouseHover(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(button_Add, "Добавить новую запись в таблицу");
        }

        private void button_Costs_MouseHover(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(button_Costs, "Перейти к расходам");
        }

        private void калькуляторКомУслугToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.Hide();
            Utilities newForm = new Utilities();
            newForm.ShowDialog();
            //this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Income newForm = new Income();
            newForm.ShowDialog();
            this.Close();

        }

        private void диаграммыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Diagrams newForm = new Diagrams();
            newForm.ShowDialog();
            this.Close();
        }

        private void графикиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Charts newForm = new Charts();
            newForm.ShowDialog();
            this.Close();
        }
    }
   
}

