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
        //Binary_search_tree<(String, String), Positovna> databaza_poist_nova = new Binary_search_tree<(string, string), Positovna>();
        public int VratPocetNemocnic()
        {
            return this.databaza_nemocnic.Size;
        }

        public bool PridajNemocnicu(String nazov)
        {
            if (nazov == string.Empty) { return false; }
            Nemocnica nemocnica = new Nemocnica();
            nemocnica.nazov_nemocnice = nazov;
            var pom = databaza_nemocnic.Insert(nazov, nemocnica);
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
            //var pomVZp = databaza_poist_nova.Insert((poistovnaVZP.nazov_poistovne, poistovnaVZP.kod_poistovne), poistovnaVZP);
            //var pomUnion = databaza_poist_nova.Insert((poistovnaUNION.nazov_poistovne, poistovnaUNION.kod_poistovne), poistovnaUNION);
            //var pomDovera = databaza_poist_nova.Insert((poistovnaDovera.nazov_poistovne, poistovnaDovera.kod_poistovne), poistovnaDovera);
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
                if (listVsetkychPacientov.ElementAt(i).VratListHospitalizacii().Count != 0)
                {
                    Hospitalizacia poslednaHospPacienta = listVsetkychPacientov.ElementAt(i).VratListHospitalizacii().Last();
                    if (poslednaHospPacienta.datum_do.Year == 0001)
                    {
                        listAktualneHospPacientov.Add(listVsetkychPacientov.ElementAt(i));
                    }
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

        public void InOrderOptimalizujPoistovne(Node<String, Positovna> parent)
        {
            if (parent != null)
            {
                InOrderOptimalizujPoistovne(parent.Left);
                parent.Data.Optimalizuj();
                //vyvaz aj stromy tohto nodu
                InOrderOptimalizujPoistovne(parent.Right);
            }
        }

        public void InOrderOptimalizujNemocnice(Node<String, Nemocnica> parent)
        {
            if (parent != null)
            {
                InOrderOptimalizujNemocnice(parent.Left);
                parent.Data.Optimalizuj();
                //vyvaz aj stromy tohto nodu
                InOrderOptimalizujNemocnice(parent.Right);
            }
        }
        public void Optimalizuj()
        {
            this.databaza_poistovni.Vyvaz(databaza_poistovni.Root);
            this.databaza_nemocnic.Vyvaz(databaza_nemocnic.Root);
            this.InOrderOptimalizujNemocnice(this.databaza_nemocnic.Root);
            //inorder prehliadkov optimalizovat kazdy
            this.InOrderOptimalizujPoistovne(databaza_poistovni.Root);

            /*for (int i = 0; i < databaza_poistovni.Size; i++)
            {

                List<Nemocnica> vsetkyNemocnice = databaza_nemocnic.ZapisVsetkyNody(databaza_nemocnic.Root);
                for (int j = 0; j < vsetkyNemocnice.Count; j++)
                {
                    vsetkyNemocnice.ElementAt(i).Optimalizuj();
                }

            }
            for (int i = 0; i < databaza_nemocnic.Size; i++)
            {

                List<Positovna> vsetkyPoistovne = databaza_poistovni.ZapisVsetkyNody(databaza_poistovni.Root);
                for (int j = 0; j < vsetkyPoistovne.Count; j++)
                {
                    vsetkyPoistovne.ElementAt(i).Optimalizuj();
                }
            }*/
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
                for (int j = 0; j < listPacientovVNemocnici.Count; j++)
                {
                    if (listPacientovVNemocnici.ElementAt(j).rod_cislo == rodCislo)
                    {
                        return listNemocnic.ElementAt(i);
                    }
                }
            }
            return null;
        }




        public void NacitajSystem(String nazovsuboru)
        {
            Nemocnica nem = null;
            Pacient pacient = null;
            StreamReader reader = new StreamReader(nazovsuboru);
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
                            List<Node<(DateTime, String), Hospitalizacia>> listNodeHospNem = new List<Node<(DateTime, String), Hospitalizacia>>();
                            List<Node<(DateTime, String), Hospitalizacia>> listNodeAktualneHospNem = new List<Node<(DateTime, String), Hospitalizacia>>();
                            List<Node<(String, String, String, String), Pacient>> listNodeAktualneHospPOIS_RCNem = new List<Node<(String, String, String, String), Pacient>>();
                            List<Node<(String, String, String, String), Pacient>> listNodeAktualneHospPOIS_P_M_RCNem = new List<Node<(String, String, String, String), Pacient>>();
                            for (int i = 0; i < listpac.Count; i++)
                            {
                                Node<String, Pacient> pac = new Node<String, Pacient>(listpac.ElementAt(i).rod_cislo, listpac.ElementAt(i));
                                listNodePacNem.Add(pac);
                            }
                            for (int i = 0; i < listhosp.Count; i++)
                            {
                                Node<(DateTime, String), Hospitalizacia> hos = new Node<(DateTime, String),
                                    Hospitalizacia>((listhosp.ElementAt(i).datum_od, listhosp.ElementAt(i).id_hospitalizacie
                                    ), listhosp.ElementAt(i));
                                listNodeHospNem.Add(hos);
                            }
                            for (int i = 0; i < listhosp.Count; i++)
                            {
                                Node<(DateTime, String), Hospitalizacia> hos =
                                    new Node<(DateTime, String),
                                    Hospitalizacia>((listhosp.ElementAt(i).datum_od, listhosp.ElementAt(i).id_hospitalizacie
                                    ), listhosp.ElementAt(i));
                                if (listNodeHospNem.ElementAt(i).Data.datum_do.Year == 0001)
                                {
                                    listNodeAktualneHospNem.Add(hos);
                                }

                            }

                            for (int i = 0; i < listNodeAktualneHospNem.Count; i++)
                            {
                                for (int j = 0; j < listNodePacNem.Count; j++)
                                {
                                    if (listNodeAktualneHospNem.ElementAt(i).Data.rod_cislo_pacienta == listNodePacNem.ElementAt(j).Data.rod_cislo)
                                    {
                                        Node<(String, String, String, String), Pacient> pac =
                                            new Node<(String, String, String, String),
                                                Pacient>((listNodePacNem.ElementAt(j).Data.kod_poistovne, listNodePacNem.ElementAt(j).Data.rod_cislo,
                                                    listNodePacNem.ElementAt(j).Data.priezvisko, listNodePacNem.ElementAt(j).Data.meno),
                                                        listNodePacNem.ElementAt(i).Data);
                                        Node<(String, String, String, String), Pacient> pac2 =
                                            new Node<(String, String, String, String),
                                                Pacient>((listNodePacNem.ElementAt(j).Data.kod_poistovne,
                                                    listNodePacNem.ElementAt(j).Data.priezvisko, listNodePacNem.ElementAt(j).Data.meno, listNodePacNem.ElementAt(j).Data.rod_cislo),
                                                        listNodePacNem.ElementAt(i).Data);

                                        listNodeAktualneHospPOIS_RCNem.Add(pac);
                                        listNodeAktualneHospPOIS_P_M_RCNem.Add(pac2);
                                    }
                                }

                            }


                            nem.HromadnyInsertPacientov(listNodePacNem, listNodeAktualneHospPOIS_RCNem, listNodeAktualneHospPOIS_P_M_RCNem);
                            nem.HromadnyInsertHospitalizacii(listNodeHospNem, listNodeAktualneHospNem);


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

                            pac.datum_narodenia = DateTime.Parse(zvysok.Substring(0, zvysok.Length), new CultureInfo("sk-SK"));
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
                List<Node<(DateTime, String), Hospitalizacia>> listNodeHosp = new List<Node<(DateTime, String), Hospitalizacia>>();
                List<Node<(DateTime, String), Hospitalizacia>> listNodeAktualneHosp = new List<Node<(DateTime, String), Hospitalizacia>>();
                List<Node<(String, String, String, String), Pacient>> listNodeAktualneHospPOIS_RC = new List<Node<(String, String, String, String), Pacient>>();
                List<Node<(String, String, String, String), Pacient>> listNodeAktualneHospPOIS_P_M_RC = new List<Node<(String, String, String, String), Pacient>>();
                for (int i = 0; i < listpac.Count; i++)
                {
                    Node<String, Pacient> pac = new Node<String, Pacient>(listpac.ElementAt(i).rod_cislo, listpac.ElementAt(i));
                    listNodePac.Add(pac);
                }
                for (int i = 0; i < listhosp.Count; i++)
                {
                    Node<(DateTime, String), Hospitalizacia> hos = new Node<(DateTime, String),
                        Hospitalizacia>((listhosp.ElementAt(i).datum_od, listhosp.ElementAt(i).id_hospitalizacie), listhosp.ElementAt(i));
                    listNodeHosp.Add(hos);
                }
                for (int i = 0; i < listhosp.Count; i++)
                {
                    Node<(DateTime, String), Hospitalizacia> hos =
                        new Node<(DateTime, String),
                        Hospitalizacia>((listhosp.ElementAt(i).datum_od, listhosp.ElementAt(i).id_hospitalizacie
                        ), listhosp.ElementAt(i));
                    if (listhosp.ElementAt(i).datum_do.Year == 0001)
                    {
                        listNodeAktualneHosp.Add(hos);
                    }

                }

                for (int i = 0; i < listNodeAktualneHosp.Count; i++)
                {
                    for (int j = 0; j < listNodePac.Count; j++)
                    {
                        if (listNodeAktualneHosp.ElementAt(i).Data.rod_cislo_pacienta == listNodePac.ElementAt(j).Data.rod_cislo)
                        {
                            Node<(String, String, String, String), Pacient> pac =
                                new Node<(String, String, String, String),
                                    Pacient>((listNodePac.ElementAt(j).Data.kod_poistovne, listNodePac.ElementAt(j).Data.rod_cislo,
                                        listNodePac.ElementAt(j).Data.priezvisko, listNodePac.ElementAt(j).Data.meno),
                                            listNodePac.ElementAt(i).Data);
                            Node<(String, String, String, String), Pacient> pac2 =
                                new Node<(String, String, String, String),
                                    Pacient>((listNodePac.ElementAt(j).Data.kod_poistovne,
                                        listNodePac.ElementAt(j).Data.priezvisko, listNodePac.ElementAt(j).Data.meno, listNodePac.ElementAt(j).Data.rod_cislo),
                                            listNodePac.ElementAt(i).Data);

                            listNodeAktualneHospPOIS_RC.Add(pac);
                            listNodeAktualneHospPOIS_P_M_RC.Add(pac2);
                        }
                    }

                }


                nem.HromadnyInsertPacientov(listNodePac, listNodeAktualneHospPOIS_RC, listNodeAktualneHospPOIS_P_M_RC);
                nem.HromadnyInsertHospitalizacii(listNodeHosp, listNodeAktualneHosp);


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
                writer = new StreamWriter("HOSP_SYS_DATA_NEMOCNICE.txt");
                List<Nemocnica> listNemocnic = this.VratListNemocnic();
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
                            writer.WriteLine(hosp.id_hospitalizacie + ";" + hosp.rod_cislo_pacienta + ";" + hosp.nazov_diagnozy + ";" + hosp.datum_od.ToString("dd.MM.yyyy") + ";" + hosp.datum_do.ToString("dd.MM.yyyy"));
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


        public void NacitajSystemPositovne(String nazovsuboru)
        {
            Positovna poistovna = null;
            //Pacient pacient = null;
            StreamReader reader = new StreamReader(nazovsuboru);
            StringBuilder builder = new StringBuilder();
            List<Poistenec> listpoist = new List<Poistenec>();

            try
            {
                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    if (!line.Contains(';'))
                    {
                        if (poistovna != null)
                        {
                            List<Node<String, Poistenec>> listNodePoistencov = new List<Node<string, Poistenec>>();
                            for (int i = 0; i < listpoist.Count; i++)
                            {
                                Node<String, Poistenec> poi = new Node<String, Poistenec>(listpoist.ElementAt(i).id_poistenca, listpoist.ElementAt(i));
                                listNodePoistencov.Add(poi);
                            }

                            poistovna.HromadnyInsertPoistencov(listNodePoistencov);


                            this.databaza_poistovni.Insert(poistovna.nazov_poistovne, poistovna);
                        }
                        poistovna = new Positovna();
                        poistovna.nazov_poistovne = line;

                        poistovna.kod_poistovne = line.Substring(0, 3).ToUpper();
                        switch (poistovna.kod_poistovne.ToString())
                        {
                            case "VŠE":
                                poistovna.kod_poistovne = "VZP";
                                break;
                            case "DÔV":
                                poistovna.kod_poistovne = "DOV";
                                break;
                            case "UNI":
                                break;
                            default:
                                break;
                        }

                    }
                    else if (line.Contains(';'))
                    {
                        var identifikator = line.Substring(0, line.IndexOf(';'));
                        var zvysok = line.Remove(0, identifikator.Length + 1); //aby oseklo az po bodkociarku

                        Poistenec poistenec = new Poistenec();
                        poistenec.id_poistenca = identifikator;
                        poistenec.rod_cislo_poistenca = zvysok.Substring(0, zvysok.IndexOf(';'));
                        listpoist.Add(poistenec);

                    }
                    builder.AppendLine(line);
                }
                // MOZEM TO ROBIT AJ JEDNOTLIVYM VKLADANIM HOSPITALIZACII A NASLEDNE PACIENTA DO NEMOCNICE - MUSELO BY VEDIET CITAT NASLEDUJUCI RIADOK INAK AKO JE TO TERAZ
                //insert poslednej
                List<Node<String, Poistenec>> listNodePois = new List<Node<string, Poistenec>>();
                for (int i = 0; i < listpoist.Count; i++)
                {
                    Node<String, Poistenec> poist = new Node<String, Poistenec>(listpoist.ElementAt(i).id_poistenca, listpoist.ElementAt(i));
                    listNodePois.Add(poist);
                }
                poistovna.HromadnyInsertPoistencov(listNodePois);


                this.databaza_poistovni.Insert(poistovna.nazov_poistovne, poistovna);

                reader.Close();

            }
            catch (Exception ex)
            {

            }
        }

        public void ZapisSystemPoistovna()
        {
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter("HOSP_SYS_DATA_POISTOVNE.txt");
                List<Positovna> listPoistovni = this.VratListPoistovni();
                for (int i = 0; i < listPoistovni.Count; i++)
                {
                    Positovna positovna = listPoistovni.ElementAt(i);
                    writer.WriteLine(positovna.nazov_poistovne);
                    List<Poistenec> listPoistencovPoistovna = positovna.VratListPoistencov();
                    for (int j = 0; j < listPoistencovPoistovna.Count; j++)
                    {
                        Poistenec pois = listPoistencovPoistovna.ElementAt(j);
                        writer.WriteLine(pois.id_poistenca + ";" + pois.rod_cislo_poistenca);
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




        public Binary_search_tree<(String, String, String, String), Pacient>  intervaloveVyhladavanie(Binary_search_tree<(String, String, String, String), Pacient> strom, String kod_poistovne)
        {
            strom.Vyvaz(strom.Root);
            (String, String) min = (kod_poistovne, "0000000000"); //minimalne rodne cislo
            (String, String) max = (kod_poistovne, "9999999999"); //maximalne rodne cislo
            Binary_search_tree<(String, String, String, String), Pacient> stromNaData = new Binary_search_tree<(String, String, String, String), Pacient>();
            if (strom.Root == null)
            {
                return null;
            }
            var current = strom.Root;
            Stack<Node<(String, String, String, String), Pacient >> s = new Stack<Node<(String, String, String, String), Pacient>>();
            while (current.Key.Item1.CompareTo(min.Item1) > 0 && current.Key.Item2.CompareTo(min.Item2) > 0 || current.Key.Item1.CompareTo(min.Item1) < 0 && current.Key.Item2.CompareTo(min.Item2) < 0 || s.Count > 0) //kym bude min nasledovnik currenta (min nebude obsahovat pismenko mensie ako je current)
            {
                while (current.Key.Item2.CompareTo(min.Item2) > 0 )
                {
                    s.Push(current);
                    current = current.Left;
                }
                current = s.Pop();
                stromNaData.Insert((current.Data.kod_poistovne,current.Data.rod_cislo,current.Data.priezvisko,current.Data.meno),current.Data);

                
                current = current.Right;
                if (current.Key.Item1.CompareTo(max.Item1) != 0)
                {
                    return null ;
                }

            }
            //Najdi najmensieho
            var lowest = current;
            while(current != null)
            {
                int compare = current.Key.Item1.CompareTo(kod_poistovne);
                if (compare == 0)
                {
                    if (lowest.Key.Item2.CompareTo(current.Key.Item2) > 0)
                    {
                        lowest = current;

                        current = current.Left;
                    }
                }
                else if (compare < 0)
                {
                    current = current.Right;
                }
                else
                {
                    current = current.Left;
                }
            }
            current = strom.Root;
            var bigest = current;
            while (current != null)
            {
                int compare = current.Key.Item1.CompareTo(kod_poistovne);
                if (compare == 0)
                {
                    if (bigest.Key.Item2.CompareTo(current.Key.Item2) < 0)
                    {
                        bigest = current;

                        current = current.Right;
                    }
                }
                else if (compare < 0)
                {
                    current = current.Right;
                }
                else
                {
                    current = current.Left;
                }
            }

            while (current != strom.Root)
            {

            }

            return stromNaData;
            
        
        
        }
    }
    
}
