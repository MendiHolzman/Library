using logicForLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LibraryDisplay
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RemoveSale : Page
    {
        ManagementArea ma = new ManagementArea();
        public RemoveSale()
        {
            this.InitializeComponent();

            isNoSales();
        }

        private void isNoSales()
        {
            if (!IsThereAreSalesInTheLibrary())
            {
                RemoveSaleBtn.IsEnabled = false;
                tbxIdRemove.Text = "";
                tbxIdRemove.IsEnabled = false;
                tblRemove.Text = "There are no sales in the library!";
            }
            else
            {
                RemoveSaleBtn.IsEnabled = true;
                tbxIdRemove.IsEnabled = true;
                tblRemove.Text = $"Write ID for deletion from 1 to {ma.GetSalesList().Count}";
            }
        }

        private bool IsThereAreSalesInTheLibrary()
        {
            if (ma.GetSalesList().Count > 0)
            {
                return true;
            }
            return false;
        }

        private void MainFromRemoveSale_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void RemoveSaleBtn_Click(object sender, RoutedEventArgs e)
        {
            if (tbxIdRemove.Text != "")
            {    
                int index = int.Parse(tbxIdRemove.Text) - 1;
                if (TryToRent(index))
                {
                    ReturnListItemsSale(index);
                    RemoveDiscount(ReturnListItemsSale(index));
                    ma.DeleteFromSalesList(index);
                    ShowMessage(index);
                    isNoSales();
                }
               
            }
            else
            {
                ShowEmptyMessage();
            }

        }

        private bool TryToRent(int index)
        {
            if (int.Parse(tbxIdRemove.Text) == 0)
            {
                ShowZeroMessage();
            }
            else
            {
                if (index >= ma.GetSalesList().Count)
                {
                    MessageOutOfRange();
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        private async void ShowZeroMessage()
        {
            await new MessageDialog("0 is not a possible index").ShowAsync();
        }


        private async void MessageOutOfRange()
        {
            await new MessageDialog("The index is larger than the number " +
                "of items in the library.").ShowAsync();
        }


        private async void ShowEmptyMessage()
        {
            await new MessageDialog("Field required").ShowAsync();
        }


        public List<Item> ReturnListItemsSale(int indexSale)
        {
            Sale sale = ma.GetSalesList()[indexSale];
            var type = sale._type;
            List<Item> itemsSale;
            List<Book> BooksSale;
            List<Item> ItemsTobooks = ma.Get().Where(m => m is Book).ToList();
            List<Book> books = ma.ConvertItemsToBooks(ItemsTobooks);

            switch (type)
            {
                case "Genre":
                    itemsSale =
              ma.Get().Where((i) => i._genre == sale._nameOfType).ToList();
                    break;
                case "Author":
                    BooksSale =
            books.Where((i) => i._author == sale._nameOfType).ToList();
                    itemsSale = ma.ConvertBooksToItems(BooksSale);
                    break;
                case "DateOfPublication":
                    itemsSale =
              ma.Get().Where((i) => i._dateOfPublication == sale._nameOfType).ToList();
                    break;
                case "PublishingCompany":
                    itemsSale =
              ma.Get().Where((i) => i._publishingCompany == sale._nameOfType).ToList();
                    break;
                default:
                    itemsSale = ma.Get().Where((i) => i._genre == sale._nameOfType).ToList();
                    break;
            }

            return itemsSale;
        }

        private void RemoveDiscount(List<Item> items)
        {
            foreach (var item in items)
            {
                item._discount = 0;
            }
        }

        private void tbxIdRemove_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            ma.InputNumbersOnly(args);
        }

        private async void ShowMessage(int index)
        {
            await new MessageDialog($"The removal of the sale index: {index + 1}, was successful!").ShowAsync();
        }

    }
}
