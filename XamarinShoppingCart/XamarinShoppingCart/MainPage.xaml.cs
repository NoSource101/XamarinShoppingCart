using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;
using XamarinShoppingCart.Model;
using System.Net.Http;
using Newtonsoft.Json;
using System.Diagnostics;
using XamarinShoppingCart.ViewModel;

namespace XamarinShoppingCart
{
    public partial class MainPage : ContentPage
    {
        //public List<CartItem> CartItems { get; set; }
        //private string CartListFile = "TempCartList.txt";

        public MainPage()
        {
            InitializeComponent();
            //CartItems = new List<CartItem>();

            //test = new ProductCatalogViewModel();
            //test.ProductList = LsProduct;
            //BindingContext = test;
            //BindingContext = this;

            BindingContext = new ProductCatalogViewModel();
        }

        void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
        }

        void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            Monkey tappedItem = e.Item as Monkey;
        }

        void OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            //lbldisp.Text = String.Format("Stepper value is {0:F1}", e.NewValue);
        }

        void OnAddToCartButtonClicked(object sender, EventArgs e)
        {
            var selectedItem = (ProductCatalog)((Button)sender).CommandParameter;
            var selfBindingContext = (ProductCatalogViewModel)BindingContext;
            selfBindingContext.CartItems.Add(
                new CartItem { Product = selectedItem.Product, Qty = selectedItem.Qty, PriceWithCurrency = selectedItem.PriceWithCurrency });
            //CartItems.Add(new CartItem { Product = selectedItem.Product, Qty = selectedItem.Qty });

            Task.Run(async () => { await ProductCatalogViewModel.SaveCartDataToFile(selfBindingContext.CartItems, ProductCatalogViewModel.CartListFile); }).Wait();

            //var menuItem = sender as Button;
            //Button button = (Button)sender;
            //var imt = (Grid)button.Parent;
            //var child = imt.Children;
            //var c = (Label)imt.Children[0];
            //var name = c.Text;
            //var c1 = (Stepper)imt.Children[6];
            //var qty = c1.Value;
        }

        //async void OnViewCartButtonClicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new Views.CartPage());
        //}
    }
}
