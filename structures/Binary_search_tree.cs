using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_information_sytem.structures
{
    internal class Binary_search_tree<K,T> where K: IComparable<K>
    {
        //konstruktor
        public int Size { get; protected set; }
        public Node<K, T> Root { get; set; }
        public Binary_search_tree()
        {
            Root = null;
            Size = 0;
        }
        Queue<Node<K, T>> queue = new Queue<Node<K, T>>();

        // vlozi Node do BST, ak sa tam Node nachadza tak vrati data najdeneho Nodu
        public T Insert(K key, T data)
        {
            var nodePom = this.FindNode(key);
            if (nodePom != null) { return default(T); }

            Node<K, T> parent = null;
            Node<K, T> current = Root;
            int compare = 0;
            while (current != null)
            {
                parent = current;
                compare = current.Key.CompareTo(key);
                current = compare < 0 ? current.Right : current.Left;
            }

            Node<K, T> newNode = new Node<K, T>(key, data); 

            if (parent != null)
                if (compare < 0)
                    parent.Right = newNode;
                else
                    parent.Left = newNode;
            else
                Root = newNode;
            newNode.Parent = parent;
            Size++;
            return data;
        }
        // najde hladany node podla zadaneho Kluca, vrati cely Node
        public Node<K, T> FindNode(K key, bool KeyNotFoundExc = false)
        {
            Node<K, T> current = Root;
            while (current != null)
            {
                int compare = current.Key.CompareTo(key);
                if (compare == 0)
                    return current;
                if (compare < 0)
                    current = current.Right;
                else
                    current = current.Left;
            }
            if (KeyNotFoundExc)
                throw new KeyNotFoundException();
            else
                return null;
        }
       //metoda na nastavenie referencii pri mazani Nodu z BST
        private void NastavData(Node<K, T> staryVrchol, Node<K, T> novyVrchol)
        {
            var staryPravy = staryVrchol.Right;
            var staryLavy = staryVrchol.Left;
            var staryParent = staryVrchol.Parent;
            bool staryRoot = staryVrchol == Root;
            var staryNode = staryVrchol;

            var novyPravy = novyVrchol.Right;
            var novyLavy = novyVrchol.Left;
            var novyStaryParent = novyVrchol.Parent;
            var novyParent = novyVrchol.Parent;
            var novyNode = novyVrchol;

            if (staryRoot)
            {
                novyVrchol.Parent = null;
            }
            else
            {
                novyVrchol.Parent = staryParent;

                if (staryVrchol.Parent.Right == staryVrchol)
                {
                    staryVrchol.Parent.Right = novyVrchol;
                }
                else
                {
                    staryVrchol.Parent.Left = novyVrchol;
                }
            }
            // osetrit ci syn mazaneho nie je novy nahradzovany - stary rovno zmizne
            if (novyStaryParent == staryVrchol) // novyStaryParent = novyVrchol.Parent este pred zmenou
            {
                if (staryVrchol.Left == novyVrchol)
                {
                    // ostava  novyVrchol.Left
                    this.FindNode(novyNode.Key).Right = staryPravy;
                    //novyVrchol.Right = staryPravy;
                    this.FindNode(staryPravy.Key).Parent = novyNode;
                    //staryVrchol.Right.Parent = novyVrchol;
                }
                else // je to jeho pravy syn
                {
                    // pravy ostava
                    this.FindNode(novyNode.Key).Left = staryLavy;
                    //novyVrchol.Left = staryLavy;
                    this.FindNode(staryLavy.Key).Parent = novyNode;
                    //staryVrchol.Left.Parent = novyVrchol;
                }
                if (staryNode == Root)
                {
                    Root = novyVrchol;
                }
            }
            else // novy vrchol nie je jeho priamy potomok
            {
                if (novyVrchol.Left == null && novyVrchol.Right != null)  // ak vrchol nema potomka staci OR lebo bud je to Nasledovnik alebo Potomok - kt moze mat prave jedneho syna
                {
                    novyVrchol.Left = staryLavy;
                    novyVrchol.Right = staryPravy;

                    staryVrchol.Right.Parent = novyNode;
                    staryVrchol.Left.Parent = novyNode;
                    this.FindNode(novyStaryParent.Key).Left = novyPravy;
                    this.FindNode(novyPravy.Key).Parent = novyStaryParent;
                    if (staryVrchol == Root)
                    {
                        Root = novyVrchol;
                        staryVrchol = null;
                    }
                }
                else if (novyVrchol.Left != null && novyVrchol.Right == null)  // ak vrchol nema potomka staci OR lebo bud je to Nasledovnik alebo Potomok - kt moze mat prave jedneho syna
                {
                    novyVrchol.Right = staryPravy;
                    novyVrchol.Left = staryLavy;

                    staryVrchol.Left.Parent = novyNode;
                    staryVrchol.Right.Parent = novyNode;
                    this.FindNode(novyStaryParent.Key).Right = novyLavy;
                    this.FindNode(novyLavy.Key).Parent = novyStaryParent;
                    if (staryVrchol == Root)
                    {
                        Root = novyVrchol;
                        staryVrchol = null;
                    }
                }
                else if (novyVrchol.Left != null) //predchodca
                {
                    novyVrchol.Left.Parent = this.FindNode(novyStaryParent.Key);
                    this.FindNode(novyStaryParent.Key).Right = novyLavy;
                    novyVrchol.Left = staryLavy;
                    novyVrchol.Right = staryPravy;
                }
                else if (novyVrchol.Right != null)
                {
                    novyVrchol.Right.Parent = this.FindNode(novyStaryParent.Key);
                    this.FindNode(novyStaryParent.Key).Left = novyPravy;
                    novyVrchol.Right = staryPravy;
                    novyVrchol.Left = staryLavy;
                }
                else if (novyVrchol.Left == null && novyVrchol.Right == null)
                {
                    if (staryLavy == novyVrchol && staryParent.Left == staryVrchol)
                    {
                        staryVrchol.Parent.Left = novyNode;
                        // ak mal nejakych potomkov
                        if (staryNode.Right != null)
                        {
                            this.FindNode(staryPravy.Key).Parent = novyNode;
                            novyVrchol.Right = staryPravy;
                        }
                        staryVrchol.Parent.Right = novyNode;
                    }
                    else if (staryLavy == novyVrchol && staryParent.Right == staryVrchol)
                    {
                        staryVrchol.Parent.Right = novyNode;

                        if (staryNode.Right != null)
                        {
                            this.FindNode(staryPravy.Key).Parent = novyNode;
                            novyVrchol.Right = staryPravy;
                        }
                        staryVrchol.Parent.Right = novyNode;
                    }
                    else if (staryPravy == novyVrchol && staryParent.Right == staryVrchol)
                    {
                        staryVrchol.Parent.Right = novyNode;
                        //ak ma stary synov
                        if (staryNode.Left != null)
                        {
                            this.FindNode(staryLavy.Key).Parent = novyNode;
                            this.FindNode(novyNode.Key).Left = staryLavy;
                        }
                        staryVrchol.Parent.Right = novyNode;
                    }
                    else if (staryPravy == novyVrchol && staryParent.Left == staryVrchol)
                    {
                        staryVrchol.Parent.Left = novyNode;

                        if (staryNode.Left != null)
                        {
                            this.FindNode(staryLavy.Key).Parent = novyNode;
                            this.FindNode(novyNode.Key).Left = staryLavy;
                        }
                        staryVrchol.Parent.Right = novyNode;
                    }
                    else //je to random potomok
                    {

                        novyVrchol.Left = staryLavy;
                        novyVrchol.Right = staryPravy;

                        staryNode.Left.Parent = novyNode;
                        staryNode.Right.Parent = novyNode;
                        //vymazat referenciu z jeho stareho parenta
                        if (novyStaryParent.Left == novyNode) { this.FindNode(novyStaryParent.Key).Left = null; }
                        if (novyStaryParent.Right == novyNode) { this.FindNode(novyStaryParent.Key).Right = null; }
                        if (staryNode == Root)
                        {
                            Root = novyNode;
                        }
                    }
                }
                else
                {
                    throw new DllNotFoundException("Nemoze nastat tento pripad.");
                }

            }
        }
        //Najdenie Nodu, ktorym nahradim mazany vrchol
        protected Node<K, T> InOrderPredchodca(Node<K, T> node)
        {
            // najpravejsi vrchol laveho podstromu
            // lavy podstrom:
            Node<K, T> predchodca = node.Left;
            // hladanie najpravejsieho vrcholu
            while (predchodca.Right != null)
                predchodca = predchodca.Right;
            return predchodca;
        }
        //Mazanie vrcholu
        public void ExtractNodee(Node<K, T> node)
        {
            if (node == null)
                throw new ArgumentNullException();
            // 2 potomkovia - ruseny vrchol sa nahradi najpravejsim vrcholom v jeho lavom podstrome (in order predchodca)
            if (node.Left != null && node.Right != null)
            {
                Node<K, T> nasledovnik;
                //Predchodca - najpravejsi z laveho
                nasledovnik = this.InOrderPredchodca(node);
                this.NastavData(node, nasledovnik);
                node = null;
            }
            else if (node.Left == null && node.Right == null) //nema ziadneho potomka tak ho mozeme odstranit a vymazat referenciu parenta
            {
                //ak mazem jedniny prvok zo ztromu
                if (node == Root)
                {
                    Root = null;
                    node = null;
                }
                else if (node.Parent.Left == node)
                {
                    node.Parent.Left = null;

                }
                else
                {
                    node.Parent.Right = null;
                }
                node = null;
            }
            //ak ma mazany vrchol laveho syna alebo lavy podstrom
            else if (node.Left != null)
            {
                if (node == Root)
                {
                    Root = node.Left;

                }
                else
                {
                    if (node.Parent.Left == node)
                    {

                        node.Parent.Left = node.Left;
                        node.Left.Parent = node.Parent;
                    }
                    else if (node.Parent.Right == node)
                    {
                        node.Parent.Right = node.Left;
                        node.Left.Parent = node.Parent;
                    }
                }
                node = null;
            }
            //ak ma mazany vrchol praveho syna alebo podstrom
            else if (node.Right != null)
            {
                if (node == Root)
                {
                    node.Right = Root;

                }
                else
                {
                    if (node.Parent.Left == node)
                    {
                        node.Parent.Left = node.Right;
                        node.Right.Parent = node.Parent;
                    }
                    else if (node.Parent.Right == node)
                    {
                        node.Parent.Right = node.Right;
                        node.Right.Parent = node.Parent;
                    }
                }
                node = null;
            }
            this.Size--;
        }
        //Prava rotacia
        private Node<K, T> RotujDoprava(Node<K, T> parent)
        {
            Node<K, T> pivot = parent.Right;
            var stary = parent;
            var staryPravy = parent.Right;
            var staryParentParenta = parent.Parent;
            var rotovany = pivot;
            var rotovanyPravy = pivot.Right;
            var rotovanyLavy = pivot.Left;

            if (pivot.Left == null)
            {
                pivot.Left = stary;
                this.FindNode(stary.Key).Parent = rotovany;
                parent.Right = null;
            }
            else
            {
                pivot.Left.Parent = stary;
                this.FindNode(stary.Key).Right = rotovanyLavy;

                pivot.Left = stary;
                this.FindNode(stary.Key).Parent = rotovany;
            }
            if (parent == Root) { Root = pivot; pivot.Parent = null; } else { pivot.Parent = staryParentParenta; this.FindNode(staryParentParenta.Key).Left = rotovany; }
            return pivot;
        }
        //Prava rotacia, kt je vyuzivana pri rotacii zdegenerovaneho stromu
        //zmenena je len posledna podmienka
        private Node<K, T> RotujDoprava2(Node<K, T> parent)
        {
            Node<K, T> pivot = parent.Right;
            var stary = parent;
            var staryPravy = parent.Right;
            var staryParentParenta = parent.Parent;
            var rotovany = pivot;
            var rotovanyPravy = pivot.Right;
            var rotovanyLavy = pivot.Left;

            if (pivot.Left == null)
            {
                pivot.Left = stary;
                this.FindNode(stary.Key).Parent = rotovany;
                parent.Right = null;
            }
            else
            {
                pivot.Left.Parent = stary;
                this.FindNode(stary.Key).Right = rotovanyLavy;

                pivot.Left = stary;
                this.FindNode(stary.Key).Parent = rotovany;
            }
            if (parent == Root) { Root = pivot; pivot.Parent = null; } else { pivot.Parent = staryParentParenta; this.FindNode(staryParentParenta.Key).Right = rotovany; }
            return pivot;
        }
        // Lava rotacia
        private Node<K, T> RotujDolava(Node<K, T> parent)
        {
            Node<K, T> pivot = parent.Left;
            var stary = parent;
            var starypravy = parent.Right;
            var staryParentParenta = parent.Parent;
            var rotovany = pivot;
            var rotovanypravy = pivot.Right;
            var rotovanylavy = pivot.Left;


            if (pivot.Right == null)
            {
                pivot.Right = stary;
                this.FindNode(stary.Key).Parent = rotovany;
                parent.Left = null;
            }
            else
            {
                pivot.Right.Parent = stary;
                this.FindNode(stary.Key).Left = rotovanypravy;

                pivot.Right = stary;
                this.FindNode(stary.Key).Parent = rotovany;
            }

            if (parent == Root) { Root = pivot; pivot.Parent = null; } else { pivot.Parent = staryParentParenta; this.FindNode(staryParentParenta.Key).Right = rotovany; }
            return pivot;
        }
        //Lava rotacia, kt je vyuzivana pri rotacii zdegenerovaneho stromu
        //zmenena je len posledna podmienka
        private Node<K, T> RotujDolava2(Node<K, T> parent)
        {
            Node<K, T> pivot = parent.Left;
            var stary = parent;
            var starypravy = parent.Right;
            var staryParentParenta = parent.Parent;
            var rotovany = pivot;
            var rotovanypravy = pivot.Right;
            var rotovanylavy = pivot.Left;


            if (pivot.Right == null)
            {
                pivot.Right = stary;
                this.FindNode(stary.Key).Parent = rotovany;
                parent.Left = null;
            }
            else
            {
                pivot.Right.Parent = stary;
                this.FindNode(stary.Key).Left = rotovanypravy;

                pivot.Right = stary;
                this.FindNode(stary.Key).Parent = rotovany;
            }

            if (parent == Root) { Root = pivot; pivot.Parent = null; } else { pivot.Parent = staryParentParenta; this.FindNode(staryParentParenta.Key).Left = rotovany; }
            return pivot;
        }

        /// <summary>
        /// Metoda na zdegenerovanie BST - nasledne vyuzita pri rotaciach
        /// </summary>
        /// <param name="node"></param>
        public void Degeneruj(Node<K, T> node)
        {
            if (node == null)
            {
                return;
            }

            var lavy = node.Left;
            var pravy = node.Right;
            if (node.Left != null)
            {
                if (node.Left.Left == null && node.Left.Right != null)
                {
                    this.RotujDoprava(node.Left);
                    lavy = lavy.Parent;
                }
            }
            if (node.Right != null)
            {
                if (node.Right.Right == null && node.Right.Left != null)
                {
                    this.RotujDolava(node.Right);
                    pravy = pravy.Parent;
                }
            }
            if (lavy != null)
            {
                while (lavy.Left != null)
                {

                    if (lavy.Right != null)
                    {
                        this.RotujDoprava(lavy);
                        lavy = lavy.Parent;
                    }
                    else
                    {
                        lavy = lavy.Left;
                    }
                    if (lavy.Left == null && lavy.Right != null)
                    {
                        this.RotujDoprava(lavy);
                        lavy = lavy.Parent;
                    }
                }
            }
            if (pravy != null)
            {
                while (pravy.Right != null)
                {

                    if (pravy.Left != null)
                    {
                        this.RotujDolava(pravy);
                        pravy = pravy.Parent;
                    }
                    else
                    {
                        pravy = pravy.Right;
                    }
                    if (pravy.Right == null && pravy.Left != null)
                    {
                        this.RotujDolava(pravy);
                        pravy = pravy.Parent;
                    }
                }
            }

        }
        // Metoda na vyvazenie zdegenerovaneho stromu
        // nasleduje po metode Degeneruj
        // Vyvazi zdegenerovany strom tak, aby vytvoril opacne V
        // Pravy a lavy podstrom Rootu budu mat rovnaku vysku alebo o 1 node rozdiel
        public void VyvazZdegenerovanyStrom(Node<K, T> node)
        {
            if (node == null)
            {
                return;
            }
            while (true)
            {
                int vyskaLavy = this.DajVyskuStromu(Root.Left);
                int vyskaPravy = this.DajVyskuStromu(Root.Right);
                if (vyskaLavy == 0 && vyskaPravy == 0 || vyskaLavy - vyskaPravy == 0 || vyskaLavy - vyskaPravy == 1 || vyskaLavy - vyskaPravy == -1)
                {
                    return;
                }
                else if (vyskaPravy - vyskaLavy < 0)
                {
                    //rotovat root do prava
                    this.RotujDolava(Root);
                }
                else
                {
                    this.RotujDoprava(Root);
                }
            }
        }
        // Metoda na nasledne vyvazenie zdegenerovaneho a upraveneho stromu
        private void vyvazStrom(Node<K, T> node)
        {
            if (node == null)
            {
                return;
            }
            while (true)
            {
                int vyskaLavy = this.DajVyskuStromu(node.Left);
                int vyskaPravy = this.DajVyskuStromu(node.Right);
                //int vyskaLavy = this.getHeight(node.Left);
                //int vyskaPravy = this.getHeight(node.Right);
                if (vyskaLavy == 0 && vyskaPravy == 0 || vyskaLavy - vyskaPravy == 0 || vyskaLavy - vyskaPravy == 1 || vyskaLavy - vyskaPravy == -1)
                {
                    return;
                }
                else if (vyskaPravy - vyskaLavy < 0)
                {
                    //rotovat root do prava
                    node = this.RotujDolava2(node);
                }
                else
                {
                    node = this.RotujDoprava2(node);
                }
            }
        }
        //Metodad ktora bdue rotovat strom podla Nodov na aktualnom lever order leveli
        //Nody z kazdeho level order levelu si ulozi do queue
        //z queueu bude vyberat postupne vrcholy a volat nad nimi metodu vyvyazStrom, ktora urobi z kazdeho vrhcolu obratene V
        public void UplneVyvaz(Node<K, T> node)
        {
            int vyskaStromu = this.DajVyskuStromu(Root);
            if (vyskaStromu < 0)
            {
                vyskaStromu = vyskaStromu * -1;
            }
            for (int i = 0; i < vyskaStromu; i++)
            {
                this.printCurrentLevel(node, i);
                while (queue.Count != 0)
                {
                    var pom = queue.Dequeue();
                    this.vyvazStrom(pom);
                }
            }
        }

        public virtual void printLevelOrder()
        {
            int h = this.DajVyskuStromu(Root);
            //int h = getHeight(Root);
            int i;
            for (i = 1; i <= h; i++)
            {
                printCurrentLevel(Root, i);
            }
        }
        //rekurzivna metona na zistenie Nodov v aktualnom leveli
        public virtual void printCurrentLevel(Node<K, T> node, int level)
        {
            if (node == null)
            {
                return;
            }
            if (level == 1)
            {
                queue.Enqueue(node);
            }
            else if (level > 1)
            {
                printCurrentLevel(node.Left, level - 1);
                printCurrentLevel(node.Right, level - 1);
            }
        }
        //metoda na vratenie vysky stromu alebo podstromu od zadaneho node
        private int DajVyskuStromu(Node<K, T> node)
        {
            if (node == null)
                return 0;
            Queue<Node<K, T>> q = new Queue<Node<K, T>>();
            q.Enqueue(node);
            int height = 0;

            while (1 == 1)
            {
                int nodeCount = q.Count;
                if (nodeCount == 0)
                    return height;
                height++;

                while (nodeCount > 0)
                {
                    Node<K, T> newnode = q.Peek();
                    q.Dequeue();
                    if (newnode.Left != null)
                        q.Enqueue(newnode.Left);
                    if (newnode.Right != null)
                        q.Enqueue(newnode.Right);
                    nodeCount--;
                }
            }
        }
        //najdenie medianu zo zadaneho listu
        public Node<K, T> NajdiMedian(List<Node<K, T>> list)
        {
            Node<K, T> medianNode = null;
            if (list.Count == 0) { return null; }
            if (list.Count % 2 == 0)
            {
                //ak je parny pocet tak vyberiem jednu z dvoch strednych
                int index = list.Count / 2;
                medianNode = list.ElementAt(index);
                //int b = list.ElementAt(index).Key;
                return medianNode;
            }
            else
            {
                int index = list.Count / 2;
                medianNode = list.ElementAt(index);
                return medianNode;
                //vyberiem presne strednu
            }
        }
        //Medianove usporiadanie listu
        public void ZapisMedianDoQueueList(List<Node<K, T>> list)
        {
            Queue<List<Node<K, T>>> queueListov = new Queue<List<Node<K, T>>>();
            List<Node<K, T>> novyListLavy = new List<Node<K, T>>();
            List<Node<K, T>> novyListPravy = new List<Node<K, T>>();
            Queue<Node<K, T>> medianQueue = new Queue<Node<K, T>>();
            if (list.Count == 0) { return; }
            queueListov.Enqueue(list);
            while (queueListov.Count != 0)
            {
                var pom = queueListov.Dequeue();
                var median = this.NajdiMedian(pom);
                medianQueue.Enqueue(median);
                int index = 0;
                for (int i = 0; i < pom.Count; i++)
                {
                    if (pom.ElementAt(i) == median)
                    {
                        index = i;
                        break;
                    }
                }
                novyListLavy = new List<Node<K, T>>();
                novyListPravy = new List<Node<K, T>>();
                for (int i = 0; i < pom.Count; i++)
                {
                    if (i < index)
                    {

                        novyListLavy.Add(pom.ElementAt(i));
                    }
                    else if (i > index)
                    {

                        novyListPravy.Add(pom.ElementAt(i));
                    }
                }
                if (novyListLavy.Count != 0) { queueListov.Enqueue(novyListLavy); }
                if (novyListPravy.Count != 0) { queueListov.Enqueue(novyListPravy); }


            }
            while (medianQueue.Count != 0)
            {
                var pomNode = medianQueue.Dequeue();
                this.Insert(pomNode.Key, pomNode.Data);
            }
        }
        //usporiadanie Listu podla Kluca
        public List<Node<K, T>> UsporiadajList(List<Node<K, T>> neusporiadanyList)
        {
            List<Node<K, T>> usporiadanyList = new List<Node<K, T>>();
            if (neusporiadanyList.Count == 0)
            {
                return null;
            }
            usporiadanyList = neusporiadanyList.OrderBy(a => a.Key).ToList();
            return usporiadanyList;
        }
        //Naplnenie prazdneho binarneho stromu zo zadaneho intervalu nodov
        //nasledne vyvazenie podla medianov
        public void NaplnStrukturu(List<Node<K, T>> zoznam)
        {
            Node<K, T> median = this.NajdiMedian(zoznam);
            this.Insert(median.Key, median.Data);

        }


        //INTERVALOVE VYHLADAVANIE NA MEDIANY, KTORE SA NA KONCI VLOZIA DO STRUKTURY
        //NIEKEDY VYNECHA PRVOK INAK JE DOBRE A PAMATOVO MENEJ NAROCNE AKO CEZ QUEUE LISTOV

        /*public void ZapisMedianDoQueue(List<Node<K,T>> list) 
        {
            if (list.Count == 0) 
            {
                return;
            }
            int indexFrom = 0;
            int indexTo = list.Count-1;
            int indexMedian = list.Count-1;
            Queue<int> medianIndexes = new Queue<int>();
            List<int> indexesFrom = new List<int>();
            List<int> indexesTo = new List<int>();
            while (list.Count() != medianIndexes.Count()) 
            {
                
                if (indexTo == indexFrom)
                {
                    medianIndexes.Enqueue(indexFrom);                    
                    indexFrom = indexesTo.ElementAt(indexesTo.Count-1) + 2;
                    if (indexFrom > list.Count()) { break; }
                    indexesTo.RemoveAt(indexesTo.Count-1);
                    indexTo = indexesTo.ElementAt(indexesTo.Count-1);
                    indexesTo.RemoveAt(indexesTo.Count-1);
                }

                indexMedian = (indexTo - indexFrom) / 2;
                if ((indexTo - indexFrom) % 2 != 0)
                {
                    indexMedian += 1;
                }            
                indexMedian = indexMedian + indexFrom;
                
                medianIndexes.Enqueue(indexMedian);
                if (indexMedian-1-indexFrom >= 0)
                {
                    indexesFrom.Add(indexFrom);
                    indexesTo.Add(indexTo);
                }
                indexTo = indexMedian-1;

            }
            int pom = medianIndexes.Count;
            for (int i = 0; i < pom; i++)
            {
                //int pom = medianIndexes.Dequeue();
                var node = list.ElementAt(medianIndexes.Dequeue());
                this.Insert(node.Key, node.Data);
            }
        }*/

    }
}
