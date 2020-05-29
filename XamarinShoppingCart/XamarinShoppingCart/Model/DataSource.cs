using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinShoppingCart.Model
{
    public static class DataSource
    {
        public static ObservableCollectionFast<CartItem> CartItems1;

        static DataSource()
        {

        }

        public static void persist(List<CartItem> collection)
        {
            //do something here
        }

        public static void initializeData(List<CartItem> listdata)
        {
            CartItems1 = new ObservableCollectionFast<CartItem>(listdata);
        }
    }
}
