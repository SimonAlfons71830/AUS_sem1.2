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
    public partial class Form3 : Form
    {
        public Informacny_system informacny_system;
        public Form3(Informacny_system informacny_system)
        {
            InitializeComponent();
            this.informacny_system = informacny_system;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String rod_cislo = textBox1.Text;
            DateTime dat_od = dateTimePicker1.Value;
            String id_hospitalizacie = dat_od.Day.ToString() + dat_od.Month.ToString() + dat_od.Year.ToString() + rod_cislo; //91220220055114312
            String nazov_diagnozy = comboBox1.Text;
            Nemocnica nemocnica = this.informacny_system.NajdiNemocnicu(comboBox2.Text);
            Pacient pacient = nemocnica.NajdiPacient(rod_cislo);
            
            


            DialogResult dr = MessageBox.Show("Začať hospitalizáciu?", "Ano", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                if (rod_cislo != String.Empty && id_hospitalizacie != string.Empty && dat_od != null)
                {
                    if (nemocnica != null && pacient != null)
                    {
                        var pom = nemocnica.PridajHospitalizaciu(id_hospitalizacie, rod_cislo, dat_od, nazov_diagnozy);
                        var pomPac = pacient.PridajHospitalizaciuPacientovi(id_hospitalizacie, rod_cislo, dat_od, nazov_diagnozy);
                        if (pom && pomPac)
                        {
                            MessageBox.Show("Hospitalizacia bola zaevidovana.");
                        }
                        else
                        {
                            MessageBox.Show("CHYBA ... Hospitalizacia nebola zaevidovana.");
                        }
                    }
                    else {
                        MessageBox.Show("Nemocnica alebo pacient neexistuje.");
                    }

                }
                this.Close();
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            List<informacny_system.Nemocnica> nemocnice = this.informacny_system.VratListNemocnic();

            for (int i = 0; i < nemocnice.Count; i++)
            {
                comboBox2.Items.Add(nemocnice[i].nazov_nemocnice);
            }
            Hospitalizacia hosp = new Hospitalizacia();
            List<string> hospitalizacie = hosp.VratListDiagnoz();
            for (int i = 0; i < hospitalizacie.Count; i++)
            {
                comboBox1.Items.Add(hospitalizacie[i]);
            }

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
