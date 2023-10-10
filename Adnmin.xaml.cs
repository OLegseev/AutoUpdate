using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace ErrorChecking
{
    /// <summary>
    /// Логика взаимодействия для Adnmin.xaml
    /// </summary>
    public partial class Adnmin : Page
    {



        public Adnmin()
        {
            InitializeComponent();
            grid2.Visibility = Visibility.Hidden;
            string connectionString = @"Data Source=DESKTOP-0MCL3BU\SQLEXPRESS;Initial Catalog=update_verсion;Integrated Security=True";
            SqlConnection myConnection = new SqlConnection(connectionString);

            myConnection.Open();

            string query = "SELECT * FROM error";

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
                errors.Add(new error { id = reader[0].ToString(), message = reader[1].ToString(), starttimr = reader[2].ToString(), endtime = reader[3].ToString(), status = reader[4].ToString(), emp = reader[5].ToString(), });
            }

            reader.Close();

            myConnection.Close();


            DG1.ItemsSource = errors;



        }


        public class error
        {
            public string id { get; set; }
            public string message { get; set; }
            public string starttimr { get; set; }
            public string endtime { get; set; }
            public string status { get; set; }
            public string emp { get; set; }
        }
        public List<error> errors = new List<error>();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            DG1.ItemsSource = null;
            List<string> err = new List<string>();
            List<string> user = new List<string>();
            try
            {
                grid2.Visibility = Visibility.Hidden;
                b2.Visibility = Visibility.Hidden;
                b1.Visibility = Visibility.Visible;
                errors.Clear();
                string connectionString = @"Data Source=DESKTOP-0MCL3BU\SQLEXPRESS;Initial Catalog=update_verсion;Integrated Security=True";
                SqlConnection myConnection = new SqlConnection(connectionString);

                myConnection.Open();

                string query = "SELECT * FROM error where stat = 'ожидает распределения'";

                SqlCommand command = new SqlCommand(query, myConnection);

                SqlDataReader reader = command.ExecuteReader();




                while (reader.Read())
                {
                    err.Add(reader[0].ToString());
                    errors.Add(new error { id = reader[0].ToString(), message = reader[1].ToString(), starttimr = reader[2].ToString(), endtime = reader[3].ToString(), status = reader[4].ToString(), emp = reader[5].ToString(), });
                }


                reader.Close();

                
                string query1 = "select logining.login_us from logining inner join employee on employee.logining_data = logining.id";

                SqlCommand command1 = new SqlCommand(query1, myConnection);

                SqlDataReader reader1 = command1.ExecuteReader();




                while (reader1.Read())
                {
                    user.Add(reader1[0].ToString());
                }


                reader1.Close();

                myConnection.Close();

                DG1.ItemsSource = errors;
            }
            catch
            {
                DG1.ItemsSource = "";
            }
            if (errors.Count == 0)
            {
                DG1.ItemsSource = null;
            }
            else
            {
                grid2.Visibility = Visibility.Visible;
                cbe.ItemsSource = err;
                cbi.ItemsSource = user;
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            List<string> err = new List<string>();
            List<string> user = new List<string>();
            DG1.ItemsSource = null;
            grid2.Visibility = Visibility.Visible;
            b1.Visibility = Visibility.Hidden;
            b2.Visibility = Visibility.Visible;
            try
            {
                errors.Clear();
                string connectionString = @"Data Source=DESKTOP-0MCL3BU\SQLEXPRESS;Initial Catalog=update_verсion;Integrated Security=True";
                SqlConnection myConnection = new SqlConnection(connectionString);

                myConnection.Open();

                string query = "SELECT * FROM error where stat = 'на выполнении'";

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
                    err.Add(reader[0].ToString());
                    errors.Add(new error { id = reader[0].ToString(), message = reader[1].ToString(), starttimr = reader[2].ToString(), endtime = reader[3].ToString(), status = reader[4].ToString(), emp = reader[5].ToString(), });
                }

                reader.Close();

                

               
                string query1 = "select logining.login_us from logining inner join employee on employee.logining_data = logining.id ";

                SqlCommand command1 = new SqlCommand(query1, myConnection);

                SqlDataReader reader1 = command1.ExecuteReader();




                while (reader1.Read())
                {
                    user.Add(reader1[0].ToString());
                }


                reader1.Close();

                myConnection.Close();

                DG1.ItemsSource = errors;
            }
            catch
            {
                DG1.ItemsSource = "";
            }
            if (errors.Count == 0)
            {
                DG1.ItemsSource = null;
            }
            else
            {
                grid2.Visibility = Visibility.Visible;
                cbe.ItemsSource = err;
                cbi.ItemsSource = user;
            }

        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            DG1.ItemsSource = null;
            try
            {
                grid2.Visibility = Visibility.Hidden;
                errors.Clear();
                string connectionString = @"Data Source=DESKTOP-0MCL3BU\SQLEXPRESS;Initial Catalog=update_verсion;Integrated Security=True";
                SqlConnection myConnection = new SqlConnection(connectionString);

                myConnection.Open();

                string query = "SELECT * FROM error";

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
                    errors.Add(new error { id = reader[0].ToString(), message = reader[1].ToString(), starttimr = reader[2].ToString(), endtime = reader[3].ToString(), status = reader[4].ToString(), emp = reader[5].ToString(), });
                }

                reader.Close();

                myConnection.Close();
                DG1.ItemsSource = errors;
            }
            catch
            {
                DG1.ItemsSource = "";
            }
            if (errors.Count == 0)
            {
                DG1.ItemsSource = null;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            

            
            string connectionString = @"Data Source=DESKTOP-0MCL3BU\SQLEXPRESS;Initial Catalog=update_verсion;Integrated Security=True";
            SqlConnection myConnection = new SqlConnection(connectionString);

            myConnection.Open();

            string query = $"update error set emp_id = 1 where id = {cbe.Text} update error set stat = 'на выполнении' where id = {cbe.Text}";

            SqlCommand command = new SqlCommand(query, myConnection);

            SqlDataReader reader = command.ExecuteReader();

           
            reader.Close();

            myConnection.Close();

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=DESKTOP-0MCL3BU\SQLEXPRESS;Initial Catalog=update_vercion;Integrated Security=True";
            SqlConnection myConnection = new SqlConnection(connectionString);

            myConnection.Open();

            string query = $"SELECT * FROM error where id = {cbe.Text}";

            SqlCommand command = new SqlCommand(query, myConnection);

            SqlDataReader reader = command.ExecuteReader();

            List<string> data = new List<string>();

            while (reader.Read())
            {
                data.Add(reader[3].ToString());
            }

            reader.Close();

            myConnection.Close();


            DateTime dateTime = Convert.ToDateTime(data[0]);
            dateTime.AddHours(1);








            SqlConnection myConnection1 = new SqlConnection(connectionString);

            myConnection1.Open();

            string query1 = $"update error set date_end = '{dateTime}' where id = 1)";

            SqlCommand command1 = new SqlCommand(query1, myConnection1);

            SqlDataReader reader1 = command1.ExecuteReader();


            reader1.Close();

            myConnection1.Close();
        }
    }
}
