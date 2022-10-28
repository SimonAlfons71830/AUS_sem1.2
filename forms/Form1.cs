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
    public partial class Form1 : Form
    {
        protected Informacny_system inf_system;
        internal Form1(Informacny_system system)
        {
            this.inf_system = system;
            InitializeComponent();
            
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //button na zacatie hospitalizacie
            var novaHospitalizacia = new Form3();
            novaHospitalizacia.ShowDialog();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var koniecHospitalizacie = new Form4();
            koniecHospitalizacie.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //pridaj pacienta

            var pridajPacienta = new Form2(inf_system);
            pridajPacienta.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Ukončiť program?", "Ano", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var pridajNemocnicu = new Form5(inf_system);
            pridajNemocnicu.ShowDialog();
        }
    }


    
}

