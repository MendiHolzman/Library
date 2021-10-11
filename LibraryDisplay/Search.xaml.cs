using logicForLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LibraryDisplay
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Search : Page
    {
        ManagementArea ma = new ManagementArea();
        public Search()
        {
            this.InitializeComponent();
        }

        private void byName_Click(object sender, RoutedEventArgs e)
        {
            if (tbxSearch.Text != "")
            {
                List<Item> res = ma.SearchByItemName(tbxSearch.Text);
                if (res.Count > 0)
                {
                    ShowSearchResults(res, tblShowRes);
                }
                else
                {
                    ShowErrorMessage();
                }
            }

            else
            {
                ShowErrorMessageStringEmpty();
            }
        }

        private void ShowErrorMessage()
        {
            tblShowRes.FontSize = 60;
            tblShowRes.Text = "No results";
        }

        private async void ShowErrorMessageStringEmpty()
        {
            await new MessageDialog("Field required").ShowAsync();
        }

        public void ShowSearchResults(List<Item> result, TextBlock tbl)
        {
            tblShowRes.FontSize = 12;
            tbl.Text = ma.ShowResultForSearch(result);
        }

        private void byAuthor_Click(object sender, RoutedEventArgs e)
        {
            if (tbxSearch.Text != "")
            {
                List<Item> res = ma.SearchByBookAuthor(tbxSearch.Text);
                if (res.Count > 0)
                {
                    ShowSearchResults(res, tblShowRes);
                }
                else
                {
                    ShowErrorMessage();
                }
            }

            else
            {
                ShowErrorMessageStringEmpty();
            }
        }

        private void byGenre_Click(object sender, RoutedEventArgs e)
        {
            if (tbxSearch.Text != "")
            {
                List<Item> res = ma.SearchByItemGenre(tbxSearch.Text);
                if (res.Count > 0)
                {
                    ShowSearchResults(res, tblShowRes);
                }
                else
                {
                    ShowErrorMessage();
                }
            }
            else
            {
                ShowErrorMessageStringEmpty();
            }
        }

        private void byBookPublishing_Click(object sender, RoutedEventArgs e)
        {

            if (tbxSearch.Text != "")
            {
                List<Item> res = ma.SearchByItemBookPublishing(tbxSearch.Text);
                if (res.Count > 0)
                {
                    ShowSearchResults(res, tblShowRes);
                }
                else
                {
                    ShowErrorMessage();
                }
            }
            else
            {
                ShowErrorMessageStringEmpty();
            }
        }

        private void MainFromSearch_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void tbxSearch_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            ma.InputLettersOnly(args);
        }

    }
}
