using System;
using System.Collections.Generic;
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
using System.Data;
using System.Data.SqlClient;


namespace lab04_DesAEA
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection connection = new SqlConnection("LAB707-15\\SQLEXPRESS02,Initial Catalog=persons");

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            //Forma Conectada Procedimiento Almacenado
            List<Person> people = new List<Person>();

            connection.Open();
            SqlCommand command = new SqlCommand("procedure_listar", connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter1 = new SqlParameter();
            parameter1.SqlDbType = SqlDbType.VarChar;
            parameter1.Size = 50;
            //parameter.Value = txtLastName.Text.Trim();
            parameter1.Value = "";
            parameter1.ParameterName = "@IdPersonID";

            SqlParameter parameter2 = new SqlParameter();
            parameter2.SqlDbType = SqlDbType.VarChar;
            parameter2.Size = 50;
            parameter2.Value = "";
            parameter2.ParameterName = "@LastName";

            SqlParameter parameter3 = new SqlParameter();
            parameter3.SqlDbType = SqlDbType.VarChar;
            parameter3.Size = 50;
            parameter3.Value = "";
            parameter3.ParameterName = "@FirstName";

            SqlParameter parameter4 = new SqlParameter();
            parameter4.SqlDbType = SqlDbType.VarChar;
            parameter4.Size = 50;
            parameter4.Value = "";
            parameter4.ParameterName = "@FullName";

            SqlParameter parameter5 = new SqlParameter();
            parameter5.SqlDbType = SqlDbType.Int;
            parameter5.Size = 50;
            parameter5.Value = "";
            parameter5.ParameterName = "@Age";

            command.Parameters.Add(parameter1);
            command.Parameters.Add(parameter2);
            command.Parameters.Add(parameter3);
            command.Parameters.Add(parameter4);
            command.Parameters.Add(parameter5);

            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                people.Add(new Person
                {
                    PersonId = dataReader["PeopleID"].ToString(),
                    LastName = dataReader["LastName"].ToString(),
                    FirstName = dataReader["FirstName"].ToString(),
                    FullName = dataReader["FullName"].ToString(),
                    Age = (int)dataReader["Age"]
                });
            }
            connection.Close();
            dgvPeople.ItemsSource = people;
        }
    }
}