using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_information_sytem.informacny_system
{
    internal class Hospitalizacia
    {
        public String rod_cislo_pacienta { get; set; }
        public String nazov_diagnozy { get; set; }
        public DateTime datum_od { get; set; }
        public DateTime datum_do { get; set; }
    }
}
