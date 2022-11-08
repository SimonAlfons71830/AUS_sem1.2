using Hospital_information_sytem.structures;
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
        //List<Hospitalizacia> hospitalizaciePacienta = new List<Hospitalizacia>();
        Binary_search_tree<(DateTime, String, String), Hospitalizacia> pacientove_hosp = new Binary_search_tree<(DateTime, String, String), Hospitalizacia>();

       
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
            (DateTime, String, String) keyHosp = (hospitalizacia.datum_od,hospitalizacia.id_hospitalizacie, hospitalizacia.rod_cislo_pacienta);
            //this.hospitalizaciePacienta.Add(hospitalizacia);
            this.pacientove_hosp.Insert(keyHosp, hospitalizacia);
            return true;
        }

        public bool PridajHosp(Hospitalizacia hosp) 
        {
            if (hosp != null)
            {
                (DateTime, String, String) keyHosp = (hosp.datum_od,hosp.id_hospitalizacie, hosp.rod_cislo_pacienta);
                //this.hospitalizaciePacienta.Add(hosp);
                this.pacientove_hosp.Insert(keyHosp, hosp);
                return true;
            }
            return false;
        }
        public Node<(DateTime, String, String),Hospitalizacia> VratPoslednuHospitalizaciu() 
        {
            if (this.pacientove_hosp.Root != null)
            {
                var posledna = this.pacientove_hosp.Root;
                while (posledna.Right != null)
                {
                    posledna = posledna.Right;
                }
                return posledna;
            }
            return null;
            
        }
        public bool JeAktualneHosp()
        {
            if (this.VratPoslednuHospitalizaciu() != null)
            {
                if (this.VratPoslednuHospitalizaciu().Data.datum_do.Year == 0001)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
            
        }
        public Hospitalizacia NajdiHospitalizaciu(Hospitalizacia hospitalizacia)
        {

            if (hospitalizacia == null) { return null; }
            var pom = this.pacientove_hosp.FindNode((hospitalizacia.datum_od,hospitalizacia.id_hospitalizacie, 
                hospitalizacia.rod_cislo_pacienta));
            if ( pom != null)
            {
                return pom.Data;
            }
            else
            {
                return null;
            }
        }

        public List<Hospitalizacia> VratListHospitalizacii()
        {
            List<Hospitalizacia> hospitaliz = this.pacientove_hosp.ZapisVsetkyNody(this.pacientove_hosp.Root);
            //List<Hospitalizacia> hospitalizacieZoradene = this.hospitalizaciePacienta.OrderBy(o => o.datum_od).ToList();
            return hospitaliz;
        }

        public void Optimalizuj()
        {
            this.pacientove_hosp.Vyvaz(this.pacientove_hosp.Root);
        }


        public bool BolHospitalizovanyTentoMesiac(DateTime mesiacArok) 
        {
            List<Hospitalizacia> pacientovehosp = this.VratListHospitalizacii();

            for (int i = 0; i < pacientovehosp.Count; i++)
            {
                if ((pacientovehosp.ElementAt(i).datum_od.Month == mesiacArok.Month && pacientovehosp.ElementAt(i).datum_od.Year == mesiacArok.Year) ||
                    (pacientovehosp.ElementAt(i).datum_do.Month == mesiacArok.Month && pacientovehosp.ElementAt(i).datum_do.Year == mesiacArok.Year))
                {
                    return true;
                }
            }
            return false; // ak to cele prejde a nenajde tak by to malo byt false
        }
        









    }


    



}
