using InterTwitter.Enums;
using Xamarin.Forms;

namespace InterTwitter.Controls
{
    public class CustomCollectionView : CollectionView
    {
        private double _oldVerticalOffset;
        public CustomCollectionView()
        {
        }

        #region -- Public properties --

        public static BindableProperty MovingStateProperty =
           BindableProperty.Create(nameof(MovingState), typeof(MovingStates), typeof(CustomCollectionView));
        public MovingStates MovingState
        {
            get => (MovingStates)GetValue(MovingStateProperty);
            set => SetValue(MovingStateProperty, value);
        }

        #endregion

        #region -- Private helpers --

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

        #endregion
    }
}
