using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
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
    /// Логика взаимодействия для User1.xaml
    /// </summary>
    public partial class User1 : Page
    {
        string a;
        public User1()
        {
            InitializeComponent();
            string connectionString = @"Data Source=DESKTOP-0MCL3BU\SQLEXPRESS;Initial Catalog=update_verсion;Integrated Security=True";
            SqlConnection myConnection = new SqlConnection(connectionString);

            myConnection.Open();

            string query = $"SELECT * FROM error where emp_id = 1";

            SqlCommand command = new SqlCommand(query, myConnection);

            SqlDataReader reader = command.ExecuteReader();

            List<string> data = new List<string>();

            while (reader.Read())
            {
                data.Add(reader[2].ToString());
                data.Add(reader[1].ToString());
                data.Add(reader[0].ToString());
            }

            reader.Close();

            myConnection.Close();

            tb2.Text = data[1].ToString();
            DateTime dateTime = Convert.ToDateTime(data[0]);
            tb1.Text = Convert.ToString(dateTime.AddMinutes(60) - DateTime.Now); 
            a = data[2].ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=DESKTOP-0MCL3BU\SQLEXPRESS;Initial Catalog=update_verсion;Integrated Security=True";
            SqlConnection myConnection = new SqlConnection(connectionString);

            myConnection.Open();

            string query = $"update error \r\nset answer = {tb3.Text} \r\nwhere id = a";

            SqlCommand command = new SqlCommand(query, myConnection);

            SqlDataReader reader = command.ExecuteReader();

            List<string> data = new List<string>();

            while (reader.Read())
            {
                data.Add(reader[2].ToString());
                data.Add(reader[1].ToString());
            }

            reader.Close();

            myConnection.Close();
        }
    }
}
