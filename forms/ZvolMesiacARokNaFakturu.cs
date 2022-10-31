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
    public partial class ZvolMesiacARokNaFakturu : Form
    {
        protected Informacny_system inf_system;
        public ZvolMesiacARokNaFakturu(Informacny_system system)
        {
            InitializeComponent();
            this.inf_system = system;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Pacient> pacientiNaFakturaciu = this.inf_system.VsetciPacientiHospVDanyMesiac(dateTimePicker1.Value);

            var zoznamPoistovni = new VypisPoistovniAIchfakturácie(this.inf_system, pacientiNaFakturaciu, dateTimePicker1.Value);
            zoznamPoistovni.ShowDialog();
        }
    }
}
