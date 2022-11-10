using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using Faker;
using Hospital_information_sytem.structures;

namespace Hospital_information_sytem.informacny_system
{
    internal class Data_Generator
    {
        private Random _random = new Random();
        List<String> pouziteNazvyNemocnic = new List<string>();
        List<Pacient> generovanyPacienti = new List<Pacient>();
        public List<String> aktivneHosp = new List<String>();
        Hospitalizacia hospitalizacia1 = new Hospitalizacia();


        

        public void GenerujPacientaVPoistenca(Informacny_system inf_system) 
        {
            Pacient pacient = new Pacient();
            pacient.meno = Faker.Name.First();
            pacient.priezvisko = Faker.Name.Last();
            pacient.datum_narodenia = Faker.Identification.DateOfBirth();
            string rok = pacient.datum_narodenia.Year.ToString().Substring(2, 2);
            int mesiac;
            if (pacient.meno.EndsWith("a"))
            {
                mesiac = pacient.datum_narodenia.Month + 50;
            }
            else
            {
                mesiac = pacient.datum_narodenia.Month;
            }
            String mesiacc = mesiac.ToString();
            if (mesiacc.Length == 1)
            {
                mesiacc = "0" + mesiacc;
            }
            string den = pacient.datum_narodenia.Day.ToString();
            if (den.Length == 1)
            {
                den = "0" + den;
            }
            //random stvorcislie za lomitkom
            String zaLomkou = _random.Next(0, 9999).ToString("D4");

            pacient.rod_cislo= rok + mesiacc + den + zaLomkou;
            int kod = _random.Next(1, 4);
            if (kod == 1)
            {
                pacient.kod_poistovne = "VZP";
            }
            else if (kod == 2)
            {
                pacient.kod_poistovne = "UNI";
            }
            else if (kod == 3) {
                pacient.kod_poistovne = "DOV";
            }
            List<Nemocnica> nemocnice = inf_system.VratListNemocnic();
            int randNemocnica = _random.Next(nemocnice.Count);
            
            Nemocnica nemocnica = inf_system.NajdiNemocnicu(nemocnice.ElementAt(randNemocnica).nazov_nemocnice);
            if (nemocnica != null)
            {
                nemocnica.PridajPacienta(pacient.meno, pacient.priezvisko, pacient.rod_cislo, pacient.datum_narodenia, pacient.kod_poistovne, nemocnica.nazov_nemocnice);
            }
            List<Positovna> poistovne = inf_system.VratListPoistovni();
            for (int i = 0; i < poistovne.Count; i++)
            {
                if (poistovne.ElementAt(i).kod_poistovne == pacient.kod_poistovne )
                {
                    Positovna poistovna = inf_system.NajdiPoistovnu(poistovne.ElementAt(i).nazov_poistovne);
                    poistovna.PridajPoistenca(pacient.rod_cislo);
                }
            }
            this.generovanyPacienti.Add(pacient);
        }
        
        public void GenerujNemocnicu(Informacny_system inf_system) 
        {
            string nazov_nemocnice = Faker.Company.Name() + " Nemocnica";
            //Nemocnica nemocnica = new Nemocnica();
            //nemocnica.nazov_nemocnice = nazov_nemocnice;
            inf_system.PridajNemocnicu(nazov_nemocnice);
            this.pouziteNazvyNemocnic.Add(nazov_nemocnice);
        }

