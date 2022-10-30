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
    public partial class Hospitalizovani8 : Form
    {   
        protected Informacny_system informacny_system;
        protected Nemocnica nemocnica;
        public Hospitalizovani8(Informacny_system system, String nem, bool usporiadaj)
        {
            InitializeComponent();
            this.informacny_system = system;
            this.nemocnica = this.informacny_system.NajdiNemocnicu(nem);
        }

        private void Hospitalizovani8_Load(object sender, EventArgs e)
        {

        }
    }
}
