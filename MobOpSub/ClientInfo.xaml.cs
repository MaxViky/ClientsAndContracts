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
    /// Логика взаимодействия для ClientInfo.xaml
    /// </summary>
    public partial class ClientInfo : Page
    {
        ConnectionSQL connection = new ConnectionSQL();
        private int _id;
        private string _avatar;
        string[] fields = new string[5] { "ФИО", "Дата рождения", "Адрес", "Аватар", "Номер договора" };
        string[] data = new string[5];
        public ClientInfo(int id)
        {
            InitializeComponent();
            _id = id;
            for (int i = 0; i < fields.Length; i++)
            {
                string showClient_data = $"SELECT [{fields[i]}] FROM clients WHERE Ид = {id}";
                connection.OpenConnect();
                SqlCommand command = new SqlCommand(showClient_data, connection.GetConnect());
                data[i] = command.ExecuteScalar().ToString();

            }
            connection.CloseConnect();
            client_name.Text = data[0];
            date.SelectedDate = Convert.ToDateTime(data[1]);
            address.Text = data[2];
            _avatar = data[3];
            picture.Text = _avatar;
            if (_avatar != "")
            {
                try
                {
                    BitmapImage bitmap = new BitmapImage(new Uri(_avatar));
                    avatar.Source = bitmap;
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка загрузки изображения");
                }
            }
            contract_n.Text = data[4];
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
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            string _date = date.SelectedDate.Value.ToString("yyyy/MM/dd");
            if (contract_n.Text == null || contract_n.Text == "")
            {
                contract_n.Text = 0.ToString();
            }
            string commandText = $"UPDATE clients SET [ФИО] = N'{client_name.Text}'" +
                $", [Дата рождения] = '{_date}'" +
                $", [Адрес] = N'{address.Text}'" +
                $", [Аватар] = N'{picture.Text}'" +
                $", [Номер договора] = {contract_n.Text}" +
                $" WHERE Ид = {_id}";
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
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            string commandText = $"DELETE FROM clients WHERE Ид = {_id}";
            try
            {
                connection.OpenConnect();
                SqlCommand command = new SqlCommand(commandText, connection.GetConnect());
                command.ExecuteNonQuery();
                connection.CloseConnect();
                MessageBox.Show("Клиент удален");
            }
            catch (Exception)
            {
                MessageBox.Show("Произошла ошибка");
            }
        }
    }
}
