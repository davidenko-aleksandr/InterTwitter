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

        public static BindableProperty MovingStateProperty =
           BindableProperty.Create(nameof(MovingState), typeof(MovingStates), typeof(AuthorizationNavigationBar), propertyChanged: OnMovingStatePropertyChanged);
        public MovingStates MovingState
        {
            get => (MovingStates)GetValue(MovingStateProperty);
            set => SetValue(MovingStateProperty, value);
        }

        public static BindableProperty EditCommandProperty =
            BindableProperty.Create(nameof(EditCommand), typeof(ICommand), typeof(AuthorizationNavigationBar));
        public ICommand EditCommand
        {
            get => (ICommand)GetValue(EditCommandProperty);
            set => SetValue(EditCommandProperty, value);
        }

        #endregion

        #region -- Private helpers --

        private static void OnMovingStatePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var tab = bindable as HomePageNavigationBar;
            var oldOffset = (MovingStates)oldValue;
            var newOffset = (MovingStates)newValue;

            if (tab is not null && oldOffset != newOffset)
            {
                switch (newOffset)
                {
                    case MovingStates.MovingUp:
                        {
                            tab.TranslateTo(0, 0);
                            break;
                        }
                    case MovingStates.MovingDown:
                        {
                            tab.TranslateTo(0, -tab.Height);
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            else
            {
                //tab is null of oldOffset == newOffset 
            }
        }

        #endregion
    }
}