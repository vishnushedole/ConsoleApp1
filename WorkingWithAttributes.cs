using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple =true)]
    public class DeveloperAttribute:System.Attribute
    {
        private string _name = string.Empty;
        public string Name { get { return _name; } set { _name = value; } }
        public string Description { get; set; } = string.Empty;

        public DeveloperAttribute(string Name) 
        {
            _name = Name;
        }
        public override string ToString()
        {
            return $"Attributes Details\n\tName : {_name}\n\tDescription : {Description}";
        }
    }
    [DeveloperAttribute("Vishnu Shedole",Description ="Eurofins TG")]
    [Developer("Vishnu S",Description ="Eurofins Training")]
    public class WorkingWithAttributes
    {
        static void listDevelopers()
        {
            List<object> attributes = typeof(WorkingWithAttributes).GetCustomAttributes(false).ToList(); 
            attributes.ForEach (c => Console.WriteLine(c) ) ;
        }
        internal static void Test()
        {
            listDevelopers();
        }
    }
}
