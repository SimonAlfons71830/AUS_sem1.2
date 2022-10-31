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

       public bool VymazNemocnicu(Nemocnica nemocnica)
       {
            this.databaza_nemocnic.ExtractNodee(this.databaza_nemocnic.FindNode(nemocnica.nazov_nemocnice));
            return true;
       }

        public List<Pacient> VratAktualneHospitalizovanychPacientov(Nemocnica nemocnica) 
        {
            List<Pacient> listAktualneHospPacientov = new List<Pacient>();
            List<Pacient> listVsetkychPacientov = nemocnica.VratListPacientov();

            for (int i = 0; i < listVsetkychPacientov.Count; i++)
            {
                Hospitalizacia poslednaHospPacienta = listVsetkychPacientov.ElementAt(i).VratListHospitalizacii().Last();
                if (poslednaHospPacienta.datum_do.Year == 0001)
                {
                    listAktualneHospPacientov.Add(listVsetkychPacientov.ElementAt(i));
                }
            }
            return listAktualneHospPacientov;
        }
        public List<Pacient> VratAktualneHospitalizovanychPacientovPodlaPoistovne(String kod_poistovne, List<Pacient> aktualneHospitalizovani)
        {
            List<Pacient> listAktualneHospPacientovPOISTOVNA = new List<Pacient>();
            for (int i = 0; i < aktualneHospitalizovani.Count; i++)
            {
                if (aktualneHospitalizovani.ElementAt(i).kod_poistovne == kod_poistovne)
                {
                    listAktualneHospPacientovPOISTOVNA.Add(aktualneHospitalizovani.ElementAt(i));
                }
            }
            return listAktualneHospPacientovPOISTOVNA;
        }
        public void Oprimalizuj()
        {
            this.databaza_poistovni.Vyvaz(databaza_poistovni.Root);
            this.databaza_nemocnic.Vyvaz(databaza_nemocnic.Root);
            for (int i = 0; i < databaza_nemocnic.Size; i++)
            {
                List<Nemocnica> vsetkyNemocnice = databaza_nemocnic.ZapisVsetkyNody(databaza_nemocnic.Root);
                for (int j = 0; j < vsetkyNemocnice.Count; j++)
                {
                    vsetkyNemocnice.ElementAt(i).Optimalizuj();
                }
            }
            for (int i = 0; i < databaza_poistovni.Size; i++)
            {
                List<Positovna> vsetkyPoistovne = databaza_poistovni.ZapisVsetkyNody(databaza_poistovni.Root);
                for (int j = 0; j < vsetkyPoistovne.Count; j++)
                {
                    vsetkyPoistovne.ElementAt(i).Optimalizuj();
                }
            }
        }

        public List<Pacient> VsetciPacientiHospVDanyMesiac(DateTime mesiacArok) 
        {
            List<Pacient> vsetciPacientiHospitalizovaniVDanyMesiac = new List<Pacient>();
            List<Nemocnica> listNemocnic = this.VratListNemocnic();
            for (int i = 0; i < listNemocnic.Count; i++)
            {
                List<Pacient> hospPacientov = listNemocnic.ElementAt(i).VratListPacientovHospVMesiac(mesiacArok); 
                if (hospPacientov.Count > 0)
                {
                    for (int j = 0; j < hospPacientov.Count; j++)
                    {
                        vsetciPacientiHospitalizovaniVDanyMesiac.Add(hospPacientov.ElementAt(j));
                    }
                    //vsetciPacientiHospitalizovaniVDanyMesiac.AddRange(listNemocnic.ElementAt(i).VratListPacientovHospVMesiac(mesiacArok));
                }
            }
            return vsetciPacientiHospitalizovaniVDanyMesiac;
        }

        public List<Pacient> PacientiPodlaPoistovnePreFormular(List<Pacient> listVsetkychPacientovMesiac, String kod_poistovne) 
        {
            List<Pacient> pacientiPodlaPoistovne = new List<Pacient>();
            for (int i = 0; i < listVsetkychPacientovMesiac.Count; i++)
            {
                if (listVsetkychPacientovMesiac.ElementAt(i).kod_poistovne == kod_poistovne)
                {
                    pacientiPodlaPoistovne.Add(listVsetkychPacientovMesiac.ElementAt(i));
                }
            }
            return pacientiPodlaPoistovne;

        }
        public Nemocnica NajdiNemocnicuPacientovi(String rodCislo) 
        {
            List<Nemocnica> listNemocnic = this.VratListNemocnic();
            for (int i = 0; i < listNemocnic.Count; i++)
            {
                List<Pacient> listPacientovVNemocnici = listNemocnic.ElementAt(i).VratListPacientov();
                for (int j = 0;  j < listPacientovVNemocnici.Count;  j++)
                {
                    if (listPacientovVNemocnici.ElementAt(j).rod_cislo == rodCislo)
                    {
                        return listNemocnic.ElementAt(i);
                    }
                }
            }
            return null;
        }




    }
    
}
