using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinShoppingCart.Model
{
    public class ProductCatalog
    {
        public Product Product { get; set; }
        public int Qty { get; set; }
        public string PriceWithCurrency { get; set; }
    }
}
