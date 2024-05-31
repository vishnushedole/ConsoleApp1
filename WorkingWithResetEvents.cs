using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class WorkingWithResetEvents
    {
        static AutoResetEvent oddEvent = new AutoResetEvent(false);
        static AutoResetEvent evenEvent = new AutoResetEvent(false);

        static void GenerateOddNumbers()
        {
            for(int i=1;i<10; i++)
            {
                evenEvent.WaitOne();
                Console.WriteLine($"{i}\t ODD");
                oddEvent.Set();
            }
        }
        static void GenerateEvenNumbers()
        {
            for(int i=0;i<10;i++)
            {
                Console.WriteLine($"{i}\t EVEN");
                evenEvent.Set();
                oddEvent.WaitOne();
            }
        }
        public static void Test()
        {
            Thread t1 = new Thread(GenerateEvenNumbers);
            Thread t2 = new Thread(GenerateOddNumbers);
            t1.Start();
            t2.Start();

            Console.WriteLine("All numbers are generated press key to exit.");
            Console.ReadKey();
        }
    }
}
