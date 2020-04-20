using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace File_Manager
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
       public string newName;
        public void button1_Click(object sender, EventArgs e)
        {
            newName = textBox1.Text;
            
            Close();
        }
    
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    
    }
}
