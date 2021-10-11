using System;
using System.Collections.Generic;
using System.Text;

namespace logicForLibrary
{
    public class ServiceDefultItems
    {
        Magazine Magazine1 = new Magazine("The New York Times magazine", "New York Times", "27/05/1989", "Fashion", 40, 0, "04/18/2021 07:22:16","");
        Magazine Magazine2 = new Magazine("Calcalist Magazine", "Calcalist", "27/05/2000", "economy", 70, 0, "", "");
        Book Book1 = new Book("The boy from the forest", "Harlan Coban", "Kinneret", "27/05/1989", "Tension", 40, 0, "04/20/2021 07:22:16", "");
        Book Book2 = new Book("Silent lies", "Michal Shalev", "Zmora Pavilion", "27/05/2000", "Original fiction", 70, 0, "", "");
        List<Item> DefultItems = new List<Item>();
        
        public ServiceDefultItems()
        {
            DefultItems.Add(Magazine1);
            DefultItems.Add(Magazine2);
            DefultItems.Add(Book1);
            DefultItems.Add(Book2);
        }
        //return Defult List.
        public List<Item> GetDefultList()
        {
            return DefultItems;
        }
    }
}
