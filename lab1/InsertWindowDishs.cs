using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace lab1
{
    public partial class InsertWindowDishs : Form
    {
        private Dictionary<string, int> Types { get; set; }

        public InsertWindowDishs()
        {
            InitializeComponent();
        }

        private void InsertWindowDishs_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedItem = null;
            textBox1.Text = "";
            textBox3.Text = "";
            comboBox1.Items.Clear();
            Types = ((Dishes)Broker.table).Types;

            foreach (var s in Types.Keys)
                comboBox1.Items.Add(s);            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedItem == null)
                    throw new Exception("Выберите тип блюда");

                var values = new List<string>();
                values.Add(textBox1.Text);
                values.Add(comboBox1.SelectedItem.ToString());
                values.Add(textBox3.Text);
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
    }
}
