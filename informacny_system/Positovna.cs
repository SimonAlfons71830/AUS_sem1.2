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
        public String kod_poistovne;
        public String nazov_poistovne;
        Binary_search_tree<String, Poistenec> Poistenci = new Binary_search_tree<string, Poistenec>();
    }
}
