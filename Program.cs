using Hospital_information_sytem.informacny_system;
using Hospital_information_sytem.structures;
using Hospital_information_sytem.tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hospital_information_sytem
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Informacny_system inf_system = new Informacny_system();
            inf_system.PridajPoistovnu();
            
            Data_Generator generator = new Data_Generator();
            for (int i = 0; i < 10; i++)
            {
                generator.GenerujNemocnicu(inf_system);
                
            }
            for (int i = 0; i < 100; i++)
            {
                generator.GenerujPacientaVPoistenca(inf_system);
            }
            for (int i = 0; i < 50; i++)
            {
                    generator.GenerujHospitalizaciu(inf_system);
            }
            


            //GUI
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(inf_system));

            /*Tests test = new Tests();

            Console.WriteLine("=====================================================================================");
            Console.WriteLine("\nTEST T1 \nOPERACIE INSERT, FIND A REMOVE");
            test.TestujInsertRemoveFind();
            Console.WriteLine("=====================================================================================");
            Console.WriteLine("\nTEST T2 \nOPERACIE VYVAZENIE - ROTACIE");
            test.TestujVyvazenie();
            Console.WriteLine("=====================================================================================");
            Console.WriteLine("\nTEST T3 \nOPERACIE VYVAZENIE - MEDIANOVE");
            test.TetstujMedianoveVkladanie();
            Console.WriteLine("=====================================================================================");*/
        }
    }
}
