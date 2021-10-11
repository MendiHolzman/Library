using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using logicForLibrary;
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LibraryDisplay
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddASale : Page
    {
        ManagementArea ma = new ManagementArea();

        public AddASale()
        {
            this.InitializeComponent();
        }
        //Go to the main.
        private void MainFromAddSale_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
        //Filling the combo box of choosing a name of a type - in the case of a genre.
        public void FillingAComboBoxByGenre()
        {
            foreach (string genre in DeletingDuplicates(Genres(ma.Get())))
            {
                cbParametersNames.Items.Add(genre);
            }
        }
        //Filling the combo box of choosing a name of a type - in the case of a Date Of Publication.
        public void FillingAComboBoxByDateOfPublication()
        {
            foreach (string dateOfPublication in DeletingDuplicates(PublicationDates(ma.Get())))
            {
                cbParametersNames.Items.Add(dateOfPublication);
            }
        }
        //Filling the combo box of choosing a name of a type - in the case of a Publishing Company.
        public void FillingAComboBoxByPublishingCompany()
        {
            foreach (string PublishingCompany in DeletingDuplicates(PublishingCompanies(ma.Get())))
            {
                cbParametersNames.Items.Add(PublishingCompany);
            }
        }
        //Filling the combo box of choosing a name of a type - in the case of a Author.
        public void FillingAComboBoxByAuthor()
        {
            List<Item> books = ma.Get().Where(m => m is Book).ToList();

            foreach (string author in DeletingDuplicates(Authors(books)))
            {
                cbParametersNames.Items.Add(author);
            }
        }
        //Receives a list of books and returns all their authors with duplicates.
        private static List<string> Authors(List<Item> books)
        {
            List<string> WithDuplicates = new List<string>();
            foreach (Book book in books)
            {
                WithDuplicates.Add(book._author);
            }

            return WithDuplicates;
        }
        //Receives a list of items and returns all their Publishing Companies with duplicates.
        private static List<string> PublishingCompanies(List<Item> items)
        {
            List<string> WithDuplicates = new List<string>();
            foreach (Item item in items)
            {
                WithDuplicates.Add(item._publishingCompany);
            }

            return WithDuplicates;
        }
        //Receives a list of items and returns all their Publication Dates with duplicates.
        private static List<string> PublicationDates(List<Item> items)
        {
            List<string> WithDuplicates = new List<string>();
            foreach (Item item in items)
            {
                WithDuplicates.Add(item._dateOfPublication);
            }

            return WithDuplicates;
        }
        //Receives a list of items and returns all their Genres with duplicates.
        private static List<string> Genres(List<Item> items)
        {
            List<string> WithDuplicates = new List<string>();
            foreach (Item item in items)
            {
                WithDuplicates.Add(item._genre);
            }
            return WithDuplicates;
        }
        //Receives a string list with duplicates and returns the list without duplicates.
        private static List<string> DeletingDuplicates(List<string> WithDuplicates)
        {
            return WithDuplicates.Distinct().ToList();
        }
        //ClearComboBox.
        public void ClearComboBox()
        {
            cbParametersNames.Items.Clear();
        }
        //Click fills the combo box by type,
        //and allows and closes buttons according to the user's location.
        private void Next1_Click(object sender, RoutedEventArgs e)
        {
            ClearComboBox();
            switch (cbParametersTypes.SelectedIndex)
            {
                case 0:
                    FillingAComboBoxByGenre();
                    break;
                case 1:
                    FillingAComboBoxByAuthor();
                    break;
                case 2:
                    FillingAComboBoxByDateOfPublication();
                    break;
                case 3:
                    FillingAComboBoxByPublishingCompany();
                    break;
                default:
                    break;
            }
            cbParametersNames.IsEnabled = true;
            cbParametersTypes.IsEnabled = false;
            Next1.IsEnabled = false;
        }
        //Click allows and closes buttons according to the user's location.
        private void Next2_Click(object sender, RoutedEventArgs e)
        {
            tbxDiscountPercentage.IsEnabled = true;
            cbParametersNames.IsEnabled = false;
            Next2.IsEnabled = false;
        }
        //Click allows and closes buttons according to the user's location.
        private void Next3_Click(object sender, RoutedEventArgs e)
        {
            AddSale.IsEnabled = true;
            tbxDiscountPercentage.IsEnabled = false;
            Next3.IsEnabled = false;
        }
        //Verifies numbers only.
        private void tbxDiscountPercentage_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }
        //Allows a button only when entering text.
        private void cbParametersTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Next1.IsEnabled = true;
        }
        //Allows a button only when entering text.
        private void cbParametersNames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Next2.IsEnabled = true;
        }
        //Allows a button only when entering text.
        private void tbxDiscountPercentage_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            if (sender.Text == "")
            {
                Next3.IsEnabled = false;
            }
            else
            {
                Next3.IsEnabled = true;
            }
        }
        //A function called Click Add Sale. 
        //Create a sale, discount, announcement, add to the sales list, and more.
        private void AddSale_Click(object sender, RoutedEventArgs e)
        {
            CreateSale();
            MakeASale(cbParametersTypes.SelectedIndex, cbParametersNames.SelectionBoxItem.ToString(), CreateSale());
            ma.AddSale(CreateSale());
            ShowMessage();
            AddSale.IsEnabled = false;
            tbxDiscountPercentage.Text = "";
        }
        //Create a discount and notice to all the items participating in the sale.
        private void MakeASale(int paramTypeIndex, string paramName, Sale sale)
        {
            List<Item> itemsSale = new List<Item>();
            List<Book> booksSale = new List<Book>();
            List<Item> ItemsTobooks = ma.Get().Where(m => m is Book).ToList();
            List<Book> books = ma.ConvertItemsToBooks(ItemsTobooks);

            switch (paramTypeIndex)
            {
                case 0:
                    itemsSale = ma.Get().Where((i) => i._genre == paramName).ToList();
                    MakeADiscountForTheItemsSale(itemsSale);
                    MakeAMessageForTheAllItemsSale(itemsSale);
                    break;
                case 1:
                    booksSale = books.Where((i) => i._author == paramName).ToList();
                    MakeADiscountForTheBooksSale(booksSale);
                    MakeAMessageForTheAllBooksSale(booksSale);
                    break;
                case 2:
                    itemsSale = ma.Get().Where((i) => i._dateOfPublication == paramName).ToList();
                    MakeADiscountForTheItemsSale(itemsSale);
                    MakeAMessageForTheAllItemsSale(itemsSale);
                    break;
                case 3:
                    itemsSale = ma.Get().Where((i) => i._publishingCompany == paramName).ToList();
                    MakeADiscountForTheItemsSale(itemsSale);
                    MakeAMessageForTheAllItemsSale(itemsSale);
                    break;
                default:
                    break;
            }
        }
        //Calculating the discount for items.
        public void MakeADiscountForTheItemsSale(List<Item> itemsSale)
        {
            int percentage = int.Parse(tbxDiscountPercentage.Text);
            foreach (var item in itemsSale)
            {
                int rentalPrice = item._rentalPrice;
                item._discount = CalculatingDiscount(percentage, rentalPrice);
            }
        }
        //Calculating discount.
        public static int CalculatingDiscount(int percentage, int rentalPrice)
        {
            return (percentage * rentalPrice) / 100;
        }
        //Calculating the discount for books.
        public void MakeADiscountForTheBooksSale(List<Book> booksSale)
        {
            foreach (Book book in booksSale)
            {
                book._discount =
                    (book._rentalPrice *
                    int.Parse(tbxDiscountPercentage.Text))
                    / 100;
            }
        }
        //Make A Message For The All Items Sale.
        public void MakeAMessageForTheAllItemsSale(List<Item> itemsSale)
        {
            foreach (Item item in itemsSale)
            {
                item._message = $"The item is on sale number {ma.GetSalesList().Count + 1}";
            }
        }
        //Make A Message For The All Books Sale.
        public void MakeAMessageForTheAllBooksSale(List<Book> booksSale)
        {
            foreach (Book book in booksSale)
            {
                book._message = $"The item is on sale number {ma.GetSalesList().Count + 1}";
            }
        }
        //Create an operation with the details from the user (administrator).
        private Sale CreateSale()
        {
            int newId = ma.GetSalesList().Count + 1;
            Sale sale = new Sale
                (
                newId,
                DateTime.Now, int.Parse(tbxDiscountPercentage.Text),
                cbParametersTypes.SelectionBoxItem.ToString(),
                cbParametersNames.SelectionBoxItem.ToString(), "new sale"
                );
            return sale;
        }
        //Message on the success of adding a promotion.
        private async void ShowMessage()
        {
            await new MessageDialog($"The sale number {ma.GetSalesList().Count} was successfully added to the library!").ShowAsync();
        }
        //Clicking on a new promotion will only open the first combo box of the type selection.
        private void newSale_Click(object sender, RoutedEventArgs e)
        {
            Next1.IsEnabled = false;
            Next2.IsEnabled = false;
            Next3.IsEnabled = false;
            cbParametersNames.IsEnabled = false;
            tbxDiscountPercentage.IsEnabled = false;
            AddSale.IsEnabled = false;
            cbParametersTypes.IsEnabled = true;
        }
    }
}
