using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Controls;

namespace MobOpSub
{
    class ConnectionSQL
    {
        SqlConnection connection = new SqlConnection(
            ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        public SqlConnection GetConnect()
        {
            return connection;
        }
        public void OpenConnect()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }
        public void CloseConnect()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
        public void ShowSQL_Data(DataGrid dg, string commandText)
        {
            using(SqlCommand command = new SqlCommand(commandText, connection))
            {
                DataTable dt = new DataTable();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dt);
                dg.ItemsSource = dt.AsDataView();
            }
        }
        public void UpdateGrid(DataGrid grid, string command)
        {
            OpenConnect();
            grid.ItemsSource = null;
            ShowSQL_Data(grid, command);
            CloseConnect();
        }
    }
}
