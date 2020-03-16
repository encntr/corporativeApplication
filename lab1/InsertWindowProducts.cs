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
    public partial class InsertWindowProducts : Form
    {
        private Dictionary<string, int> units;

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
            
            units = ((Products)Broker.table).units;
            foreach (string s in units.Keys) comboBox1.Items.Add(s);            
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
                if (comboBox1.SelectedItem == null) throw new Exception("Выберите единицу измерения");
                List<string> vals = new List<string>();
                vals.Add(textBox1.Text);
                vals.Add(comboBox1.SelectedItem.ToString());
                vals.Add(textBox3.Text);
                Broker.table.Add(vals);
                Close();
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
    }
}
