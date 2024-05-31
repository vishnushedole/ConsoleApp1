using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ConsoleApp1
{
    public class  TestClass
    {
        private int _id;
        private string _name;

        public TestClass(int id, string name)
        {
            _id = id;
            _name = name;
        }
        public int Id { get { return _id; } set { _id = value;} }
        public string Name { get { return _name; } set { _name = value; } }

        public void Show()
        {
            Console.WriteLine($"ID: {_id} Name:{_name}");
        }
    }
    internal class WorkingWithReflection
    {
        public static void Test()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            // Find the module = projectname.exe/ projectname.dll
            Module mod = assembly.GetModule("ConsoleApp1.dll");
            Type t = mod.GetType("ConsoleApp1.TestClass");
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("--------- Type Information --------")
                .AppendFormat("Name: {0}\n", t.Name)
                .AppendFormat("Full Name : {0}\n", t.FullName)
                .AppendFormat("Visibility: {0}\n",( t.IsPublic)?"Public":"Not Public")
                .AppendFormat("Base Class: {0}\n", t.BaseType.FullName);
            sb.AppendLine();


            var bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
            FieldInfo[] fields = t.GetFields(bindingAttr: bindingFlags);
     
            sb.AppendLine("\n---------------- Field Infomration ------------");
            foreach(FieldInfo field in fields)
            {
                sb.AppendFormat("Field Name: {0}\n", field.Name)
                    .AppendFormat("Field Type: {0}\n", field.FieldType.FullName)
                    .AppendFormat("Visibility: {0}\n", field.IsPublic ? "Public" : "Not Public")
                    .AppendFormat("Declaring Type: {0}\n", field.DeclaringType.FullName);
                    //.AppendFormat("Initial value:{0}\n", field.GetRawConstantValue());
            }
            sb.AppendLine();

            ConstructorInfo[] constructors = t.GetConstructors(bindingAttr: bindingFlags);

         
            sb.AppendLine("\n---------------- Constructor Infomration ------------");
            foreach (ConstructorInfo ctor in constructors)
            {
                sb.AppendFormat("Field Name: {0}\n", ctor.Name)
                    .AppendFormat("Visibility: {0}\n", ctor.IsPrivate ? "Public" :(ctor.IsPublic)?"Public":(ctor.IsAssembly)?"Internal":"Protected")
                    .AppendFormat("Has parameters : {0}\n", ctor.GetParameters().Length>0?"Yes":"No");
                  foreach(ParameterInfo par in  ctor.GetParameters())
                {
                    sb.AppendFormat("Parameter Name: {0}, Parameter Type: {1}", par.Name, par.ParameterType.FullName);
                }
                sb.AppendLine();
            }
            Console.Write(sb.ToString());

            Console.WriteLine("Attempting to instantiate the type");
            object obj = Activator.CreateInstance(t, 999, "Hello World");
            Console.WriteLine("Object created");
            t.InvokeMember(
                name: "Show",
                invokeAttr: BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod,
                binder: null,
                target: obj,
                args: null);
        }
    }
}
