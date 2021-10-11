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
    public sealed partial class Edit : Page
    {
        ManagementArea ma = new ManagementArea();
        public Edit()
        {
            this.InitializeComponent();
        }

        private void EditItem_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(tbxIndex.Text) - 1;
            EditFunc(index);
        }

        private void EditFunc(int index)
        {
            DateTimeOffset date1 = this.dpDateOfPublication.Date;
            string resString1 = date1.Day + "/" + date1.Month + "/" + date1.Year + "";

            if (IsFieldsAreFull(resString1))
            {
                EditDetails(index, resString1);
                ShowMessage(index);
            }
        }

        private bool IsIndexNotEmpty(string index)
        {
            if (index != "")
            {
                return true;
            }
            else
            {
                ShowEmptyMessage();
                return false;
            }
        }

        private bool IsFieldsAreFull(string resString)
        {
            if (
                tbxItemName.Text == "" || tbxPublishingCompany.Text == ""
                || tbxGenre.Text == "" ||
                tbxRentalPrice.Text == "" || resString == "1/1/1601"
                )
            {
                ShowErrorMessage();
                return false;
            }
            else
            {
                return true; ;
            }
        }

        private bool IsNotOutOfRange(int index)
        {
            if (index >= ma.Get().Count)
            {
                MessageOutOfRange();
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool IsIndexNotZero(int index)
        {
            if (index != 0)
            {
                return true;
            }
            else
            {
                ShowZeroMessage();
                return false;
            }
        }

        private async void MessageOutOfRange()
        {
            await new MessageDialog("The index is larger than the number " +
                "of items in the library.").ShowAsync();
        }

        private async void ShowZeroMessage()
        {
            await new MessageDialog("0 is not a possible index").ShowAsync();
        }

        private async void ShowEmptyMessage()
        {
            await new MessageDialog("Field required").ShowAsync();
        }

        public void EditDetails(int index, string date)
        {
            Item item = ma.Get()[index];
            item._name = tbxItemName.Text;
            item._publishingCompany = tbxPublishingCompany.Text;
            item._genre = tbxGenre.Text;
            item._rentalPrice = int.Parse(tbxRentalPrice.Text);
            item._dateOfPublication = date;
            if (item is Book)
            {
                Book book = (Book)item;
                book._author = tbxAuthor.Text;
            }
            else
            {
                AuthorNotUpdated();
            }
        }

        private async void AuthorNotUpdated()
        {
            await new MessageDialog("Author not updated because you edited a magazine.").ShowAsync();
        }

        private void MainFromEdit_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void tbxIndex_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            ma.InputNumbersOnly(args);
        }

        private void tbxItemName_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            ma.InputLettersOnly(args);

        }

        private async void ShowMessage(int index)
        {
            await new MessageDialog($"The item number {index + 1} was successfully editted!").ShowAsync();
        }

        private async void ShowErrorMessage()
        {
            await new MessageDialog("All fields are required").ShowAsync();
        }

        private void SelectedIndex_Click(object sender, RoutedEventArgs e)
        {
            if (IsIndexNotEmpty(tbxIndex.Text))
            {
                int index = int.Parse(tbxIndex.Text);
                if (IsIndexNotZero(index))
                {
                    if (IsNotOutOfRange(index))
                    {
                        Enabled();
                    }
                }  
            }
        }

        private void Enabled()
        {
            tbxIndex.IsEnabled = false;
            tbxAuthor.IsEnabled = true;
            tbxItemName.IsEnabled = true;
            tbxRentalPrice.IsEnabled = true;
            tbxGenre.IsEnabled = true;
            tbxPublishingCompany.IsEnabled = true;
            dpDateOfPublication.IsEnabled = true;
            EditItemBtn.IsEnabled = true;
            SelectedIndex.IsEnabled = false;
        }
    }
}
