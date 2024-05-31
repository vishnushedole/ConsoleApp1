using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Products
    {
        #region Fields
        private int _productId = 0;
        private string _productName = string.Empty;
        private decimal _unitPrice = 0M;
        private short _unitsInStock = 0;
        private bool _isDiscounted = false;
        #endregion

        #region Methods 
        public void show()
        {
            Console.WriteLine($"Id:{_productId}, Name:{_productName}, Price:{_productName}, " +
                $"Price:{_unitPrice}, Stock:{_unitsInStock}, Discounted:{_isDiscounted}"
                );
        }
        #endregion
        #region Properties
        public int ProductId
        {
            get { return _productId; }
            set { _productId = value; }
        }
        public string ProductName
        {
            get { return _productName; }
            set { _productName = value; }
        }
        public decimal UnitPrice
        {
            get { return _unitPrice; }
            set { _unitPrice = value; }
        }
        public short UnitsInStock
        {
            get { return _unitsInStock; }
            set { _unitsInStock = value; }
        }
        public bool IsDiscounted
        {
            get { return _isDiscounted; }
            set { _isDiscounted = value; }
        }
        public string Description
        {
            get; set;
        }
        #endregion
        #region Constructor
        private Products():this(0)
        {
            /*_productId = 0;
            _productName = string.Empty;
            _unitPrice = 0;
            _unitsInStock = 0;
            _isDiscounted = false;*/
            Console.WriteLine("Products() called");
        }
        public Products(int productId):this(productId,string.Empty)
        {
            /*_productId = productId;
            _productName = string.Empty;
            _unitPrice = 0;
            _unitsInStock = 0;
            _isDiscounted = false;*/
            Console.WriteLine("Products(int productId) called");
        }
        public Products(int productId,string name):this(productId,name,0)
        {
            /*productId = productId;
                _productName = name;
            _unitPrice = 0;
            _unitsInStock = 0;
            _isDiscounted = false;*/
            Console.WriteLine("Products(int productId,string name) called");
        }
        public Products(int productId,string name,decimal price)
            :this(productId,name,price,0)
        {
            /*_productId = productId;
            _productName = name;
            _unitPrice = price;
            _unitsInStock = 0;
            _isDiscounted = false;*/
            Console.WriteLine("Products(int productId,string name,decimal price)");
        }
        public Products(int productId,string name,decimal price,short stock):this(productId,name,price,stock,false)
        {
            /*_productId = productId;
            _productName = name;
            _unitPrice = price;
            _unitsInStock = stock;
            _isDiscounted = false;*/
            Console.WriteLine("Products(int productId,string name,decimal price,short stock) called");
        }
        public Products(int productId,string name,decimal price,short stock,bool discounted)
        {
            _productId = productId;
            _productName = name;
            _unitPrice = price;
            _unitsInStock = stock;
            _isDiscounted = discounted;
            Console.WriteLine("Products(int productId,string name,decimal price,short stock,bool discounted) called");
        }
        #endregion

    }
}
