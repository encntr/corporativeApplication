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
    static class Manager
    {
        public static Table currentTable;
        private static int currentInd;
        public static List<Table> tables = new List<Table>();
        private static List<Form> InsertWindows = new List<Form>();
        private static List<Form> UpdateWindows = new List<Form>();

        public static void Load()
        {
            string constr = GetConnectionString(""); ;
            while (constr == null)
            {
                MessageBox.Show("Ошибка.");
                constr = GetConnectionString("");
            }
           
            Connection.Con = new SqlConnection(constr);

            tables.Add(new Types());        InsertWindows.Add(new InsertWindowTypes());       UpdateWindows.Add(new UpdateWindowType());
            tables.Add(new Units());        InsertWindows.Add(new InsertWindowUnits());       UpdateWindows.Add(new UpdateWindowUnit());
            tables.Add(new Products());     InsertWindows.Add(new InsertWindowProducts());    UpdateWindows.Add(new UpdateWindowProduct());
            tables.Add(new Dishes());       InsertWindows.Add(new InsertWindowDishs());       UpdateWindows.Add(new UpdateWindowDish());
            tables.Add(new Calculations()); InsertWindows.Add(new InsertWindowCalculation()); UpdateWindows.Add(new UpdateWindowCalculation());
            tables.Add(new Report());       InsertWindows.Add(new InsertWindowReport());      UpdateWindows.Add(new UpdateWindowReport());
            setCurrentTable(0);
            currentInd = 0;

            tables[1].depTables.Add(tables[2]);
            tables[0].depTables.Add(tables[3]);
            tables[2].depTables.Add(tables[4]);
            tables[3].depTables.Add(tables[4]);
            tables[3].depTables.Add(tables[5]);
        }
        
        public static void setCurrentTable(int index)
        {
            currentTable = tables[index];
            currentInd = index;
        }

        public static void DeleteFromCurrentTable(List<string> vals)
        {
            if (vals.Count == 0) return;
            //Broker.table = currentTable;
            DeleteWindow dw = new DeleteWindow();
            dw.StartPosition = FormStartPosition.CenterParent;
            dw.ShowDialog();
            try
            {
                if (Broker.f) { currentTable.Prepare(); currentTable.Delete(vals); Broker.f = false; }
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        public static void UpdateCurrentTable(List<string> oldValues)
        {
            if (oldValues.Count == 0) return;
            Broker.vals = oldValues;
            Broker.table = currentTable;
            currentTable.Prepare();
            UpdateWindows[currentInd].StartPosition = FormStartPosition.CenterParent;
            UpdateWindows[currentInd].ShowDialog();                       
        }

        public static void InsertIntoCurrentTable()
        {
            InsertWindows[currentInd].StartPosition = FormStartPosition.CenterParent;
            Broker.table = currentTable;
            currentTable.Prepare();
            InsertWindows[currentInd].ShowDialog();
            //if (Broker.f) {  currentTable.Add(Broker.vals); Broker.f = false; }
        }

        public static string GetConnectionString(string supposedValue)
        {
            DataConnectionDialog connectionDialog =
               new DataConnectionDialog { TopMost = true };
            connectionDialog.DataSources.Add(DataSource.SqlDataSource);
            connectionDialog.SelectedDataSource = DataSource.SqlDataSource;
            connectionDialog.SelectedDataProvider = DataProvider.SqlDataProvider;

            SqlConnectionStringBuilder connectionBuilder =
               new SqlConnectionStringBuilder(supposedValue ?? string.Empty)
               { MultipleActiveResultSets = true };

            connectionDialog.ConnectionString = connectionBuilder.ToString();

            if (DataConnectionDialog.Show(connectionDialog) != DialogResult.OK)
                return null;

            return connectionDialog.ConnectionString;
        }

        public static void ShowReport()
        {
            string sum;
            string sum1;
            List<DataTable> dts = ((Report)tables[5]).GenerateReport(out sum, out sum1);
            
            ReportWindow w = new ReportWindow(dts, sum, sum1);
            w.StartPosition = FormStartPosition.CenterParent;
            w.ShowDialog();
        }
    }
}
