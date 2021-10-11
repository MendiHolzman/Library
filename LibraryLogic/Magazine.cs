using System;
using System.Collections.Generic;
using System.Text;

namespace logicForLibrary
{
    public class Magazine : Item
    {
        public Magazine(string name, string publishingCompany, string dateOfPublication,
            string genre, int rentalPrice, int discount, string rentalDate, string message)
            : base
            (name, publishingCompany, dateOfPublication,
             genre, rentalPrice, discount, rentalDate, message)
        { }
    }
}
