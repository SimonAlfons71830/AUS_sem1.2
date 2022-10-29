using Hospital_information_sytem.informacny_system;
using Hospital_information_sytem.structures;
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
    public partial class NovaNemocnica : Form
    {
        public Informacny_system informacny_system;
        public NovaNemocnica(Informacny_system system)
        {
            informacny_system = system;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            String nazov;
            nazov = textBox1.Text;
            

            DialogResult dr = MessageBox.Show("Pridať nemocnicu?", "Ano", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                if (informacny_system.PridajNemocnicu(nazov))
                {
                    MessageBox.Show("Nemocnica bola pridana.");
                    this.Close();
                }
                else { MessageBox.Show("Nepodarilo sa pridat nemocnicu."); }       
            }
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = textBox1.Enabled;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
