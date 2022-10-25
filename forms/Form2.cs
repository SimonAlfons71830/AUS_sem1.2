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
    public partial class Form2 : Form
    {
        protected Informacny_system inf_system;
        public Form2(Informacny_system inf_system)
        {
            InitializeComponent();
            this.inf_system = inf_system;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //priezvisko 
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //meno pacienta
            

              
            
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //rodne cislo
            
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //datum narodenia
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //??
            //button1.Enabled = textBox1.Enabled && textBox2.Enabled && textBox3.Enabled && comboBox1.Enabled && dateTimePicker1.Enabled;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            /*comboBox1.Items.Add("VZP");
            comboBox1.Items.Add("DÔVERA");
            comboBox1.Items.Add("UNION");*/
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //pridaj pacienta do databazy
           
            String meno = textBox1.Text;
            String priezvisko = textBox2.Text;
            String rod_cislo = textBox3.Text;
            DateTime datum_narodenia = dateTimePicker1.Value;
            String nazov_nemocnice = comboBox2.Text;
            String nazov_poistovne = comboBox1.Text;
            

            DialogResult dr = MessageBox.Show("Chcete ulozit pacienta?", "Ano", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                if (meno != string.Empty && priezvisko != string.Empty && rod_cislo != string.Empty && datum_narodenia != null) 
                {
                    var nemocnica = inf_system.NajdiNemocnicu(nazov_nemocnice);
                    if (nemocnica != null)
                    {
                        var pacient = nemocnica.PridajPacienta(meno, priezvisko, rod_cislo, datum_narodenia, nazov_poistovne, nazov_nemocnice);
                        if (pacient)
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
    }
}
