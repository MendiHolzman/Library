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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LibraryDisplay
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        const int password = 770;
        static bool flag = true;
        static bool flagPassword = false;
        readonly ServiceDefultItems sdi = new ServiceDefultItems();
        readonly ManagementArea ma = new ManagementArea();
        public MainPage()
        {
            this.InitializeComponent();

            if (flag)
            {
                CreateDefultItems();
                ma.NoticeOfDelay();
            }

            if (flagPassword)
            {
                LoginManagerSucceeded();
            }
            else
            {
                LogOutManagerSucceeded();
            }
        }

        private void CreateDefultItems()
        {
            foreach (Item item in sdi.GetDefultList())
            {
                if (item is Book book)
                {
                    ma.AddBook(book);
                }
                else
                {
                    ma.AddMagazine((Magazine)item);
                }
            }
            flag = false;
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Add));
        }

        private void ShowAll_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Show));
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Delete));
        }

        private void SearchForItems_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Search));
        }

        private void EditItem_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Edit));
        }

        private void RentItem_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Rent));
        }

        private void DailyReport_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DailyReport));
        }

        private void LoginManagerbtn_Click(object sender, RoutedEventArgs e)
        {
            if (tbxPassword.Text == "")
            {
                ShowErrorMessage();
            }
            else
            {
                int tbxPasswordres = int.Parse(tbxPassword.Text);
                if (tbxPasswordres == password)
                {
                    LoginManagerSucceeded();
                }
                else
                {
                    ShowMistakeMessage();
                }
            }
        }

        private async void ShowMistakeMessage()
        {
            await new MessageDialog("The password is incorrect").ShowAsync();
        }

        private async void ShowErrorMessage()
        {
            await new MessageDialog("A password field is required.").ShowAsync();
        }

        private void LoginManagerSucceeded()
        {
            VisibilityForManager();
            ChangeTitleToManager();
            flagPassword = true;
        }

        private void ChangeTitleToManager()
        {
            titel.Text = "Welcome Manager!";
        }

        private void VisibilityForManager()
        {
            AddItem.Visibility = Visibility.Visible;
            DeleteItem.Visibility = Visibility.Visible;
            EditItem.Visibility = Visibility.Visible;
            dailyReport.Visibility = Visibility.Visible;
            AddSale.Visibility = Visibility.Visible;
            RemoveASale.Visibility = Visibility.Visible;

            tbxPassword.Visibility = Visibility.Collapsed;
            LoginManagerbtn.Visibility = Visibility.Collapsed;

            LogOutbtn.Visibility = Visibility.Visible;
        }

        private void VisibilityCollapsedForManager()
        {
            AddItem.Visibility = Visibility.Collapsed;
            DeleteItem.Visibility = Visibility.Collapsed;
            EditItem.Visibility = Visibility.Collapsed;
            dailyReport.Visibility = Visibility.Collapsed;
            AddSale.Visibility = Visibility.Collapsed;
            RemoveASale.Visibility = Visibility.Collapsed;

            tbxPassword.Visibility = Visibility.Visible;
            LoginManagerbtn.Visibility = Visibility.Visible;

            tbxPassword.Text = "";
            LogOutbtn.Visibility = Visibility.Collapsed;
            LoginManagerbtn.IsEnabled = false;
        }

        private void Returnbtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Return));
        }

        private void AddSale_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddASale));
        }

        private void SalesScreen_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ShowSales));
        }

        private void RemoveASale_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(RemoveSale));
        }

        private void TbxPassword_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            ma.InputNumbersOnly(args);
        }
 
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Exit();
        }

        private async static void Exit()
        {
            MessageDialog dialog = new MessageDialog("Are you sure you want to exit?");
            dialog.Commands.Add(new UICommand("Yes", null));
            dialog.Commands.Add(new UICommand("No", null));
            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 1;
            var cmd = await dialog.ShowAsync();

            if (cmd.Label == "Yes")
            {
                App.Current.Exit();
            }
            else
            {
                await new MessageDialog("Staying is the right choice ... (:").ShowAsync();
            }
        }

        private void TbxPassword_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            LoginManagerbtn.IsEnabled = true;
        }

        private void LogOutbtn_Click(object sender, RoutedEventArgs e)
        {
            LogOutManagerSucceeded();

        }

        private void LogOutManagerSucceeded()
        {
            VisibilityCollapsedForManager();
            ChangeTitleToGuest();
            flagPassword = false;

        }

        private void ChangeTitleToGuest()
        {
            titel.Text = "Welcome to the library!";
        }
    }
}
