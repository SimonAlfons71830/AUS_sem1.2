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
        public Binary_search_tree<String, Pacient> pacienti = new Binary_search_tree<string, Pacient>();
        public Binary_search_tree<(String, String, String, String), Pacient> pac_podla_mena = new Binary_search_tree<( String, String, String, String), Pacient>();
        //Binary_search_tree<String, Hospitalizacia> hospitalizacie = new Binary_search_tree<string, Hospitalizacia>();
        public Binary_search_tree<(DateTime,String), Hospitalizacia> hospitalizacie_nove = new Binary_search_tree<(DateTime, String), Hospitalizacia>();

        public Binary_search_tree<(DateTime, String), Hospitalizacia> aktualne_hospitalizacie = new Binary_search_tree<(DateTime, String), Hospitalizacia>();
        public Binary_search_tree<(String, String, String, String), Pacient> aktualne_hospitalizovani_POIS_RC = new Binary_search_tree<(String, String, String, String), Pacient>();
        public Binary_search_tree<(String,String, String,String), Pacient> aktualne_hospitalizovani_POIS_Priezv_men_RC = new Binary_search_tree<(String, String, String, String), Pacient>();




        public bool PridajPacienta(String meno, String priezvisko, String rod_cislo, DateTime datum_narodenia, String kod_poistovne, String nazov_nemocnice)
        {
            if (rod_cislo == string.Empty || priezvisko == string.Empty || meno == string.Empty || datum_narodenia == null ||
                kod_poistovne == string.Empty || nazov_nemocnice == string.Empty) { return false; }
            Pacient pacient = new Pacient();
            pacient.rod_cislo = rod_cislo;
            pacient.datum_narodenia = datum_narodenia;
            pacient.meno = meno;
            pacient.priezvisko = priezvisko;
            pacient.kod_poistovne = kod_poistovne;
            
            var pom = this.pacienti.Insert(rod_cislo, pacient);

            (String, String, String, String) keyPac = (pacient.priezvisko, pacient.meno, pacient.rod_cislo, pacient.kod_poistovne);
            var pompom = this.pac_podla_mena.Insert(keyPac,pacient);
            

            if (pompom == null) { return false; } 
            return true;

        }

        

        public void TraverseInOrder(Node<(String,String,String),Pacient> parent, Binary_search_tree<(String,String,String),Pacient> strom, (String, String) key)
        {
            if (parent != null)
            {
                TraverseInOrder(parent.Left,strom,key);
                if (parent.Key.Item1.CompareTo(key.Item1) == 0 && parent.Key.Item2.CompareTo(key.Item2)==0)
                {
                    strom.Insert((key.Item1, key.Item2, parent.Key.Item3), parent.Data);
                }
                Console.Write(parent.Data );
                TraverseInOrder(parent.Right,strom,key);
            }
        }

        public void InOrderMeno(Node<(String, String, String, String), Pacient> parent, Binary_search_tree<(String,  String, String, String), Pacient> strom, (String, String) key) 
        {
            if ( parent == null)
            {
                return;
            }


            Stack< Node<(String, String, String, String), Pacient> > s = new Stack<Node<(String, String, String, String), Pacient>>();
            Node<(String, String, String, String), Pacient> curr = parent;

            
            while (curr != null || s.Count > 0)
            {

                while (curr != null)
                {
                    s.Push(curr);
                    curr = curr.Left;
                }
                curr = s.Pop();

                if (curr.Key.Item1.CompareTo(key.Item1) == 0 && curr.Key.Item2.CompareTo(key.Item2) == 0)
                {
                    strom.Insert((key.Item1, key.Item2, curr.Data.rod_cislo, curr.Data.kod_poistovne), curr.Data);
                }

                curr = curr.Right;
            }


        }
        
        public Binary_search_tree<(String,String, String, String), Pacient> NajdiPacientPodlaMena(String priezvisko, String meno)
        {
            Binary_search_tree<(String, String, String, String), Pacient> pacientiPodlaMena = new Binary_search_tree<(String, String, String, String), Pacient>();
            (String, String) key = (priezvisko, meno);
            var current = this.pac_podla_mena.Root;
            while (current != null)
            {
                int compare = current.Key.Item1.CompareTo(key.Item1);
                if (compare == 0)
                {
                    
                    while (current != null)
                    {
                        compare = current.Key.Item2.CompareTo(key.Item2);
                        if (compare == 0)
                        {
                            this.InOrderMeno(current, pacientiPodlaMena,key);
                            return pacientiPodlaMena;
                        }
                        if (compare<0)
                        {
                            current = current.Right;
                        }
                        else
                        {
                            current=current.Left;
                        }
                    }
                    
                }        
                    //return current.Data;
                if (compare < 0)
                    current = current.Right;
                else
                    current = current.Left;
            }
            if (pacientiPodlaMena.Size != 0)
            {
                return pacientiPodlaMena;
            }
            else
            {
                return null;
            }
        }

        public Pacient NajdiPacient(String rod_cislo)
        {
            if (rod_cislo == string.Empty) { return null; }
            if (pacienti.FindNode(rod_cislo) == null)
            {
                return null;
            }
            else
            {
                var pom = pacienti.FindNode(rod_cislo).Data;
                return pom;
            }
            
        }

        public List<Pacient> VratListPacientov()
        {
            return this.pac_podla_mena.ZapisVsetkyNody(pac_podla_mena.Root);
        }
        public List<Pacient> VratListAktualneHospPacientovPoslaPoisRC()
        {
            return this.aktualne_hospitalizovani_POIS_RC.ZapisVsetkyNody(aktualne_hospitalizovani_POIS_RC.Root);
        }
        public List<Pacient> VratListAktualneHospPacientovPoslaPoisMena()
        {
            return this.aktualne_hospitalizovani_POIS_Priezv_men_RC.ZapisVsetkyNody(aktualne_hospitalizovani_POIS_RC.Root);
        }

        public void TraverseInOrderPreAktualneHosp(Node<(DateTime,String), Hospitalizacia> parent)
        {
            if (parent != null)
            {
                TraverseInOrderPreAktualneHosp(parent.Left);
                if (parent.Data.datum_do.Year == 0001)
                {
                    (DateTime, String) key = (parent.Data.datum_od, parent.Data.rod_cislo_pacienta);
                    this.aktualne_hospitalizacie.Insert(key,parent.Data);
                }
                TraverseInOrderPreAktualneHosp(parent.Right);
            }
        }


        public bool PridajHospi(Hospitalizacia hosp)
        {
            if (hosp != null)
            {
                (DateTime, String) keyHosp = (hosp.datum_od,hosp.id_hospitalizacie);
                //this.hospitalizaciePacienta.Add(hosp);
                this.hospitalizacie_nove.Insert(keyHosp, hosp);
                return true;
            }
            return false;
        }

        public Hospitalizacia NajdiHospitalizaciu(String id_hospitalizacie)
        {
            int rok = Int32.Parse(id_hospitalizacie.Substring(4, 4));
            int mesiac = Int32.Parse(id_hospitalizacie.Substring(2, 2));
            int den = Int32.Parse(id_hospitalizacie.Substring(0, 2));
            DateTime dateTime = new DateTime(Int32.Parse(id_hospitalizacie.Substring(4, 4)), 
                Int32.Parse(id_hospitalizacie.Substring(2, 2)),
                Int32.Parse(id_hospitalizacie.Substring(0, 2))
                );
            String rod_cislo = id_hospitalizacie.Substring(8, 10);

            if (id_hospitalizacie == string.Empty) { return null; }
            var pom = hospitalizacie_nove.FindNode((dateTime,id_hospitalizacie)).Data;
            if (pom == null) { return null; } else { return pom; }
        }

        public List<Hospitalizacia> VratListHospitalizacii()
        {
            return this.hospitalizacie_nove.ZapisVsetkyNody(hospitalizacie_nove.Root);
        }
        public List<Hospitalizacia> VratListAktivnychHospitalizacii()
        {
            return this.aktualne_hospitalizacie.ZapisVsetkyNody(aktualne_hospitalizacie.Root);
        }
        public List<Node<String, Pacient>> VratMazanychPacientovAkoListNodov() 
        {
            List<Pacient> listRusenychPacientov =this.VratListPacientov();
            List<Node<String, Pacient>> listNodovPac= new List<Node<String, Pacient>>();

            for (int i = 0; i < listRusenychPacientov.Count; i++)
            {
                Node<String, Pacient> nodePacient = this.pacienti.FindNode(listRusenychPacientov.ElementAt(i).rod_cislo);
                listNodovPac.Add(nodePacient);
            }

            return listNodovPac;
        }

        public List<Node<(String,String,String,String), Pacient>> VratMazanychAktualnychPacientovAkoListNodovPrePoisRC()
        {
            List<Pacient> listRusenychPacientov = this.VratListAktualneHospPacientovPoslaPoisRC();
            List<Node<(String, String, String, String), Pacient>> listNodovPac = new List<Node<(String, String, String, String), Pacient>>();

            for (int i = 0; i < listRusenychPacientov.Count; i++)
            {
                Node<(String, String, String, String), Pacient> nodePacient = this.aktualne_hospitalizovani_POIS_RC.FindNode((listRusenychPacientov.ElementAt(i).kod_poistovne,
                    listRusenychPacientov.ElementAt(i).rod_cislo, listRusenychPacientov.ElementAt(i).priezvisko, listRusenychPacientov.ElementAt(i).meno));
                listNodovPac.Add(nodePacient);
            }

            return listNodovPac;
        }


        public List<Node<(String, String, String, String), Pacient>> VratMazanychAktualnychPacientovAkoListNodovPrePoisMENO()
        {
            List<Pacient> listRusenychPacientov = this.VratListAktualneHospPacientovPoslaPoisMena();
            List<Node<(String, String, String, String), Pacient>> listNodovPac = new List<Node<(String, String, String, String), Pacient>>();

            for (int i = 0; i < listRusenychPacientov.Count; i++)
            {
                Node<(String, String, String, String), Pacient> nodePacient = this.aktualne_hospitalizovani_POIS_Priezv_men_RC.FindNode((listRusenychPacientov.ElementAt(i).kod_poistovne,
                    listRusenychPacientov.ElementAt(i).priezvisko, listRusenychPacientov.ElementAt(i).meno, listRusenychPacientov.ElementAt(i).rod_cislo));
                listNodovPac.Add(nodePacient);
            }

            return listNodovPac;
        }
        public List<Node<(String, String, String, String), Pacient>> VratMazanychPacientovAkoListNodov2()
        {
            List<Pacient> listRusenychPacientov = this.VratListPacientov();
            List<Node<(String,String,String, String), Pacient>> listNodovPac = new List<Node<(String, String, String, String), Pacient>>();

            for (int i = 0; i < listRusenychPacientov.Count; i++)
            {
                Node<(String, String, String, String), Pacient> nodePacient = this.pac_podla_mena.FindNode((listRusenychPacientov.ElementAt(i).priezvisko, listRusenychPacientov.ElementAt(i).meno, listRusenychPacientov.ElementAt(i).rod_cislo,listRusenychPacientov.ElementAt(i).kod_poistovne));
                listNodovPac.Add(nodePacient);
            }

            return listNodovPac;
        }
        public List<Node<(DateTime, String), Hospitalizacia>> VratMazaneHospitalizacieAkoListNodov() 
        {
            List<Hospitalizacia> listRusenychHospitalizacii = this.VratListHospitalizacii();

            List<Node<(DateTime, String), Hospitalizacia>> listNodovHsop = new List<Node<(DateTime, String), Hospitalizacia>>();

            for (int i = 0; i < listRusenychHospitalizacii.Count; i++)
            {
                Node<(DateTime, String), Hospitalizacia> nodeHospitalizacie =
                    this.hospitalizacie_nove.FindNode((listRusenychHospitalizacii.ElementAt(i).datum_od,listRusenychHospitalizacii.ElementAt(i).id_hospitalizacie)) ;
                listNodovHsop.Add(nodeHospitalizacie);
            }

            return listNodovHsop;
        }

        public List<Node<(DateTime, String), Hospitalizacia>> VratMazaneAktualneHospitalizacieAkoListNodov()
        {
            List<Hospitalizacia> listRusenychAktualnychHospitalizacii = this.VratListAktivnychHospitalizacii();

            List<Node<(DateTime, String), Hospitalizacia>> listNodovHsop = new List<Node<(DateTime, String), Hospitalizacia>>();

            for (int i = 0; i < listRusenychAktualnychHospitalizacii.Count; i++)
            {
                Node<(DateTime, String), Hospitalizacia> nodeHospitalizacie =
                    this.aktualne_hospitalizacie.FindNode((listRusenychAktualnychHospitalizacii.ElementAt(i).datum_od, listRusenychAktualnychHospitalizacii.ElementAt(i).id_hospitalizacie));
                listNodovHsop.Add(nodeHospitalizacie);
            }

            return listNodovHsop;
        }




        public void HromadnyInsertPacientov(List<Node<String,Pacient>> listPacientov, List<Node<(String,String,String,String), Pacient>> listAktPacPoiRC, List<Node<(String, String, String, String), Pacient>> listAktPacPOIMENO) 
        {
            
            this.pacienti.ZapisMedianDoQueueList(listPacientov);
            List<Node<(String, String, String, String), Pacient>> listNodePacientov = new List<Node<(string, string, string, String), Pacient>>();
            for (int i = 0; i < listPacientov.Count; i++)
            {
                
                var key = (listPacientov.ElementAt(i).Data.priezvisko, listPacientov.ElementAt(i).Data.meno, listPacientov.ElementAt(i).Data.rod_cislo, listPacientov.ElementAt(i).Data.kod_poistovne);
                var node = new Node<(String, String, String, String), Pacient>(key, listPacientov.ElementAt(i).Data);
                listNodePacientov.Add(node);
            }
            this.pac_podla_mena.ZapisMedianDoQueueList(listNodePacientov);
            this.aktualne_hospitalizovani_POIS_RC.ZapisMedianDoQueueList(listAktPacPoiRC);
            this.aktualne_hospitalizovani_POIS_Priezv_men_RC.ZapisMedianDoQueueList(listAktPacPOIMENO);

        }
        public void HromadnyInsertPacientovNovyStrom(List<Node<(String,String,String, String), Pacient>> listPacientov)
        {
            this.pac_podla_mena.ZapisMedianDoQueueList(listPacientov);
            //this.pac_podla_mena.ZapisMedianDoQueueList(listPacientov);

        }

        public void HromadnyInsertHospitalizacii(List<Node<(DateTime,String ), Hospitalizacia>> listHospitalizacii, List<Node<(DateTime, String), Hospitalizacia>> listaktualnychHospitalizacii)
        {
            this.hospitalizacie_nove.ZapisMedianDoQueueList(listHospitalizacii);
            this.aktualne_hospitalizacie.ZapisMedianDoQueueList(listaktualnychHospitalizacii);
        }
        



        public void InOrderOptimalizujHospitalizaciePacienta(Node<String, Pacient> parent)
        {
            if (parent != null)
            {
                InOrderOptimalizujHospitalizaciePacienta(parent.Left);
                parent.Data.Optimalizuj();
                //vyvaz aj stromy tohto nodu
                InOrderOptimalizujHospitalizaciePacienta(parent.Right);
            }
        }
        public void InOrderOptimalizujHospitalizaciePacienta2(Node<(String,String,String), Pacient> parent)
        {
            if (parent != null)
            {
                InOrderOptimalizujHospitalizaciePacienta2(parent.Left);
                parent.Data.Optimalizuj();
                //vyvaz aj stromy tohto nodu
                InOrderOptimalizujHospitalizaciePacienta2(parent.Right);
            }
        }
        public void Optimalizuj() 
        {
            this.pac_podla_mena.Vyvaz(this.pac_podla_mena.Root);
            
            this.pacienti.Vyvaz(this.pacienti.Root);
            this.InOrderOptimalizujHospitalizaciePacienta(this.pacienti.Root);
            //this.InOrderOptimalizujHospitalizaciePacienta2(this.pac_podla_mena.Root);
            this.hospitalizacie_nove.Vyvaz(this.hospitalizacie_nove.Root);
        }

        public List<Pacient> VratListPacientovHospVMesiac(DateTime mesiacArok) 
        {
            List<Pacient> pacientiHospitalizovaniVDanyMesiac = new List<Pacient>();
            List<Pacient> listPacientovTejtoNemocnice = this.VratListPacientov();
            for (int i = 0; i < listPacientovTejtoNemocnice.Count; i++)
            {
                if (listPacientovTejtoNemocnice.ElementAt(i).BolHospitalizovanyTentoMesiac(mesiacArok))
                {
                    pacientiHospitalizovaniVDanyMesiac.Add(listPacientovTejtoNemocnice.ElementAt(i));
                }
            }
            return pacientiHospitalizovaniVDanyMesiac;

        }






    }
    
}
