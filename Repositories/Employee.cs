using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Repositories
{
    //Entity Class
    public class Employee
    {
        public int EmployeeId { get; set; } 
        public string FirstName { get; set; }

        public string LastName { get; set; }
       
        public DateTime HireDate { get; set; }
    }
    //public class Products
    //{
    //    public int ProductId { get; set; }
    //    public string ProductName { get; set; }
    //    public bool Discounted { get; set; }
    //}
    //Context class
    public class EmployeeDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                connectionString: @"Server=(local);database=northwind;integrated security=sspi;trustservercertificate=true;
            multipleactiveresultsets=true");
        }
        public DbSet<Employee> Employees { get; set; }
        //public DbSet<Products> Products { get; set; }
    }
}
