using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleAppVS2022
{
    public class GenericCollection<T>
    {
        List<T> list = new List<T>();


        public void Add(T item) { list.Add(item); }
        public void Remove(T item) { list.Remove(item); }

        public T GetAt(int position) { return list[position]; }

        public T[] GetAll() { return list.ToArray(); }

        public T this[int index]
        {
            get { return list[index]; }
            set { list[index] = value; }

        }
        public int Count { get { return list.Count; } }
    }
    public class CollectionManager
    {
        internal static void Test()
        {
            GenericCollection<int> intColl = new GenericCollection<int>();
            intColl.Add(100); intColl.Add(200);
            int x = intColl.GetAt(0);
            GenericCollection<string> strColl = new GenericCollection<string>();
            strColl.Add("111"); strColl.Add("another");
            string s = strColl.GetAt(1);

            GenericCollection<Products> prodColl = new GenericCollection<Products>();

            prodColl.Add(new Products(10)); prodColl.Add(new Products(20));
            var p = prodColl[0];


            int i = 10, j = 20;
            Swap(ref i, ref j);
            double d1 = 1.0, d2 = 2.0;
            Swap(ref d1, ref d2);
            Products p1 = new Products(10), p2 = new Products(11);
            Swap(ref p1, ref p2);

            string s1 = "sa", s2 = "dsd";
            Swap(ref s1, ref s2);
        }
        static void Swap<T>(ref T item1, ref T item2)
        {
            T temp = item1;
            item1 = item2;
            item2 = temp;
        }
    }
}