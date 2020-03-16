using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace lab1
{
    public partial class InsertWindowProducts : Form
    {
        private Dictionary<string, int> Units { get; set; }

        public InsertWindowProducts()
        {
            InitializeComponent();
        }

        private void InsertWindowProducts_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedItem = null;
            textBox1.Text = "";
            textBox3.Text = "";
            comboBox1.Items.Clear();
            
            Units = ((Products)Broker.table).Units;
            foreach (var s in Units.Keys)
                comboBox1.Items.Add(s);            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Broker.f = false;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedItem == null)
                    throw new Exception("Выберите единицу измерения");

                var values = new List<string>();
                values.Add(textBox1.Text);
                values.Add(comboBox1.SelectedItem.ToString());
                values.Add(textBox3.Text);
                Broker.table.Add(values);
                Close();
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
    }
}
