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
            //GUI
            Informacny_system system = new Informacny_system();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(system));

            
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
