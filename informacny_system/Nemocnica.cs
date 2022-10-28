using Hospital_information_sytem.structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_information_sytem.informacny_system
{
    public class Nemocnica
    {
        public String nazov_nemocnice;
        Binary_search_tree<String, Pacient> pacienti = new Binary_search_tree<string, Pacient>();
        Binary_search_tree<String, Hospitalizacia> hospitalizacie = new Binary_search_tree<string, Hospitalizacia>();

        public bool PridajPacienta(String meno, String priezvisko, String rod_cislo, DateTime datum_narodenia, String kod_poistovne, String nazov_nemocnice)
        {
            if (rod_cislo == string.Empty || priezvisko == string.Empty || meno == string.Empty || datum_narodenia == null ||
                kod_poistovne == string.Empty || nazov_nemocnice == string.Empty) { return false; }
            Pacient pacient = new Pacient();
            pacient.rod_cislo = rod_cislo;
            pacient.datum_narodenia = datum_narodenia;
            pacient.meno = meno;
            pacient.priezvisko = priezvisko;
            pacient.kod_poistovne = kod_poistovne;
            var pom = this.pacienti.Insert(rod_cislo, pacient);
            if (pom == null) { return false; } 
            return true;

        }


    }
    
}
