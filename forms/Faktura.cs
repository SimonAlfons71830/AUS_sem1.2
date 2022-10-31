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
    public partial class Faktura : Form
    {
        protected Informacny_system inf_system;
        protected List<Pacient> listPacientov;
        protected String kod_poistovne;
        DateTime datum;
        public Faktura(Informacny_system system, List<Pacient> listPacientov, String kod_poistovne, DateTime datum)
        {
            InitializeComponent();
            this.inf_system = system;
            this.listPacientov = listPacientov;
            this.kod_poistovne = kod_poistovne;
            this.datum = datum;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            //nazov poistovne
        }

        private void label2_Click(object sender, EventArgs e)
        {
            //pocet dni hospitalizacie
        }

        private void Faktura_Load(object sender, EventArgs e)
        {
            switch (kod_poistovne)
            {

                case "VZP":
                    this.kod_poistovne = "Všeobecná Zdravotná Poisťovňa";
                    break;

                case "UNI":
                    this.kod_poistovne = "UNION";
                    break;

                default:
                    this.kod_poistovne = "Dôvera";
                    break;
            }

            label5.Text = this.inf_system.NajdiPoistovnu(kod_poistovne).nazov_poistovne;
            

            int pom =0 ;
            for (int i = 0; i < listPacientov.Count; i++)
            {
                List<Hospitalizacia> pacientoveHosp = listPacientov.ElementAt(i).VratListHospitalizacii();
                for (int j = 0; j< pacientoveHosp.Count; j++){
                    if (pacientoveHosp.ElementAt(j).datum_od.Month == datum.Month)
                    {

                        DateTime koniec = new DateTime(datum.Year, datum.Month, 31);
                        pom += (koniec - pacientoveHosp.ElementAt(j).datum_od).Days;
                    }
                    else
                    {
                        DateTime zaciatok = new DateTime(datum.Year, datum.Month, 1);
                        pom += (pacientoveHosp.ElementAt(j).datum_do - zaciatok).Days;
                    }
                }
            }
            label2.Text = pom.ToString();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
