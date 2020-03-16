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

    public partial class UpdateWindowReport : Form
    {
        Dictionary<string, int> dishs = new Dictionary<string, int>();
        public UpdateWindowReport()
        {
            InitializeComponent();
        }

        private void UpdateWindowReport_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();            
            textBox3.Text = Broker.vals[1];
            dishs = ((Report)Broker.table).dishs;
            foreach (string s in dishs.Keys) comboBox1.Items.Add(s);
            comboBox1.SelectedItem = Broker.vals[0];            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> vals = new List<string>();               
                vals.Add(comboBox1.SelectedItem.ToString());
                vals.Add(textBox3.Text);
                Broker.table.Update(Broker.vals, vals);
                Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
    }
}
