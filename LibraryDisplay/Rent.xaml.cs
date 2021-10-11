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
    public sealed partial class Rent : Page
    {
        string dateNow = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year + "";
        ManagementArea ma = new ManagementArea();
        public Rent()
        {
            this.InitializeComponent();

            ma.AddToTbl(tblRent);
        }

        private void MainFromRent_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void rentItemBtn_Click(object sender, RoutedEventArgs e)
        {
            RentFunc();
        }

        private void RentFunc()
        {
            if (tbxRent.Text != "")
            {
                int index = int.Parse(tbxRent.Text) - 1;
                if (TryToRent(index))
                {
                    RentItemFromLibrary(index);
                    ShowMessage(index);
                }
            }
            else
            {
                ShowEmptyMessage();
            }
        }

        private bool TryToRent(int index)
        {
            if (int.Parse(tbxRent.Text) == 0)
            {
                ShowZeroMessage();
            }
            else
            {
                if (index >= ma.Get().Count)
                {
                    MessageOutOfRange();
                }
                else
                {
                    if (ma.Get()[index]._rentalDate != "")
                    {
                        MessageItemHasBeenRented();
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private async void MessageOutOfRange()
        {
            await new MessageDialog("The index is larger than the number " +
                "of items in the library.").ShowAsync();
        }

        private async void MessageItemHasBeenRented()
        {
            await new MessageDialog("The item in the selected index is already rented." +
                " Please select another item.").ShowAsync();
        }

        private async void ShowZeroMessage()
        {
            await new MessageDialog("0 is not a possible index").ShowAsync();
        }

        private async void ShowEmptyMessage()
        {
            await new MessageDialog("Field required").ShowAsync();
        }

        private void RentItemFromLibrary(int index)
        {
            Item item = ma.Get()[index];
            item._rentalDate = dateNow;
            item._message = "Thank you for renting from us, pleasant reading!";
        }

        private void tbxRent_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            ma.InputNumbersOnly(args);
        }

        private async void ShowMessage(int index)
        {
            int rentalPrice = ma.Get()[index]._rentalPrice - ma.Get()[index]._discount;
            await new MessageDialog($"Item id {index + 1} successfully rented.\n" +
                $"The rental price is: {rentalPrice}").ShowAsync();
        }

    }
}
