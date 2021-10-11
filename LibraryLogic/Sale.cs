using System;
using System.Collections.Generic;
using System.Text;

namespace logicForLibrary
{
    public class Sale
    {
        public int _id;
        public DateTime _startDate;
        public int _discountPercentage;
        public string _type;
        public string _nameOfType;
        public string _message;

        public Sale(int id, DateTime startDate, int discountPercentage,
            string type, string nameOfType, string message)
        {
            _id = id;
            _startDate = startDate;
            _discountPercentage = discountPercentage;
            _type = type;
            _nameOfType = nameOfType;
            _message = message;
        }
        //ToString of sale.
        public override string ToString()
        {
            return $"id of Sale - {_id} \n start Date - {_startDate} \n discount Percentage - {_discountPercentage} \n type - {_type} \n" +
                $"name Of Type - {_nameOfType} \n message - {_message}\n";
        }
    }
}
