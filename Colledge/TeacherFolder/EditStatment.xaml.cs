using Colledge.ClassFolder;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Colledge.TeacherFolder
{
    /// <summary>
    /// Логика взаимодействия для EditStatment.xaml
    /// </summary>
    public partial class EditStatment : Window
    {
        CBClass cB;
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-QFAG2BJ\SQLEXPRESS;
                                                        Initial Catalog=Colledge;
                                                        Integrated Security=True");
        SqlCommand SqlCommand;
        SqlDataReader dataReader;
        public EditStatment()
        {
            InitializeComponent();
            cB = new CBClass();
        }
        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlConnection.Open();
                SqlCommand =
                    new SqlCommand("Update " +
                    "dbo.[statments] " +
                    $"Set [Name of Discipline] ='{Discipline.Text}'," +
                    $"[Name of Student]='{NameStudent.Text}'," +
                    $"[Second Name]='{SurnameStudent.Text}'," +
                    $"[Therd Name]='{TherdStudent.Text}'," +
                    $"[Name of Teacher]='{NamePrepod.Text}'," +
                    $"[Second Name Teacher]='{SurnamePrepod.Text}'," +
                    $"[Therd Name Teache]='{TherdNamePrepod.Text}'," +
                    $"[Date Of The Pass]='{PassDate.Text}'," +
                    $"[Year]='{Date.Text}'," +
                    $"[Semestr]='{Semestr.Text}'," +
                    $"[Group]='{Group.Text}'," +
                    $"[Grade]='{Grade.Text}'," +
                    $"Where IDUser='{VariableClass.Statment}'",
                    sqlConnection);
                SqlCommand.ExecuteNonQuery();
                MBClass.InfoMb("Sucsess");                   
                    
            }
            catch (Exception ex)
            {
                MBClass.ErrorMb(ex);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlConnection.Open();
                SqlCommand = new SqlCommand("Select * from dbo.[statment] " +
                    $"Where IDUser='{VariableClass.Statment}'",
                    sqlConnection);
                dataReader = SqlCommand.ExecuteReader();
                dataReader.Read();
                Discipline.Text = dataReader[1].ToString();
                NameStudent.Text = dataReader[2].ToString();
            }
            catch (Exception ex)
            {
                MBClass.ErrorMb(ex);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MBClass.ExitMB();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            new TeacherWindow().ShowDialog();
        }
    }
}
