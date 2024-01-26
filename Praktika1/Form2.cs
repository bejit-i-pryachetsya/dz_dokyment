using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Npgsql.Internal;
using SixLabors.Fonts;
using ZXing;
using ZXing.Common;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Praktika1
{
    public partial class Form2 : Form
    {






        public class QRCodeGenerator
        {
            public static void GenerateQRCode(string text, string filePath)
            {
                var writer = new BarcodeWriter
                {
                    Format = BarcodeFormat.QR_CODE,
                    Options = new EncodingOptions
                    {
                        Width = 400,
                        Height = 400
                    }
                };

                var qrCodeBitmap = writer.Write(text);
                qrCodeBitmap.Save(filePath, ImageFormat.Png);
            }
        }







        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }





        public Form2()
        {
            InitializeComponent();
            label1.Text = UserData.Username;
            
            DB db = new DB();
            dataGridView2.DataSource= db.GetProgramms();
            dataGridView1.DataSource = db.GetPerson();

            dataGridView1.Columns[0].HeaderText = "Идентификатор";
            dataGridView1.Columns[1].HeaderText = "ФИО";
            dataGridView1.Columns[2].HeaderText = "Дата рождения";
            dataGridView1.Columns[3].HeaderText = "Город";
            dataGridView1.Columns[4].HeaderText = "Улица";
            dataGridView1.Columns[5].HeaderText = "Дом";
            dataGridView1.Columns[6].HeaderText = "Квартира";
            dataGridView1.Columns[7].HeaderText = "Серия паспорта";
            dataGridView1.Columns[8].HeaderText = "Номер паспорта";
            dataGridView1.Columns[9].HeaderText = "Кем выдан паспорт";
            dataGridView1.Columns[10].HeaderText = "Дата выдачи паспорта";
            dataGridView1.Columns[11].HeaderText = "Номер телефона";


            dataGridView2.Columns[0].HeaderText = "Идентификатор";
            dataGridView2.Columns[1].HeaderText = "Название";
            dataGridView2.Columns[2].HeaderText = "Срок обучения мес.";
            dataGridView2.Columns[3].HeaderText = "Квалификация";
            dataGridView2.Columns[4].HeaderText = "Стоимость руб./мес.";



            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = false;

        }
  
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form1 = new Form1();
            form1.Closed += (s, args) => this.Close();
            form1.Show();
        }

        string FIO;
        string birthday;
        string city;
        string street;
        string house;
        string apartment;
        string passport_s;
        string passport_n;
        string vidan;
        string vidan_d;
        string number;

        int x;
        string programma;
        string cost;

        private void button2_Click(object sender, EventArgs e)
        {

            if (FIO != null && programma != null)
            {
                BaseFont baseFont = BaseFont.CreateFont(@"D:\лабы дойникова\Praktika1\Praktika1\arialmt.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Font font1 = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.BOLD);
                using (FileStream stream = new FileStream($@"C:\Users\Acer\Desktop\Договор {FIO}.pdf", FileMode.Create))
                {
                    Document document = new Document();
                    PdfWriter.GetInstance(document, stream);
                    document.Open();
                    String pr = Environment.NewLine;
                    String phrase0 = "                                           Договор оказания образовательных услуг"; 
                    String phrase = "Г. Воронеж 1 февраля 2024 года.";                 
                    String phrase1 = "ООО «Nikita Pudge», в лице генерального директора Сушкевича Никиты Александровича, действующего на основании Устава общества, именуемого в дальнейшем Исполнитель, с одной стороны";
                    String phrase2 = "И";
                    String phrase3 = $"{FIO}, {birthday} года рождения, проживающий по адресу: город {city}, улица {street}, дом {house}, квартира {apartment}, паспорт: серия {passport_s}, номер {passport_n}, выданный {vidan} {vidan_d}, номер телефона: {number}, именуемый в дальнейшем Заказчик, с другой стороны заключили настоящий договор о нижеследующем.";
                    String phrase4 = "Предмет";
                    String phrase5 = $"В соответствии с настоящим соглашением Исполнитель в лице ООО «Nikita Pudge» обязуется оказать Заказчику в лице {FIO}, за оговоренную договором плату, следующие образовательные услуги:";
                    String phrase6 = $"● Обучающий курс « {programma}», обучение предполагает прохождение online лекций связанных с заявленной тематикой.";                 
                    String phrase7 = "Заключительные положения";
                    String phrase8 = "● Настоящий договор составлен в двух экземплярах. Один экземпляр передается Заказчику, другой передается Исполнителю. ";
                    String phrase9 = "● По всем моментам, которые не оговорены в настоящем соглашении, стороны руководствуются действующим законодательством Российской Федерации. ";                  
                    String phrase10 = $"Подпись директора: ____" + Environment.NewLine + Environment.NewLine + "Подпись плательщика: ____";  
      
                    document.Add(new Paragraph(phrase0, font1));
                    document.Add(new Paragraph(pr, font));
                    document.Add(new Paragraph(phrase, font));
                    document.Add(new Paragraph(pr, font));
                    document.Add(new Paragraph(phrase1, font));
                    document.Add(new Paragraph(phrase2, font));
                    document.Add(new Paragraph(phrase3, font));
                    document.Add(new Paragraph(pr, font));
                    document.Add(new Paragraph(phrase4, font1));
                    document.Add(new Paragraph(phrase5, font));
                    document.Add(new Paragraph(phrase6, font));
                    document.Add(new Paragraph(pr, font));
                    document.Add(new Paragraph(phrase7, font1));
                    document.Add(new Paragraph(phrase8, font));
                    document.Add(new Paragraph(phrase9, font));
                    document.Add(new Paragraph(pr, font));
                    document.Add(new Paragraph(phrase10, font1));
                   
                    document.Close();
                    MessageBox.Show($"Договор успешно создан! Он находится в: C:\\Users\\Acer\\Desktop\\Договор {FIO}.pdf", ":)");
                }
            }
            else
            {
                MessageBox.Show("Выберите данные для заполнения договора!", "Ошибка!");
            }
            

        }





        private void button3_Click(object sender, EventArgs e)
        {
            if (FIO != null && programma != null)
            {


                string qrtext = "https://web4-new.online.sberbank.ru/main";
                QRCodeEncoder encoder = new QRCodeEncoder();
                Bitmap qrcode = encoder.Encode(qrtext);

                using (FileStream stream = new FileStream($@"C:\Users\Acer\Desktop\Квитанция {FIO}.pdf", FileMode.Create))
                {
                    Document document = new Document(PageSize.A4, 0, 0, 0, 0);
                    PdfWriter.GetInstance(document, stream);
                    document.Open();

                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(qrcode, System.Drawing.Imaging.ImageFormat.Bmp);
                    img.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
                    img.ScalePercent(60); // Установите масштабирование изображения по вашему желанию

                    BaseFont baseFont = BaseFont.CreateFont(@"D:\лабы дойникова\Praktika1\Praktika1\arialmt.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);

                    // Создание таблицы
                    PdfPTable table = new PdfPTable(2);

                    // Устанавливаем ширину столбцов
                    float[] columnWidths = { 30f, 120f };
                    table.SetWidths(columnWidths);

                    
                    
                    document.Add((new Paragraph($"                                           Квитанция на оплату оказания обучающих услуг. ", font)));
                    String pr = Environment.NewLine;
                    document.Add(new Paragraph(pr, font));





                    // Добавление заголовка
                    PdfPCell qrCell = new PdfPCell(new Phrase("QR Code", font));
                    qrCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    qrCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    table.AddCell(qrCell);




                    PdfPCell dataCell = new PdfPCell(new Phrase("Данные квитанции", font));
                    dataCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    dataCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    table.AddCell(dataCell);
                    



                    // Создание и добавление QR кода в первую ячейку
                    PdfPCell qrCodeCell = new PdfPCell(img);
                    qrCodeCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    qrCodeCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    table.AddCell(qrCodeCell);






                    PdfPTable table1 = new PdfPTable(1);
                    PdfPCell Vgty = new PdfPCell(new Phrase("Воронежский Государственный Технический Университет", font));
                    Vgty.HorizontalAlignment = Element.ALIGN_CENTER;
                    Vgty.VerticalAlignment = Element.ALIGN_MIDDLE;
                    table1.AddCell(Vgty);
                    table1.AddCell((new Paragraph($"ИНН: 7707083893 КПП: 366402001", font)));
                    table1.AddCell((new Paragraph($"Номер Счёта получателя: 40817810113004014905", font)));
                    PdfPCell Bank = new PdfPCell(new Phrase("БИК 042007001 ОТДЕЛЕНИЕ ВОРОНЕЖ БАНКА РОССИИ в городе Воронеже", font));
                    Bank.HorizontalAlignment = Element.ALIGN_CENTER;
                    Bank.VerticalAlignment = Element.ALIGN_MIDDLE;
                    table1.AddCell(Bank);
                    table1.AddCell((new Paragraph($"Договор: об оплате за обучающие курсы", font)));
                    table1.AddCell((new Paragraph($@"ФИО обучающегося: {FIO}", font)));
                    table1.AddCell((new Paragraph($"Назначение: оплата за online обучение", font)));
                    table1.AddCell((new Paragraph($"ФИО плательщика: {FIO}", font)));
                    table1.AddCell((new Paragraph($"Адрес плательщика: г. {city}, ул. {street}, д. {house}, кв. {apartment}", font)));
                    table1.AddCell((new Paragraph($"КБК: 01294801948104104", font)));
                    table1.AddCell((new Paragraph($"ОКТМО: 23719419", font)));
                    table1.AddCell((new Paragraph($"Сумма: {cost} рублей", font)));
                    table1.AddCell((new Paragraph($"С условиями договора об оплате ознакомлен и согласен. Подпись плательщика: ____", font)));
                    table.AddCell(table1);
                    
                    // Добавление таблицы в документ
                    document.Add(table);

                    document.Close();

                    MessageBox.Show($"Договор успешно создан! Он находится в: C:\\Users\\Acer\\Desktop\\Квитанция {FIO}.pdf", ":)");
                }




            }
            else
            {
                MessageBox.Show("Выберите данные для заполнения квитанции!", "Ошибка!");
            }
        }










        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


            int x = e.RowIndex;
            FIO = dataGridView1.Rows[x].Cells[1].Value.ToString();
            birthday = dataGridView1.Rows[x].Cells[2].Value.ToString();
            city = dataGridView1.Rows[x].Cells[3].Value.ToString();
            street = dataGridView1.Rows[x].Cells[4].Value.ToString();
            house = dataGridView1.Rows[x].Cells[5].Value.ToString();
            apartment = dataGridView1.Rows[x].Cells[6].Value.ToString();
            passport_s = dataGridView1.Rows[x].Cells[7].Value.ToString();
            passport_n = dataGridView1.Rows[x].Cells[8].Value.ToString();
            vidan = dataGridView1.Rows[x].Cells[9].Value.ToString();
            vidan_d = dataGridView1.Rows[x].Cells[10].Value.ToString();
            number = dataGridView1.Rows[x].Cells[11].Value.ToString();
            

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            x = e.RowIndex;
            programma = dataGridView2.Rows[x].Cells[1].Value.ToString();
            cost = dataGridView2.Rows[x].Cells[4].Value.ToString();
        }

       
    }
}
