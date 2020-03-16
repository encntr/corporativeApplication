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
    public partial class UpdateWindowProduct : Form
    {
        private Dictionary<string, int> units;
        public UpdateWindowProduct()
        {
            InitializeComponent();
        }

        private void UpdateWindowProduct_Load(object sender, EventArgs e)
        {
            textBox1.Text = Broker.vals[0];
            textBox3.Text = Broker.vals[2];
            comboBox1.Items.Clear();
            units = ((Products)Broker.table).units;
            foreach (string s in units.Keys) comboBox1.Items.Add(s);
            comboBox1.SelectedItem = Broker.vals[1];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> vals = new List<string>();
                vals.Add(textBox1.Text);
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