        public void GenerujHospitalizaciu(Informacny_system informacny_system)
        {
            
           
            //hospitalizacia.PridajDiagnozy();
            //hospitalizacia.LoadDataFromFile();
            Nemocnica nem = informacny_system.NajdiNemocnicu(this.pouziteNazvyNemocnic.ElementAt(_random.Next(this.pouziteNazvyNemocnic.Count)));
            List<Pacient> aktualnyPacientiNemocnice = nem.VratListPacientov();
            Pacient pac = aktualnyPacientiNemocnice.ElementAt(_random.Next(aktualnyPacientiNemocnice.Count));
            var poslednaHospPacienta = pac.VratPoslednuHospitalizaciu();
            DateTime start;
            if (poslednaHospPacienta != null)
            {
                //ukonci predoslu a zacne dalsiu alebo rovno zacne dalsiu
                if (poslednaHospPacienta.Data.datum_do.Year == 0001)
                {
                    DateTime koniec;
                    koniec = new DateTime(poslednaHospPacienta.Data.datum_od.Year,
                        poslednaHospPacienta.Data.datum_od.Month,
                        poslednaHospPacienta.Data.datum_od.Day);
                    var randDatKoniec = koniec.AddDays(_random.Next(3, 15));
                    if (randDatKoniec >= DateTime.Today)
                    {
                        return;
                    }
                    //var haldana = nem.hospitalizacie_nove.FindNode(poslednaHospPacienta.Key);
                    
                   /* var pompom = nem.aktualne_hospitalizacie.FindNode(poslednaHospPacienta.Key);
                    var size = nem.aktualne_hospitalizacie.Size;
                    nem.aktualne_hospitalizacie.ExtractNodee(pompom);*/
                    


                    poslednaHospPacienta.Data.datum_do = randDatKoniec;
                    
                    
                }
                start = new DateTime(poslednaHospPacienta.Data.datum_do.Year,
                        poslednaHospPacienta.Data.datum_do.Month,
                        poslednaHospPacienta.Data.datum_do.Day).AddDays(1);
                //prida jeden den aby hospitalizacie nezacinali v ten isty den ako skoncia 
            }
            else //pacientoveHospitalizacie.Count == 0
            { 
                //RANDOM DATE OD NARODENIA PACIENTA AZ PO DNES NA ZACATIE HOSPITALIZACIE
                start = new DateTime(pac.datum_narodenia.Year, pac.datum_narodenia.Month, pac.datum_narodenia.Day);
            }
            int range = (DateTime.Today - start).Days;
            if(range < 0) 
            { 
                range =+ 2; 
            }
            var randDat = start.AddDays(_random.Next(range));

            String denID;
            if (randDat.Day < 10)
            {
                denID = "0" + randDat.Day.ToString();
            }
            else
            {
                denID = randDat.Day.ToString();
            }

            String mesiacID;
            if (randDat.Month < 10)
            {
                mesiacID = "0" + randDat.Month.ToString();
            }
            else
            {
                mesiacID = randDat.Month.ToString();
            }
            Hospitalizacia hosp = new Hospitalizacia();
            hosp.LoadDataFromFile();
            String rokID = randDat.Year.ToString();
            String id_hospitalizacie = denID + mesiacID + rokID + pac.rod_cislo;
            hosp.id_hospitalizacie = id_hospitalizacie;
            List<String> diagnozy = hosp.VratListDiagnoz();
            String diagnozaNazov = diagnozy.ElementAt(_random.Next(diagnozy.Count));
            hosp.nazov_diagnozy = diagnozaNazov;
            hosp.datum_od = randDat;
            hosp.rod_cislo_pacienta = pac.rod_cislo;

            pac.pacientove_hosp.Insert((hosp.datum_od,hosp.id_hospitalizacie),hosp);
            nem.hospitalizacie_nove.Insert((hosp.datum_od,hosp.id_hospitalizacie),hosp);
            //nem.aktualne_hospitalizacie.Insert((hosp.datum_od, hosp.id_hospitalizacie), hosp);
            //nem.aktualne_hospitalizacie.Insert((hosp.datum_od,hosp.id_hospitalizacie),hosp);
        }


        public void GenerujHospitalizaciu2(Informacny_system informacny_system) 
        {
            Nemocnica nem = informacny_system.NajdiNemocnicu(this.pouziteNazvyNemocnic.ElementAt(_random.Next(this.pouziteNazvyNemocnic.Count)));
            List<Pacient> aktualnyPacientiNemocnice = nem.VratListPacientov();
            Pacient pac = aktualnyPacientiNemocnice.ElementAt(_random.Next(aktualnyPacientiNemocnice.Count));
            var poslednaHospPacienta = pac.VratPoslednuHospitalizaciu();
            DateTime start;
            DateTime randDatStart;
            Hospitalizacia hosp = new Hospitalizacia();
            if (poslednaHospPacienta == null)
            {
                start = new DateTime(pac.datum_narodenia.Year, pac.datum_narodenia.Month, pac.datum_narodenia.Day);
            }
            else
            {
                start = new DateTime(poslednaHospPacienta.Data.datum_do.Year,
                        poslednaHospPacienta.Data.datum_do.Month,
                        poslednaHospPacienta.Data.datum_do.Day).AddDays(1);
            }
                int range = (DateTime.Today - start).Days;
            if (range < 0)
            {
                range = +2;
            }
            randDatStart = start.AddDays(_random.Next(range));

                String denID;
            if (randDatStart.Day < 10)
            {
                denID = "0" + randDatStart.Day.ToString();
            }
            else
            {
                denID = randDatStart.Day.ToString();
            }

                String mesiacID;
            if (randDatStart.Month < 10)
            {
                mesiacID = "0" + randDatStart.Month.ToString();
            }
            else
            {
                mesiacID = randDatStart.Month.ToString();
            }
            hosp.LoadDataFromFile();
            List<String> diagnozy = hosp.VratListDiagnoz();


            DateTime koniec;
            /*koniec = new DateTime(poslednaHospPacienta.Data.datum_od.Year,
                poslednaHospPacienta.Data.datum_od.Month,
                poslednaHospPacienta.Data.datum_od.Day);*/
            var randDatKoniec = start.AddDays(_random.Next(3, 15));

            if (randDatStart.Date == DateTime.Today)
            {
                return;
            }
            if (randDatKoniec.Date >= DateTime.Today)
            {
                return;
            }

            hosp.datum_od = randDatStart;
            hosp.datum_do = randDatKoniec;
            hosp.rod_cislo_pacienta = pac.rod_cislo;
            hosp.id_hospitalizacie = denID + mesiacID + randDatStart.Year.ToString()+ pac.rod_cislo;
            hosp.nazov_diagnozy = diagnozy.ElementAt(_random.Next(diagnozy.Count));
            pac.PridajHosp(hosp);
            nem.PridajHospi(hosp);
            pac.pacientove_hosp.Vyvaz(pac.pacientove_hosp.Root);
        }


