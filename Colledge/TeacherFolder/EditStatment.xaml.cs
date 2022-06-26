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
                    "dbo.[TrueUser] " +
                    $"Set [Login] ='{LoginTb.Text}'," +
                    $"[Password]='{PasswordTb.Text}'," +
                    $"RoleId='{RoleCb.SelectedValue.ToString()}' " +
                    $"Where IDUser='{VariableClass.Statment}'",
                    sqlConnection);
                SqlCommand.ExecuteNonQuery();
                MBClass.InfoMb($"Данные пользователя " +
                    $"{LoginTb.Text}" +
                    $"успешно отредактированы");
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
            cB.RoleCBLoad(RoleCb);
            try
            {
                sqlConnection.Open();
                SqlCommand = new SqlCommand("Select * from dbo.[TrueUser] " +
                    $"Where IDUser='{VariableClass.UserId}'",
                    sqlConnection);
                dataReader = SqlCommand.ExecuteReader();
                dataReader.Read();
                LoginTb.Text = dataReader[1].ToString();
                PasswordTb.Text = dataReader[2].ToString();
                RoleCb.SelectedValue = dataReader[5].ToString();
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
