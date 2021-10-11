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
    public sealed partial class Delete : Page
    {
        ManagementArea ma = new ManagementArea();

        public Delete()
        {
            this.InitializeComponent();

            isNoItems();
        }
        //Return is There Are Items In The Library.
        private bool IsThereAreItemsInTheLibrary()
        {
            if (ma.Get().Count > 0)
            {
                return true;
            }
            return false;
        }
        //On click Delete butten.
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            DeleteFunc();
        }
        //The delete function checks if empty and more. If properly deleted from the list.
        private void DeleteFunc()
        {
            if (tbxWriteToDelete.Text != "")
            {
                int index = int.Parse(tbxWriteToDelete.Text) - 1;
                if (TryToDelete(index))
                {
                    ma.DeleteItem(index);
                    ShowMessage(index);
                    isNoItems();
                }
            }
            else
            {
                ShowEmptyMessage();
            }
        }
        //Blocking actions and error message if there are no items in the directory.
        private void isNoItems()
        {
            if (!IsThereAreItemsInTheLibrary())
            {
                deleteBtn.IsEnabled = false;
                tbxWriteToDelete.Text = "";
                tbxWriteToDelete.IsEnabled = false;
                tblWriteToDelete.Text = "There are no items in the library!";
            }       
        }
        //Checks index anomalies.
        private bool TryToDelete(int index)
        {
            if (int.Parse(tbxWriteToDelete.Text) == 0)
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
                    return true;
                }
            }
            return false;
        }
        //Message Out Of Range.
        private async void MessageOutOfRange()
        {
            await new MessageDialog("The index is larger than the number " +
                "of items in the library.").ShowAsync();
        }
        //Show Zero Message.
        private async void ShowZeroMessage()
        {
            await new MessageDialog("0 is not a possible index").ShowAsync();
        }
        //Show Empty Message.
        private async void ShowEmptyMessage()
        {
            await new MessageDialog("Field required").ShowAsync();
        }
        //Go to the main.
        private void MainFromDelete_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
        //Numbers Only.
        private void tbxWriteToDelete_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            ma.InputNumbersOnly(args);
        }
        //Delete Success Message.
        private async void ShowMessage(int index)
        {
            await new MessageDialog($"The item number {index + 1} was successfully deleted from the library!").ShowAsync();
        }

    }
}
