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
    /// Логика взаимодействия для contractInfo.xaml
    /// </summary>
    public partial class contractInfo : Page
    {
        ConnectionSQL connection = new ConnectionSQL();
        string[] fields = new string[3] {"Дата", "Телефонный номер", "Тариф"};
        string[] data = new string[3];
        public int id;
        public contractInfo(int _id)
        {
            InitializeComponent();
            id = _id;
            client.ItemsSource = connection.GetDataFromBase().AsDataView();
            client.DisplayMemberPath = "Ид";
            client.SelectedValuePath = "Ид";
            for (int i = 0; i < fields.Length; i++)
            {
                string showContract_data = $"SELECT [{fields[i]}] FROM Contract WHERE [Номер договора] = {id}";
                connection.OpenConnect();
                SqlCommand command = new SqlCommand(showContract_data, connection.GetConnect());
                data[i] = command.ExecuteScalar().ToString();
            }
            try
            {
                dateContract.SelectedDate = Convert.ToDateTime(data[0]);
            }
            catch (Exception)
            {
                dateContract.SelectedDate = null;
            }       
            phone.Text = data[1];
            tariff.Text = data[2];
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            string date = dateContract.SelectedDate.Value.ToString("yyyy/MM/dd");
            string commandText = $"UPDATE Contract SET [Дата] = '{date}'" +
                $", [Телефонный номер] = N'{phone.Text}'" +
                $", [Тариф] = N'{tariff.Text}'" +
                $", [Ид клиента] = {client.Text}" +
                $" WHERE [Номер договора] = {id}";
            try
            {
                connection.OpenConnect();
                SqlCommand command = new SqlCommand(commandText, connection.GetConnect());
                command.ExecuteNonQuery();
                connection.CloseConnect();
                MessageBox.Show("Данные обновлены");

            }
            catch (Exception)
            {
                MessageBox.Show("Произошла ошибка");
            }
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            string commandText = $"DELETE FROM Contract WHERE [Номер договора] = {id}";
            try
            {
                connection.OpenConnect();
                SqlCommand command = new SqlCommand(commandText, connection.GetConnect());
                command.ExecuteNonQuery();
                connection.CloseConnect();
                MessageBox.Show("Контракт удален");
            }
            catch (Exception)
            {
                MessageBox.Show("Произошла ошибка");
            }
        }
    }
}
