using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class WorkingWithThreads
    {
        internal static void Test()
        {
            ThreadStart ts = new ThreadStart(Run);
            ParameterizedThreadStart pts = new ParameterizedThreadStart(RunObject);

            Thread t1 = new Thread(Run);
            Thread t2 = new Thread(RunObject);
            Thread t4 = new Thread(ts);
            Thread t3 = new Thread(pts);

            Console.WriteLine("All threads created. Press a key to start.");
            Console.ReadKey();
            Products p = new Products(101, "Example", 10M, 10);
            t1.Name = "first";
            t2.Name = "second";
            t3.Name = "third";
            t4.Name = "fourth";
            t1.Start();
            t2.Start();
            t4.Start();
            t3.Start(p);
        }

        // functions which matches the delegate signatures
        // Matches ThreadStart delegate signature
        static void Run()  // No compulsion to name it as Run
        {
            string name = Thread.CurrentThread.Name;
            Console.WriteLine($"Current thread {name}");
            Thread.Sleep(millisecondsTimeout: 1000);
            Console.WriteLine($"Thread {name} Resumed execution.");
            Console.WriteLine($"Thread {name} Exited");
        }
        // Matches 
        static void RunObject(object state)
        {
            if(state != null)
            {
                if(state is Products p)
                {
                    p.show();
                }
            }
            string name = Thread.CurrentThread.Name;
            Console.WriteLine($"Current thread {name}");
            Thread.Sleep(millisecondsTimeout: 1000);
            Console.WriteLine($"Thread {name} Resumed execution.");
            Console.WriteLine($"Thread {name} Exited");
        }
    }
}
