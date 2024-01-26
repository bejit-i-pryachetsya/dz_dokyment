using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace Praktika1
{

    public static class UserData
    {
        public static string Username { get; set; }
    }
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label4.Visible = false;
            textBox3.Visible = false;
            button2.Visible = false;
        }




        private bool IsPasswordValid(string password)
        {
            // Проверка длины пароля
            if (password.Length < 8)
                return false;

            // Проверка количества букв, цифр и знаков
            int letterCount = Regex.Matches(password, "[a-zA-Z]").Count;
            int digitCount = Regex.Matches(password, "[0-9]").Count;
            int symbolCount = Regex.Matches(password, "[@#%)(.<]").Count;

            if (letterCount < 5 || digitCount < 3 || symbolCount < 1)
                return false;

            // Пароль соответствует требованиям
            return true;
        }





        // переменные чтобы считать неудачные попытки входа и переменная проверяющая изменил ли пользователь свой пароль
        bool ex = true;
        int popitki;
        bool podtvrdit = false;

        private void button1_Click(object sender, EventArgs e)
        {
           
            if (textBox1.Text == string.Empty)
            {
                MessageBox.Show("Введите логин!", "Ошибка!");
                ex = false;
            }
            else if (textBox2.Text == string.Empty)
            {
                MessageBox.Show("Введите пароль!", "Ошибка!");
                textBox3.Clear();
                ex = false;
            }  
            else if (podtvrdit == true)
            {

                bool ex1 = true;

                if (textBox1.Text == string.Empty)
                {
                    MessageBox.Show("Введите логин!", "Ошибка!");
                    ex1 = false;
                }
                else if (textBox2.Text == string.Empty)
                {
                    MessageBox.Show("Введите новый пароль!", "Ошибка!");
                    ex1 = false;
                }
                else if (textBox3.Text == string.Empty)
                {
                    MessageBox.Show("Введите подтверждение нового пароля!", "Ошибка!");
                    ex1 = false;
                }
                else if(textBox2.Text != textBox3.Text)
                {
                    MessageBox.Show("Пароли не совпадают!", "Ошибка!");
                    textBox2.Clear();
                    textBox3.Clear();
                    ex1 = false;
                }
                else if (textBox2.Text != textBox3.Text)
                {
                    MessageBox.Show("Пароли не совпадают!", "Ошибка!");
                    textBox2.Clear();
                    textBox3.Clear();
                    ex1 = false;
                }



                else if (ex1 == true)
                {


                    DB new_pass = new DB();
                    DataTable check_user = new_pass.CheckUser(textBox1.Text);
                   

                    if (IsPasswordValid(textBox2.Text) == false)
                    {
                        MessageBox.Show("Пароль должен содержать не менее 5 букв, 3 цифр и знаки @#%)(.<!", "Ошибка!");
                        textBox2.Clear();
                        textBox3.Clear();
                    }
                    

                    else if (check_user.Rows.Count == 0)
                    {
                        MessageBox.Show("Пользователь не наеден. Проверьте правильность ввода логина.", "Ошибка!");
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox3.Clear();
                    }


                    else if (check_user.Rows.Count > 0)
                    {


                      




                        MessageBox.Show("Пароль успешно изменён!", ":)");
                        new_pass.SetPassword(textBox2.Text, textBox1.Text);

                        textBox1.Clear();
                        textBox2.Clear();
                        textBox2.PasswordChar = '*';
                        label4.Visible = false;
                        textBox3.Visible = false;
                        button1.Text = "Войти";
                        textBox3.Clear();
                        podtvrdit = false;

                    }

                }
                
            }
            else if (ex = true)
            {
                DB dB = new DB();



                DataTable user =  dB.GetUser(textBox1.Text, textBox2.Text);


                if (user.Rows.Count > 0)
                {


                    MessageBox.Show("Вход в систему выполнен успешно!", ":)");

                    DataTable ID = dB.GetRights(textBox1.Text, textBox2.Text);
                    int id_rights = Convert.ToInt32(ID.Rows[0][0]);
                    UserData.Username = textBox1.Text;
                    if (id_rights == 1)
                    {
                        this.Hide();
                        var form3 = new Form3();
                        form3.Closed += (s, args) => this.Close();
                        form3.Show();

                    }
                    else if (id_rights == 2)
                    {
                        this.Hide();
                        var form2 = new Form2();
                        form2.Closed += (s, args) => this.Close();
                        form2.Show();

                    }







                }

                else if (user.Rows.Count == 0)
                {
                    popitki++;
                    MessageBox.Show("Пользоавтель не наеден!", ":(");
                    textBox1.Clear();
                    textBox2.Clear();
                    if (popitki > 2)
                    {
                       
                        
                        button2.Visible = true;
                        popitki = 0;
                    }
                }
               
            }
            



        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            popitki = 0;
            podtvrdit = true;

            textBox1.Clear();
            textBox2.Clear();

            button2.Visible = false;
            button1.Text = "Подтвердить изменения";

            label4.Visible = true;
            textBox3.Visible = true;
            textBox2.PasswordChar = '\0';
            label3.Text = "Новый прароль";
            label1.Text = "Сброс пароля";
        }

       
    }
}
