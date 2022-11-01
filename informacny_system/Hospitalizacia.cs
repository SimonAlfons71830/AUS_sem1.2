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
        public String id_hospitalizacie { get; set; }
        public String rod_cislo_pacienta { get; set; }
        public String nazov_diagnozy { get; set; }
        public DateTime datum_od { get; set; }
        public DateTime datum_do { get; set; }
        List<String> listDiagnoz = new List<string>();

        public void PridajDiagnozy() 
        {
            this.listDiagnoz.Add("INFEKČNÁ ALEBO PARAZITOVÁ CHOROBA");
            this.listDiagnoz.Add("NÁDOR");
            this.listDiagnoz.Add("CHOROBA KRVI");
            this.listDiagnoz.Add("ENDOKRINNÉ, NUTRIČNÉ A METABOLICKÉ CHOROBY");
            this.listDiagnoz.Add("DUŠEVNÉ PORUCHY");
            this.listDiagnoz.Add("CHOROBY NERVOVEJ SÚSTAVY");
            this.listDiagnoz.Add("CHOROBY OKA");
            this.listDiagnoz.Add("CHOROBY UCHA");
            this.listDiagnoz.Add("CHOROBY OBEHOVEJ SÚSTAVY");
            this.listDiagnoz.Add("CHOROBY TRÁVIACEJ SÚSTAVY");
            this.listDiagnoz.Add("CHOROBY DÝCHACEJ SÚSTAVY");
            this.listDiagnoz.Add("CHOROBY KOŽE");
            this.listDiagnoz.Add("CHOROBY SVALOVEJ A KOSTROVEJ SÚSTAVY");
            this.listDiagnoz.Add("GRAVIDITA");
            this.listDiagnoz.Add("VRODENÉ CHYBY, DEFORMITY A CHROMOZÓMOVÉ ANOMÁLIE");
            this.listDiagnoz.Add("SUBJEKTÍVNE A OBJEKTÍVNE PRÍZNAKY");
            this.listDiagnoz.Add("PORANENIA, OTRAVY");
            this.listDiagnoz.Add("INÉ...");
        }

        public void LoadDataFromFile() 
        { 
            StreamReader reader = new StreamReader("diagnozy.txt.txt");
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
