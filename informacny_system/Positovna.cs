﻿using Hospital_information_sytem.structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_information_sytem.informacny_system
{
    
    public class Positovna
    {
        private Random _random = new Random();
        public String kod_poistovne;
        public String nazov_poistovne;
        Binary_search_tree<String, Poistenec> poistenci = new Binary_search_tree<string, Poistenec>();
        Binary_search_tree<(String, String), Poistenec> poistenci_novi = new Binary_search_tree<(String, String), Poistenec>();
        public bool PridajPoistenca(String rod_cislo)
        {
            if (rod_cislo == string.Empty) { return false; }
            Poistenec poistenec = new Poistenec();
            poistenec.rod_cislo_poistenca = rod_cislo;
            poistenec.id_poistenca = rod_cislo;
            (String, String) keyPoistenec = (poistenec.id_poistenca, poistenec.rod_cislo_poistenca);
            //var pom = this.poistenci.Insert(rod_cislo, poistenec);
            var pompom = poistenci_novi.Insert(keyPoistenec, poistenec);
            if (pompom == null || pompom == null) { return false; }
            return true;

        }
        public Poistenec NajdiPoistenca(String id_poistenca)
        {
            if (id_poistenca == string.Empty) { return null; }
            var pom = poistenci.FindNode(id_poistenca).Data;
            
            if (pom == null) { return null; } else { return pom; }
        }
        public List<Poistenec> VratListPoistencov()
        {
            return this.poistenci.ZapisVsetkyNody(poistenci.Root);
        }

        public void Optimalizuj()
        {
            this.poistenci.Vyvaz(poistenci.Root);
        }
        public void HromadnyInsertPoistencov(List<Node<String, Poistenec>> listPoistencov)
        {
            this.poistenci.ZapisMedianDoQueueList(listPoistencov);

        }

    }
}
