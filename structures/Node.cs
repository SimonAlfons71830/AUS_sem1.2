using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_information_sytem.structures
{
    public class Node<K,T> where K : IComparable<K>
    {
        public K Key { get; set; }
        public T Data { get; set; }
        public Node<K, T> Parent;
        public Node<K, T> Left;
        public Node<K, T> Right;
        public int height;
        public void DisplayNode()
        {
            Console.Write(Data + " ");
        }
        public Node(K key, T data)
        {
            Data = data;
            Key = key;
            height = 0;
        }
    }
}
