using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace lab1
{
    public partial class InsertWindowUnits : Form
    {
        public InsertWindowUnits()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var values = new List<string>();
                values.Add(textBox1.Text);
                Broker.table.Add(values);
                Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Broker.f = false;
            Close();
        }

        private void InsertWindowUnits_Load(object sender, EventArgs e)
        {
            textBox1.Text = "";            
        }
    }
}
