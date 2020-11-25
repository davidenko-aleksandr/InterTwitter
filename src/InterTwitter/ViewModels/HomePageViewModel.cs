using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using InterTwitter.Helpers;
using InterTwitter.Models;
using Prism.Navigation;
using Xamarin.Forms;

namespace InterTwitter.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        private int _lastItemAppearedIdx = 0;
        private bool _lastDirectionWasUp = false;

        public HomePageViewModel(INavigationService navigationService)
                                : base(navigationService)
        {
            FillTheList();
        }

        private List<TestModel> _items;
        public List<TestModel> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        public double LastY { get; set; }

        public ICommand ScrollCommand => SingleExecutionCommand.FromFunc<double>(OnScrollCommandAsync);


        private async Task OnScrollCommandAsync(double scrollY)
        {

            if (scrollY > LastY)
            {
                //BarMargin = new Thickness(BarMargin.Left, BarMargin.Top - 1, BarMargin.Right, BarMargin.Bottom);
            }
            else if (scrollY < LastY)
            {
                //BarMargin = new Thickness(BarMargin.Left, BarMargin.Top + 1, BarMargin.Right, BarMargin.Bottom);
            }

            LastY = scrollY;

        }

        private void FillTheList()
        {
            var collection = new List<TestModel>();

            for(int i = 0; i < 20; i++)
            {
                collection.Add(new TestModel() { Content = (i + 1).ToString() });
            }

            Items = collection;
        }
    }
}
