using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hospital_information_sytem
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //priezvisko 
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //meno pacienta
            
              
            
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //rodne cislo
            
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //datum narodenia
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //??
            button1.Enabled = textBox1.Enabled && textBox2.Enabled && textBox3.Enabled && comboBox1.Enabled && dateTimePicker1.Enabled;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            /*comboBox1.Items.Add("VZP");
            comboBox1.Items.Add("DÔVERA");
            comboBox1.Items.Add("UNION");*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            //pridaj pacienta do databazy
            DialogResult dr = MessageBox.Show("Chcete ulozit pacienta?", "Ano", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                this.Close();
            }
            
        }
    }
}
