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
    public partial class ReportWindow : Form
    {
        List<DataTable> dts = new List<DataTable>();
        string sum;
        string sum1;
        public ReportWindow()
        {
            InitializeComponent();
        }

        public ReportWindow(List<DataTable> dts, string sum, string sum1)
        {
            InitializeComponent();
            this.sum = sum;
            this.sum1 = sum1;
            this.dts = dts;
            dataGridView1.DataSource = dts[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            DataGridView dgw = (DataGridView)sender;
            int d = dgw.Width / dgw.ColumnCount;
            for (int i = 0; i < dgw.ColumnCount; i++) dgw.Columns[i].Width = d;
        }

        private void ReportWindow_Load(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dts[0];
            textBox1.Text = "Итог: " + sum;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dts[1];
            textBox1.Text = "Итог:  " + sum1;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
