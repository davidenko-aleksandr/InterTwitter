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

        private double LastKnownY { get; set; }

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

        private MovingStates _movingBarState;
        public MovingStates MovingBarState
        {
            get => _movingBarState;
            set => SetProperty(ref _movingBarState, value);
        }

        private MovingStates _movingButtonState;
        public MovingStates MovingButtonState
        {
            get => _movingButtonState;
            set => SetProperty(ref _movingButtonState, value);
        }

        private double _offsetY;
        public double OffsetY
        {
            get => _offsetY;
            set => SetProperty(ref _offsetY, value);
        }

        public ICommand ScrollCommand => SingleExecutionCommand.FromFunc<double>(OnScrollCommandAsync);

        private async Task OnScrollCommandAsync(double OffSet)
        {


            OffsetY = OffSet;
            //if (OffSet > LastKnownY)
            //{
            //    if (MovingBarState is not MovingStates.MovingUp)
            //    {
            //        MovingBarState = MovingStates.MovingUp;
            //        //MovingButtonState = MovingStates.MovingDown;
            //    }
            //    else
            //    {
            //        Debug.WriteLine("MovingState is MovingUp");
            //    }
            //}
            //else if (OffSet < LastKnownY)
            //{
            //    if (MovingBarState is not MovingStates.MovingDown)
            //    {
            //        MovingBarState = MovingStates.MovingDown;
            //        //MovingButtonState = MovingStates.MovingUp;
            //    }
            //    else
            //    {
            //        Debug.WriteLine("MovingState is MovingDown");
            //    }
            //}

            //Debug.WriteLine(LastKnownY.ToString() + " " + OffSet.ToString());
            //LastKnownY = OffSet;
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
