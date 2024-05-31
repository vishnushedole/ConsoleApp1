using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class MemoryManagement:IDisposable
    {
        private int _counter;
        private readonly int MaxSize = 10_000;
        //10_000 -> underscore is called as digit separator -c# language feature
        //readonly -> like constant with a leeway
        //         -> readonly variables can be assigned values during the construction process.
        //         -> post object construction process, it is like constant (cannot change)

        private string[] names;
        public MemoryManagement(int counter)
        {
            _counter = counter;
            names = new string[MaxSize];
            for (int i = 0; i < MaxSize; i++)
            {
                names[i] = $"This is some long string created to quickly fill up memory {i}";

            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\t\tObject {_counter} created");
            Console.ResetColor();
        }
        ~MemoryManagement()
        {
            names = null!;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\t\t\tObject {_counter} Finilized");
            Console.ResetColor();

        }
        internal static void Test()
        {
            MemoryManagement mm;
            for(int i=0;i<100;i++)
            {
                mm = new MemoryManagement(i);
                if(i>35 && i<75)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Generation of 'mm' is {0}", GC.GetGeneration(mm));
                    Console.ResetColor();
                    if(i>40 && i<70)
                    {
                        // Forced collection attempt
                        GC.Collect();
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Generation of 'mm' is {0}", GC.GetGeneration(mm));
                        Console.ResetColor();
                        if(i>50 && i<60)
                        {
                            GC.Collect();
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            Console.WriteLine("Generation of 'mm' is {0}", GC.GetGeneration(mm));
                            Console.ResetColor();
                        }
                    }
                }

                if (i > 85 && i < 95)
                    mm.Dispose();
            }
            Console.WriteLine("Press a key to terminate....");
            Console.ReadKey();

            using(mm = new MemoryManagement(123456))
            {
                Console.WriteLine("From within the using block");
            }
        }

        public void Dispose()
        {
            names = null;
            
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"\t\t\t\tObject {_counter} Disposed");
            Console.ResetColor();
            // Removes the entry of this object from the finalizer list
            GC.SuppressFinalize(this);
        }
    }
}
