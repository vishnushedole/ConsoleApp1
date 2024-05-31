using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public interface IBase
    {
        void DoWork();
    }
    public interface IChild : IBase
    {
        void PerformTask();
        string Name { get; set; }
    }
    public interface ITransact
    {
        void PerformTask();

        /*
         * Defining method of interface in interface is Allowed 
         * void DoWork()
        {
            Console.WriteLine("ITransact.DoWrok() called.");
        }*/
    }
    public class InterfaceImplementation : IBase,IChild,ITransact
    {
        public void DoWork()
        {
            Console.WriteLine("InterfaceImplementation.DoWork() called.");
        }
        public void PerformTask() { 
            Console.WriteLine("InterfaceImplementation.PerformTask() called.");
            // IChild.PerformTask(); Not allowed 
        }
        public String Name { get; set; }

        // Explict implementation of the interface
        void IChild.PerformTask() { Console.WriteLine("IChild.PerformTask() called."); }
        void ITransact.PerformTask() { Console.WriteLine("ITransact.PerformTask() called."); }
    }

    public class Interfaces
    {
        internal static void Test()
        {
            InterfaceImplementation ii = new InterfaceImplementation();
            ii.DoWork();
            ii.PerformTask();
            ii.Name = "Test";
            Console.WriteLine("ii.Name: {0}", ii.Name);

            // Dynamic / Late Binding
            IBase ib = ii; //ib is called as the interface pointer
            ib.DoWork();
            IChild ic = ii;
            ic.DoWork();
            ic.PerformTask();
            ic.Name = "Test";
            ITransact it = ii;
            it.PerformTask();
            
        }
    }
}
