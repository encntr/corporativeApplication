using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace lab1
{
    public partial class UpdateWindowDish : Form
    {
        private Dictionary<string, int> _types;

        public UpdateWindowDish()
        {
            InitializeComponent();
        }

        private void UpdateWindowDish_Load(object sender, EventArgs e)
        {
            textBox1.Text = Broker.vals[0];
            textBox3.Text = Broker.vals[2];
            comboBox1.Items.Clear();
            _types = ((Dishes)Broker.table).Types;

            foreach (string s in _types.Keys)
                comboBox1.Items.Add(s);
            comboBox1.SelectedItem = Broker.vals[1];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var values = new List<string>();
                values.Add(textBox1.Text);
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
