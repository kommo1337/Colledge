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

namespace Colledge.TeacherFolder
{
    /// <summary>
    /// Логика взаимодействия для TeacherWindow.xaml
    /// </summary>
    public partial class TeacherWindow : Window
    {
        DGClass dGClass;
        public TeacherWindow()
        {
            InitializeComponent();
            dGClass = new DGClass(StatementDG);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dGClass.LoadDG("Select * From dbo.[statements]");
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            dGClass.LoadDG("Select * From dbo.[statements] " +
                $"Where [Name of Discipline] Like '%{SearchTb.Text}%' ");
        }

        private void AddIm_Click(object sender, RoutedEventArgs e)
        {
            new AddStatmentWindow().ShowDialog();
            dGClass.LoadDG("Select * From dbo.[statements]");
        }

        private void ListUserDG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (StatementDG.SelectedItem == null)
            {
                MBClass.ErrorMb("Вы не выбрали строку");
            }
            else
            {
                try
                {
                    VariableClass.Statment = dGClass.SelectId();
                    new EditStatment().ShowDialog();
                    dGClass.LoadDG("Select * From dbo.[statments]");
                }
                catch (Exception ex)
                {
                    MBClass.ErrorMb(ex);
                }
            }
        }


    }
}
