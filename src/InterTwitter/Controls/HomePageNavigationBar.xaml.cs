using InterTwitter.Enums;
using System;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace InterTwitter.Controls
{
    public partial class HomePageNavigationBar : Grid
    {
        public HomePageNavigationBar()
        {
            InitializeComponent();
        }

        #region -- Public properties --

        public static BindableProperty GoBackCommandProperty =
            BindableProperty.Create(nameof(GoBackCommand), typeof(ICommand), typeof(AuthorizationNavigationBar));
        public ICommand GoBackCommand
        {
            get => (ICommand)GetValue(GoBackCommandProperty);
            set => SetValue(GoBackCommandProperty, value);
        }

        public static BindableProperty OffsetYProperty =
           BindableProperty.Create(nameof(OffsetY), typeof(double), typeof(AuthorizationNavigationBar), propertyChanged: OnOffsetYPropertyChanged);
        public double OffsetY
        {
            get => (double)GetValue(OffsetYProperty);
            set => SetValue(OffsetYProperty, value);
        }

        public static BindableProperty EditCommandProperty =
            BindableProperty.Create(nameof(EditCommand), typeof(ICommand), typeof(AuthorizationNavigationBar));
        public ICommand EditCommand
        {
            get => (ICommand)GetValue(EditCommandProperty);
            set => SetValue(EditCommandProperty, value);
        }

        public MovingStates MovingState { get; set; }
        #endregion

        #region -- Private helpers --

        private static void OnOffsetYPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var tab = bindable as HomePageNavigationBar;
            var oldOffset = (double)oldValue;
            var newOffset = (double)newValue;

            if (tab is not null)
            {
                if (newOffset > oldOffset)
                {
                    if (tab.MovingState != MovingStates.Closed)
                    {
                        tab.TranslateTo(0, -tab.Height);
                        tab.MovingState = MovingStates.Closed;
                    }
                }
                else if ()
                {

                }
                else
                {
                    if (tab.MovingState != MovingStates.Opened)
                    {
                        tab.TranslateTo(0, 0);
                        tab.MovingState = MovingStates.Opened;
                    }
                }
            }
            else
            {

            }
        }

        private static void OnMovingStatePropertyChanged(BindableObject bindable, object oldvalue, object newValue)
        {

            var tab = bindable as HomePageNavigationBar;
            var newState = (MovingStates)newValue;

            if (tab is not null)
            {



                //switch (newState)
                //{
                //    case MovingStates.MovingUp:
                //        {
                //            tab.TranslateTo(0, -tab.Height);
                //            tab.TranslationY 
                //            break;
                //        }
                //    case MovingStates.MovingDown:
                //        {
                //            tab.TranslateTo(0, 0);
                //            break;
                //        }
                //    default:
                //        {
                //            break;
                //        }
                //}
            }
            else
            {
                Debug.WriteLine("Tab is null");
            }
        }

        #endregion
    }
}