using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace lab1
{
    public partial class InsertWindowReport : Form
    {
        private Dictionary<string, int> _dishs = new Dictionary<string, int>();
        public InsertWindowReport()
        {
            InitializeComponent();
        }

        private void InsertWindowReport_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedItem = null;
            comboBox1.Items.Clear();            
            textBox3.Text = "";            
            _dishs = ((Report)Broker.table).Dishs;
            foreach (var s in _dishs.Keys)
                comboBox1.Items.Add(s);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedItem == null)
                    throw new Exception("Выберите блюдо");

                var values = new List<string>
                {
                    comboBox1.SelectedItem.ToString(), textBox3.Text
                };

                Broker.table.Add(values);
                Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
    }
}
