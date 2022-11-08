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
    public partial class ZvolOdDoANemocnicu : Form
    {
        protected Informacny_system inf_system;
        public ZvolOdDoANemocnicu(Informacny_system system)
        {
            InitializeComponent();
            this.inf_system = system;
        }

        private void ZvolOdDoANemocnicu_Load(object sender, EventArgs e)
        {

            List<informacny_system.Nemocnica> nemocnice = this.inf_system.VratListNemocnic();

            for (int i = 0; i < nemocnice.Count; i++)
            {
                comboBox1.Items.Add(nemocnice[i].nazov_nemocnice);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //button1.Enabled = comboBox1.Enabled;
            //funkcia vrat hosp od do z nemocnice 

           /* Nemocnica nem = this.inf_system.NajdiNemocnicu(comboBox1.Text);
            
            List<Pacient> vsetciPacienti = nem.VratListPacientov();
            List<Pacient> hospitalizovanyOdDo = new List<Pacient>();
            for (int i = 0; i < vsetciPacienti.Count; i++)
            {
                List<Hospitalizacia> hospPacienta = vsetciPacienti.ElementAt(i).VratListHospitalizacii();
                
                if (hospPacienta.Count!= 0)
                {
                    for (int j = 0; j < hospPacienta.Count; j++)
                    {
                        if (hospPacienta.ElementAt(j).datum_do.Year == 0001 && hospPacienta.ElementAt(j).datum_do == DateTime.Today )
                        {
                            //moze vypisat aj neukoncene ak je datum do DNES
                            if (hospPacienta.ElementAt(j).datum_od >= dateTimePicker1.Value)
                            {
                                hospitalizovanyOdDo.Add(vsetciPacienti.ElementAt(i));
                            }
                        }
                        else 
                        {   
                            if (hospPacienta.ElementAt(j).datum_od >= dateTimePicker1.Value && hospPacienta.ElementAt(j).datum_do <= dateTimePicker2.Value && hospPacienta.ElementAt(j).datum_do.Year != 0001)
                            {
                                hospitalizovanyOdDo.Add(vsetciPacienti.ElementAt(i));
                            } 
                        }
                        
                    }

                }


            }


            var hospOdDoPac = new UdajeOPacientochMeno(this.inf_system, hospitalizovanyOdDo, nem);
            hospOdDoPac.ShowDialog();
*/
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = comboBox1.Enabled;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
