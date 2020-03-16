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
    public partial class UpdateWindowCalculation : Form
    {
        Dictionary<string, int> products = new Dictionary<string, int>();
        Dictionary<string, int> dishs = new Dictionary<string, int>();
        public UpdateWindowCalculation()
        {
            InitializeComponent();
        }

        private void UpdateWindowCalculation_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            textBox3.Text = Broker.vals[2];
            products = ((Calculations)Broker.table).products;
            foreach (string s in products.Keys) comboBox1.Items.Add(s);
            dishs = ((Calculations)Broker.table).dishs;
            foreach (string s in dishs.Keys) comboBox2.Items.Add(s);
            comboBox1.SelectedItem = Broker.vals[1];
            comboBox2.SelectedItem = Broker.vals[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> vals = new List<string>();
                vals.Add(comboBox2.SelectedItem.ToString());
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

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
