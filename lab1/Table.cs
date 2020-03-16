using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace lab1
{
    abstract class Table
    {        
        public DataTable FrontDt { get; set; }
        public DataTable BackDt { get; set; }
        public SqlDataAdapter FrontAdp { get; set; }
        public SqlDataAdapter BackAdp { get; set; }
        public List<Table> DepTables = new List<Table>();

        public abstract void Add(List<string> values);

        public abstract void Delete(List<string> values);

        public abstract void Update(List<string> oldValues, List<string> newValues);       

        public abstract void Prepare();

        public void Execute()
        {
            Connection.Con.Open();
            var sqlCommandBuilder = new SqlCommandBuilder(BackAdp);
            BackAdp.Update(BackDt);
            Refresh();
            Connection.Con.Close();
        }

        public void Refresh()
        {
            bool isOpen = false;
            if (Connection.Con.State == ConnectionState.Closed)
            {
                Connection.Con.Open();
                isOpen = true;
            }
            BackDt.Clear();
            BackAdp.Fill(BackDt);

            FrontDt.Clear();
            FrontAdp.Fill(FrontDt);

            foreach (Table T in DepTables)
                T.Refresh();
            if (isOpen)
                Connection.Con.Close();
        }
    }
}
