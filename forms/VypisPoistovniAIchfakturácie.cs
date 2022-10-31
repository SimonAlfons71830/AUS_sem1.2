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
    public partial class VypisPoistovniAIchfakturácie : Form
    {
        protected Informacny_system inf_system;
        List<Pacient> listPacientov;
        DateTime datum;
        public VypisPoistovniAIchfakturácie(Informacny_system system, List<Pacient> pacientiNaFakturaciu, DateTime datum)
        {
            InitializeComponent();
            this.inf_system = system;
            this.listPacientov = pacientiNaFakturaciu;
            this.datum = datum;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var firstSelectedItem = listView1.SelectedItems[0];
                var zobrazInfo = new Faktura(this.inf_system, this.inf_system.PacientiPodlaPoistovnePreFormular(listPacientov,firstSelectedItem.Text), firstSelectedItem.Text, datum);
                zobrazInfo.ShowDialog();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void VypisPoistovniAIchfakturácie_Load(object sender, EventArgs e)
        {
            List<Positovna> listPoistovni = this.inf_system.VratListPoistovni();
            for (int i = 0; i < listPoistovni.Count; i++)
            {
                ListViewItem item = new ListViewItem(listPoistovni.ElementAt(i).kod_poistovne);
                item.SubItems.Add(listPoistovni.ElementAt(i).nazov_poistovne);
                item.SubItems.Add(listPoistovni.ElementAt(i).VratListPoistencov().Count.ToString());
                listView1.Items.Add(item);
            }
            label2.Text = listPoistovni.Count.ToString();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
