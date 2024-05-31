using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class ExceptionHandling
    {
        public static void ThrowsException()
        {
            int a= 0; int b=10;
            int c = b / a;
        }
        public static void HandleException()
        {
            try
            {
            RaiseCustomException();
            }catch(CustomeException ce)
            {
                Console.WriteLine("Error");
                Console.WriteLine(ce.Message);
                Console.WriteLine(ce.CustomMessage);
            }
            catch(DivideByZeroException dbze)
            {
                Console.WriteLine($"ERROR : {dbze.Message}");
            }
            catch(ArithmeticException dbge)
            {
                Console.WriteLine($"ERROR: {dbge.Message}");

            }
            catch(Exception ex)
            {
                Console.WriteLine($"ERROR : {ex.Message}");
            }
            finally
            {
                Console.WriteLine($"FINALLY : This block is executed");
            }
            Console.WriteLine($"Some other code can get executed after this");
        }
        public static void RaiseCustomException()
        {
            CustomeException cm = new CustomeException("This is custom exception")
            {
                CustomMessage = "This is Error"
            };
            throw cm;
        }
        public static void Test()
        {
            HandleException();

        }
    }
}
