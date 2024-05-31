using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    /*sealed*/ class BaseClass
    {
        private int PrivateInt;
        protected int ProtectedInt;
        public int PublicInt;
        internal int InternalInt;
        protected internal int ProtectedInternalInt;

        public BaseClass()
        {
            Console.WriteLine("BaseClass.cotr(void) called.");
        }
        public BaseClass(int number)
        {
           PrivateInt = PublicInt = ProtectedInt = InternalInt= ProtectedInternalInt = number;
        }
        public /*sealed*/ virtual void Show()
        {
            StringBuilder sb = new StringBuilder(); // strings are immutable in .NET
            sb.AppendLine("***** BaseClass.Show() ********")
                .AppendLine($"Private Int: {PrivateInt}")
                .AppendLine($"Public Int: {PublicInt}")
                .AppendLine($"Protected Int: {ProtectedInt}")
                .AppendLine($"Internal Int: {InternalInt}")
                .AppendLine($"Protected Internal Int: {ProtectedInternalInt}");
            Console.WriteLine(sb.ToString());
            // string s = "Hello"; s = s + "world";
        }
    }
    class DerivedClass : BaseClass
    {
        public DerivedClass():base()
        {
            Console.WriteLine("DerivedClass .ctor( void ) called. ");
        }
        public DerivedClass(int number):base(number)
        {
            //privateInt = number;
            //protectedInt = publicInt = internalInt = protInternalInt = number;
            Console.WriteLine("DerivedClass .ctor(int ) called. ");
        }
         public override void Show()
        {
            StringBuilder sb = new StringBuilder(); // strings are immutable in .NET
            sb.AppendLine("***** DerivedClass.Show() ********")
                //.AppendLine($"Private Int: {PrivateInt}")
                .AppendLine($"Public Int: {PublicInt}")
                .AppendLine($"Protected Int: {ProtectedInt}")
                .AppendLine($"Internal Int: {InternalInt}")
                .AppendLine($"Protected Internal Int: {ProtectedInternalInt}");
            Console.WriteLine(sb.ToString());
            // string s = "Hello"; s = s + "world";
        }
    }
    internal class Inheritance:BaseClass
    {
        public static void Test()
        {
            BaseClass bc1 = new BaseClass();
            //bc1.Show();
            BaseClass bc2 = new BaseClass(1234);
            //bc2.Show();
            DerivedClass dc1 = new DerivedClass();
            //dc1.Show();
            DerivedClass dc2 = new DerivedClass(9876);
            //dc2.Show();
            bc1 = dc1; // bc1 will store the base of dc1
            bc1.Show(); // It will call derived class show() because of virtual table

        }
    }
}
