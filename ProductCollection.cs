using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class ProductCollection:System.Collections.CollectionBase
    {
        //Method Expression initializer
        public void Add(Products product)=>List.Add(product);

        public Products GetAt(int position)
        {
            return (Products)List[position];
        }
        public void Remove(Products product)
        {
            List.Remove(product);
        }
        public Products[] GetAll()
        {
            Products[] products = new Products[List.Count];
            List.CopyTo(products, 0);
            return products;
        }

        public Products this[int index]
        {
            get { return List[index] as Products; } set { List[index] = value; }
        }
        //Property Expression
        public int Length {  get => List.Count;  }


    }
}
