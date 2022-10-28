using Hospital_information_sytem.informacny_system;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_information_sytem.structures
{
    public class Informacny_system
    {
        Binary_search_tree<String, Nemocnica> databaza_nemocnic = new Binary_search_tree<String, Nemocnica>();
        Binary_search_tree<String, Positovna> databaza_poistovni = new Binary_search_tree<string, Positovna>();
        public int VratPocetNemocnic()
        {
            return this.databaza_nemocnic.Size;
        }

        public bool PridajNemocnicu(String nazov)
        {
            if (nazov == string.Empty ) { return false; }
            Nemocnica nemocnica = new Nemocnica();
            nemocnica.nazov_nemocnice = nazov;
            var pom = databaza_nemocnic.Insert(nazov,nemocnica);
            if (pom == null)
            {
                return false;
            }
            return true;
        }

        public bool PridajPoistovnu()
        {
            Positovna poistovnaVZP = new Positovna();
            Positovna poistovnaUNION = new Positovna();
            Positovna poistovnaDovera = new Positovna();
            poistovnaVZP.nazov_poistovne = "Všeobecná Zdravotná Poisťovňa";
            poistovnaUNION.nazov_poistovne = "UNION";
            poistovnaDovera.nazov_poistovne = "Dôvera";
            poistovnaVZP.kod_poistovne = "VZP";
            poistovnaUNION.kod_poistovne = "UNI";
            poistovnaDovera.kod_poistovne = "DOV";
            var pomVZP = databaza_poistovni.Insert(poistovnaVZP.nazov_poistovne, poistovnaVZP);
            var pomUNION = databaza_poistovni.Insert(poistovnaUNION.nazov_poistovne, poistovnaUNION);
            var pomDOVERA = databaza_poistovni.Insert(poistovnaDovera.nazov_poistovne, poistovnaDovera);
            if (pomVZP == null || pomUNION == null || pomDOVERA == null)
            {
                return false;
            }
            return true;
        }

        public Nemocnica NajdiNemocnicu(String nazov) 
        {
            if (nazov == string.Empty) { return null; }
            var pom = databaza_nemocnic.FindNode(nazov).Data;
            if (pom == null) { return null; } else { return pom; }
        }
        public Positovna NajdiPoistovnu(String nazov)
        {
            if (nazov == string.Empty) { return null; }
            var pom = databaza_poistovni.FindNode(nazov).Data;
            if (pom == null) { return null; } else { return pom; }
        }
        public List<Nemocnica> VratListNemocnic() 
        {
            return this.databaza_nemocnic.ZapisVsetkyNody(databaza_nemocnic.Root);
        }

        public List<Positovna> VratListPoistovni()
        {
            return this.databaza_poistovni.ZapisVsetkyNody(databaza_poistovni.Root);
        }
        
        
    }
    
}
