using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Product
    {
        private string _name;
        private string _productCode;
        private string _brandName;
        private int _stockLeft;
        private double _price;

        public string Name { get { return _name; } set { _name = value; } }
        public string ProductCode { get { return _productCode; } set { _productCode = value; } }

        public string BrandName { get { return _brandName; } set { _brandName = value; } }
        public int StockLeft { get { return _stockLeft; } set { _stockLeft = value; } }

        public double Price { get { return _price; } set { _price = value; } }

        Product()
        {

        }
        public Product(string _name, string _productCode, string _brandName, int _stockLeft, double _price)
        {
            this._name = _name;
            this._productCode = _productCode;
            this._brandName = _brandName;
            this._stockLeft = _stockLeft;
            this._price = _price;
        }
    }

    public class Program1
    {
 //       public static void Main()
 //       {
 //           List<Product> products = new List<Product>() {
 //new Product("Dairy milk","#CA 12 DA-159","Cadbury",500,25),
 //new Product("Milky bar","#NE 18 MI-162","Nestle",600,10),
 //new Product("Lifebuoy","#UN 20 LI-285","Unilever",195,31),
 //new Product("Almond oil","#BA 11 AL-789","Bajaj",400,72),
 //new Product("Fuse","#CA 17 FU-110","Cadbury",300,20)
 //};
 //           List<Product> Result = new List<Product>();
 //           Console.WriteLine("Enter a search type:\n1.By brand name\n2.By price");
 //           int choice = int.Parse(Console.ReadLine());
 //           if (choice == 1)
 //           {
 //               Console.WriteLine("Enter the brand name:");
 //               string name = Console.ReadLine();
 //               bool found = true;
 //               var pds = new List<Product>();
 //               foreach (Product p in products)
 //               {

 //                   if (p.BrandName == name)
 //                   {
 //                       found = true;
 //                       pds.Add(p);
 //                       //Console.WriteLine("{0,-15} {1,-15} {2,-12} {3,-12} {4,-7}", p.Name, p.ProductCode, p.BrandName, p.StockLeft, p.Price);
 //                   }
 //               }
 //               if (!found)
 //               {
 //                   Console.WriteLine("No such product is present");
 //               }
 //               else
 //               {
 //                   Console.WriteLine("{0,-15} {1,-15} {2,-12} {3,-12} {4,-7}", "Name", "Product Code", "Brand Name", "Stock Left", "Price");
 //                   foreach (var p in pds)
 //                   {
 //                       Console.WriteLine("{0,-15} {1,-15} {2,-12} {3,-12} {4,-7}", p.Name, p.ProductCode, p.BrandName, p.StockLeft, p.Price);
 //                   }
 //               }

 //           }
 //           else if (choice == 2)
 //           {
 //               Console.WriteLine("Enter the price:");
 //               double price = double.Parse(Console.ReadLine());
 //               bool found = false;
 //               var pds = new List<Product>();
 //               foreach (Product p in products)
 //               {

 //                   if (p.Price == price)
 //                   {
 //                       found = true;
 //                       pds.Add(p);
 //                       //Console.WriteLine("{0,-15} {1,-15} {2,-12} {3,-12} {4,-7}", p.Name, p.ProductCode, p.BrandName, p.StockLeft, p.Price);
 //                   }
 //               }
 //               if (!found)
 //               {
 //                   Console.WriteLine("No such product is present");
 //               }
 //               else
 //               {
 //                   Console.WriteLine("{0,-15} {1,-15} {2,-12} {3,-12} {4,-7}", "Name", "Product Code", "Brand Name", "Stock Left", "Price");
 //                   foreach (var p in pds)
 //                   {
 //                       Console.WriteLine("{0,-15} {1,-15} {2,-12} {3,-12} {4,-7}", p.Name, p.ProductCode, p.BrandName, p.StockLeft, p.Price);
 //                   }
 //               }
 //           }
 //           else
 //           {
 //               Console.WriteLine("Invalid choice");
 //           }
 //       }
    }
}
