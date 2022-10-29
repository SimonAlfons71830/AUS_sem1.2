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
        List<Hospitalizacia> hospitalizaciePacienta = new List<Hospitalizacia>();

        public bool PridajHospitalizaciuPacientovi(String id_hospitalizacie, String rod_cislo, DateTime dat_od, String nazov_diagnozy)
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
            this.hospitalizaciePacienta.Add(hospitalizacia);
            return true;
        }
        public Hospitalizacia NajdiHospitalizaciu(Hospitalizacia hospitalizacia)
        {
            if (hospitalizacia == null) { return null; }
            if (this.hospitalizaciePacienta.Contains(hospitalizacia)) {
                for (int i = 0; i < hospitalizaciePacienta.Count; i++)
                {
                    if (hospitalizaciePacienta.ElementAt(i) == hospitalizacia)
                    {
                        return hospitalizacia;
                    }
                }
            }
            return null;
        }

        public List<Hospitalizacia> VratListHospitalizacii()
        {
            List<Hospitalizacia> hospitalizacieZoradene = this.hospitalizaciePacienta.OrderBy(o => o.datum_od).ToList();
            return hospitalizacieZoradene;
        }

        









    }


    



}
