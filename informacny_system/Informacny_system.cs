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
        public Nemocnica NajdiNemocnicu(String nazov) 
        {
            if (nazov == string.Empty) { return null; }
            var pom = databaza_nemocnic.FindNode(nazov).Data;
            if (pom == null) { return null; } else { return pom; }
        }



    }
    
}
