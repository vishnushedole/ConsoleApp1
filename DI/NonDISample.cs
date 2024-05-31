using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.DI
{
    internal class NonDISampleClass
    {
        public void serve()
        {
            Console.WriteLine("NonDISampleClass.Serve() called.");
        }
    }
    public class NonDIClientClass
    {
        private NonDISampleClass _service;
        public NonDIClientClass()
        {
            _service = new NonDISampleClass();
        }
        public void Start()
        {
            Console.WriteLine("Service started.");
        }
    }
}
