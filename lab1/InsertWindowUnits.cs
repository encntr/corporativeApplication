using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                List<string> vals = new List<string>();
                vals.Add(textBox1.Text);
                Broker.table.Add(vals);
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
