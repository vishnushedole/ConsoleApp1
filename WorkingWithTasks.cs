using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class WorkingWithTasks
    {
        public static void Run()
        {
            Console.WriteLine("Run() task got executed");
        }
        internal static async void Test()
        {
            Console.WriteLine("Different styles of creating tasks");
            Task t1 = new Task(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("Task T1 executed");
            });
            t1.Start();
            Task t2 = new Task((p) =>
            {
                if (p is Products x)
                    x.show();
            },new Products(1,"sample",123));
            t2.Start();

            Task t3 = new Task(Run);
            t3.Start();

            Task t4 = Task.Run(() =>
            {
                Console.WriteLine("Task 4 started.");
            });

            Task<decimal> t5 = new Task<decimal>((p) =>
            {
                Console.WriteLine("Task 5 started.");
                if (p is Products x)
                {
                    var stockValue = x.UnitsInStock * x.UnitPrice;
                    return stockValue;
                }
                else
                    return 0M;
            }, new Products(1, "sample", 112, 123));
            t5.Start();

            Task t6 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Task 6 started.");
            });

            var value = t5.Result;  // call is blocked till the result is retrieved.
            Console.WriteLine($" out put of task5 : {value}");

            Func<decimal, decimal, decimal> f2 = (a, b) => a * b;
            var t7 = Task.FromResult<decimal>(t5.Result);
            //t7.Start();
            //var result = t7.Result;
            var result = t7.GetAwaiter().GetResult();
            Console.WriteLine($"Decimal multiplication yeilds {result}");

            var changedTask = Task<int>.Factory.StartNew(() =>
            {
                Console.WriteLine("Task 8 started.");
                return 600;
            }).ContinueWith<int>(t =>
            {
                Console.WriteLine($"Continued task 8 {t.Result}");
                return 100;
            }).ContinueWith<string>(t =>
            {
                Console.WriteLine($"Continued task 8 {t.Result}");
                return "Hello world";
            });

            var msg = await GetAddressAsync();
            await Console.Out.WriteLineAsync( msg );
            ParallelTasks();
            Console.WriteLine("Task started waiting for execution.");
            Task.WaitAll(t1,t2,t3,t4,t5,t6,t7,changedTask);
            Console.ReadKey();
        
        }
        static string GetAddress(string input)
        {
            Thread.Sleep(1000);
            if (input == "Microsoft" || input == "Orcle")
                return $"{input} address found";
            else
                return $"{input} address not found";
        }
        static async Task<string>GetAddressAsync()
        {
            Console.WriteLine("Enter the name:");
            string name = Console.ReadLine();
            var t = new Task<string>((str) => GetAddress(str.ToString()!), name);
            return t.Result;
        }
        internal static void ParallelTasks()
        {
            Parallel.Invoke(
                 ()=>Console.WriteLine("Circle is drawn"),
                    () =>  Console.WriteLine("Rectangle drawn"),
                    () =>  Console.WriteLine("Square drawn"),
                        () =>  Console.WriteLine("Traingle drawn")
                );
            Console.WriteLine("Start all drawing, waiting for completion");
            Console.ReadKey();
        }
    }
}
