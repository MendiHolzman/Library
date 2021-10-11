using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace logicForLibrary
{
    public class ManagementArea
    {
        readonly ListManagement _listManagement = new ListManagement();
        readonly SaleList _saleList = new SaleList();
        //Add a book to the list.
        public void AddBook(Book book)
        {
            _listManagement.AddToList(book);
        }
        //Add a item to the list.
        public void AddMagazine(Magazine magazine)
        {
            _listManagement.AddToList(magazine);
        }
        //Returns the list of items.
        public List<Item> Get()
        {
            return _listManagement.GetList();
        }
        //Add to instruction block text on index range.
        public void AddToTbl(TextBlock block)
        {
            block.Text += $"\n Select an index of an item between 1 and {Get().Count}";
        }
        //Return string of all items.
        public string Show()
        {
            return _listManagement.ToString();
        }
        //Returns a string of search result items.
        public string ShowResultForSearch(List<Item> items)
        {
            string res = "";
            foreach (var item in items)
            {
                res += item.ToString() + "\n";
            }
            return res;
        }
        //Delete Item from the list.
        public void DeleteItem(int index)
        {
            _listManagement.DeleteFromList(index);
        }
        //Search By Item Name
        public List<Item> SearchByItemName(string text)
        {
            List<Item> SortedList = new List<Item>();
            foreach (Item item in _listManagement.GetList())
            {
                if (item._name.Contains(text))
                {
                    SortedList.Add(item);
                }
            }
            return SortedList;
        }
        //Search By Item Author.
        public List<Item> SearchByBookAuthor(string text)
        {
            List<Item> SortedList = new List<Item>();
            foreach (Item item in _listManagement.GetList())
            {
                if (item is Book book)
                {
                    if (book._author.Contains(text))
                    {
                        SortedList.Add(book);
                    }
                }
            }
            return SortedList;
        }
        //Search By Item Genre.
        public List<Item> SearchByItemGenre(string text)
        {
            List<Item> SortedList = new List<Item>();
            foreach (Item item in _listManagement.GetList())
            {
                if (item._genre.Contains(text))
                {
                    SortedList.Add(item);
                }
            }
            return SortedList;
        }
        //Search By Item BookPublishing.
        public List<Item> SearchByItemBookPublishing(string text)
        {
            List<Item> SortedList = new List<Item>();
            foreach (Item item in _listManagement.GetList())
            {
                if (item._publishingCompany.Contains(text))
                {
                    SortedList.Add(item);
                }
            }
            return SortedList;
        }
        //Displays a delay message.
        public string CreateAMessage()
        {
            return "There is a delay in returning the item";
        }
        //Check if there is a delay.
        public void NoticeOfDelay()
        {
            foreach (var item in Get())
            {
                if (item._rentalDate != "")
                {
                    DateTime StartDate = DateTime.Parse(item._rentalDate);
                    DateTime EndDate = DateTime.Now;
                    if ((EndDate - StartDate).TotalDays > 14)
                    {
                        item._message = CreateAMessage();
                    }
                }
            }
        }
        //Add a sale to the sales list.
        public void AddSale(Sale sale)
        {
            _saleList.AddSaleToList(sale);
        }
        //Return string of all Sales.
        public string ShowSaleList()
        {
            return _saleList.ToString();
        }
        //Returning the sales list.
        public List<Sale> GetSalesList()
        {
            return _saleList.GetSaleList();
        }
        //Delete a sale from the list.
        public void DeleteFromSalesList(int index)
        {
            _saleList.DeleteFromSaleList(index);
        }
        //A function that ensures that the input is only numeric.
        public void InputNumbersOnly(TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }
        //regex that matches disallowed text.
        private static readonly Regex _regex = new Regex("[^a-zA-Z]");
        //A function that ensures that the input is only Letters.
        public void InputLettersOnly(TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = _regex.IsMatch(args.NewText);
        }
        //Convert Items To Books.
        public List<Book> ConvertItemsToBooks(List<Item> items)
        {
            List<Book> books = new List<Book>();
            foreach (Book book in items)
            {
                books.Add(book);
            }
            return books;
        }
        //Convert Books To Items.
        public List<Item> ConvertBooksToItems(List<Book> books)
        {
            List<Item> items = new List<Item>();
            foreach (Item item in books)
            {
                items.Add(item);
            }
            return items;
        }
        //Is All Fields Are Full
        public bool IsAllFieldsAreFull(
           string ItemName, string PublishingCompany, string Genre,
           string RentalPrice, string date, string author
           )
        {
            if (
                 ItemName == "" || PublishingCompany == ""
                || Genre == "" || RentalPrice == ""
                || date == "1/1/1601"
                )
            {
                return false;
            }
            else
            {
                return true;
            }
        } 
    }
}
