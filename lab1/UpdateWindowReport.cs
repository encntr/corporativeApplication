using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace lab1
{

    public partial class UpdateWindowReport : Form
    {
        private Dictionary<string, int> _dishs = new Dictionary<string, int>();

        public UpdateWindowReport()
        {
            InitializeComponent();
        }

        private void UpdateWindowReport_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();            
            textBox3.Text = Broker.vals[1];
            _dishs = ((Report)Broker.table).Dishs;

            foreach (string s in _dishs.Keys)
                comboBox1.Items.Add(s);
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
                var values = new List<string>();               
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
    }
}
