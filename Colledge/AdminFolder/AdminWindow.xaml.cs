using Colledge.AdminFolder;
using Colledge.ClassFolder;
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
using System.Windows.Shapes;

namespace Colledge.WindowsFolder
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        DGClass dGClass;
        public AdminWindow()
        {
            InitializeComponent();
            dGClass = new DGClass(ListUserDG);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dGClass.LoadDG("Select * From dbo.[TrueUser]");
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            dGClass.LoadDG("Select * From dbo.[TrueUser] " +
                $"Where Login Like '%{SearchTb.Text}%' ");
        }

        private void AddIm_Click(object sender, RoutedEventArgs e)
        {
            new AddUserWindow().ShowDialog();
            dGClass.LoadDG("Select * From dbo.[TrueUser]");
        }

        private void ListUserDG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ListUserDG.SelectedItem == null)
            {
                MBClass.ErrorMb("Вы не выбрали строку");
            }
            else
            {
                try
                {
                    VariableClass.UserId = dGClass.SelectId();
                    new EditUserWindow().ShowDialog();
                    dGClass.LoadDG("Select * From dbo.[TrueUser]");
                }
                catch (Exception ex)
                {
                    MBClass.ErrorMb(ex);
                }
            }
        }
    }
}

