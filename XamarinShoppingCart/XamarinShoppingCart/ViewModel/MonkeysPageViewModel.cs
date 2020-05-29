using System.Collections.Generic;
using XamarinShoppingCart.Model;

namespace XamarinShoppingCart.ViewModel
{
    public class MonkeysPageViewModel : ViewModelBase
    {
        public IList<Monkey> Monkeys { get { return MonkeyData.Monkeys; } }
        public IList<Product> ProductLs { get; set; }

        Monkey selectedMonkey;
        public Monkey SelectedMonkey
        {
            get { return selectedMonkey; }
            set
            {
                if (selectedMonkey != value)
                {
                    selectedMonkey = value;
                    OnPropertyChanged();
                }
            }
        }

    }
}
