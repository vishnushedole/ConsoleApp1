using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    // step1 : Declaration
    public delegate int ArithmeticDelegate(int a, int b);
        
    internal class WorkingWithDelegates
    {
        static int Add(int x, int y) { return x + y; }
        public int Minus(int x, int y) { return x - y; }
        static int Multiply(int x,int y) { return x * y; }

        // void Main()
        //void Main(string[] args)
        //int Main()
        //int Main(string[] args)
        internal static void Test()
        {
            WorkingWithDelegates wd1 = new WorkingWithDelegates();

            // step2 : Instantiation
            ArithmeticDelegate ad = new ArithmeticDelegate(Add);
            int a = 10, b = 20, result = 0;
            // step3 : Invocation (style 1)
            result = ad(a, b);
            Console.WriteLine($"Test 1: ad({a}, {b}) returned {result}");
            a += 10; b += 30;
            // step3 : Invocation (style 2)
            result = ad.Invoke(a, b);
            Console.WriteLine($"Test 2: ad.Invoke({a},{b}) returned {result}");

            // Multicasting
            // Adding multiple signature to the Delegate
            // List of delegates where each delegate holds one address
            ad += new ArithmeticDelegate(wd1.Minus);
            ad += new ArithmeticDelegate(Multiply); // ==> ad = (ArithematicDelegate)Delegate.Combine(ad,new ArithematicDelegate(Multiply));


            a = 40; b = 40;
            result = ad.Invoke(a, b);
            Console.WriteLine($"Test 3: ad.Invoke({a}, {b}) returned {result}");

            // Anonymous method - Unnamed method
            ad += delegate (int x, int y)
            {
                return y > 0 ? x / y : 0;
            };
            a = 100; b = 4;
            result  = ad.Invoke(a, b);
            Console.WriteLine($"Test 4: ad.Invoke({a},{b}) returned {result}");

            // ad -= new ArithmeticDelegate(Multiply);
            /*ad -= delegate (int x, int y)
            {
                return y > 0 ? x / y : 0;
            };*/

            Console.WriteLine(ad.Invoke(a, b));

            // ad -= new ArithematicDelegates(wd1.Minus);
            // Lambda Functions
            ad += (x, y) => x % y;
            // Expression Lambda -> contains a single statement/expression
            // Statement Lambda -> constains multiple statement/expression
            a = 10; b = 7;
            result = ad.Invoke(a, b);
            Console.WriteLine($"Test 5: ad.Invoke({a},{b}) returned {result}");
            //Arguments to Lambda expressions can be passed as follows:
            //()    -> Empty Brackets with zero arguments
            //(a) |a -> with/without brackets for one argument
            //(a,b,...n)-> with brackets for more than one arguments
            InvokeDelegatesManually(ad);
        } 
        static void InvokeDelegatesManually(ArithmeticDelegate arithDel)
        {
            foreach(Delegate del in arithDel.GetInvocationList())
            {
                if (del.Method.Name.StartsWith("M"))
                {
                    object result = del.DynamicInvoke(1000, 10);
                    Console.WriteLine($"{del.Method.Name} returns {result}");
                }
            }
        }
    }
}
