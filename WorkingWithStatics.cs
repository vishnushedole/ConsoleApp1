using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp1
{
    class Statics
    {
        public static int StaticField = 1234;
        public int InstanceField = 4321;

        static Statics()
        {
            StaticField = 9876;
            Console.WriteLine("Statics.StaticConstructor called.");
        }
        internal Statics()
        {
            InstanceField = 6789;
            StaticField = 5678;
            Console.WriteLine("Static.InternalConstructor called.");
        }
        internal static void StaticShow()
        {
            Console.WriteLine($"Statics.StaticShow() return {StaticField}");
        }
        internal void InstanceShow()
        {
            Console.WriteLine($"Statics.InstanceShow() return {StaticField}");
            Console.WriteLine($"Statics.InstanceShow() return {InstanceField}");
        }

    }
    internal class WorkingWithStatics
    {
        public static void Test()
        {
            Statics s = new Statics();
            s.InstanceShow();
            Statics s1 = new Statics();
            s1.InstanceField = 111;
        }
    }
}
