using Hospital_information_sytem.structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_information_sytem.informacny_system
{
    
    public class Positovna
    {
        private Random _random = new Random();
        public String kod_poistovne;
        public String nazov_poistovne;
        Binary_search_tree<String, Poistenec> poistenci = new Binary_search_tree<string, Poistenec>();
        public bool PridajPoistenca(String rod_cislo)
        {
            if (rod_cislo == string.Empty) { return false; }
            Poistenec poistenec = new Poistenec();
            poistenec.rod_cislo_poistenca = rod_cislo;
            /*if (kod_poistovne == "VZP")
            {
                poistenec.id_poistenca = _random.Next(1000, 1999).ToString();
            }
            if (kod_poistovne == "UNI")
            {
                poistenec.id_poistenca = _random.Next(2000,2999).ToString();
            }
            if (kod_poistovne == "DVO")
            {
                poistenec.id_poistenca = _random.Next(3000,3999).ToString();
            }*/
            poistenec.id_poistenca = rod_cislo;
            var pom = this.poistenci.Insert(rod_cislo, poistenec);
            if (pom == null) { return false; }
            return true;

        }
        public Poistenec NajdiPoistenca(String id_poistenca)
        {
            if (id_poistenca == string.Empty) { return null; }
            var pom = poistenci.FindNode(id_poistenca).Data;
            if (pom == null) { return null; } else { return pom; }
        }
        public List<Poistenec> VratListPoistencov()
        {
            return this.poistenci.ZapisVsetkyNody(poistenci.Root);
        }



    }
}
