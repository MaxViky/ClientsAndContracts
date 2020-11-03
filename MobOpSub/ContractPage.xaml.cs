using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MobOpSub
{
    /// <summary>
    /// Логика взаимодействия для ContractPage.xaml
    /// </summary>
    public partial class ContractPage : Page
    {
        string command = "SELECT Contract.*, ФИО" +
            " FROM Contract LEFT JOIN clients" +
            " on Contract.[Ид клиента] = clients.[Ид]";
        ConnectionSQL connection = new ConnectionSQL();
        public ContractPage()
        {
            InitializeComponent();
            client.ItemsSource = connection.GetDataFromBase().AsDataView();
            client.DisplayMemberPath = "ФИО";
            client.SelectedValuePath = "Ид";
            Manager.contract_dataGrid = contract_data;
            Manager.command_contract = command;
            connection.ShowSQL_Data(contract_data, command);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string date = dateContract.SelectedDate.Value.ToString("yyyy/MM/dd");
            string commandText = $"INSERT INTO Contract" +
                $" VALUES('{date}', N'{phone.Text}', N'{tariff.Text}', {client.SelectedValue})";
            try
            {
                connection.OpenConnect();
                SqlCommand command = new SqlCommand(commandText, connection.GetConnect());
                command.ExecuteNonQuery();
                connection.CloseConnect();
                connection.UpdateGrid(contract_data, Manager.command_contract);
                MessageBox.Show("Контракт добавлен");
            }
            catch (Exception)
            {
                MessageBox.Show("Произошла ошибка");
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string commandText = $"SELECT * FROM Contract WHERE ";
            if (phone.Text != null || phone.Text != "")
            {
                commandText += $"[Телефонный номер] LIKE N'%{phone.Text}%'";
            }
            if (tariff.Text != null || tariff.Text != "")
            {
                commandText += $" and Тариф LIKE N'%{tariff.Text}%'";
            }
            if (client.SelectedIndex != -1 || client.Text != "")
            {
                commandText += $" and [Ид клиента] = {client.Text}";
            }
                connection.UpdateGrid(Manager.contract_dataGrid, commandText);
            
            
        }

        private void contract_data_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int _id = (int)((DataRowView)contract_data.SelectedItems[0]).Row["Номер договора"];
                Manager.MainFrame.Navigate(new contractInfo(_id));
            }
            catch (Exception)
            {
                MessageBox.Show("Выберите правильный контракт");
            }
            
        }
    }
}
