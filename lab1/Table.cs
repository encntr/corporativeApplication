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
    abstract class Table
    {        
        public DataTable frontDt;
        public DataTable backDt;
        public SqlDataAdapter frontAdp;
        public SqlDataAdapter backAdp;        
        public List<Table> depTables = new List<Table>();        

        public abstract void Add(List<string> values);

        public abstract void Delete(List<string> values);

        public abstract void Update(List<string> oldValues, List<string> newValues);        

        public abstract void Prepare();

        public void Execute()
        {
            Connection.Con.Open();
            SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(backAdp);
            backAdp.Update(backDt);
            Refresh();
            Connection.Con.Close();
        }

        public void Refresh()
        {
            bool f = false;
            if(Connection.Con.State == ConnectionState.Closed) { Connection.Con.Open();  f = true; }
            backDt.Clear();
            backAdp.Fill(backDt);

            frontDt.Clear();
            frontAdp.Fill(frontDt);

            foreach (Table T in depTables) T.Refresh();
            if (f) Connection.Con.Close();
        }
    }
}
