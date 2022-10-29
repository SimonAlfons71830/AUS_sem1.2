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

namespace Hospital_information_sytem.forms
{
    public partial class ZadajRC : Form
    {
        public Informacny_system informacny_system;
        public String nemocnicaVKtorejHladamPacienta;
        public ZadajRC(Informacny_system informacny_system, String nazov_nemocnice)
        {
            InitializeComponent();
            this.informacny_system = informacny_system;
            this.nemocnicaVKtorejHladamPacienta = nazov_nemocnice;
        }

        private void Form7_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = textBox1.Enabled;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var nemocnica = this.informacny_system.NajdiNemocnicu(nemocnicaVKtorejHladamPacienta);
            if (nemocnica.NajdiPacient(textBox1.Text) == null) 
            {
                MessageBox.Show("Neexistuje pacient s takymto rodnym cislom.");
            }
            else
            {
                var udajeopacientovi = new UdajeOPacientovi(informacny_system, nemocnicaVKtorejHladamPacienta, textBox1.Text);
                udajeopacientovi.ShowDialog();
                this.Close();
            }
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
