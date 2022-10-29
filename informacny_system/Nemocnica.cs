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

        public Pacient NajdiPacient(String rod_cislo)
        {
            if (rod_cislo == string.Empty) { return null; }
            if (pacienti.FindNode(rod_cislo) == null)
            {
                return null;
            }
            else
            {
                var pom = pacienti.FindNode(rod_cislo).Data;
                return pom;
            }
            
        }

        public List<Pacient> VratListPacientov()
        {
            return this.pacienti.ZapisVsetkyNody(pacienti.Root);
        }

        public bool PridajHospitalizaciu(String id_hospitalizacie, String rod_cislo , DateTime dat_od, String nazov_diagnozy)
        {
            if (rod_cislo == String.Empty || id_hospitalizacie == String.Empty || nazov_diagnozy == String.Empty || dat_od == null)
            {
                return false;
            }
            Hospitalizacia hospitalizacia = new Hospitalizacia();
            hospitalizacia.id_hospitalizacie = id_hospitalizacie;
            hospitalizacia.rod_cislo_pacienta = rod_cislo;
            hospitalizacia.datum_od = dat_od;
            hospitalizacia.nazov_diagnozy = nazov_diagnozy;
            var pom = this.hospitalizacie.Insert(id_hospitalizacie, hospitalizacia);
            if (pom == null) 
            {
                return false;
            }
            return true;
        }
        public Hospitalizacia NajdiHospitalizaciu(String id_hospitalizacie)
        {
            if (id_hospitalizacie == string.Empty) { return null; }
            var pom = hospitalizacie.FindNode(id_hospitalizacie).Data;
            if (pom == null) { return null; } else { return pom; }
        }

        public List<Hospitalizacia> VratListHospitalizacii()
        {
            return this.hospitalizacie.ZapisVsetkyNody(hospitalizacie.Root);
        }




    }
    
}
