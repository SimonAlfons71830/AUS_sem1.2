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
    public partial class AktualneHospitalizovani1 : Form
    {
        protected Informacny_system inf_system;
        String kod;
        public AktualneHospitalizovani1(Informacny_system system)
        {
            InitializeComponent();
            this.inf_system = system;
            kod = "";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            comboBox2.Visible = checkBox1.Checked;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox2.Text)
            {

                case "Všeobecná Zdravotná Poisťovňa":
                    this.kod = "VZP";
                    break;

                case "UNION":
                    this.kod = "UNI";
                    break;

                default:
                    this.kod = "DVO";
                    break;
            }
        }

        private void AktualneHospitalizovani1_Load(object sender, EventArgs e)
        {
            List<informacny_system.Nemocnica> nemocnice = this.inf_system.VratListNemocnic();

            for (int i = 0; i < nemocnice.Count; i++)
            {
                comboBox1.Items.Add(nemocnice[i].nazov_nemocnice);
            }
            List<informacny_system.Positovna> poistovne = this.inf_system.VratListPoistovni();
            for (int i = 0; i < poistovne.Count; i++)
            {
                comboBox2.Items.Add(poistovne[i].nazov_poistovne);
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = comboBox1.Created;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Nemocnica nemocnica = this.inf_system.NajdiNemocnicu(comboBox1.Text);
            List<Pacient> listAktualneHospPacientov = new List<Pacient>();
            List<Pacient> listVsetkychPacientov = nemocnica.VratListPacientov();

            for (int i = 0; i < listVsetkychPacientov.Count; i++)
            {
                Hospitalizacia poslednaHospPacienta = listVsetkychPacientov.ElementAt(i).VratListHospitalizacii().Last();
                if (poslednaHospPacienta.datum_do.Year == 0001)
                {
                    listAktualneHospPacientov.Add(listVsetkychPacientov.ElementAt(i));
                }
            }

            



            if (comboBox1.Created && !checkBox1.Checked)
            {
                if (checkBox2.Checked)
                {
                    //List<Pacient> zoradenyAktPac = listAktualneHospPacientov.OrderBy(o => o.rod_cislo).ToList();
                    //VACSI ZMYSEL DAVA ZORADIT PODLA DATUMU NARODENIA + UZ DO BVS SA ZORADUJU PODLA ROD.CISLA
                    List<Pacient> zoradenyAktPac = listAktualneHospPacientov.OrderBy(o => o.datum_narodenia).ToList();

                    var uloha8 = new UdajeOPacientochMeno(this.inf_system, zoradenyAktPac, nemocnica);
                    //var uloha8 = new Hospitalizovani8(this.inf_system, comboBox1.Text, checkBox2.Checked);
                    uloha8.ShowDialog();
                }
                else
                {
                    var uloha8 = new UdajeOPacientochMeno(this.inf_system, listAktualneHospPacientov, nemocnica);
                    uloha8.ShowDialog();
                }
                //var uloha8 = new Hospitalizovani8(this.inf_system, comboBox1.Text, checkBox2.Checked);

            }
            else if (comboBox1.Created && checkBox1.Checked)
            {
                List<Pacient> listAktualneHospPacientovPOISTOVNA = new List<Pacient>();

                for (int i = 0; i < listAktualneHospPacientov.Count; i++)
                {
                    if (listAktualneHospPacientov.ElementAt(i).kod_poistovne == this.kod)
                    {
                        listAktualneHospPacientovPOISTOVNA.Add(listAktualneHospPacientov.ElementAt(i));
                    }
                }
                if (checkBox2.Checked)
                {
                    List<Pacient> zoradenyy = listAktualneHospPacientovPOISTOVNA.OrderBy(o => o.datum_narodenia).ToList();
                    var uloha9a10 = new UdajeOPacientochMeno(this.inf_system, zoradenyy, nemocnica);
                    uloha9a10.ShowDialog();
                }
                else
                {
                    var uloha9a10 = new UdajeOPacientochMeno(this.inf_system, listAktualneHospPacientovPOISTOVNA, nemocnica);
                    uloha9a10.ShowDialog();
                }

                
            }
            else
            {
                MessageBox.Show("Musíš zvoliť aspoň nemocnicu.");
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
