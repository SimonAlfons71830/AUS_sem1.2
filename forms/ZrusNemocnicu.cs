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
    public partial class ZrusNemocnicu : Form
    {
        protected Informacny_system inf_system;
        public ZrusNemocnicu(Informacny_system system)
        {
            InitializeComponent();
            this.inf_system = system;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.Created)
            {
                label3.Text = this.inf_system.NajdiNemocnicu(comboBox1.Text).VratListPacientov().Count.ToString();
                label6.Text = this.inf_system.NajdiNemocnicu(comboBox1.Text).VratListHospitalizacii().Count.ToString();
            }
            comboBox2.Items.Remove(comboBox1.SelectedItem);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ZrusNemocnicu_Load(object sender, EventArgs e)
        {
            List<informacny_system.Nemocnica> nemocnice = this.inf_system.VratListNemocnic();

            for (int i = 0; i < nemocnice.Count; i++)
            {
                comboBox1.Items.Add(nemocnice[i].nazov_nemocnice);
                comboBox2.Items.Add(nemocnice[i].nazov_nemocnice);
            }
            

        }

        private void label3_Click(object sender, EventArgs e)
        {
           
        }

        private void label6_Click(object sender, EventArgs e)
        {
            
        }

        private void label8_Click(object sender, EventArgs e)
        {
            
        }

        private void label10_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (comboBox2.Created)
            {
                label8.Text = this.inf_system.NajdiNemocnicu(comboBox2.Text).VratListPacientov().Count.ToString();
                label10.Text = this.inf_system.NajdiNemocnicu(comboBox2.Text).VratListHospitalizacii().Count.ToString();
            }
            button1.Enabled = comboBox1.Created && comboBox2.Created;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (comboBox1.Created && !comboBox2.Created)
            {
                MessageBox.Show("Nezadali ste náhradnú nemocnicu.");
            }
            else if (!comboBox1.Created && comboBox2.Created)
            {
                MessageBox.Show("Nezadali ste rušenú nemocnicu.");
            }
            DialogResult dr = MessageBox.Show("Chcete vymazat nemocnicu?", "Ano", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                Nemocnica nahradna = this.inf_system.NajdiNemocnicu(comboBox2.Text);
                Nemocnica rusena = this.inf_system.NajdiNemocnicu(comboBox1.Text);

                List<Node<(String,String,DateTime), Hospitalizacia>> listRusenychHosp = rusena.VratMazaneHospitalizacieAkoListNodov(); ;
                List<Node<String, Pacient>> listRusenychPac = rusena.VratMazanychPacientovAkoListNodov();
                List<Node<(String, String, String), Pacient>> listRusenychHospNove = rusena.VratMazanychPacientovAkoListNodov2();

                nahradna.HromadnyInsertHospitalizacii(listRusenychHosp);
                nahradna.HromadnyInsertPacientov(listRusenychPac);
                nahradna.HromadnyInsertPacientovNovyStrom(listRusenychHospNove);

                if (this.inf_system.VymazNemocnicu(this.inf_system.NajdiNemocnicu(comboBox1.Text)))
                {
                    MessageBox.Show("Podarilo sa vymazat nemocnicu.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Nepodarilo sa vymazat nemocnicu.");
                }
                



                
            }
        }
    }
}
