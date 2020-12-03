using InterTwitter.Enums;
using System.Windows.Input;
using Xamarin.Forms;

namespace InterTwitter.Controls
{
    public partial class TabbedPageNavigationBar : Grid
    {
        public TabbedPageNavigationBar()
        {
            InitializeComponent();
        }

        #region -- Public properties --

        public static BindableProperty LeftIconClickCommandProperty =
            BindableProperty.Create(nameof(LeftIconClickCommand), typeof(ICommand), typeof(TabbedPageNavigationBar));
        public ICommand LeftIconClickCommand
        {
            get => (ICommand)GetValue(LeftIconClickCommandProperty);
            set => SetValue(LeftIconClickCommandProperty, value);
        }

        public static BindableProperty RightIconClickCommandProperty =
    BindableProperty.Create(nameof(RightIconClickCommand), typeof(ICommand), typeof(TabbedPageNavigationBar));
        public ICommand RightIconClickCommand
        {
            get => (ICommand)GetValue(RightIconClickCommandProperty);
            set => SetValue(RightIconClickCommandProperty, value);
        }

        public static BindableProperty MovingStateProperty =
           BindableProperty.Create(nameof(MovingState), typeof(MovingStates), typeof(TabbedPageNavigationBar), propertyChanged: OnMovingStatePropertyChanged);
        public MovingStates MovingState
        {
            get => (MovingStates)GetValue(MovingStateProperty);
            set => SetValue(MovingStateProperty, value);
        }

        public static BindableProperty TitleTextProperty =
            BindableProperty.Create(nameof(TitleText), typeof(string), typeof(TabbedPageNavigationBar));
        public string TitleText
        {
            get => (string)GetValue(TitleTextProperty);
            set => SetValue(TitleTextProperty, value);
        }

        public static BindableProperty LeftIconSourceProperty =
            BindableProperty.Create(nameof(LeftIconSource), typeof(string), typeof(TabbedPageNavigationBar));
        public string LeftIconSource
        {
            get => (string)GetValue(LeftIconSourceProperty);
            set => SetValue(LeftIconSourceProperty, value);
        }

        public static BindableProperty IsLeftIconVisibleProperty =
            BindableProperty.Create(nameof(IsLeftIconVisible), typeof(bool), typeof(TabbedPageNavigationBar));
        public bool IsLeftIconVisible
        {
            get => (bool)GetValue(IsLeftIconVisibleProperty);
            set => SetValue(IsLeftIconVisibleProperty, value);
        }

        public static BindableProperty RightIconSourceProperty =
            BindableProperty.Create(nameof(RightIconSource), typeof(string), typeof(TabbedPageNavigationBar));
        public string RightIconSource
        {
            get => (string)GetValue(RightIconSourceProperty);
            set => SetValue(RightIconSourceProperty, value);
        }

        public static BindableProperty IsRightIconVisibleProperty =
            BindableProperty.Create(nameof(IsRightIconVisible), typeof(bool), typeof(TabbedPageNavigationBar));
        public bool IsRightIconVisible
        {
            get => (bool)GetValue(IsRightIconVisibleProperty);
            set => SetValue(IsRightIconVisibleProperty, value);
        }

        public static BindableProperty IsBottomLineVisibleProperty =
            BindableProperty.Create(nameof(IsBottomLineVisible), typeof(bool), typeof(TabbedPageNavigationBar));
        public bool IsBottomLineVisible
        {
            get => (bool)GetValue(IsBottomLineVisibleProperty);
            set => SetValue(IsBottomLineVisibleProperty, value);
        }

        #endregion

        #region -- Private helpers --

        private static void OnMovingStatePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var tab = bindable as TabbedPageNavigationBar;
            var oldOffset = (MovingStates)oldValue;
            var newOffset = (MovingStates)newValue;

            if (tab is not null && oldOffset != newOffset)
            {
                switch (newOffset)
                {
                    case MovingStates.MovingUp:
                        {
                            tab.TranslateTo(0, 0);
                            tab.FadeTo(1, 300);
                            break;
                        }

                    case MovingStates.MovingDown:
                        {
                            tab.TranslateTo(0, -tab.Height);
                            tab.FadeTo(0, 300);
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