using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class AuthorManager
    {
        public static Authors[] AuthorsArray = new Authors[3];
        static int currentIndex = 0;

        public Authors CreateNewAuthor(int id,string firstName,string lastName,string city)
        {
            Authors author = new Authors();
            author.Id = id;
            author.FirstName = firstName;
            author.LastName = lastName;
            author.City = city;

            if(currentIndex == AuthorsArray.Length )
            {
                Authors[] NewAuthorsArray = new Authors[AuthorsArray.Length + 10];   
                Array.Copy(AuthorsArray,NewAuthorsArray, currentIndex);
                AuthorsArray = NewAuthorsArray;
            }
            AuthorsArray[currentIndex++] = author;
            return author;
        }
        public void UpdateAuthor(Authors Oldauthor,string id,string firstName,string lastName,string city) 
        {
            
            Oldauthor.Id = (id.Length > 0) ? int.Parse(id) : Oldauthor.Id;
            Oldauthor.FirstName = (firstName.Length > 0) ? firstName : Oldauthor.FirstName;
            Oldauthor.LastName = (lastName.Length > 0) ? lastName : Oldauthor.LastName;
            Oldauthor.City = (city.Length > 0) ? city : Oldauthor.City;
        }
        public Authors[] ListAllAuthors()
        {
            return AuthorsArray;
        }
        public Authors FindById(int id)
        {
            foreach (Authors author in AuthorsArray)
            {
                if(author is not null && author.Id == id)
                return author;
            }
            return null;
        }
        public bool RemoveAuthor(int id)
        {
            bool found = false;
           for(int i=0;i<currentIndex;i++)
            {
                if(found)
                {
                    AuthorsArray[i-1] = AuthorsArray[i];
                }
                if (AuthorsArray[i].Id == id)
                {
                    found = true;
                    AuthorsArray[i] = null;
                }
            }
            AuthorsArray[currentIndex-1] = null;

            if(found)
            currentIndex--;

            return found;
        }
    }
}