        public void GenerujNeukoncenuHospitalizaciu(Informacny_system informacny_system) 
        {
            Nemocnica nem = informacny_system.NajdiNemocnicu(this.pouziteNazvyNemocnic.ElementAt(_random.Next(this.pouziteNazvyNemocnic.Count)));
            List<Pacient> aktualnyPacientiNemocnice = nem.VratListPacientov();
            Pacient pac = aktualnyPacientiNemocnice.ElementAt(_random.Next(aktualnyPacientiNemocnice.Count));
            var poslednaHospPacienta = pac.VratPoslednuHospitalizaciu();
            DateTime start;
            DateTime randDatStart;
            Hospitalizacia hosp = new Hospitalizacia();
            if (poslednaHospPacienta == null)
            {
                start = new DateTime(pac.datum_narodenia.Year, pac.datum_narodenia.Month, pac.datum_narodenia.Day);
            } else if (poslednaHospPacienta.Data.datum_do.Year == 0001) 
            {
                return;
            }
            else
            {
                start = new DateTime(poslednaHospPacienta.Data.datum_do.Year,
                        poslednaHospPacienta.Data.datum_do.Month,
                        poslednaHospPacienta.Data.datum_do.Day).AddDays(1);
            }
            int range = (DateTime.Today - start).Days;
            if (range < 0)
            {
                range = +2;
            }
            randDatStart = start.AddDays(_random.Next(range));

            String denID;
            if (randDatStart.Day < 10)
            {
                denID = "0" + randDatStart.Day.ToString();
            }
            else
            {
                denID = randDatStart.Day.ToString();
            }

            String mesiacID;
            if (randDatStart.Month < 10)
            {
                mesiacID = "0" + randDatStart.Month.ToString();
            }
            else
            {
                mesiacID = randDatStart.Month.ToString();
            }
            hosp.LoadDataFromFile();
            List<String> diagnozy = hosp.VratListDiagnoz();


            if (randDatStart.Date == DateTime.Today)
            {
                return;
            }
            

            hosp.datum_od = randDatStart;
            hosp.rod_cislo_pacienta = pac.rod_cislo;
            hosp.id_hospitalizacie = denID + mesiacID + randDatStart.Year.ToString() + pac.rod_cislo;
            hosp.nazov_diagnozy = diagnozy.ElementAt(_random.Next(diagnozy.Count));
            pac.PridajHosp(hosp);
            nem.PridajHospi(hosp);
            nem.aktualne_hospitalizacie.Insert((hosp.datum_od, hosp.id_hospitalizacie), hosp);
            nem.aktualne_hospitalizovani_POIS_RC.Insert((pac.kod_poistovne, pac.rod_cislo, pac.priezvisko, pac.meno),pac);
            nem.aktualne_hospitalizovani_POIS_Priezv_men_RC.Insert((pac.kod_poistovne, pac.priezvisko, pac.meno, pac.rod_cislo), pac);
        }

        /*public void UkonciHospitalizaciuPacienta(Pacient pacient, Nemocnica nemocnica) 
        {
            Hospitalizacia hosp = pacient.VratListHospitalizacii().Last();
            
            
            List<Hospitalizacia> hospNemoList = nemocnica.VratListHospitalizacii();
            Hospitalizacia hospNem = new Hospitalizacia();
            for (int i = 0; i < hospNemoList.Count; i++)
            {
                if (hospNemoList.ElementAt(i).rod_cislo_pacienta == pacient.rod_cislo)
                {
                    hospNem = hospNemoList.ElementAt(i);
                }
            }
            DateTime koniec;
            koniec = new DateTime(hosp.datum_od.Year, hosp.datum_od.Month, hosp.datum_od.Day);
            //int range = (DateTime.Today - koniec).Days;
            //3-14 dni moze trvat hospitalizacia
            var randDat = koniec.AddDays(_random.Next(3,15));
            if (randDat >= DateTime.Today)
            {
                return;
            }
            hosp.datum_do = randDat;
            hospNem.datum_do = randDat;
            this.aktivneHosp.Remove(hosp.id_hospitalizacie);
        }*/

    }
}
