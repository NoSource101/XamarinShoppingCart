﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinShoppingCart.ViewModel;

namespace XamarinShoppingCart.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MonkeysPage : ContentPage
	{
		public MonkeysPage ()
		{
			InitializeComponent ();
            BindingContext = new MonkeysPageViewModel();
        }
	}
}