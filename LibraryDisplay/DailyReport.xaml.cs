using logicForLibrary;
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

//The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LibraryDisplay
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DailyReport : Page
    {
        string dateNow = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year + "";
        ManagementArea ma = new ManagementArea();
        public DailyReport()
        {
            this.InitializeComponent();
            AddCurrentDateToUIElementText(tbldailyReport, dateNow);
            UpdateDailyReport();
        }
        //UpdateDailyReport.
        private void UpdateDailyReport()
        {
            TotalItemStatus();
            TotalBooksStatus();
            TotalMagazinesStatus();
            TotalItemsBorrowed();
        }
        //Total Items Borrowed.
        private void TotalItemsBorrowed()
        {
            List<Item> ItemsBorrowed = ma.Get().Where(m => m._rentalDate != "").ToList();
            tblTotalBorrowedProductsShow.Text = ItemsBorrowed.Count.ToString();
        }
        //Total Magazines Status.
        private void TotalMagazinesStatus()
        {
            List<Item> magazines = ma.Get().Where(m => m is Magazine).ToList();
            tblTotalMagazinesShow.Text = magazines.Count.ToString();
        }
        //Total Books Status.
        private void TotalBooksStatus()
        {
            List<Item> books = ma.Get().Where(m => m is Book).ToList();
            tblTotalBooksShow.Text = books.Count.ToString();
        }
        //Total all Item Status.
        private void TotalItemStatus()
        {
            string count = ma.Get().Count.ToString();
            tblTotalProductsShow.Text = count;
        }
        //Go to the Main.
        private void MainFromDailyReport_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
        //Add the current time to a block text.
        private void AddCurrentDateToUIElementText(UIElement uiElement, string dateNow)
        {
            if (uiElement is TextBlock)
            {
                TextBlock block = (TextBlock)uiElement;
                block.Text += "\n" + dateNow;
            }
        }
    }
}
