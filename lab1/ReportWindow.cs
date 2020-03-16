using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace lab1
{
    public partial class ReportWindow : Form
    {
        private List<DataTable> _dts = new List<DataTable>();
        private string _sum;
        private string _sum1;

        public ReportWindow()
        {
            InitializeComponent();
        }

        public ReportWindow(List<DataTable> dts, string sum, string sum1)
        {
            InitializeComponent();
            _sum = sum;
            _sum1 = sum1;
            _dts = dts;
            dataGridView1.DataSource = dts[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            var dgw = (DataGridView)sender;
            int d = dgw.Width / dgw.ColumnCount;

            for (int i = 0; i < dgw.ColumnCount; i++)
                dgw.Columns[i].Width = d;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = _dts[0];
            textBox1.Text = "Итог: " + _sum;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = _dts[1];
            textBox1.Text = "Итог:  " + _sum1;
        }
    }
}
