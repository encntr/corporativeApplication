using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.ConnectionUI;
using System.Data.SqlClient;

namespace lab1
{
    static class Manager
    {
        public static Table CurrentTable { get; set; }
        private static int CurrentInd { get; set; }

        public static List<Table> Tables = new List<Table>();
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

            Tables.Add(new Types());        InsertWindows.Add(new InsertWindowTypes());       UpdateWindows.Add(new UpdateWindowType());
            Tables.Add(new Units());        InsertWindows.Add(new InsertWindowUnits());       UpdateWindows.Add(new UpdateWindowUnit());
            Tables.Add(new Products());     InsertWindows.Add(new InsertWindowProducts());    UpdateWindows.Add(new UpdateWindowProduct());
            Tables.Add(new Dishes());       InsertWindows.Add(new InsertWindowDishs());       UpdateWindows.Add(new UpdateWindowDish());
            Tables.Add(new Calculations()); InsertWindows.Add(new InsertWindowCalculation()); UpdateWindows.Add(new UpdateWindowCalculation());
            Tables.Add(new Report());       InsertWindows.Add(new InsertWindowReport());      UpdateWindows.Add(new UpdateWindowReport());
            SetCurrentTable(0);
            CurrentInd = 0;

            Tables[1].DepTables.Add(Tables[2]);
            Tables[0].DepTables.Add(Tables[3]);
            Tables[2].DepTables.Add(Tables[4]);
            Tables[3].DepTables.Add(Tables[4]);
            Tables[3].DepTables.Add(Tables[5]);
        }
        
        public static void SetCurrentTable(int index)
        {
            CurrentTable = Tables[index];
            CurrentInd = index;
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
                if (Broker.f)
                {
                    CurrentTable.Prepare();
                    CurrentTable.Delete(vals);
                    Broker.f = false;
                }
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        public static void UpdateCurrentTable(List<string> oldValues)
        {
            if (oldValues.Count == 0)
                return;

            Broker.vals = oldValues;
            Broker.table = CurrentTable;
            CurrentTable.Prepare();
            UpdateWindows[CurrentInd].StartPosition = FormStartPosition.CenterParent;
            UpdateWindows[CurrentInd].ShowDialog();                       
        }

        public static void InsertIntoCurrentTable()
        {
            InsertWindows[CurrentInd].StartPosition = FormStartPosition.CenterParent;
            Broker.table = CurrentTable;
            CurrentTable.Prepare();
            InsertWindows[CurrentInd].ShowDialog();
            //if (Broker.f) {  currentTable.Add(Broker.vals); Broker.f = false; }
        }

        public static string GetConnectionString(string supposedValue)
        {
            var connectionDialog = new DataConnectionDialog { TopMost = true };

            connectionDialog.DataSources.Add(DataSource.SqlDataSource);
            connectionDialog.SelectedDataSource = DataSource.SqlDataSource;
            connectionDialog.SelectedDataProvider = DataProvider.SqlDataProvider;

            var connectionBuilder = new SqlConnectionStringBuilder(supposedValue ?? string.Empty)
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
            var dts = ((Report)Tables[5]).GenerateReport(out sum, out sum1);
            
            var reportWindow = new ReportWindow(dts, sum, sum1);
            reportWindow.StartPosition = FormStartPosition.CenterParent;
            reportWindow.ShowDialog();
        }
    }
}
