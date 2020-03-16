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
    public partial class InsertWindowReport : Form
    {
        Dictionary<string, int> dishs = new Dictionary<string, int>();
        public InsertWindowReport()
        {
            InitializeComponent();
        }

        private void InsertWindowReport_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedItem = null;
            comboBox1.Items.Clear();            
            textBox3.Text = "";            
            dishs = ((Report)Broker.table).dishs;
            foreach (string s in dishs.Keys) comboBox1.Items.Add(s);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedItem == null) throw new Exception("Выберите блюдо");
                List<string> vals = new List<string>();
                vals.Add(comboBox1.SelectedItem.ToString());                
                vals.Add(textBox3.Text);
                Broker.table.Add(vals);
                Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
    }
}
