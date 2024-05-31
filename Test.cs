using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class TestClass1
    {
  public static void Test()
        {
            int x = 0, y = 0, z = 0;
            try
            {
                  z = x / y;
                  Console.Write("Success,");
                 }
            catch (Exception ex)
            {
                  Console.Write("Error,");
                 }
            finally
            {
                  Console.Write("Complete,");
                 }
              Console.WriteLine("Terminating");
             }
 }
}
