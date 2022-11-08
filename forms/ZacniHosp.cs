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
    public partial class ZacniHosp : Form
    {
        public Informacny_system informacny_system;
        public ZacniHosp(Informacny_system informacny_system)
        {
            InitializeComponent();
            this.informacny_system = informacny_system;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String rok = dateTimePicker1.Value.Year.ToString();
            String mesiac = dateTimePicker1.Value.Month.ToString();
            String den = dateTimePicker1.Value.Day.ToString();
            if (mesiac.Length == 1)
            {
                mesiac = "0" + mesiac;
            }
            if (den.Length == 1)
            {
                den = "0" + den;
            }


            String id_hospitalizacie = den + mesiac + rok + textBox1.Text; 
            //String nazov_diagnozy = comboBox1.Text;
            Nemocnica nemocnica = this.informacny_system.NajdiNemocnicu(comboBox2.Text);
            Pacient pacient = nemocnica.NajdiPacient(textBox1.Text);
            
            


            DialogResult dr = MessageBox.Show("Začať hospitalizáciu?", "Ano", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                if (textBox1.Text != String.Empty && id_hospitalizacie != string.Empty && dateTimePicker1.Value != null)
                {
                    if (nemocnica != null && pacient != null)
                    {
                        //var pom = nemocnica.PridajHospitalizaciu(id_hospitalizacie, rod_cislo, dat_od, nazov_diagnozy);
                        // var pomPac = pacient.PridajHospitalizaciuPacientovi(id_hospitalizacie, rod_cislo, dat_od, nazov_diagnozy);
                        Hospitalizacia hospi = new Hospitalizacia();
                        hospi.id_hospitalizacie = id_hospitalizacie;
                        hospi.rod_cislo_pacienta = textBox1.Text;
                        hospi.datum_od = dateTimePicker1.Value;
                        hospi.nazov_diagnozy = comboBox1.Text;
                        var pom = nemocnica.PridajHospi(hospi);
                        //var pom = nemocnica.PridajHospitalizaciu(id_hospitalizacie, textBox1.Text, dateTimePicker1.Value, comboBox1.Text);
                        //var pomPac = pacient.PridajHospitalizaciuPacientovi(id_hospitalizacie, textBox1.Text, dateTimePicker1.Value, comboBox1.Text);
                        if (pacient.JeAktualneHosp()) //datum do .year = 0001 - nema este ukoncenu predchadzajucu
                        {
                            MessageBox.Show("Pacient je este hospitalizovany.");
                        }
                        else
                        {
                            var pomPac = pacient.PridajHosp(hospi);
                            if (pom && pomPac)
                            {
                                MessageBox.Show("Hospitalizacia bola zaevidovana.");

                            }
                            else
                            {
                                MessageBox.Show("CHYBA ... Hospitalizacia nebola zaevidovana.");
                            }
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
            hosp.LoadDataFromFile();
            List<string> hospitalizacie = hosp.VratListDiagnoz();
            for (int i = 0; i < hospitalizacie.Count; i++)
            {
                comboBox1.Items.Add(hospitalizacie.ElementAt(i));
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
