﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Authors
    {
        private int _id=0;
        private string _firstName="";
        private string _lastName="";
        private string _city="";

        public int Id { get { return _id; } set { _id = value; } }  
        public string FirstName { get { return _firstName; } set { _firstName = value; } }
        public string LastName { get { return _lastName; } set { _lastName = value; } }
        public string City { get { return _city; } set { _city = value; } }

        public void ShowDetails()
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"Author Details:\n\t\tId: {Id}\n\t\tName: {FirstName} {LastName}\n\t\tCity: {City}");
            Console.WriteLine("------------------------------------------");
        }

    }
    
}
