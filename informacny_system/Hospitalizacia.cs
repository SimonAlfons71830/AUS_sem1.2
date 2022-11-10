using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_information_sytem.informacny_system
{
    public class Hospitalizacia 
    {
        //public (String, String, String, DateTime, DateTime) key { get; set; }
        public String id_hospitalizacie { get; set; }
        public String rod_cislo_pacienta { get; set; }
        public String nazov_diagnozy { get; set; }
        public DateTime datum_od { get; set; }
        public DateTime datum_do { get; set; }
        List<String> listDiagnoz = new List<string>();

        public void LoadDataFromFile() 
        { 
            StreamReader reader = new StreamReader("../../diagnozy.txt");
            //String size = Convert.ToString(reader.ReadLine());
            while (!reader.EndOfStream)
            {
                this.listDiagnoz.Add(reader.ReadLine());

            }

            reader.Close();

        }
        public List<String> VratListDiagnoz() 
        {
            return this.listDiagnoz;
        }

        
    }
}
