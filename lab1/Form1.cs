using System;
using System.Collections.Generic;
using System.Windows.Forms;

//DESKTOP-G8JABSB

namespace lab1
{
    public partial class MainForm : Form
    {     
        public MainForm()
        {
            InitializeComponent();
            Manager.Load();
            dataGridView1.DataSource = Manager.CurrentTable.FrontDt;
        }        

        private void buttonExit_Click(object sender, EventArgs e)
        {            
            Close();
        }

        private void radioButtonDishType_CheckedChanged(object sender, EventArgs e)
        {
            Manager.SetCurrentTable(0);
            dataGridView1.DataSource = Manager.CurrentTable.FrontDt;
        }

        private void radioButtonDishs_CheckedChanged(object sender, EventArgs e)
        {
            Manager.SetCurrentTable(3);
            dataGridView1.DataSource = Manager.CurrentTable.FrontDt;
        }

        private void radioButtonEdIzm_CheckedChanged(object sender, EventArgs e)
        {
            Manager.SetCurrentTable(1);
            dataGridView1.DataSource = Manager.CurrentTable.FrontDt;
        }

        private void radioButtonProducts_CheckedChanged(object sender, EventArgs e)
        {
            Manager.SetCurrentTable(2);
            dataGridView1.DataSource = Manager.CurrentTable.FrontDt;
        }

        private void radioButtonCalculation_CheckedChanged(object sender, EventArgs e)
        {
            Manager.SetCurrentTable(4);
            dataGridView1.DataSource = Manager.CurrentTable.FrontDt;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {            
            Manager.InsertIntoCurrentTable();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            var values = GetCurrentData();
            Manager.UpdateCurrentTable(values);
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            var values = GetCurrentData();
            Manager.DeleteFromCurrentTable(values);
        }

        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {            
            var dgw = (DataGridView)sender;
            int d = 750 / dgw.ColumnCount;
            for (int i = 0; i < dgw.ColumnCount; i++)
                dgw.Columns[i].Width = d;
        }

        private List<string> GetCurrentData()
        {
            var ans = new List<string>();
            if (dataGridView1.CurrentRow == null)
                return ans;

            for (int i = 0; i < dataGridView1.ColumnCount; i++)
                ans.Add(dataGridView1[i, dataGridView1.CurrentRow.Index].Value.ToString());
            return ans;
        }

        private void radioButtonReport_CheckedChanged(object sender, EventArgs e)
        {
            buttonReport.Enabled = !buttonReport.Enabled;
            Manager.SetCurrentTable(5);
            dataGridView1.DataSource = Manager.CurrentTable.FrontDt;
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            Manager.ShowReport();
        }
    }
}
