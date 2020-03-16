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
    public partial class InsertWindowCalculation : Form
    {
        Dictionary<string, int> products = new Dictionary<string, int>();
        Dictionary<string, int> dishs = new Dictionary<string, int>();
        public InsertWindowCalculation()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedItem == null) throw new Exception("Выберите продукт");
                if (comboBox2.SelectedItem == null) throw new Exception("Выберите блюдо");
                List<string> vals = new List<string>();
                vals.Add(comboBox2.SelectedItem.ToString());
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

        private void InsertWindowCalculation_Load(object sender, EventArgs e)
        {
            //Point p1 = comboBox1.Location; Size s1 = comboBox1.Size;
            //Point p2 = comboBox2.Location; Size s2 = comboBox1.Size;
            //comboBox1 = new ComboBox(); comboBox1.Location = p1; comboBox1.Size = s1;
            //comboBox2 = new ComboBox(); comboBox2.Location = p2; comboBox1.Size = s2;
            //Controls.Add(comboBox1); Controls.Add(comboBox2);
            comboBox1.SelectedItem = null;
            comboBox2.SelectedItem = null;
            textBox3.Text = "";
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            products = ((Calculations)Broker.table).products;
            foreach (string s in products.Keys) comboBox1.Items.Add(s);
            dishs = ((Calculations)Broker.table).dishs;
            foreach (string s in dishs.Keys) comboBox2.Items.Add(s);
        }
    }
}
