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
	public partial class CartPage : ContentPage
	{
        //public List<CartItem> CartItems { get; set; }

        public CartPage ()
		{ 
			InitializeComponent ();
            //FillCart();
            //BindingContext = this;
            BindingContext = new CartPageViewModel();

            //var asd = new CartPageViewModel();
            //asd.CartItems = CartItems;
            //BindingContext = asd;
        }

        void OnRemoveButtonClicked(object sender, EventArgs e)
        {
            var selectedItem = (CartItem)((Button)sender).CommandParameter;
            var selfBindingContext = (CartPageViewModel)BindingContext;
            selfBindingContext.CartItems.Remove(selectedItem);
            //CartItems.Add(new CartItem { Product = testVar.Product, Qty = testVar.Qty });

            Task.Run(async () => { await ProductCatalogViewModel.SaveCartDataToFile(selfBindingContext.CartItems, ProductCatalogViewModel.CartListFile); }).Wait();
        }

        //void FillCart()
        //{
        //    CartItems = new List<CartItem>();

        //    CartItems.Add(new CartItem
        //    {
        //        Product = new Product()
        //        {
        //            Id = "1001",
        //            Name = "Test Item",
        //            Regular_price = 1.00F,
        //            Sku = "TI1102",
        //            Type = "Unknown",
        //            Images = new List<ProductImage>
        //            {
        //                new ProductImage()
        //                {
        //                    Src = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"
        //                }
        //            }
        //        },
        //        Qty = 1
        //    });

        //    CartItems.Add(new CartItem
        //    {
        //        Product = new Product()
        //        {
        //            Id = "1002",
        //            Name = "Test Item2",
        //            Regular_price = 2.00F,
        //            Sku = "TI1103",
        //            Type = "Unknown",
        //            Images = new List<ProductImage>
        //            {
        //                new ProductImage()
        //                {
        //                    Src = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"
        //                }
        //            }
        //        },
        //        Qty = 1
        //    });

        //    CartItems.Add(new CartItem
        //    {
        //        Product = new Product()
        //        {
        //            Id = "1003",
        //            Name = "Test Item3",
        //            Regular_price = 4.00F,
        //            Sku = "TI1122",
        //            Type = "Unknown",
        //            Images = new List<ProductImage>
        //            {
        //                new ProductImage()
        //                {
        //                    Src = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"
        //                }
        //            }
        //        },
        //        Qty = 1
        //    });
        //}

        //async void OnViewCatalogButtonClicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new Views.CartPage());
        //}

        //async void OnPreviousPageButtonClicked(object sender, EventArgs e)
        //{
        //    await Navigation.PopAsync();
        //}
    }
}