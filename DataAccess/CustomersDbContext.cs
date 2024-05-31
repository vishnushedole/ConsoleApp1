using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.DataAccess
{
    public class CustomersDbContext:Microsoft.EntityFrameworkCore.DbContext
    {
        // Property name should match the table name and it sholud be plural
        public DbSet<Customer> Customers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Use the SqlServer .NET Data Provider Classes - SqlConnection
            optionsBuilder.UseSqlServer(
                connectionString: @"Server=(local);database=northwind;integrated security=sspi;trustservercertificate=true;
            multipleactiveresultsets=true");

            // creating a logger for inspecting the generated SQL
            optionsBuilder.LogTo (Console.Out.WriteLine);
        }
    }
}
