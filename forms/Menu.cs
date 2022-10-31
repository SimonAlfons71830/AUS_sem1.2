using Hospital_information_sytem.forms;
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
    public partial class Menu : Form
    {
        protected Informacny_system inf_system;
        public List<Hospitalizacia> aktivneHospitalizacie;
        internal Menu(Informacny_system system)
        {
            this.inf_system = system;
            InitializeComponent();
            //this.aktivneHospitalizacie = inf_system;
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToLongDateString();
            label2.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //button na zacatie hospitalizacie
            var novaHospitalizacia = new ZacniHosp(inf_system);
            novaHospitalizacia.ShowDialog();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var koniecHospitalizacie = new UkonciHosp(inf_system);
            koniecHospitalizacie.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //pridaj pacienta

            var pridajPacienta = new PridajPacienta(inf_system);
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
            var pridajNemocnicu = new NovaNemocnica(inf_system);
            pridajNemocnicu.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var zaznamzOpacientovi = new ZvolNemocnicu(inf_system);
            zaznamzOpacientovi.ShowDialog();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            var zoznamNemocnic = new Nemocnice(inf_system);
            zoznamNemocnic.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var zaznamOPacientoch = new ZvolNemocnicuAMenoP(inf_system);
            zaznamOPacientoch.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var zrusNemocnicu = new ZrusNemocnicu(inf_system);
            zrusNemocnicu.ShowDialog();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var aktualneHops = new AktualneHospitalizovani1(this.inf_system);
            aktualneHops.ShowDialog();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            var zvolHospOdDo = new ZvolOdDoANemocnicu(this.inf_system);
            zvolHospOdDo.ShowDialog();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            this.inf_system.Oprimalizuj();
            MessageBox.Show("Optimalizacia dokoncena.");
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            var zvolMesiac = new ZvolMesiacARokNaFakturu(this.inf_system);
            zvolMesiac.ShowDialog();
        }
    }


    
}

