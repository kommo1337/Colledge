using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;
using System.Windows.Controls;

namespace Colledge
{
    internal class CBClass
    {
        SqlConnection SqlConnection = new SqlConnection(@"Data Source=DESKTOP-QFAG2BJ\SQLEXPRESS;
                                                        Initial Catalog=Colledge;
                                                        Integrated Security=True");
        SqlDataAdapter sqlData;
        DataSet dataSet;
        public void RoleCBLoad(ComboBox comboBox)
        {
            try
            {
                SqlConnection.Open();
                sqlData = new SqlDataAdapter("Select * " +
                    "From dbo.[Rolee]",
                    SqlConnection);
                dataSet = new DataSet();
                sqlData.Fill(dataSet, "[Rolee]");
                comboBox.ItemsSource = dataSet.Tables["[Rolee]"].DefaultView;
                comboBox.DisplayMemberPath = dataSet.
                    Tables["[Rolee]"].Columns["RoleName"].ToString();
                comboBox.SelectedValuePath = dataSet.
                   Tables["[Rolee]"].Columns["[№ p/p]"].ToString();
            }
            catch (Exception ex)
            {
                MBClass.ErrorMb(ex);
            }
            finally
            {
                SqlConnection.Close();
            }
        }
    }
}
