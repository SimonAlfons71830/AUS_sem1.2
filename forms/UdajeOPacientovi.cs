﻿using Hospital_information_sytem.informacny_system;
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
    public partial class UdajeOPacientovi : Form
    {
        public Informacny_system informacny_system;
        public Nemocnica nemocnica;
        public Pacient pacient;
        
        public UdajeOPacientovi(Informacny_system inf_system, Nemocnica nem, String rodne_cislo)
        {
            InitializeComponent();
            this.informacny_system = inf_system;
            this.nemocnica = nem;
            this.pacient = nemocnica.NajdiPacient(rodne_cislo);
            
        }

        private void UdajeOPacientovi_Load(object sender, EventArgs e)
        {
            label5.Text = pacient.meno;
            label1.Text = pacient.priezvisko;
            label2.Text = pacient.rod_cislo.Substring(0,6) + "/" + pacient.rod_cislo.Substring(6,4);
            label3.Text = pacient.datum_narodenia.ToString();
            label4.Text = pacient.kod_poistovne;

            

            //zapise z BVS cez inOrder do listu, ktory sa vypisuje v listview
            List<Hospitalizacia> hospitalizacie = pacient.VratListHospitalizacii();
            //List<Hospitalizacia> hospitalizacie = hospitalizaciee.OrderBy(o => o.datum_od).ToList();
            for (int i = 0; i < hospitalizacie.Count; i++)
            {
                ListViewItem item = new ListViewItem(hospitalizacie.ElementAt(i).id_hospitalizacie);
                String datum_od = hospitalizacie.ElementAt(i).datum_od.Day.ToString() + "." + hospitalizacie.ElementAt(i).datum_od.Month.ToString() +
                    "." + hospitalizacie.ElementAt(i).datum_od.Year.ToString();
                String datum_do;
                if (hospitalizacie.ElementAt(i).datum_do.Year == 0001)
                {
                    datum_do = "-";
                }
                else
                {
                    datum_do = hospitalizacie.ElementAt(i).datum_do.Day.ToString() + "." + hospitalizacie.ElementAt(i).datum_do.Month.ToString() +
                    "." + hospitalizacie.ElementAt(i).datum_do.Year.ToString();
                }
                item.SubItems.Add(datum_od);
                item.SubItems.Add(datum_do);
                item.SubItems.Add(hospitalizacie.ElementAt(i).nazov_diagnozy);
                listView1.Items.Add(item);
            }
            label8.Text = hospitalizacie.Count.ToString();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {  
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
