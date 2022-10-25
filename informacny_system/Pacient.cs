using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_information_sytem.informacny_system
{
    public class Pacient
    {
        public String rod_cislo { get; set; }
        public String meno { get; set; }
        public String priezvisko { get; set; }
        public String kod_poistovne { get; set; }
        public DateTime datum_narodenia { get; set; }
        List<Hospitalizacia> hospitalizacie = new List<Hospitalizacia>();
    }


}
