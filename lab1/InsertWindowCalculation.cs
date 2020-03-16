using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace lab1
{
    public partial class InsertWindowCalculation : Form
    {
        private Dictionary<string, int> Products { get; set; }
        private Dictionary<string, int> Dishs { get; set; }

        public InsertWindowCalculation()
        {
            InitializeComponent();
            Init();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedItem == null)
                    throw new Exception("Выберите продукт");
                if (comboBox2.SelectedItem == null)
                    throw new Exception("Выберите блюдо");

                var values = new List<string>();
                values.Add(comboBox2.SelectedItem.ToString());
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

            Products = ((Calculations)Broker.table).Products;

            foreach (var s in Products.Keys)
                comboBox1.Items.Add(s);
            Dishs = ((Calculations)Broker.table).Dishs;

            foreach (var s in Dishs.Keys)
                comboBox2.Items.Add(s);
        }

        private void Init()
        {
            Products = new Dictionary<string, int>();
            Dishs = new Dictionary<string, int>();
        }
    }
}
