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
    public sealed partial class Return : Page
    {
        ManagementArea ma = new ManagementArea();
        public Return()
        {
            this.InitializeComponent();

            ma.AddToTbl(tblReturn);
        }

        private void MainFromReturn_Click(object sender, RoutedEventArgs e)
        {
            App.TryGoBack();
        }

        private void returnItemToLibrary(int index)
        {
            Item item = ma.Get()[index];
            item._rentalDate = "";
            item._message = "Thanks for returning the item.";
        }

        private void returnItemBtn_Click(object sender, RoutedEventArgs e)
        {
            ReturnFunc();
        }

        private void ReturnFunc()
        {
            if (tbxReturn.Text != "")
            {
                int index = int.Parse(tbxReturn.Text) - 1;
                if (TryToReturn(index))
                {
                    returnItemToLibrary(index);
                    ShowMessage(index);
                }
            }
            else
            {
                ShowEmptyMessage();
            }
        }

        private bool TryToReturn(int index)
        {
            if (int.Parse(tbxReturn.Text) == 0)
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
                    if (ma.Get()[index]._rentalDate == "")
                    {
                        MessageTheItemHasBeenReturned();
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

        private async void MessageTheItemHasBeenReturned()
        {
            await new MessageDialog("The selected item has already been returned.").ShowAsync();
        }

        private async void ShowZeroMessage()
        {
            await new MessageDialog("0 is not a possible index").ShowAsync();
        }

        private async void ShowEmptyMessage()
        {
            await new MessageDialog("Field required").ShowAsync();
        }

        private void tbxReturn_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            ma.InputNumbersOnly(args);
        }

        private async void ShowMessage(int index)
        {
            await new MessageDialog($"Item id {index + 1} successfully returned").ShowAsync();
        }
    }
}
