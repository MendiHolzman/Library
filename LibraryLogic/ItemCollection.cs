using System;
using System.Collections.Generic;
using System.Text;

namespace logicForLibrary
{
    public abstract class ItemCollection
    {
        public abstract List<Item> GetList();

        public abstract void AddToList(Item item);

        public abstract void DeleteFromList(int index);
    }
}
