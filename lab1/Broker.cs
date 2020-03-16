using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.ConnectionUI;
using System.Configuration;
using System.Data.SqlClient;

namespace lab1
{
    static class Broker
    {
        public static bool f = false;
        public static List<string> vals = new List<string>();
        //public static List<string> oldVals = new List<string>();
        public static Table table;        
    }
}
