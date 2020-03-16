using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace lab1
{
    public partial class UpdateWindowUnit : Form
    {
        public UpdateWindowUnit()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var values = new List<string>();
                values.Add(textBox1.Text);
                Broker.table.Update(Broker.vals, values);
                Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void UpdateWindowUnit_Load(object sender, EventArgs e)
        {
            textBox1.Text = Broker.vals[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
