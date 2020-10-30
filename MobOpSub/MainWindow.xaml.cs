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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MobOpSub
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ConnectionSQL connection = new ConnectionSQL();
        ClientsPage clientsPage = new ClientsPage();
        ContractPage contractPage = new ContractPage();
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(clientsPage);
            Manager.MainFrame = MainFrame;
            section.Text = clientsPage.Title;
        }
        private void GoClient_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(clientsPage);
            Manager.MainFrame = MainFrame;
            connection.UpdateGrid(Manager.client_dataGrid, Manager.command_client);
            section.Text = clientsPage.Title;
        }
        private void GoContract_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(contractPage);
            Manager.MainFrame = MainFrame;
            connection.UpdateGrid(Manager.contract_dataGrid, Manager.command_contract);
            section.Text = contractPage.Title;
        }
    }
}
