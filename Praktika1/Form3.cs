using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Praktika1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            label1.Text = UserData.Username;
            DB db = new DB();
            dataGridView2.DataSource = db.GetProgramms();
            dataGridView2.Columns[0].HeaderText = "Идентификатор";
            dataGridView2.Columns[1].HeaderText = "Название";
            dataGridView2.Columns[2].HeaderText = "Срок обучения мес.";
            dataGridView2.Columns[3].HeaderText = "Квалификация";
            dataGridView2.Columns[4].HeaderText = "Стоимость руб./мес.";
            dataGridView2.RowHeadersVisible = false;
            dataGridView2.AllowUserToAddRows = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form1 = new Form1();
            form1.Closed += (s, args) => this.Close();
            form1.Show();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

      
       
        private bool IsSrokValid(string x)
        {
            if (x.AsEnumerable().Any(ch => char.IsLetter(ch)) == true)
            return false;

            // Проверка длины пароля
            if (x.Length < 2)
            return false;


            if (Convert.ToInt32(x) > 12)
            return false;
            // Проверка количества букв, цифр и знаков

            int symbolCount = Regex.Matches(x, "[@#%)(.<]").Count;

            if (symbolCount > 0)
            return false;

            // Пароль соответствует требованиям
            return true;
        }

        private bool IsCostValid(string x)
        {
            if (x.AsEnumerable().Any(ch => char.IsLetter(ch)) == true)
                return false;

          


           

            int symbolCount = Regex.Matches(x, "[@#%)(.<]").Count;

            if (symbolCount > 0)
                return false;

            // Пароль соответствует требованиям
            return true;
        }


        private void button3_Click(object sender, EventArgs e)
        {

            bool val = true;

            if (textBox1.Text == string.Empty)
            {
                MessageBox.Show("Все поля должны быть заполнены!", "Ошибка!");
                val = false;
            }
            else if (textBox2.Text == string.Empty)
            {
                MessageBox.Show("Все поля должны быть заполнены!", "Ошибка!");
                
                val = false;

            }
            else if (textBox3.Text == string.Empty)
            {
                MessageBox.Show("Все поля должны быть заполнены!", "Ошибка!");
               
                val = false;

            }
            else if (textBox4.Text == string.Empty)
            {
                MessageBox.Show("Все поля должны быть заполнены!", "Ошибка!");
               
                val = false;

            }
            else if (textBox5.Text == string.Empty)
            {
                MessageBox.Show("Все поля должны быть заполнены!", "Ошибка!");
              
                val = false;

            }
            else if (textBox1.Text == string.Empty)
            {
                MessageBox.Show("Все поля должны быть заполнены!", "Ошибка!");
               
                val = false;

            }
            else if (IsSrokValid(textBox3.Text) == false)
            {
                MessageBox.Show("Введите корректный срок обучения (мес.)!", "Ошибка!");
                textBox3.Clear();
                val = false;

            }
            else if (IsCostValid(textBox5.Text) == false)
            {
                MessageBox.Show("Введите корректную стоимость обучения (руб.)!", "Ошибка!");
                textBox5.Clear();
                val = false;

            }
            else if (val == true)
            {
                DB dB = new DB();
                dB.SetProgramm(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text);
                dataGridView2.DataSource = dB.GetProgramms();
                dataGridView2.Columns[0].HeaderText = "Идентификатор";
                dataGridView2.Columns[1].HeaderText = "Название";
                dataGridView2.Columns[2].HeaderText = "Срок обучения мес.";
                dataGridView2.Columns[3].HeaderText = "Квалификация";
                dataGridView2.Columns[4].HeaderText = "Стоимость руб./мес.";
                dataGridView2.RowHeadersVisible = false;
                dataGridView2.AllowUserToAddRows = false;
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
            }




        }
    }
}
