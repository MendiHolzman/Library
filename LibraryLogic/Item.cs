using System;

namespace logicForLibrary
{
    public abstract class Item
    {
        public string _name;
        public string _publishingCompany;
        public string _dateOfPublication { get; set; }
        public string _genre { get; set; }
        public int _rentalPrice { get; set; }
        public int _discount { get; set; }
        public string _rentalDate { get; set; }
        public string _message { get; set; }

        public Item(string name, string publishingCompany, string dateOfPublication,
            string genre, int rentalPrice, int discount, string rentalDate, string message)
        {
            _name = name;
            _publishingCompany = publishingCompany;
            _dateOfPublication = dateOfPublication;
            _genre = genre;
            _rentalPrice = rentalPrice;
            _discount = discount;
            _rentalDate = rentalDate;
            _message = message;
        }
        //To string of an item.
        public override string ToString()
        {
            return $"name - {_name} \n publishing Company - {_publishingCompany} \n date Of Publication - {_dateOfPublication} \n" +
                $"genre - {_genre} \n rentalPrice - {_rentalPrice}\n" +
                $"discount - {_discount} \n rental Date - {_rentalDate} \n message - {_message} \n";         
        }
    }
}
