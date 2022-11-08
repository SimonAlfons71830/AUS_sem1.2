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
    
    public partial class Nemocnice : Form
    {
        public Informacny_system inf_system;
        public Nemocnice(Informacny_system system)
        {
            InitializeComponent();
            this.inf_system = system;
        }

        private void Nemocnice_Load(object sender, EventArgs e)
        {
            List<Nemocnica> nemocnice = this.inf_system.VratListNemocnic();
            for (int i = 0; i < nemocnice.Count; i++)
            {
                ListViewItem item = new ListViewItem((i+1).ToString());
                
                item.SubItems.Add(nemocnice.ElementAt(i).nazov_nemocnice);
                item.SubItems.Add(nemocnice.ElementAt(i).VratListPacientov().Count.ToString()); 
                item.SubItems.Add(nemocnice.ElementAt(i).VratListHospitalizacii().Count.ToString());
                listView1.Items.Add(item);
            }
            label2.Text = nemocnice.Count.ToString();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
       
        }
    }
}
