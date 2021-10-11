using System;
using System.Collections.Generic;
using System.Text;

namespace logicForLibrary
{
    public class SaleList
    {
        public static List<Sale> mySaleList { get; set; } = new List<Sale> { };
        //Get Sale List;
        public List<Sale> GetSaleList()
        {
            return mySaleList;
        }
        //Ass Sale to List;
        public void AddSaleToList(Sale sale)
        {
            mySaleList.Add(sale);
        }
        //Delete sale by index From Sale List;
        public void DeleteFromSaleList(int index)
        {
            mySaleList.RemoveRange(index , 1);
        }
        //ToString of Sale List;
        public override string ToString()
        {
            string res = "";
            foreach (var item in mySaleList)
            {
                res += item.ToString() + "\n";
            }
            return res;
        }
    }
}
