using Hospital_information_sytem.informacny_system;
using Hospital_information_sytem.structures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hospital_information_sytem
{
    public partial class NovaNemocnica : Form
    {
        public Informacny_system informacny_system;
        protected Nemocnica nem;
        public NovaNemocnica(Informacny_system system)
        {
            informacny_system = system;
            InitializeComponent();
            this.nem = new Nemocnica();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Pridať nemocnicu?", "Ano", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                //this.informacny_system.PridajNemocnicu(this.nem.nazov_nemocnice);
                if (informacny_system.RegistrujNemocnicu(this.nem))
                {
                    MessageBox.Show("Nemocnica bola pridana.");
                    this.Close();
                }
                else { MessageBox.Show("Nepodarilo sa pridat nemocnicu."); }       
            }
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = textBox1.Enabled;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            String filepath = String.Empty;
            String fileExt = string.Empty;
            //Nemocnica nem = new Nemocnica();
            List<Pacient> listPac = new List<Pacient>();
            List<Hospitalizacia> listHosp = new List<Hospitalizacia>();
            List<Poistenec> listPotencialnychpoistVZP = new List<Poistenec>();
            List<Poistenec> listPotencialnychpoistDVO = new List<Poistenec>();
            List<Poistenec> listPotencialnychpoistUNI = new List<Poistenec>();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filepath = openFileDialog.FileName;
                fileExt = Path.GetExtension(filepath);


                textBox1.Text = File.ReadLines(filepath).First(); // gets the first line from file.

                if (fileExt.CompareTo(".txt") == 0)
                {
                    try
                    {
                        StreamReader reader = new StreamReader(filepath);
                        StringBuilder builder = new StringBuilder();
                        string line = "";
                        while ((line = reader.ReadLine())!=null)
                        {
                            if (line.Contains("Nemocnica"))
                            {
                                textBox1.Text = line;
                                this.nem.nazov_nemocnice = textBox1.Text;
                            }
                            else if (line.Contains(';'))
                            {
                                var identifikator = line.Substring(0, line.IndexOf(';'));
                                var zvysok = line.Remove(0, identifikator.Length+1); //aby oseklo az po bodkociarku
                                if (identifikator.Length == 10) //je to pacient
                                {
                                    Pacient pac = new Pacient();
                                    pac.rod_cislo = identifikator;
                                    pac.meno = zvysok.Substring(0, zvysok.IndexOf(';'));
                                    zvysok = zvysok.Remove(0, pac.meno.Length+1);
                                    pac.priezvisko = zvysok.Substring(0, zvysok.IndexOf(';'));
                                    zvysok = zvysok.Remove(0, pac.priezvisko.Length + 1);
                                    pac.kod_poistovne = zvysok.Substring(0, zvysok.IndexOf(';'));
                                    zvysok = zvysok.Remove(0, pac.kod_poistovne.Length + 1);

                                    pac.datum_narodenia = DateTime.Parse(zvysok.Substring(0, zvysok.Length));
                                    listPac.Add(pac);
                                    Poistenec poistenec = new Poistenec();
                                    poistenec.id_poistenca = pac.rod_cislo;
                                    poistenec.rod_cislo_poistenca = pac.rod_cislo;
                                    switch (pac.kod_poistovne.ToString())
                                    {
                                        case "VZP":
                                            listPotencialnychpoistVZP.Add(poistenec);
                                            break;
                                        case "DOV":
                                            listPotencialnychpoistDVO.Add(poistenec);
                                            break;
                                        case "UNI":
                                            listPotencialnychpoistUNI.Add(poistenec);
                                            break;
                                        default:
                                            break;
                                    }
                                    //this.nem.PridajPacienta(pac.meno,pac.priezvisko,pac.rod_cislo,pac.datum_narodenia,pac.kod_poistovne,nem.nazov_nemocnice);
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
                                    zvysok = zvysok.Remove(0, datumod.Length+1);
                                    var datumdo = zvysok;

                                    hosp.datum_od = DateTime.Parse(datumod, new CultureInfo("sk-SK"));
                                    hosp.datum_do = DateTime.Parse(datumdo, new CultureInfo("sk-SK"));
                                    for (int i = 0; i < listPac.Count; i++)
                                    {
                                        if (listPac.ElementAt(i).rod_cislo == hosp.rod_cislo_pacienta)
                                        {
                                            listPac.ElementAt(i).PridajHosp(hosp);
                                        }
                                    }
                                    listHosp.Add(hosp);
                                }
                            }
                            builder.AppendLine(line);
                        }
                        List<Node<String, Pacient>> listNodePac = new List<Node<string, Pacient>>();
                        List<Node<(DateTime, String), Hospitalizacia>> listNodeHosp = new List<Node<(DateTime, String), Hospitalizacia>>();
                        List<Node<(DateTime, String), Hospitalizacia>> listNodeAktualneHosp = new List<Node<(DateTime, String), Hospitalizacia>>();
                        List<Node<(String,String, String, String), Pacient>> listNodeAktualneHospPOIS_RC = new List<Node<(String, String, String, String), Pacient>>();
                        List<Node<(String, String, String, String), Pacient>> listNodeAktualneHospPOIS_P_M_RC = new List<Node<(String, String, String, String), Pacient>>();
                        for (int i = 0; i < listPac.Count; i++)
                        {
                            Node<String, Pacient> pac = new Node<String, Pacient>(listPac.ElementAt(i).rod_cislo, listPac.ElementAt(i));
                            listNodePac.Add(pac);
                        }
                        for (int i = 0; i < listHosp.Count; i++)
                        {
                            Node<(DateTime, String), Hospitalizacia> hos = 
                                new Node<(DateTime, String), 
                                Hospitalizacia>((listHosp.ElementAt(i).datum_od,listHosp.ElementAt(i).id_hospitalizacie
                                ), listHosp.ElementAt(i));
                            listNodeHosp.Add(hos);
                        }

                        for (int i = 0; i < listHosp.Count; i++)
                        {
                            Node<(DateTime, String), Hospitalizacia> hos =
                                new Node<(DateTime, String),
                                Hospitalizacia>((listHosp.ElementAt(i).datum_od, listHosp.ElementAt(i).id_hospitalizacie
                                ), listHosp.ElementAt(i));
                            if (listHosp.ElementAt(i).datum_do.Year==0001)
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

                        this.nem.HromadnyInsertPacientov(listNodePac, listNodeAktualneHospPOIS_RC, listNodeAktualneHospPOIS_P_M_RC);
                        this.nem.HromadnyInsertHospitalizacii(listNodeHosp,listNodeAktualneHosp);

                        //hromadny insert do aktualnychhosppodla POIS a RC
                        //hromadny insert do aktualnych hosp podla pois priezv mena a rc




                        List<Node<String, Poistenec>> listNodePoistVZP = new List<Node<string, Poistenec>>();
                        for (int i = 0; i < listPotencialnychpoistVZP.Count; i++)
                        {
                            Node<String, Poistenec> pois = new Node<String, Poistenec>(listPotencialnychpoistVZP.ElementAt(i).id_poistenca, listPotencialnychpoistVZP.ElementAt(i));
                            listNodePoistVZP.Add(pois);
                        }
                        this.informacny_system.NajdiPoistovnu("Všeobecná Zdravotná Poisťovňa").HromadnyInsertPoistencov(listNodePoistVZP);

                        List<Node<String, Poistenec>> listNodePoistUNI = new List<Node<string, Poistenec>>();
                        for (int i = 0; i < listPotencialnychpoistUNI.Count; i++)
                        {
                            Node<String, Poistenec> poist = new Node<String, Poistenec>(listPotencialnychpoistUNI.ElementAt(i).id_poistenca, listPotencialnychpoistUNI.ElementAt(i));
                            listNodePoistUNI.Add(poist);
                        }
                        this.informacny_system.NajdiPoistovnu("UNION").HromadnyInsertPoistencov(listNodePoistUNI);

                        List<Node<String, Poistenec>> listNodePoistDOV = new List<Node<string, Poistenec>>();
                        for (int i = 0; i < listPotencialnychpoistDVO.Count; i++)
                        {
                            Node<String, Poistenec> poiste = new Node<String, Poistenec>(listPotencialnychpoistDVO.ElementAt(i).id_poistenca, listPotencialnychpoistDVO.ElementAt(i));
                            listNodePoistDOV.Add(poiste);
                        }
                        this.informacny_system.NajdiPoistovnu("Dôvera").HromadnyInsertPoistencov(listNodePoistDOV);


                        reader.Close();
                        richTextBox1.Text = builder.ToString();
                        
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
