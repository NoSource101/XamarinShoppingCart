using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinShoppingCart.Model;
using XamarinShoppingCart.ViewModel;

namespace XamarinShoppingCart.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainTabPage : TabbedPage
    {

        //public List<CartItem> CartItems { get; set; }
        //private string CartListFile = "TempCartList.txt";

        public MainTabPage ()
        {      
            Children.Add(new MainPage());
            Children.Add(new CartPage());

            //InitializeComponent();
            CurrentPageChanged += OnCurrentPageChanged;   
        }

        private void OnCurrentPageChanged(object sender, EventArgs e)
        {
            var tabbedPage = (TabbedPage)sender;


            var MainPage = (MainPage)tabbedPage.Children[0];
            var CartPage = (CartPage)tabbedPage.Children[1];
            var CartPageBindingContext = (CartPageViewModel)CartPage.BindingContext;
            var MainPageBindingContext = (ProductCatalogViewModel)MainPage.BindingContext;

            if (tabbedPage.CurrentPage.Title == "Cart")
            {

                CartPageBindingContext.CartItems.Clear();
                CartPageBindingContext.CartItems.AddRange(MainPageBindingContext.CartItems);
            }
            else if (tabbedPage.CurrentPage.Title == "Catalog")
            {
                MainPageBindingContext.CartItems.Clear();
                MainPageBindingContext.CartItems.AddRange(CartPageBindingContext.CartItems);
            }


        }


    }
}