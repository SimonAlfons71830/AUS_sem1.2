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
    public partial class ZvolNemocnicuAMenoP : Form
    {
        protected Informacny_system inf_system;
        public ZvolNemocnicuAMenoP(Informacny_system inf_sys)
        {
            InitializeComponent();
            this.inf_system = inf_sys;
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = textBox1.Enabled;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //String meno = textBox1.Text;
            //String priezvisko = textBox2.Text;
            

            Nemocnica nem = this.inf_system.NajdiNemocnicu(comboBox1.Text);

            Binary_search_tree<(String,String,String),Pacient> stromPacientov = nem.NajdiPacientPodlaMena(textBox2.Text, textBox1.Text);
            //List<Pacient> pac = nem.NajdiPacientaPodlaMeno(textBox2.Text, textBox1.Text);

            if (stromPacientov != null)
            {
                var udajeOPacientochPodlaMeno = new UdajeOPacientochMeno(inf_system, stromPacientov , nem);
                udajeOPacientochPodlaMeno.ShowDialog();
            }
            else
            {
                MessageBox.Show("Pacient sa v nemocnici nenachadza.");
            }


        }

        private void ZvolNemocnicuAMenoP_Load(object sender, EventArgs e)
        {

            List<informacny_system.Nemocnica> nemocnice = this.inf_system.VratListNemocnic();

            for (int i = 0; i < nemocnice.Count; i++)
            {
                comboBox1.Items.Add(nemocnice[i].nazov_nemocnice);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
