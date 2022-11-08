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
    public partial class UkonciHosp : Form
    {
        public Informacny_system informacny_system;
        public UkonciHosp(Informacny_system system)
        {
            InitializeComponent();
            this.informacny_system = system;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //String rod_cislo = textBox1.Text;
            //DateTime dat_do = dateTimePicker1.Value;
            Nemocnica nemocnica = this.informacny_system.NajdiNemocnicu(comboBox2.Text);
            Pacient pacient = nemocnica.NajdiPacient(textBox1.Text);
            if (nemocnica == null && pacient == null)
            {
                MessageBox.Show("Nemocnica alebo pacient neexistuje.");
            }
            else
            {
                DialogResult dr = MessageBox.Show("Ukončiť hospitalizáciu?", "Ano", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                var hospitalizaciaPosledna = pacient.VratPoslednuHospitalizaciu();
                if (hospitalizaciaPosledna == null)
                {
                    MessageBox.Show("Pacient nema ziadnu hospitalizaciu.");
                }
                else
                {
                    if (dr == DialogResult.Yes)
                    {
                        if (textBox1.Text != String.Empty && dateTimePicker1.Value != null)
                        {
                                var pomNemocnica = nemocnica.NajdiHospitalizaciu(hospitalizaciaPosledna.Data.id_hospitalizacie);

                            if (hospitalizaciaPosledna.Data.datum_do.Year == 0001 && pomNemocnica.datum_do.Year == 0001)
                            {
                                //this.informacny_system.NajdiNemocnicu(nemocnica.nazov_nemocnice).NajdiPacient(pacient.rod_cislo).NajdiHospitalizaciu(pom).datum_do = dat_do;
                                hospitalizaciaPosledna.Data.datum_do = dateTimePicker1.Value;
                                pomNemocnica.datum_do = dateTimePicker1.Value;
                                MessageBox.Show("Hospitalizacia bola ukončená.");
                            }
                            else
                            {
                                MessageBox.Show("CHYBA ... Hospitalizacia nebola ukončená.");
                            }
                        }
                        this.Close();
                    }
                }
            }
            
            
           

        }

        private void UkonciHosp_Load(object sender, EventArgs e)
        {


            List<informacny_system.Nemocnica> nemocnice = this.informacny_system.VratListNemocnic();

            for (int i = 0; i < nemocnice.Count; i++)
            {
                comboBox2.Items.Add(nemocnice[i].nazov_nemocnice);
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
