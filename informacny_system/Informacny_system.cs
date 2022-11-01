using Hospital_information_sytem.informacny_system;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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

        public bool RegistrujNemocnicu(Nemocnica nem) 
        {
            if (nem != null)
            {
                this.databaza_nemocnic.Insert(nem.nazov_nemocnice, nem);
                return true;
            }
            return false;
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




        public void NacitajSystem()
        {
            Nemocnica nem = null;
            Pacient pacient = null;
            StreamReader reader = new StreamReader("ExportData.txt");
            StringBuilder builder = new StringBuilder();
            List<Pacient> listpac = new List<Pacient>();
            List<Hospitalizacia> listhosp = new List<Hospitalizacia>();
            
            try
            {
                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains("Nemocnica"))
                    {
                        if (nem != null)
                        {
                            List<Node<String, Pacient>> listNodePacNem = new List<Node<string, Pacient>>();
                            List<Node<String, Hospitalizacia>> listNodeHospNem = new List<Node<string, Hospitalizacia>>();
                            for (int i = 0; i < listpac.Count; i++)
                            {
                                Node<String, Pacient> pac = new Node<String, Pacient>(listpac.ElementAt(i).rod_cislo, listpac.ElementAt(i));
                                listNodePacNem.Add(pac);
                            }
                            for (int i = 0; i < listhosp.Count; i++)
                            {
                                Node<String, Hospitalizacia> hos = new Node<String, Hospitalizacia>(listhosp.ElementAt(i).id_hospitalizacie, listhosp.ElementAt(i));
                                listNodeHospNem.Add(hos);
                            }
                            nem.HromadnyInsertPacientov(listNodePacNem);
                            nem.HromadnyInsertHospitalizacii(listNodeHospNem);
                            

                            this.databaza_nemocnic.Insert(nem.nazov_nemocnice, nem);
                        }
                        nem = new Nemocnica();
                        nem.nazov_nemocnice = line;
                        
                    }
                    else if (line.Contains(';'))
                    {
                        var identifikator = line.Substring(0, line.IndexOf(';'));
                        var zvysok = line.Remove(0, identifikator.Length + 1); //aby oseklo az po bodkociarku
                        if (identifikator.Length == 10) //je to pacient
                        {
                            Pacient pac = new Pacient();
                            pac.rod_cislo = identifikator;
                            pac.meno = zvysok.Substring(0, zvysok.IndexOf(';'));
                            zvysok = zvysok.Remove(0, pac.meno.Length + 1);
                            pac.priezvisko = zvysok.Substring(0, zvysok.IndexOf(';'));
                            zvysok = zvysok.Remove(0, pac.priezvisko.Length + 1);
                            pac.kod_poistovne = zvysok.Substring(0, zvysok.IndexOf(';'));
                            zvysok = zvysok.Remove(0, pac.kod_poistovne.Length + 1);

                            pac.datum_narodenia = DateTime.Parse(zvysok.Substring(0, zvysok.Length),new CultureInfo("sk-SK"));
                            listpac.Add(pac);
                            //nem.PridajPacienta();
                            
                        }
                        else //hospitalizacia
                        {
                            Hospitalizacia hosp = new Hospitalizacia();
                            hosp.id_hospitalizacie = identifikator;
                            hosp.rod_cislo_pacienta = zvysok.Substring(0, zvysok.IndexOf(';'));
                            zvysok = zvysok.Remove(0, hosp.rod_cislo_pacienta.Length + 1);

                            hosp.nazov_diagnozy = zvysok.Substring(0, zvysok.IndexOf(';'));
                            zvysok = zvysok.Remove(0, hosp.nazov_diagnozy.Length + 1);
                            var datumod = zvysok.Substring(0, zvysok.IndexOf(';'));
                            zvysok = zvysok.Remove(0, datumod.Length + 1);
                            var datumdo = zvysok;

                            hosp.datum_od = DateTime.Parse(datumod, new CultureInfo("sk-SK"));
                            hosp.datum_do = DateTime.Parse(datumdo, new CultureInfo("sk-SK"));
                            for (int i = 0; i < listpac.Count; i++)
                            {
                                if (listpac.ElementAt(i).rod_cislo == hosp.rod_cislo_pacienta)
                                {
                                    listpac.ElementAt(i).PridajHosp(hosp);
                                }
                            }
                            listhosp.Add(hosp);
                            //pacient.PridajHosp(hosp);
                            
                        }
                    }
                    builder.AppendLine(line);
                   
                }
                // MOZEM TO ROBIT AJ JEDNOTLIVYM VKLADANIM HOSPITALIZACII A NASLEDNE PACIENTA DO NEMOCNICE - MUSELO BY VEDIET CITAT NASLEDUJUCI RIADOK INAK AKO JE TO TERAZ
                //insert poslednej
                List<Node<String, Pacient>> listNodePac = new List<Node<string, Pacient>>();
                List<Node<String, Hospitalizacia>> listNodeHosp = new List<Node<string, Hospitalizacia>>();
                for (int i = 0; i < listpac.Count; i++)
                {
                    Node<String, Pacient> pac = new Node<String, Pacient>(listpac.ElementAt(i).rod_cislo, listpac.ElementAt(i));
                    listNodePac.Add(pac);
                }
                for (int i = 0; i < listhosp.Count; i++)
                {
                    Node<String, Hospitalizacia> hos = new Node<String, Hospitalizacia>(listhosp.ElementAt(i).id_hospitalizacie, listhosp.ElementAt(i));
                    listNodeHosp.Add(hos);
                }
                nem.HromadnyInsertPacientov(listNodePac);
                nem.HromadnyInsertHospitalizacii(listNodeHosp);


                this.databaza_nemocnic.Insert(nem.nazov_nemocnice, nem);

                reader.Close();

            }
            catch (Exception ex)
            {

            }
                

        }

        public void ZapisSystem() 
        {
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter("ExportData.txt");
                List<Nemocnica> listNemocnic =  this.VratListNemocnic();
                for (int i = 0; i < listNemocnic.Count; i++)
                {
                    Nemocnica nem = listNemocnic.ElementAt(i);
                    writer.WriteLine(nem.nazov_nemocnice);
                    List<Pacient> listPacientovNemocnica = nem.VratListPacientov();
                    for (int j = 0; j < listPacientovNemocnica.Count; j++)
                    {
                        Pacient pac = listPacientovNemocnica.ElementAt(j);
                        writer.WriteLine(pac.rod_cislo + ";" + pac.meno + ";" + pac.priezvisko + ";" + pac.kod_poistovne + ";" + pac.datum_narodenia.ToString("dd.MM.yyyy"));
                        List<Hospitalizacia> listHospPacienta = pac.VratListHospitalizacii();
                        for (int k = 0; k < listHospPacienta.Count; k++)
                        {
                            Hospitalizacia hosp = listHospPacienta.ElementAt(k);
                            writer.WriteLine(hosp.id_hospitalizacie+";"+hosp.rod_cislo_pacienta+";"+hosp.nazov_diagnozy+";"+hosp.datum_od.ToString("dd.MM.yyyy")+";"+hosp.datum_do.ToString("dd.MM.yyyy"));
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally 
            {
                writer.Close();
            }
        }





    }
    
}
