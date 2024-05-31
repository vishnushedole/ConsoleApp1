using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public delegate void RangeDelegate();
    public class Marks 
    {
        //1.1 private data members
        private int _id;
        private int _marks;
        public event RangeDelegate del;

        public int Id { get { return _id; } set { _id = value; } }
        public int marks { get { return _marks; } set {
                if (value >10 && value<90)
                {
                _marks = value;
                }
                else
                {
                    del?.Invoke();
                }
            } }


    }

   
    internal class MarksDelegate
    {
        public static void Test()
        {
            Marks m = new Marks();
            Console.WriteLine("Enter Id:");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter marks:");
            int marks = int.Parse(Console.ReadLine());
            m.del += () =>
            {
                Console.WriteLine("Out of bound");
            };
            m.Id = id;
            m.marks = marks;
        }
    }
}
