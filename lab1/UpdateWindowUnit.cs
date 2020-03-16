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
                List<string> vals = new List<string>();
                vals.Add(textBox1.Text);
                Broker.table.Update(Broker.vals, vals);
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
