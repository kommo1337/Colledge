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
    /// Логика взаимодействия для AddStatmentWindow.xaml
    /// </summary>
    public partial class AddStatmentWindow : Window
    {
        
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-QFAG2BJ\SQLEXPRESS;
                                                        Initial Catalog=Colledge;
                                                        Integrated Security=True");
        SqlCommand SqlCommand;

        public AddStatmentWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           // cB.RoleCBLoad(RoleCb);
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MBClass.ExitMB();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            new TeacherWindow().ShowDialog();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {

                try
                {
                    sqlConnection.Open();
                    SqlCommand = new SqlCommand($"Insert Into dbo.[statment] " +
                        "(Discipline,NameStudent,SurnameStudent) " +
                        $"Values ('{SurnameStudent.Text}'," +
                        $"'{Discipline.Text}'," +
                        $"'{NameStudent.Text}'," +
                        sqlConnection);
                    SqlCommand.ExecuteNonQuery();
                    MBClass.InfoMb("Успешно");
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
