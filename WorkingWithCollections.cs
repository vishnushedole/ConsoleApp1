using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class WorkingWithCollections
    {
        internal static void Test()
        {
            ArrayList list = new ArrayList();
            list.Add(1);
            list.Add("Two");
            list.Add(true);
            list.Add(DateTime.Now);
            list.Add(1234.5678);

            foreach(var item in list)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine(list[2]);
            var p = list[2];

            Hashtable ht = new Hashtable();
            ht.Add(1, "one");
            ht.Add(2, "two");
            ht.Add(3, "three");
            ht.Add(4,"four");

            if (!ht.ContainsKey(4)) ht.Add(4, "four");
            else ht.Add(5, "five");
            foreach(var key in ht.Keys)
            {
                Console.WriteLine($"{key} - {ht[key]}");
            }

            var item1 = ht[4].ToString();
            Console.WriteLine(item1);

            SortedList sl = new SortedList();
            sl.Add(1, "one");
            sl.Add(2, "two");
            sl.Add(3, "three");
            sl.Add(4, "four");
            if (!sl.ContainsKey(4)) sl.Add(4, "four");
            else sl.Add(5, "five");

            foreach (var key in sl.Keys)
            {
                Console.WriteLine($"{key} - {sl[key]}");
            }
            sl[4] = "forty";
            string item3 = sl[4].ToString();
            Console.WriteLine(item3);

            Stack st = new Stack();
            st.Push(1);
            st.Push("one");
            st.Push(true);
            foreach(var item in st)
                Console.WriteLine(item);
            var item4 = st.Pop();
            Console.WriteLine($"Popped {item4}");

            Queue q = new Queue();
            q.Enqueue(1);
            q.Enqueue("one");
            q.Enqueue(true);
            foreach(var item in q)
                Console.WriteLine(item);
            var item5 = q.Dequeue();
            Console.WriteLine($"Dequeued element :{item5}");
            foreach (var item in q) Console.WriteLine(item);
        }
    }
}
