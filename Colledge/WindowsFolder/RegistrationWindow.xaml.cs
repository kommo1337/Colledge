using System;
using System.Data.SqlClient;
using System.Windows;
using Colledge;

namespace Colledge.WindowsFolder
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-QFAG2BJ\SQLEXPRESS;
                                                        Initial Catalog=Colledge;
                                                        Integrated Security=True");
        SqlCommand sqlCommand = new SqlCommand();
        public RegistrationWindow()
        {
            InitializeComponent();
        }
        private void RegistrationBtn_Click(object sender, RoutedEventArgs e)
        {
            string pass = PasswordPsb.Password;
            string zagl = "QWERTYUIOPASDFGHJKLZXCVBNM";
            string mal = "qwertyuiopasdfghjklzxcvbnm";
            string cif = "123457890";
            string znak = "!@#$%^&*()_+";
            if (Preod.IsChecked == true || Admin.IsChecked == true)
            {
                sqlConnection.Open();
                sqlCommand.CommandText = @"Select Count(*) From dbo.TrueUser Where Login = " + $"'{LoginTb.Text}'";
                sqlCommand.Connection = sqlConnection;
                var n = sqlCommand.ExecuteScalar();
                if ((int)n > 0)
                {
                    MBClass.ErrorMb("Bad");
                    sqlConnection.Close();
                }
                else
                {
                    sqlConnection.Close();
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
                    else if (zagl.IndexOfAny(pass.ToCharArray()) == -1)
                    {
                        MBClass.ErrorMb("Пароль должен содержать заглавную букву");
                        PasswordPsb.Focus();
                    }
                    else if (mal.IndexOfAny(pass.ToCharArray()) == -1)
                    {
                        MBClass.ErrorMb("Пароль должен содержать маленькую букву");
                        PasswordPsb.Focus();
                    }
                    else if (cif.IndexOfAny(pass.ToCharArray()) == -1)
                    {
                        MBClass.ErrorMb("Пароль должен содержать цифру");
                        PasswordPsb.Focus();
                    }
                    else if (znak.IndexOfAny(pass.ToCharArray()) == -1)
                    {
                        MBClass.ErrorMb("Пароль должен содержать " +
                            "один из этих знаков " + znak);
                        PasswordPsb.Focus();
                    }
                    else if (string.IsNullOrWhiteSpace(PasswordDoublePsb.Password))
                    {
                        MBClass.ErrorMb("Некоректный повтор пароля");
                        PasswordDoublePsb.Focus();
                    }
                    else if (PasswordDoublePsb.Password != PasswordPsb.Password)
                    {
                        MBClass.ErrorMb("Пароли не совпадают");
                        PasswordDoublePsb.Focus();
                        PasswordPsb.Focus();
                    }

                    else
                    {
                        try
                        {
                            sqlConnection.Open();
                            sqlCommand = new SqlCommand("Insert Into TrueUser " +
                                "(Login,Password, IDRole) Values " +
                                $"('{LoginTb.Text}','{PasswordPsb.Password}',2)",
                                sqlConnection);
                            sqlCommand.ExecuteNonQuery();
                            MBClass.InfoMb("Пользователь зарегистрирован");
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
            }
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            AutorisationWindow window = new AutorisationWindow();
            window.ShowDialog();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

    }
}
