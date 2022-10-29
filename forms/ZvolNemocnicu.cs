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
    public partial class ZvolNemocnicu : Form
    {
        public Informacny_system informacny_system;
        public ZvolNemocnicu(Informacny_system informacny_system)
        {
            InitializeComponent();
            this.informacny_system = informacny_system;
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            List<informacny_system.Nemocnica> nemocnice = this.informacny_system.VratListNemocnic();

            for (int i = 0; i < nemocnice.Count; i++)
            {
                comboBox1.Items.Add(nemocnice[i].nazov_nemocnice);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = comboBox1.Enabled;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var zaznamzOpacientovi = new ZadajRC(informacny_system, comboBox1.Text);
            zaznamzOpacientovi.ShowDialog();
            this.Close();
        }
    }
}
