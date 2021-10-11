using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Documents;

namespace logicForLibrary
{
    public class ListManagement : ItemCollection
    {
        public static List<Item> myList { get; set; } = new List<Item> { };
        //Add item to list of items.
        public override void AddToList(Item item)
        {
            myList.Add(item);
        }
        //List Get function.
        public override List<Item> GetList()
        {
            return myList;
        }
        //The function gets an index to delete and deletes from the list.
        public override void DeleteFromList(int index)
        {
            myList.RemoveRange(index, 1);
        }
        //To string of list of items.
        public override string ToString()
        {
            int indexer = 1;
            string res = "";
            foreach (var item in myList)
            {
                var txtBold = new Bold();
                 res += "item number "
                    + indexer + ":\n" +
                item.ToString() + "\n";
                indexer++;     
            }
            return res;
        }
    }
}
