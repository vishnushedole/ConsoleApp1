using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Synchronization
    {
        private int counter = 0;

        public  void Run()
        {
            string name = Thread.CurrentThread.Name;
            Console.WriteLine($"Thread {name} enters main()");
            while(counter<100)
            {
                int temp = counter;
                temp += 1;
                Thread.Sleep(1);
                Console.WriteLine($"Thread {name} encounterd counter {counter}");
                counter = temp;
            }
            Console.WriteLine($"Thread {name} Exiting now");

        }
        public void RunInterlocked()
        {
            string name = Thread.CurrentThread.Name;
            Console.WriteLine($"Thread {name} enters main()");
            while (counter < 100)
            {
                Interlocked.Increment(ref counter);
                //Thread.Sleep(1);
                Console.WriteLine($"Thread {name} encounterd counter {counter}");
              
            }
            Console.WriteLine($"Thread {name} Exiting now");

        }
        public static object _syncRoot = new object(); // DUMMY OBJECT

        public void RunMoniter()
        {
            string name = Thread.CurrentThread.Name;
            Console.WriteLine($"Thread {name} enters Run()");
            while (counter<100)
            {
                //Monitor.Enter(_syncRoot); // places a lock on the synclock byte of the object - synclock Bit is set
                Monitor.TryEnter(obj: _syncRoot, millisecondsTimeout: 2000);
                int temp = counter;
                temp++;
                Thread.Sleep(1);
                Console.WriteLine($"Thread {name} reports counter at {counter}");
                counter = temp;
                //Monitor.PulseAll(_syncRoot);  // notify all waiting threads
                Monitor.Exit(_syncRoot); // unlock the synclock bit of the synclock byte
            }
            Console.WriteLine($"Thread {name} completes execution , Exiting now");
        }
        static bool createdNew;
        static Mutex mtx = new Mutex(initiallyOwned:false,"Name",createdNew:out createdNew);

        public void RunMutex()
        {
            string name = Thread.CurrentThread.Name;
            Console.WriteLine($"Thread {name} enters {nameof(RunMutex)}");
            mtx.WaitOne();
            Console.WriteLine($"Thread {name} obtains mutux");
            Console.WriteLine("Simulating some activites");
            Thread.Sleep(1000);
            Console.WriteLine("Simulating some more activites");
            Console.WriteLine("Completed.");
            mtx.ReleaseMutex();
            Console.WriteLine($"Thread {name} exits");
        }
        // createdNew ->a new mutux is created or wether an existing one is obtained
        Semaphore sem = new Semaphore(initialCount: 0, maximumCount: 3);

        public void RunSemaphore()
        {
            string name = Thread.CurrentThread.Name;
            Console.WriteLine($"Thread {name} enters {nameof(RunMutex)}");
            sem.WaitOne();
            Console.WriteLine($"Thread {name} obtains mutux");
            Console.WriteLine("Simulating some activites");
            Thread.Sleep(1000);
            Console.WriteLine("Simulating some more activites");
            Console.WriteLine("Completed.");
            sem.Release(3);
            Console.WriteLine($"Thread {name} exits");
        }

        public void RunLock()
        {
            string name = Thread.CurrentThread.Name;
            Console.WriteLine($"Thread {name} enters Run()");
            while(counter<100)
            {

            lock (_syncRoot)
            {
                int temp = counter;
                temp += 1;
                Thread.Sleep(1000);
                Console.WriteLine($"Thread {name} reports counter {counter}");
                counter = temp;
            }
            }
            Console.WriteLine($"Thread {name} completes execution , Exiting now");
        }
        public static void Test()
        {
            Thread[] myThreads = new Thread[5];
            string[] names = { "first", "second", "third", "fourth", "fifth" };
            Synchronization s = new Synchronization();
            for(int i=0;i<5;i++)
            {
                myThreads[i] = new Thread(s.RunLock);
                myThreads[i].Name = names[i];
                myThreads[i].Start();
            }
            Console.WriteLine("All threads started waiting for completion.");
            Console.WriteLine("Press key to terminate");
            Console.ReadKey();
        }

    }
}
