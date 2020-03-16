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
    public partial class InsertWindowDishs : Form
    {
        private Dictionary<string, int> types;

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
            types = ((Dishes)Broker.table).types;
            foreach (string s in types.Keys) comboBox1.Items.Add(s);            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedItem == null) throw new Exception("Выберите тип блюда");
                List<string> vals = new List<string>();
                vals.Add(textBox1.Text);
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

        private void button1_Click(object sender, EventArgs e)
        {
            //Broker.f = false;
            Close();
        }
    }
}
