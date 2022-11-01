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
            var pocetDni = 0;

            var start = new DateTime(this.datum.Year, this.datum.Month, 1);
            var koniec = new DateTime();

            if (this.datum.Month == 1 || this.datum.Month == 3 || this.datum.Month == 5 || this.datum.Month == 7 || this.datum.Month == 8 || this.datum.Month == 10 || this.datum.Month == 12)
            {
                koniec = new DateTime(this.datum.Year, this.datum.Month, 31);
            }
            else if (this.datum.Month == 2)
            {
                koniec = new DateTime(this.datum.Year, this.datum.Month, 28);
            }
            else
            {
                koniec = new DateTime(this.datum.Year, this.datum.Month, 30);
            }
            
            int pom = 0;

            for (int i = 0; i < listPacientov.Count; i++)
            {
                List<Hospitalizacia> listhospPac = this.listPacientov.ElementAt(i).VratListHospitalizacii();
                Hospitalizacia hospitalizacia = new Hospitalizacia();
                for (int j = 0; j < listhospPac.Count; j++)
                {
                    if (listhospPac.ElementAt(j).datum_do.Month == this.datum.Month && listhospPac.ElementAt(j).datum_do.Year == this.datum.Year ||
                        listhospPac.ElementAt(j).datum_od.Month == this.datum.Month && listhospPac.ElementAt(j).datum_od.Year ==  this.datum.Year)
                    {
                        hospitalizacia = listhospPac.ElementAt(j);
                        if (hospitalizacia.datum_od >= start && hospitalizacia.datum_do <= koniec && hospitalizacia.datum_do.Year != 0001)
                        {
                            pocetDni += (hospitalizacia.datum_do - hospitalizacia.datum_od).Days;
                        }
                        else if (hospitalizacia.datum_od <= start && hospitalizacia.datum_do <= koniec && hospitalizacia.datum_do.Year != 0001) //hosp zacala v predosly mesiac a skoncila v dany
                        {
                            pocetDni += (hospitalizacia.datum_do - start).Days;
                        }
                        else if(hospitalizacia.datum_od >= start && hospitalizacia.datum_do >= koniec) // hosp zacala v dany mesiac ale skoncila buduci
                        {
                            pocetDni += (koniec - hospitalizacia.datum_od).Days;
                        }
                        else if(hospitalizacia.datum_do.Year == 0001)
                        {
                            pocetDni += (koniec - hospitalizacia.datum_od).Days;
                        }
                    }
                }

                ListViewItem item = new ListViewItem(listPacientov.ElementAt(i).rod_cislo);
                item.SubItems.Add(listPacientov.ElementAt(i).priezvisko);
                item.SubItems.Add(listPacientov.ElementAt(i).meno);
                item.SubItems.Add(hospitalizacia.nazov_diagnozy);
                listView1.Items.Add(item);
                pom++;
            }

            label2.Text = pocetDni.ToString();
            label6.Text = pom.ToString();


            // SELECT DAY FROM CALENDAR TO SHOW PATIENT HOSPITALIZED THAT DAY
            monthCalendar1.MaxSelectionCount = 1; //can choose only 1 day
            var startTime = new DateTime(this.datum.Year, this.datum.Month, 1);
            DateTime koniecTime;
            if (this.datum.Month == 1 || this.datum.Month == 3 || this.datum.Month == 5 || this.datum.Month == 7 || this.datum.Month == 8 || this.datum.Month == 10 || this.datum.Month == 12)
            {
                koniecTime = new DateTime(this.datum.Year, this.datum.Month, 31);
            }
            else if (this.datum.Month == 2)
            {
                koniecTime = new DateTime(this.datum.Year, this.datum.Month, 28);
            }
            else
            {
                koniecTime = new DateTime(this.datum.Year, this.datum.Month, 30);
            }
            monthCalendar1.SetSelectionRange(startTime, koniecTime);

            // DateTime denNaZobrazenie = monthCalendar1.;
            
            






        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                
                var firstSelectedItem = listView1.SelectedItems[0];
                Nemocnica nem = this.inf_system.NajdiNemocnicuPacientovi(firstSelectedItem.Text);
                if (nem != null)
                {
                    var zobrazInfo = new UdajeOPacientovi(this.inf_system, nem.nazov_nemocnice, firstSelectedItem.Text);
                    zobrazInfo.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Nepodarilo sa najst nemocnicu v ktorej je pacient hospitalizovany.");
                }
                
               
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            //fixnut kalendar len na mesiac ktory bol zadany
            listView2.Items.Clear();
            var datumnaVyhladanie =  monthCalendar1.SelectionStart;
            List<Pacient> list = new List<Pacient>();

            for (int i = 0; i < this.listPacientov.Count; i++)
            {
                List <Hospitalizacia> listHospitalizacii = this.listPacientov.ElementAt(i).VratListHospitalizacii();
                for (int j = 0; j < listHospitalizacii.Count; j++)
                {
                    if (listHospitalizacii.ElementAt(j).datum_od <= datumnaVyhladanie && listHospitalizacii.ElementAt(j).datum_do >= datumnaVyhladanie && listHospitalizacii.ElementAt(j).datum_do.Year != 0001 )
                    {
                        list.Add(this.listPacientov.ElementAt(i));
                    }
                    else if (listHospitalizacii.ElementAt(j).datum_od <= datumnaVyhladanie && listHospitalizacii.ElementAt(j).datum_do.Year == 0001)
                    {
                        list.Add(this.listPacientov.ElementAt(i));
                    }
                    
                }

            }

            for (int k = 0; k < list.Count; k++)
            {
                ListViewItem item = new ListViewItem(list.ElementAt(k).rod_cislo);
                item.SubItems.Add(listPacientov.ElementAt(k).priezvisko);
                item.SubItems.Add(listPacientov.ElementAt(k).meno);
                if (!listView2.Items.ContainsKey(list.ElementAt(k).rod_cislo))
                {
                    listView2.Items.Add(item);
                }
                
                
            }
            label8.Text = listView2.Items.Count.ToString();
            
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {

                var firstSelectedItem = listView1.SelectedItems[0];
                Nemocnica nem = this.inf_system.NajdiNemocnicuPacientovi(firstSelectedItem.Text);
                if (nem != null)
                {
                    var zobrazInfo = new UdajeOPacientovi(this.inf_system, nem.nazov_nemocnice, firstSelectedItem.Text);
                    zobrazInfo.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Nepodarilo sa najst nemocnicu v ktorej je pacient hospitalizovany.");
                }


            }
        }
    }
}
