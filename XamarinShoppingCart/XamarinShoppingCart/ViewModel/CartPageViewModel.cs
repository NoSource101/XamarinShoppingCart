using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using XamarinShoppingCart.Model;

namespace XamarinShoppingCart.ViewModel
{
    public class CartPageViewModel : ViewModelBase
    {
        public ObservableCollectionFast<CartItem> CartItems { get; set; }

        public CartPageViewModel()
        {
            CartItems = new ObservableCollectionFast<CartItem>();

            //FillCart();
            //PropertyChanged += PropertyHasChanged;
        }

        //void FillCart()
        //{
        //    CartItems = new ObservableCollectionFast<CartItem>();

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

    }
}
