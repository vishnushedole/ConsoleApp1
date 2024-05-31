using ConsoleApp1.DataAccess;
using ConsoleApp1.Repositories;
using ConsoleAppVS2022;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace ConsoleApp1
{
    public static class Utilities
    {

        public static bool IsValid(this string email) { return email.Contains("@"); }
    }
   
    internal class Program
    {
        public static void TestAuthors()
        {
            Authors author = new Authors();
            AuthorManager authorManager = new AuthorManager();
            int choice = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("***************** Author Management *************");
                Console.WriteLine("********** 1. Create Author");
                Console.WriteLine("********** 2. Update Author");
                Console.WriteLine("********** 3. Display Authors");
                Console.WriteLine("********** 4. Find Author by Id");
                Console.WriteLine("********** 5. Remove Author by Id");
                Console.WriteLine("**********");
                Console.WriteLine("********** 0. Quit");
                Console.WriteLine("*************************************************");
                Console.WriteLine("Enter Choice _");
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    if (choice == 1)
                    {

                        Console.WriteLine("Enter Id:");
                        int id = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter First Name:");
                        string firstName = Console.ReadLine();
                        Console.WriteLine("Enter Last Name:");
                        string lastName = Console.ReadLine();
                        Console.WriteLine("Enter City");
                        string city = Console.ReadLine();

                        author = authorManager.CreateNewAuthor(id, firstName, lastName, city);

                        author.ShowDetails();
                    }
                    //Console.WriteLine("Create called");}
                    else if (choice == 2)
                    {
                    
                        Console.WriteLine("Update Author details: (Previous values are displayed in brackets)");
                        Console.WriteLine($"Enter the Id of Author:");
                        string id = Console.ReadLine();

                        Authors Targetauthor = authorManager.FindById(int.Parse(id));
                        if (Targetauthor != null)
                        {
                            Console.WriteLine($"First Name ({author.FirstName}):");
                            string firstName = Console.ReadLine();

                            Console.WriteLine($"Last Name ({author.LastName}):");
                            string lastName = Console.ReadLine();

                            Console.WriteLine($"City ({author.City}):");
                            string city = Console.ReadLine();

                            authorManager.UpdateAuthor(author, id, firstName, lastName, city);
                            author.ShowDetails();
                        }
                        else
                        {
                            Console.WriteLine($"Author with {id} not found");
                        }
                        //Console.WriteLine("Update called");
                    }
                    else if (choice == 3)
                    {
                        var AuhtorsArray = authorManager.ListAllAuthors();
                        foreach (var aut in AuhtorsArray)
                        {
                            aut?.ShowDetails();
                        }
                    }
                    else if(choice == 4)
                    {
                        Console.WriteLine("Enter Id to find Author");
                        int id = int.Parse(Console.ReadLine());
                        Authors Targetauthor = authorManager.FindById(id);
                        if (Targetauthor != null)
                            Targetauthor.ShowDetails();
                        else
                            Console.WriteLine($"Author with id :{id} Not found");
                    }
                    else if(choice ==5)
                    {
                        Console.WriteLine($"Enter the id of author:");
                        int id = int.Parse(Console.ReadLine());
                        if (authorManager.RemoveAuthor(id))
                            Console.WriteLine($"Author with {id} removed.");
                        else
                            Console.WriteLine($"Author with {id} Not found");
                    }
                    else if (choice == 0)
                    {
                        Console.WriteLine("Quit the program");
                        break;
                    }
                    else

                    {
                        Console.WriteLine("Invaid number");
                    }
                    }
                    else
                    {
                        Console.WriteLine("Only numbers are allowed");
                        choice = -1;
                    }
                    Console.ReadKey();
                } while (choice != 0) ;

            }
        static void TestEmployeeRepository()
        {
            Action<Employee> PrintDetails = (c) => Console.WriteLine($"{c.EmployeeId}, {c.FirstName}");
            EmployeeRepository employeeRepository = new EmployeeRepository();
            var Employees = employeeRepository.GetAll();
            Employees.ToList().ForEach(x => Console.WriteLine($"{x.EmployeeId},{x.FirstName}"));

            var emp = employeeRepository.FindById(1);
            PrintDetails(emp);
            emp = new Employee
            {
                EmployeeId = 1,
                FirstName = "Updated Name",
                LastName = "Choudhary",
                HireDate = DateTime.Now
            };
            //employeeRepository.Upsert(emp);
            ////refetch to check the insert
            //var items = employeeRepository.GetAll();
            //items.ToList().ForEach(x => PrintDetails(x));

            Console.WriteLine("Removing Employee");
            employeeRepository.RemoveById(10);

            var items = employeeRepository.GetAll();
            items.ToList().ForEach(x => PrintDetails(x));
        }
        static void Main(string[] args)
        {
            //Console.WriteLine("Enter the email:");
            //string s = new string(Console.ReadLine());
            //if (s.IsValid())
            //    Console.WriteLine("Valid email");

            /*for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(i);
            }
            Sample.Test();*/
            //TestProducts();
            /*string email = "someone@gmail.com";
            if(email.IsValid())
            //if(Utilities.IsValid("someone@gmail.com"))
            {
                Console.WriteLine("valid email");
            }*/
            //MemoryManagement.Test();
            //WorkingWithDelegates.Test();
            //EventDelegationModel.Test();
            //MarksDelegate.Test();
            //TestAuthors();
            //ExceptionHandling.Test();
            //WorkingWithCollections.Test();
            /*Action ac = () => Console.WriteLine("Action delegate without arguments called.");
            Action<int> ac1 = (input) => { Console.WriteLine(input); };
            Action<int, int, string> ac2 = (num1, num2, str) =>
            {
                Console.WriteLine($"Arg0 {num1}");

                Console.WriteLine($"Arg0 {num2}");

                Console.WriteLine($"Arg0 {str}");
            };

            ac();
            ac1(100);
            ac2(10, 20, "input");

            Func<string> f = () => { return "hello World";  };
            Func<int, string> f1 = (num) => num.ToString();
            Func<int, int, int> f2 = (num1, num2) => num1 + num2;
            Console.WriteLine(f());
            Console.WriteLine(f1(10));
            Console.WriteLine(f2(10, 20));

            Predicate<int> CheckEven = (x) => x % 2 == 0;
            Action<LinkedList<string>, string> PrintList = (words, testmsg) =>
            {
                Console.WriteLine(testmsg);
                foreach (var word in words)
                {
                    Console.WriteLine(word + " ");
                }
                Console.WriteLine();
            };
            string[] words = { "eurofins", "microsoft", "oracle" };
            LinkedList<string> companies = new LinkedList<string>(words);
            PrintList(companies, "List items");
            companies.AddFirst("Amadeus");
            PrintList(companies, "New item added");
            LinkedListNode<string> startNode = companies.First;
            while(startNode != null)
            {
                Console.WriteLine(startNode.Value);
                startNode = startNode.Next;
            }*/
            //WorkingWithThreads.Test();
            //Synchronization.Test();
            //WorkingWithResetEvents.Test();
            //NumberInWords.Test();
            //AllocateSeats.Test();
            //WorkingWithThreadPools.Test();
            //WorkingWithTasks.Test();
            //ParallelProgramming.Test();
            //WorkingWithAttributes.Test();
            //WorkingWithReflection.Test();
            //FileSystemManipulation.Test();
            //WorkingWithStreams.Test();
            //TestClass1.Test();
            WorkingWithADONET.Test();
            //CustomerDataAccess cda = new CustomerDataAccess();
            //WorkingWithADONET.Test8();
            //WorkingWithDataSet.Test2();
            //LinqOperators.Test();

            //CustomersDbContext db  = new CustomersDbContext();

            //Add a new Customer object
            //Customer custObj = new Customer
            //{
            //    CustomerId = "66778",
            //    CompanyName = "66778",
            //    ContactName = "66778",
            //    City = "66778",
            //    Country = "66778"
            //};
            //db.Customers.Add(custObj);

            // Remove customer
            //db.Customers.Remove(custObj);

            //db.SaveChanges();
            //var list = db.Customers.ToList();

            //foreach(var item in list) Console.WriteLine(item.CustomerId);

            //var list = db.Customers.FromSql($"select customerid,companyname,contactname,city,country from customers").ToList();

            //foreach (var item in list)
            //    Console.WriteLine($"{item.CustomerId} - {item.CompanyName}");

            //var q1 = from c in db.Customers
            //         .AsEnumerable()
            //         where c.Country == "USA"
            //         orderby c.CompanyName
            //         select c;
            //q1.ToList().ForEach(c =>
            //{
            //    Console.WriteLine("ID: {0}, Company: {1}", c.CustomerId, c.CompanyName);
            //    Console.WriteLine("\tContact: {0}, Location: {1}-{2}", c.ContactName, c.City, c.Country);
            //});

            //Customer c = cda.GetCustomerById(1);

            //Console.WriteLine("Id:{0}\tCompany Name:{1}\tContact:{2}\tCity:{3}\tCountry:{4}", c.CustomerId,c.CompanyName,c.ContactName,c.City,c.Country);
            //Func<string, string> GetInput = (text) =>
            //{
            //    Console.WriteLine($"Enter {text} :");
            //    var str = Console.ReadLine();
            //    return str;
            //};

            //Customer cust = new Customer
            //{
            //    CustomerId = GetInput("Customer Id"),
            //    CompanyName = GetInput("Company Name"),
            //    ContactName = GetInput("Contact Name"),
            //    City = GetInput("City"),
            //    Country = GetInput("Country")
            //};
            //cda.Insert(cust);

            //Func<string, string, string> GetInputForUpdate = (text, currentValue) =>
            //{
            //    Console.WriteLine($"Enter {text} [{currentValue}]");
            //    var str = Console.ReadLine();
            //    return str.Length == 0 ? currentValue : str;
            //};
            //var id = "12345";

            //var cust = cda.GetCustomerById(int.Parse(id));

            //cda.UpdateCustomer(cust);
            //TestEmployeeRepository();
            //WorkingWithWebApi.test();
        }
        static void TestProducts()
            {
                /*Products p = new Products();
                p.ProductId = 10;
                p.show();
                Console.WriteLine(p.ProductId);
                Products p2 = new Products
                {
                    ProductId = 10,
                    ProductName = "Eurofins",
                    UnitPrice = 10M
                };*/
                /*Products p3 = new Products(1);
                Products p4 = new Products(2, "sample");*/
                //Products p5 = new Products(3, "Sample1", 123.4M);
                //Inheritance.Test();
                //Interfaces.Test();
                WorkingWithStatics.Test();
            }
        }
    }

