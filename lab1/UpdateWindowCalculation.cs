using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace lab1
{
    public partial class UpdateWindowCalculation : Form
    {
        private Dictionary<string, int> _products = new Dictionary<string, int>();
        private Dictionary<string, int> _dishs = new Dictionary<string, int>();

        public UpdateWindowCalculation()
        {
            InitializeComponent();
        }

        private void UpdateWindowCalculation_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            textBox3.Text = Broker.vals[2];
            _products = ((Calculations)Broker.table).Products;

            foreach (string s in _products.Keys)
                comboBox1.Items.Add(s);

            _dishs = ((Calculations)Broker.table).Dishs;

            foreach (string s in _dishs.Keys)
                comboBox2.Items.Add(s);

            comboBox1.SelectedItem = Broker.vals[1];
            comboBox2.SelectedItem = Broker.vals[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var values = new List<string>();
                values.Add(comboBox2.SelectedItem.ToString());
                values.Add(comboBox1.SelectedItem.ToString());
                values.Add(textBox3.Text);
                Broker.table.Update(Broker.vals, values);
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
