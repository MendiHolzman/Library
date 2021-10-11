using System;
using System.Collections.Generic;
using System.Text;

namespace logicForLibrary
{
    public class Book : Item
    {
        public string _author;
        public Book
            (string name, string author, string publishingCompany, string dateOfPublication,
            string genre, int rentalPrice, int discount, string rentalDate, string message)
            : base
            (name, publishingCompany, dateOfPublication,
             genre, rentalPrice, discount, rentalDate, message)
        {
            _author = author;
        }

        //To string of a book that also includes an author
        public override string ToString()
        {
            return $"name - {_name} \n author {_author}  \n publishing Company - {_publishingCompany} \n date Of Publication - {_dateOfPublication} \n" +
             $"genre - {_genre} \n rentalPrice - {_rentalPrice}\n" +
             $"discount - {_discount} \n rental Date - {_rentalDate} \n message - {_message} \n";
        }
    }
}
