using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using InterTwitter.Enums;
using InterTwitter.Helpers;
using InterTwitter.Models;
using Prism.Navigation;

namespace InterTwitter.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {

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

        private MovingStates _movingState;
        public MovingStates MovingState
        {
            get => _movingState;
            set => SetProperty(ref _movingState, value);
        }

        public double LastY { get; set; }

        public ICommand ScrollCommand => SingleExecutionCommand.FromFunc<double>(OnScrollCommandAsync);


        private async Task OnScrollCommandAsync(double scrollY)
        {

            if (scrollY > LastY)
            {
                if (MovingState is not MovingStates.MovingDown)
                {
                    MovingState = MovingStates.MovingDown;
                }
                else
                {
                    Debug.WriteLine("MovingState is MovingDown");
                }
            }
            else if (scrollY < LastY)
            {
                if (MovingState is not MovingStates.MovingUp)
                {
                    MovingState = MovingStates.MovingUp;
                }
                else
                {
                    Debug.WriteLine("MovingState is MovingUp");
                }
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
