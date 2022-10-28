using Hospital_information_sytem.structures;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_information_sytem.tests
{
    /*internal class Tests
    {
    }*/
    public class Tests
    {
        int pocetOperacii = 100000;
        int podielInsert = 20000;
        int podielRemove = 60000;
        int podielFind = 20000;

        int pocetVykonanychOperacii;
        int pocetInsert;
        int pocetRemove;
        int pocetFind;
        Binary_search_tree<int, String> binary = new Binary_search_tree<int, String>();
        List<int> availableKeys = new List<int>();
        List<int> usedKeys = new List<int>();
        int passed = 0;
        int failed = 0;
        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public void TestujInsertRemoveFind()
        {
            for (int i = 0; i < 100001; i++)
            {
                availableKeys.Add(i);
            }


            for (int i = 0; i < podielRemove; i++)
            {
                String data = RandomString(5);
                int index = random.Next(availableKeys.Count);
                int key = availableKeys[index];
                binary.Insert(key, data);
                //if (binary.Insert(key,data) == null) { failed++; }
                usedKeys.Add(key);
                availableKeys.RemoveAt(index);
                //BTreePrinter.Print(binary.Root);
            }

            for (int i = 0; i < pocetOperacii; i++)
            {
                int cislo = random.Next(pocetOperacii - i);
                //BTreePrinter.Print(binary.Root);
                if (cislo < podielInsert) //0 - 20k
                {
                    int oldSize = this.binary.Size;
                    String insertData = RandomString(5);
                    int insertIndex = random.Next(availableKeys.Count);
                    int insertKey = availableKeys[insertIndex];

                    binary.Insert(insertKey, insertData);
                    if (oldSize + 1 == this.binary.Size)
                    {
                        passed++;
                    }
                    else
                    {
                        failed++;
                    }
                    usedKeys.Add(insertKey);
                    availableKeys.RemoveAt(insertIndex);
                    podielInsert--;
                    pocetInsert++;
                    pocetVykonanychOperacii++;
                }
                else if (podielInsert <= cislo && cislo < (podielInsert + podielRemove)) // 20 - 40k
                {
                    int oldSize = this.binary.Size;
                    int removeIndex = random.Next(usedKeys.Count);
                    int removeKey = usedKeys[removeIndex];

                    var node = binary.FindNode(removeKey);
                    if (node == null) { failed++; return; }
                    binary.ExtractNodee(node);
                    if (oldSize - 1 == this.binary.Size)
                    {
                        passed++;
                    }
                    else
                    {
                        failed++;
                    }
                    availableKeys.Add(removeKey);
                    usedKeys.RemoveAt(removeIndex);
                    podielRemove--;
                    pocetVykonanychOperacii++;
                    pocetRemove++;
                }
                else  // 40 - 100k
                {
                    int tryFindIndex = random.Next(usedKeys.Count);
                    int tryFindKey = usedKeys[tryFindIndex];
                    //int data = rand();
                    var pomNodeKey = tryFindKey;

                    var node = binary.FindNode(tryFindKey);
                    if (node == null) { failed++; return; }
                    if (pomNodeKey == node.Key)
                    {
                        passed++;
                    }
                    else
                    {
                        failed++;
                    }
                    podielFind--;
                    pocetVykonanychOperacii++;
                    pocetFind++;
                }
            }

            Console.WriteLine("POCET VYKONANYCH OPERACII : " + pocetVykonanychOperacii + "\n     " +
                                "pocet operacii insert : " + pocetInsert + "\n       " +
                                "pocet operacii find : " + pocetFind + "\n        " +
                                "pocet operacii remove : " + pocetRemove);
            Console.WriteLine("SUMAR TESTOV : \n\tpassed : " + passed + "\n\tfailed : " + failed);

        }
        List<int> inOrderList = new List<int>();

        private void InOrderUsporiadanie(Node<int, String> node)
        {
            if (node != null)
            {
                this.InOrderUsporiadanie(node.Left);
                inOrderList.Add(node.Key);
                this.InOrderUsporiadanie(node.Right);
            }
        }
        public void TestujVyvazenie()
        {
            List<int> availableKeysForVyvazenie = new List<int>();
            List<int> usedKeysForVyvazenie = new List<int>();
            for (int i = 0; i < 100001; i++)
            {
                availableKeysForVyvazenie.Add(i);
            }
            var strom = new Binary_search_tree<int, String>();
            for (int i = 0; i < 10000; i++)
            {
                int index = random.Next(0, availableKeysForVyvazenie.Count);
                int key = availableKeysForVyvazenie.ElementAt(index);
                String data = RandomString(5);
                usedKeysForVyvazenie.Add(key);
                availableKeysForVyvazenie.Remove(key);
                strom.Insert(key, data);
            }
            this.InOrderUsporiadanie(strom.Root);
           

            var timer = new Stopwatch();
            timer.Start();
            //vyvazenie stromu
            strom.Degeneruj(strom.Root);
            strom.VyvazZdegenerovanyStrom(strom.Root);
            strom.UplneVyvaz(strom.Root);
            timer.Stop();
            TimeSpan timeTaken = timer.Elapsed;

            usedKeysForVyvazenie.Sort();

            if (usedKeysForVyvazenie.Count == inOrderList.Count)
            {
                for (int i = 0; i < usedKeysForVyvazenie.Count; i++)
                {
                    //Console.WriteLine(usedKeysForVyvazenie.ElementAt(i) + " \t" + inOrderList.ElementAt(i) + "\n");
                    if (usedKeysForVyvazenie.ElementAt(i) == inOrderList.ElementAt(i))
                    {
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("Prvky v listoch sa nezhoduju.");
                        return;
                    }
                }
            }
            else
            {
                Console.WriteLine("Nezhoda v dlzke porovnavanych listov.");
                return;
            }
            if (this.JeVybalancovany(strom.Root))
            {
                Console.WriteLine("Strom vybalancovany je.");
            }
            Console.WriteLine("Vsetky prvky sa zhoduju s In Order prehliadkou - vyvazenie prebehlo v poriadku.");
            Console.WriteLine("Pocet vlozenycha a nasledne otestovanych prvkov: " + usedKeysForVyvazenie.Count);
            Console.WriteLine("Trvanie: " + timeTaken.ToString(@"m\:ss\.fff"));
        }
        List<int> inOrderList2 = new List<int>();

        private void InOrderUsporiadanie2(Node<int, String> node)
        {
            if (node != null)
            {
                this.InOrderUsporiadanie2(node.Left);
                inOrderList2.Add(node.Key);
                this.InOrderUsporiadanie2(node.Right);
            }
        }
        public void TetstujMedianoveVkladanie()
        {
            List<int> availableKeysForMedianoveVkladanie = new List<int>();
            List<int> usedKeysForMedianoveVkladanie = new List<int>();
            Node<int, String> node = new Node<int, string>(default, default);
            for (int i = 0; i < 100001; i++)
            {
                availableKeysForMedianoveVkladanie.Add(i);
            }
            var strom2 = new Binary_search_tree<int, String>();
            List<Node<int, String>> listNodov = new List<Node<int, string>>();

            for (int i = 0; i < 10000; i++)
            {
                node = new Node<int, string>(default, default);
                int index = random.Next(0, availableKeysForMedianoveVkladanie.Count);
                node.Key = availableKeysForMedianoveVkladanie.ElementAt(index);
                node.Data = RandomString(5);
                usedKeysForMedianoveVkladanie.Add(node.Key);
                //Console.WriteLine("Pridany : " + node.Key);
                availableKeysForMedianoveVkladanie.Remove(node.Key);
                //Console.WriteLine("Vymazany : " + node.Key);
                listNodov.Add(node);
            }

            listNodov = listNodov.OrderBy(a => a.Key).ToList();
            /*
            Console.WriteLine("List nodov na vlozenie: \n");
            for (int i = 0; i < listNodov.Count; i++)
            {
                Console.WriteLine(listNodov.ElementAt(i).Key + ", ");
            }*/
            var timer = new Stopwatch();
            timer.Start();
            strom2.ZapisMedianDoQueueList(listNodov);
            timer.Stop();
            TimeSpan timeTaken = timer.Elapsed;
            this.InOrderUsporiadanie2(strom2.Root);
            

            usedKeysForMedianoveVkladanie.Sort();
            if (usedKeysForMedianoveVkladanie.Count == inOrderList2.Count)
            {
                for (int i = 0; i < usedKeysForMedianoveVkladanie.Count; i++)
                {
                    if (usedKeysForMedianoveVkladanie.ElementAt(i) == inOrderList2.ElementAt(i))
                    {
                        //Console.WriteLine(usedKeysForMedianoveVkladanie.ElementAt(i) + " \t" + inOrderList2.ElementAt(i) + "\n");
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("Prvky v listoch sa nezhoduju.");
                        return;
                    }
                    
                }
            }
            else
            {
                Console.WriteLine("Nezhoda v dlzke porovnavanych listov.");
                return;
            }
            if (this.JeVybalancovany(strom2.Root))
            {
                Console.WriteLine("Strom vybalancovany je.");
            }
            Console.WriteLine("Vsetky prvky sa zhoduju s In Order prehliadkou - medianove vlozenie a nasledne vyvazenie prebehlo v poriadku.");
            Console.WriteLine("Pocet vlozenycha a nasledne otestovanych prvkov: " + usedKeysForMedianoveVkladanie.Count);
            Console.WriteLine("Trvanie: " + timeTaken.ToString(@"m\:ss\.fff"));
        }
        public bool JeVybalancovany(Node<int, String> node)
        {
            int lavyVyska;
            int pravyVyska;
            if (node == null)
            {
                return true;
            }
            lavyVyska = binary.DajVyskuStromu(node.Left);
            pravyVyska = binary.DajVyskuStromu(node.Right);

            if (Math.Abs(lavyVyska - pravyVyska) <= 1 && JeVybalancovany(node.Left)
                && JeVybalancovany(node.Right))
            {
                return true;
            }
            return false;
        }

    }
}
