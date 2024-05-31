using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class LinqOperators
    {
        //List initializers
        static List<string> cities = new List<string>
        {
            "Bengaluru","Chennai","Hydrabad","Vizag","Panaji","Thiruvananthapuram","Mumbai",
            "Gandhi Nagar","Jaipur","Chandigarh","Shimla","Dehra dun","Srinagar","Leh","Lucknow",
            "Patna","Raipur","Bhubaneswar","kolkata","Gangtok","Dispur","Itanagar","Aizwal","Imphal",
            "kohima","Shillong","Bhopal","Agartala","Ranchi","New Delhi","Punducherry","Kavaratti","Port Blair"
        };
        internal static void Test()
        {
            //BasicQuerying();
            //ProjectionOperator();
            //RestrictionOperators();
            //SortingOperators();
            //ElementOperators();
            //AggregationOperators();
            //PartitionOperators();
            GroupingOperators();
        }
        static Action<IEnumerable<string>, string> PrintTheList = (list, header) =>
        {
            Console.WriteLine($"**************** {header} *******************");
            Console.WriteLine();
            foreach(var item in list)
            {
                Console.Write($"{item,-20}");
            }
            Console.WriteLine("\n**********************************************");
            Console.WriteLine();
        };
        static int counter = 1;
        static string connectionString = @"Server=(local);database=northwind;integrated security=sspi;trustservercertificate=true;
            multipleactiveresultsets=true";
        static void DataSetsWithLinq()
        {
            DataSet ds = new DataSet();
            var sqlText1 = "SELECT CategoryId,CategoryName,Description FROM Categories";

            SqlConnection con = new SqlConnection(connectionString);
            con.StateChange += (sender, args) =>
            {
                Console.WriteLine($"State Changed to: {args.CurrentState} from {args.OriginalState}");
            };
            SqlDataAdapter adapter = new SqlDataAdapter(
                selectCommandText: sqlText1,
                selectConnection: con);
            adapter.Fill(ds, "Categories");

            var q = from row in ds.Tables["Categories"].AsEnumerable()
                    where row.Field<int>("CategoryId") < 5
                    select row;
            //Queryable -> SELECT * FROM Categories WHERE CateogryId < 5
            //Enumerable -> SELECT * FROM Categories;
            foreach(var item in q)
            {
                Console.WriteLine($"{item[0]} - {item[1]}");
            }
            var rows = ds.Tables["Categories"].Select("CategoryId < 5");
            foreach(var row in rows)
            {

            }
            foreach(DataRow row in ds.Tables["Categories"].Rows)
            {
                if (int.Parse(row[0].ToString()) < 5) { Console.WriteLine(row); }
            }
        }

        private static void Con_StateChange(object sender, StateChangeEventArgs e)
        {
            throw new NotImplementedException();
        }

        static void GroupingOperators()
        {
            var q1 = from c in cities
                     group c by c[0] into item
                     select item;
            Console.WriteLine($"Cities grouped by the first letter");
            foreach(var group in q1)
            {
                Console.WriteLine($"Group {group.Key}");
                group.ToList().ForEach(c => Console.WriteLine($"{c}"));
                Console.WriteLine();
            }
        }
        static void PartitionOperators()
        {
            // partition the data into smaller sections
            //Take, Skip, TakeWhile, SkipWhile, Chunk
            var q1 = cities.Take(10); //Takes 10 items and skips the remaining items
            var q2 = cities.Skip(20); //skips 20 items and takes the remaining items
            PrintTheList(q1, $"Test {counter++}. Take 10 items");
            PrintTheList(q2, $"Test {counter++}. Skip 20 items");
            var q3 = cities.Skip(5).Take(25).Skip(15).Take(7).Skip(2);
            PrintTheList(q3, $"Test {counter++}. S5T25S15T7S2");
            //Chunk operator - divide the set into smaller uniform sections
            var q4 = cities.Chunk(11);
            int i = 1;
            foreach(var section in q4)
            {
                Console.WriteLine($"Chunk {i++}");
                foreach(var item in section)
                {
                    Console.Write($"{item,-28}");
                }
                Console.WriteLine();
            }

            //TakeWhile -> takes the rows as long as the condition is true, stops when the condition is false
            //SkipWhile -> skips the rows as long as the condition is true, starts when the condition becomes false
            var q5 = cities.TakeWhile(c => c.Length < 10);
            var q6 = cities.SkipWhile(c => c.Length < 10);
            PrintTheList(q5, $"Test {counter++}. Take While Length <10 ");
            PrintTheList(q6,$"Test {counter++}. Skip While Length <10 ");

        }
        static void ProjectionOperator()
        {
            //Select, SelectMany, Zip
            var q1 = from c in cities
                     select new
                     {
                         StartsWith = c[0],
                         Length = c.Length,
                         Name = c
                     };

            Console.WriteLine($"*************** Test {counter++} Projection Query Syntax *******************");
            Console.WriteLine();
            foreach(var item in q1)
            {
                Console.WriteLine($"Starts With={item.StartsWith,-2}Length={item.Length:00} Name={item.Name}");
            }
            Console.WriteLine("\n****************************************************************************");
            Console.WriteLine();

            var q2 = cities.Select(c => new
            {
                StartsWith = c[0],
                Length = c.Length,
                Name = c
            });

            Console.WriteLine($"*************** Test {counter++} Projection Method Syntax *******************");
            Console.WriteLine();
            foreach (var item in q2)
            {
                Console.WriteLine($"Starts With={item.StartsWith,-2}Length={item.Length:00} Name={item.Name}");
            }
            Console.WriteLine("\n****************************************************************************");
            Console.WriteLine();

            var numbers = new List<int> { 1, 2, 3, 4, 5, 6 };
            var words = new List<string> { "A", "V", "E", "R", "Y" };
            foreach(var zippedItem in numbers.Zip(words))
            {
                Console.WriteLine($"{zippedItem.First} = {zippedItem.Second}");
            }
        }
        static void AggregationOperators()
        {
            //Sum, Min, Max, Count, Average
            //Forces Immediate Execution 
            var sum = cities.Sum(c => c.Length);
            var avg = cities.Average(c => c.Length);
            var min = cities.Min(c => c.Length);
            var max = cities.Max(c => c.Length);
            var count = cities.Count();
            Console.WriteLine($"Test {counter++}. Aggregation Operators");
            Console.WriteLine($"Sum:{sum}, Min:{min}, Max:{max}, Avg:{avg}, Count:{count}");

        }
        static void ElementOperators()
        {
            //First, Last, Single, ElementAt
            //FirstOrDefault, LastOrDefault,SingleOrDefault,ElementAtOrDefault
            //cannot be used with the declarative query syntax, used with the method syntex only
            var first = cities.First();
            var last = cities.Last();
            var itemAt = cities.ElementAt(4);
            //var firstMatching = cities.First(c => c.Length > 20); // throws exception, if no match is found
            //var lastMatching = cities.Last(c => c.Length < 10); // throws execption, if no match is found
            var firstMatching = cities.FirstOrDefault(c => c.Length > 20);
            var lastMatching = cities.LastOrDefault(c => c.Length < 10);
            var itemMatching = cities.ElementAtOrDefault(28);
            Console.WriteLine($"Test {counter++}. First Last Operators");
            Console.WriteLine($"First: {first}, Last: {last}, ElementAt(4): {itemAt}");
            Console.WriteLine($"Test {counter++}. First Last Matching Values");
            Console.WriteLine($"First Matching: {firstMatching}, Last Matching: {lastMatching}, Item At 28:{itemMatching}");
        }
        static void SortingOperators()
        {
            // OrderBy, OrderByDescending --> sort on the primary column
            // ThenBy, ThenByDescending   --> sort on the secondary column after sorting on the primary column
            // Reverse - reverse the order of elements
            var q1 = from c in cities
                     orderby c[0] descending, c[1] ascending  // WB, XC, TA, XA --> XA, XC, WB, TA
                     select c;
            PrintTheList(q1, $"Test {counter++}. Sorting on c[0] D, C[1] A");

            var q2 = cities
                .OrderBy(c => c[0])
                .ThenByDescending(c => c.Length)
                .ThenByDescending(c => c[1]);
            PrintTheList(q2, $"Test {counter++}. Sorting on c[0], A.Length D, c[1] D");

            var q3 = cities.Reverse<string>();
            PrintTheList(q3, $"Test {counter++}. Reversal");
        }
        static void RestrictionOperators()
        {
            //Where
            var q1 = from c in cities
                     where c.Length > 10
                     select c;
            PrintTheList(q1, $"Test {counter++}. Filter Length > 10 ");

            var q2 = cities.Where(c => c.Contains("an")).Select(c => c);
            PrintTheList(q2, $"Test {counter++}. Filter Name contains 'na'");

            // Complex filters using && and || operators
            var q3 = from c in cities
                     where c.Length > 8 || c.StartsWith("B")
                     select c;
            PrintTheList(q3, $"Test {counter++}. Filter Name starts with 'B' or Length > 8");
        }
        static void BasicQuerying()
        {
            var q1 = from item in cities
                     select item;
            //defered query - Lazy initialization - query does not hold the data.
            PrintTheList(q1, $"Test {counter++} Basic Querying");

            // METHOD SYNTAX
            var q2 = cities.Select(c => c);
            PrintTheList(q2, $"Test {counter++} Method Querying");
        }
    }
}
