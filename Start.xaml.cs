using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ErrorChecking
{
    /// <summary>
    /// Логика взаимодействия для Start.xaml
    /// </summary>
    public partial class Start : Page
    {
        public Start()
        {
            InitializeComponent();
            upd.Visibility = Visibility.Hidden;
            WebClient wc = new WebClient();
            vers.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString(2);
            string ver = wc.DownloadString("https://gypinex.ru/deniska/ver.txt");
            if (curver != ver)
            {
                MessageBox.Show("Доступно новая версия приложения!!");
                upd.Visibility = Visibility.Visible;
            }
            frameG.NavigationService.Navigate(new Adnmin());
        }



        string curver = Assembly.GetExecutingAssembly().GetName().Version.ToString(2);
        string exename = AppDomain.CurrentDomain.FriendlyName;
        string exepath = Assembly.GetEntryAssembly().Location;



        public void Cmd(string line)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "cmd",
                Arguments = $"/c {line}",
                WindowStyle = ProcessWindowStyle.Hidden

            });
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (WebClient wc = new WebClient())
            {
                string ver = wc.DownloadString("https://gypinex.ru/deniska/ver.txt");

                string connStr = @"Data Source=DESKTOP-0MCL3BU\SQLEXPRESS;Initial Catalog=update_verсion;Integrated Security=True";






                if (curver != ver)
                {
                    DateTime date1 = new DateTime(2015, 7, 20, 18, 30, 25);

                    SqlConnection conn = new SqlConnection(connStr);
                    try
                    {
                        //пробуем подключится
                        SqlCommand cmdCreateDataBase = new SqlCommand(string.Format(/*$"drop database update_version"+*/
$"create database update_version " +
$"use update_version " +

$"create table data_statistic " +
$"( " +
 $"id int primary key identity not null, " +
 $"version_id nvarchar(50) not null, " +
 $"path_update nvarchar(50) not null, " +
 $"date_update datetime not null " +
$") " +

$"create table logining " +
$"(" +
$"id int primary key identity not null, " +
$"login_us nvarchar(50) not null, " +
$"password_us nvarchar(50) not null, " +
$") " +

$"insert logining(login_us, password_us) " +
$"values('User1', '123') " +
$"create table error " +
$"(" +
$"id int primary key identity not null, " +
$"messageerr nvarchar(500) not null, " +
$"date_add datetime not null, " +
$"date_end datetime not null, " +
$"stat nvarchar(50) not null, " +
$"emp_id int foreign key references employee(id), " +
$"answer nvarchar(50) " +
$")" +
$"insert error(messageerr, date_add, date_end, stat) " +
$"values('asdsad', '01.10.2007 12:30:30', '01.10.2007 13:30:30', 'подтверждение') " +


$"create table employee " +
$"(" +
$"id int primary key identity not null, " +
$"logining_data  int foreign key references logining(id), " +
$"stat nvarchar(50) " +
$") " +



$"insert employee(logining_data) " +
$"insert data_statistic(version_id,path_update,date_update) values('{ver}', 'https://gypinex.ru/deniska/AutoUpdate.exe', '{date1}')"), conn);
                        conn.Open();
                        cmdCreateDataBase.ExecuteNonQuery();
                        conn.Close();


                    }
                    catch (SqlException se)
                    {

                    }
                    finally
                    {

                        conn.Close();
                        conn.Dispose();
                    }
                    wc.DownloadFile("https://gypinex.ru/deniska/AutoUpdate.exe", "new.exe");
                    Cmd($"taskkill /f /im \"{exename}\" && timeout /t 1 && del \"{exepath}\" && ren new.exe \"{exename}\" && \"{exepath}\"");
                    //Process.Start("new.exe");


                }

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int[] mass = new int[] { 1, 2, 3, 4 };
                for (int i = 0; i < 10; i++)
                {
                    mass[i] = 12;
                }
            }
            catch (Exception ex)
            {
                ExTrob(ex);
            }

        }
        public void ExTrob(Exception ex)
        {
            MessageBox.Show("Возникла предвиденная ошибка!!!");

            string from = @"appdeveloperex@yandex.ru";
            string pass = "saagyriwcvkvrpmm";
            MailMessage mess = new MailMessage();
            mess.To.Add(@"AppDeveloperEx@yandex.ru"); // кому отправляем
            mess.From = new MailAddress(from);
            // тема письма
            mess.Subject = "Появилась ошибка";
            // текст письма
            mess.Body = Convert.ToString(ex);
            //mess.Attachments.Add(new Attachment($"Process.txt"));
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 587);
            // логин и пароль
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential(from.Split('@')[0], pass);
            smtp.Send(mess);

            string connStr = @"Data Source=DESKTOP-0MCL3BU\SQLEXPRESS;Initial Catalog=update_verсion;Integrated Security=True";


            DateTime date1 = new DateTime(2015, 7, 20, 18, 30, 25);

            SqlConnection conn = new SqlConnection(connStr);
            try
            {
                //пробуем подключится
                DateTime dateTime = new DateTime();
                dateTime = DateTime.Now.AddHours(1);

                SqlCommand cmdCreateDataBase = new SqlCommand(string.Format($"insert error(messageerr,date_add,date_end,stat) values('{ex}','{Convert.ToString(DateTime.Now)}','{Convert.ToString(dateTime)}','ожидает распределения')"), conn);
                conn.Open();
                cmdCreateDataBase.ExecuteNonQuery();
                conn.Close();

            }
            catch (SqlException se)
            {

            }

        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {

        }
    }
}

