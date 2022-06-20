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
using Colledge.ClassFolder;
using Colledge.WindowsFolder;

namespace Colledge.AdminFolder
{
    /// <summary>
    /// Логика взаимодействия для AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
          CBClass cB;
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-QFAG2BJ\SQLEXPRESS;
                                                        Initial Catalog=Colledge;
                                                        Integrated Security=True");
            SqlCommand SqlCommand;
        
            public AddUserWindow()
            {

                InitializeComponent();

                cB = new CBClass();

            }
            private void Window_Loaded(object sender, RoutedEventArgs e)
            {
                cB.RoleCBLoad(RoleCb);
            }

            private void ExitBtn_Click(object sender, RoutedEventArgs e)
            {
                MBClass.ExitMB();
            }

            private void BackBtn_Click(object sender, RoutedEventArgs e)
            {
                new AdminWindow().ShowDialog();
            }

            private void AddBtn_Click(object sender, RoutedEventArgs e)
            {
                string pass = PasswordTb.Text;
                string zagl = "QWERTYUIOPASDFGHJKLZXCVBNM";
                string mal = "qwertyuiopasdfghjklzxcvbnm";
                string cif = "123457890";
                string znak = "!@#$%^&*()_+";

                if (string.IsNullOrWhiteSpace(LoginTb.Text))
                {
                    MBClass.ErrorMb("Некоректный логин");
                    LoginTb.Focus();
                }
                else if (string.IsNullOrWhiteSpace(PasswordTb.Text))
                {
                    MBClass.ErrorMb("Некоректный пароль");
                    PasswordTb.Focus();
                }
                else if (zagl.IndexOfAny(pass.ToCharArray()) == -1)
                {
                    MBClass.ErrorMb("Пароль должен содержать заглавную букву");
                    PasswordTb.Focus();
                }
                else if (mal.IndexOfAny(pass.ToCharArray()) == -1)
                {
                    MBClass.ErrorMb("Пароль должен содержать маленькую букву");
                    PasswordTb.Focus();
                }
                else if (cif.IndexOfAny(pass.ToCharArray()) == -1)
                {
                    MBClass.ErrorMb("Пароль должен содержать цифру");
                    PasswordTb.Focus();
                }
                else if (znak.IndexOfAny(pass.ToCharArray()) == -1)
                {
                    MBClass.ErrorMb("Пароль должен содержать " +
                        "один из этих знаков " + znak);
                    PasswordTb.Focus();
                }
                else if (RoleCb.SelectedIndex == -1)
                {
                    MBClass.ErrorMb("Не выбрана роль");
                    RoleCb.Focus();
                }
                else
                {
                    try
                    {
                    
                    sqlConnection.Open();
                        SqlCommand = new SqlCommand($"Insert Into dbo.[TrueUser] " +
                            "(Login,Password,IDRole) " +
                            $"Values ('{LoginTb.Text}'," +
                            $"'{PasswordTb.Text}'," +
                            $"'{RoleCb.SelectedValue.ToString()}')",
                            sqlConnection);
                        SqlCommand.ExecuteNonQuery();
                        MBClass.InfoMb($"Пользователь {LoginTb.Text} " + "успешно добавлен");
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
