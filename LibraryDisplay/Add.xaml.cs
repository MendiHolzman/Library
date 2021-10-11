using logicForLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
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
    public sealed partial class Add : Page
    {
        ManagementArea ma = new ManagementArea();
        public Add()
        {
            this.InitializeComponent();
        }
        //Go to The Main.
        private void toTheMain_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
        //A function called when you click Add Item.
        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            DateTimeOffset dateOffset = this.dpDateOfPublication.Date;
            string dateStr = dateOffset.Day + "/" + dateOffset.Month + "/" + dateOffset.Year + "";
            if (ma.IsAllFieldsAreFull(tbxItemName.Text, tbxPublishingCompany.Text,
                tbxGenre.Text, tbxRentalPrice.Text, dateStr, tbxAuthor.Text
                ))
            {
                AddNewItem(dateStr);
                ShowMessage();
            }
            else
            {
                ShowErrorMessage();
            }


        }
        //View a message when you successfully add an item.
        private async void ShowMessage()
        {
            await new MessageDialog($"The item was successfully added to the library!").ShowAsync();
        }
        // Adding a new item: A Boolean function that
        // returns a lie in case one of
        // the fields is empty.
        // And if all the
        // fields are full create a book if there
        // is an author and a magazine if there is
        // no author and return truth.
        public bool AddNewItem(string date)
        {
            if (tbxAuthor.Text == string.Empty)
            {
                Magazine magazine = new Magazine(
                tbxItemName.Text, tbxPublishingCompany.Text,
                date, tbxGenre.Text, int.Parse(tbxRentalPrice.Text),
                0, "", "");
                ma.AddMagazine(magazine);
                return false;
            }
            else
            {
                Book book = new Book(
                tbxItemName.Text, tbxAuthor.Text, tbxPublishingCompany.Text,
                date, tbxGenre.Text, int.Parse(tbxRentalPrice.Text),
                0, "", "");
                ma.AddBook(book);
                return true;
            }
        }
        //Before Text Changing - Make sure the input is numbers only.
        public void tbxRentalPrice_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            ma.InputNumbersOnly(args);
        }
        //Before Text Changing - Make sure the input is Letters only.
        private void tbxItemName_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            ma.InputLettersOnly(args);
        }
        //Show Error Message - If there are empty fields.
        private async void ShowErrorMessage()
        {
            await new MessageDialog("All fields are required").ShowAsync();
        }
    }
}

