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

//DESKTOP-G8JABSB

namespace lab1
{
    public partial class MainForm : Form
    {     
        public MainForm()
        {
            InitializeComponent();
            Manager.Load();
            dataGridView1.DataSource = Manager.currentTable.frontDt;
        }        

        private void buttonExit_Click(object sender, EventArgs e)
        {            
            Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
         
        }

        private void radioButtonDishType_CheckedChanged(object sender, EventArgs e)
        {
            Manager.setCurrentTable(0);
            dataGridView1.DataSource = Manager.currentTable.frontDt;
        }

        private void Table_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void radioButtonDishs_CheckedChanged(object sender, EventArgs e)
        {
            Manager.setCurrentTable(3);
            dataGridView1.DataSource = Manager.currentTable.frontDt;
        }

        private void radioButtonEdIzm_CheckedChanged(object sender, EventArgs e)
        {
            Manager.setCurrentTable(1);
            dataGridView1.DataSource = Manager.currentTable.frontDt;
        }

        private void radioButtonProducts_CheckedChanged(object sender, EventArgs e)
        {
            Manager.setCurrentTable(2);
            dataGridView1.DataSource = Manager.currentTable.frontDt;
        }

        private void radioButtonCalculation_CheckedChanged(object sender, EventArgs e)
        {
            Manager.setCurrentTable(4);
            dataGridView1.DataSource = Manager.currentTable.frontDt;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {            
            Manager.InsertIntoCurrentTable();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            List<string> vals = getCurrentData();
            Manager.UpdateCurrentTable(vals);
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            List<string> vals = getCurrentData();
            Manager.DeleteFromCurrentTable(vals);
        }

        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {            
            DataGridView dgw = (DataGridView)sender;
            int d = 750 / dgw.ColumnCount;
            for (int i = 0; i < dgw.ColumnCount; i++) dgw.Columns[i].Width = d;
        }

        private List<string> getCurrentData()
        {
            List<string> ans = new List<string>();
            if (dataGridView1.CurrentRow == null) return ans;
            for (int i = 0; i < dataGridView1.ColumnCount; i++) ans.Add(dataGridView1[i, dataGridView1.CurrentRow.Index].Value.ToString());
            return ans;
        }

        private void radioButtonReport_Leave(object sender, EventArgs e)
        {
            //buttonReport.Enabled = false;
        }

        private void radioButtonReport_CheckedChanged(object sender, EventArgs e)
        {
            buttonReport.Enabled = !buttonReport.Enabled;
            Manager.setCurrentTable(5);
            dataGridView1.DataSource = Manager.currentTable.frontDt;
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            Manager.ShowReport();
        }
    }
}
