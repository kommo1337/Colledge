using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data.SqlClient;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Colledge.AdminFolder;
using Colledge;
using Colledge.TeacherFolder;

namespace Colledge.WindowsFolder
{
    /// <summary>
    /// Логика взаимодействия для AutorisationWindow.xaml
    /// </summary>
    public partial class AutorisationWindow : Window
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-QFAG2BJ\SQLEXPRESS;
                                                        Initial Catalog=Colledge;
                                                        Integrated Security=True");
        SqlCommand SqlCommand;
        public AutorisationWindow()
        {
            InitializeComponent();
        }
        private void LogInBth_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LoginTb.Text))
            {
                MBClass.ErrorMb("Некоректный логин");
                LoginTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(PasswordPsb.Password))
            {
                MBClass.ErrorMb("Некоректный пароль");
                PasswordPsb.Focus();
            }
            else
            {
                try
                {
                    sqlConnection.Open();
                    SqlCommand = new SqlCommand(@"Select * From dbo.TrueUser Where Login = " + $"'{LoginTb.Text}' and Password = " + $"'{ PasswordPsb.Password}'");
                    SqlCommand.Connection = sqlConnection;
                    //dataReader = SqlCommand.ExecuteReader();
                    //dataReader.Read();
                    var n = SqlCommand.ExecuteReader();
                    if (!n.HasRows)
                    {
                        MBClass.ErrorMb("Введеный пароль не коректен");
                        PasswordPsb.Focus();

                    }
                    else
                    {
                        n.Read();
                        switch (n[3])
                         {
                            case 2:
                                new AdminWindow().ShowDialog();
                                Close();
                                break;
                            case 1:
                                 new TeacherWindow().ShowDialog();
                                Close();
                                 break;
                         }
                    }
                    sqlConnection.Close();

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
        }

        private void RegistrationBtn_Click(object sender, RoutedEventArgs e)
        {
            new RegistrationWindow().ShowDialog();
        }

        private void LogOutBth_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
