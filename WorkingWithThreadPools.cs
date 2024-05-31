using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class WorkingWithThreadPools
    {
        static void TaskToRun(object state)
        {
            if(state == null)
            {
                Console.WriteLine("State is null.");
            }
            else
            {
                if (state is Products p)
                    p.show();
                else
                    Console.WriteLine("TaskToRun() is called and complets here.");
            }
        }
        internal static void Test()
        {
            ThreadPool.GetMaxThreads(out int workerThreads, out int ioThreads);
            Console.WriteLine("Max Threads in the CLR ThreadPool: Worker {0}, IO:{1}", workerThreads, ioThreads);
            for(int i=0;i<10;i++)
            {
                if (i % 3 == 0)
                    ThreadPool.QueueUserWorkItem(TaskToRun!, new Products(i, "sample",i*324,Convert.ToInt16(i*87)));
                else
                    ThreadPool.QueueUserWorkItem(TaskToRun!, i);
                Thread.Sleep(1);
            }
            Console.WriteLine("Pooled tasks created. waiting for completion.");
            Console.ReadKey();
            ThreadPool.GetAvailableThreads(out  workerThreads, out  ioThreads);
            Console.WriteLine("Available Threads in the CLR ThreadPool: Worker {0}, IO:{1}", workerThreads, ioThreads);
        }
    }
}
