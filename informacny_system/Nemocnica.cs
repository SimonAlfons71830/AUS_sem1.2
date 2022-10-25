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
    }
}
