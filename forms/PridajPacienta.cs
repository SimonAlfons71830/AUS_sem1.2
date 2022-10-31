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

namespace Hospital_information_sytem
{
    public partial class PridajPacienta : Form
    {
        protected Informacny_system inf_system;
        public PridajPacienta(Informacny_system inf_system)
        {
            InitializeComponent();
            this.inf_system = inf_system;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        private void label7_Click(object sender, EventArgs e)
        {

        }
        private void Form2_Load(object sender, EventArgs e)
        {
            
            List<informacny_system.Nemocnica> nemocnice = this.inf_system.VratListNemocnic();

            for (int i = 0; i < nemocnice.Count; i++)
            {
                comboBox2.Items.Add(nemocnice[i].nazov_nemocnice);
            }
            List<informacny_system.Positovna> poistovne = this.inf_system.VratListPoistovni();
            for (int i = 0; i < poistovne.Count; i++)
            {
                comboBox1.Items.Add(poistovne[i].nazov_poistovne);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //pridaj pacienta do databazy
           
            /*String meno = textBox1.Text;
            String priezvisko = textBox2.Text;
            String rod_cislo = textBox3.Text;
            DateTime datum_narodenia = dateTimePicker1.Value;
            String nazov_nemocnice = comboBox2.Text;
            String nazov_poistovne = comboBox1.Text;*/
            

            DialogResult dr = MessageBox.Show("Chcete ulozit pacienta?", "Ano", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                if(textBox1.Text != String.Empty && textBox2.Text != String.Empty && textBox3.Text != String.Empty && dateTimePicker1.Value != null)
                //if (meno != string.Empty && priezvisko != string.Empty && rod_cislo != string.Empty && datum_narodenia != null) 
                {
                    //var nemocnica = inf_system.NajdiNemocnicu(nazov_nemocnice);
                    //var poistovna = inf_system.NajdiPoistovnu(nazov_poistovne);

                    var nemocnica = inf_system.NajdiNemocnicu(comboBox2.Text);
                    var poistovna = inf_system.NajdiPoistovnu(comboBox1.Text);
                    if (nemocnica != null && poistovna != null)
                    {

                        //var pacient = nemocnica.PridajPacienta(meno, priezvisko, rod_cislo, datum_narodenia, poistovna.kod_poistovne, nazov_nemocnice);
                        //var poistenec = poistovna.PridajPoistenca(rod_cislo);

                        var pacient = nemocnica.PridajPacienta(textBox1.Text, textBox2.Text,textBox3.Text, dateTimePicker1.Value, poistovna.kod_poistovne, comboBox2.Text);
                        var poistenec = poistovna.PridajPoistenca(textBox3.Text);
                        if (pacient && poistenec)
                        {
                            MessageBox.Show("Pacient bol pridany do nemocnice.");

                        }
                        else
                        {
                            MessageBox.Show("Pacienta sa nepodarilo pridat do nemocnice.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Zadanú nemocnicu sa nepdarilo nájsť.");
                    }
                }
                this.Close();
            }
            
        }

        private void label7_Click_1(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        { 
        }

        private void panelMenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
