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
    public partial class DeleteWindow : Form
    {
        public DeleteWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Broker.f = true;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Broker.f = false;
            Close();
        }
    }
}
