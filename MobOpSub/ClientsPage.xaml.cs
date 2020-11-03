using Microsoft.Win32;
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
    /// Логика взаимодействия для ClientsPage.xaml
    /// </summary>
    public partial class ClientsPage : Page
    {
        DataTable dt = new DataTable();
        ConnectionSQL connection = new ConnectionSQL();
        string showData = "SELECT Ид, ФИО, [Дата рождения], Адрес, [Номер договора], [Тариф]" +
            " FROM clients LEFT JOIN Contract on clients.Ид = Contract.[Ид клиента]";
        private string _name;
        private string _date;
        private string _address;
        private string _avatar;
        private int n_contract;
        public ClientsPage()
        {
            InitializeComponent();
            connection.OpenConnect();
            connection.ShowSQL_Data(client_data, showData);
            Manager.client_dataGrid = client_data;
            Manager.command_client = showData;
            connection.CloseConnect();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _name = client_name.Text;
                _date = date.SelectedDate.Value.ToString("yyyy/MM/dd");
                _address = address.Text;
                _avatar = picture.Text;
                string commandText = "INSERT INTO clients" +
                    $" VALUES(N'{_name}', '{_date}', N'{_address}', N'{_avatar}')";
                connection.OpenConnect();
                SqlCommand command = new SqlCommand(commandText, connection.GetConnect());
                command.ExecuteNonQuery();
                connection.CloseConnect();
                connection.UpdateGrid(Manager.client_dataGrid, Manager.command_client);
                MessageBox.Show("Клиент добавлен");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка. \n Сообщение:" + ex.ToString());
            }   
        }

        private void SelectImage_Click(object sender, RoutedEventArgs e)
        {
            var d = new OpenFileDialog();
            if (d.ShowDialog().GetValueOrDefault(false))
            {
                _avatar = d.FileName;
                picture.Text = _avatar;
            }
        }

        private void client_data_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int _id = (int)((DataRowView)client_data.SelectedItems[0]).Row["Ид"];
                Manager.MainFrame.Navigate(new ClientInfo(_id));
            }
            catch (Exception)
            {
                MessageBox.Show("Выберите правильного клиента");
                throw;
            }

        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string commandText = $"SELECT * FROM clients WHERE ";
            if (client_name.Text != null || client_name.Text != "")
            {
                commandText += $"ФИО LIKE N'%{client_name.Text}%'";
            }
            if (address.Text != null || address.Text != "")
            {
                commandText += $" and Адрес LIKE N'%{address.Text}%'";
            }
            connection.UpdateGrid(Manager.client_dataGrid, commandText);
        }
    }
}
