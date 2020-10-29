using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MobOpSub
{
    class Manager
    {
        public static Frame MainFrame { get; set; }
        public static DataGrid client_dataGrid{ get; set; }
        public static string command_client { get; set; }
        public static DataGrid contract_dataGrid { get; set; }
        public static string command_contract { get; set; }
    }
}
