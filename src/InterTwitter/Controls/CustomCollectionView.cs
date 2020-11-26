using InterTwitter.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace InterTwitter.Controls
{
    public class CustomCollectionView : CollectionView
    {
        private double _oldVerticalOffset;
        public CustomCollectionView()
        {

        }

        public static BindableProperty MovingStateProperty =
           BindableProperty.Create(nameof(MovingState), typeof(MovingStates), typeof(CustomCollectionView));
        public MovingStates MovingState
        {
            get => (MovingStates)GetValue(MovingStateProperty);
            set => SetValue(MovingStateProperty, value);
        }

        protected override void OnScrolled(ItemsViewScrolledEventArgs e)
        {
            base.OnScrolled(e);

            if (e.VerticalOffset == 0d)
            {
                MovingState = MovingStates.MovingUp;
            }
            else if (e.VerticalOffset > _oldVerticalOffset)
            {
                MovingState = MovingStates.MovingDown;
            }
            else
            {
                MovingState = MovingStates.MovingUp;
            }

            _oldVerticalOffset = e.VerticalOffset;
        }

    }
}
