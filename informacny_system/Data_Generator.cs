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
            Hospitalizacia hospitalizacia = new Hospitalizacia();
            hospitalizacia.LoadDataFromFile();
            //hospitalizacia.PridajDiagnozy();
            //hospitalizacia.LoadDataFromFile();
            Nemocnica nem = informacny_system.NajdiNemocnicu(this.pouziteNazvyNemocnic.ElementAt(_random.Next(this.pouziteNazvyNemocnic.Count)));
            List<Pacient> aktualnyPacientiNemocnice = nem.VratListPacientov();
            Pacient pac = aktualnyPacientiNemocnice.ElementAt(_random.Next(aktualnyPacientiNemocnice.Count));
            List<Hospitalizacia> pacientoveHospitalizacie = pac.VratListHospitalizacii();
            DateTime start;
            if (pacientoveHospitalizacie.Count > 0)
            {
                Hospitalizacia posledna = pacientoveHospitalizacie.Last();
                
                if (posledna.datum_do.Year == 0001)
                {
                    this.UkonciHospitalizaciuPacienta(pac, nem);
                    return;
                }
                else
                {
                    start = new DateTime(posledna.datum_do.Year,
                        posledna.datum_do.Month, posledna.datum_do.Day).AddDays(1); //prida jeden den aby hospitalizacie nezacinali v ten isty den ako skoncia 
                }
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

            String rokID = randDat.Year.ToString().Substring(2, 2);
            String id_hospitalizacie = denID + mesiacID + rokID + pac.rod_cislo;
            List<String> diagnozy = hospitalizacia.VratListDiagnoz();
            String diagnozaNazov = diagnozy.ElementAt(_random.Next(diagnozy.Count));

            nem.PridajHospitalizaciu(id_hospitalizacie, pac.rod_cislo, randDat, diagnozaNazov);
            pac.PridajHospitalizaciuPacientovi(id_hospitalizacie, pac.rod_cislo, randDat, diagnozaNazov);
            this.aktivneHosp.Add(id_hospitalizacie);
        }
        
        
        public void UkonciHospitalizaciuPacienta(Pacient pacient, Nemocnica nemocnica) 
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
        }
        
    }
}
