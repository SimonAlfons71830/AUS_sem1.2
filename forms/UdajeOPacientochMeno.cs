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
    public partial class UdajeOPacientochMeno : Form
    {
        protected Informacny_system inf_system;
        protected List<Pacient> pacienti;
        protected Nemocnica nemocnica;
        public UdajeOPacientochMeno(Informacny_system system, List<Pacient> pac, Nemocnica nem)
        {
            InitializeComponent();
            this.inf_system = system;
            this.pacienti = pac;
            this.nemocnica = nem;
        }

        private void UdajeOPacientochMeno_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < pacienti.Count; i++)
            {
                ListViewItem item = new ListViewItem(pacienti.ElementAt(i).rod_cislo);
                item.SubItems.Add(pacienti.ElementAt(i).meno);
                item.SubItems.Add(pacienti.ElementAt(i).priezvisko);
                item.SubItems.Add(pacienti.ElementAt(i).datum_narodenia.ToString());
                item.SubItems.Add(pacienti.ElementAt(i).kod_poistovne);
                item.SubItems.Add(pacienti.ElementAt(i).VratListHospitalizacii().Count.ToString());
                listView1.Items.Add(item);
            }
            label2.Text = pacienti.Count.ToString();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count>0)
            {
                var firstSelectedItem = listView1.SelectedItems[0];
                var zobrazInfo = new UdajeOPacientovi(inf_system, this.nemocnica.nazov_nemocnice , firstSelectedItem.Text);
                zobrazInfo.ShowDialog();
            }
            
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
