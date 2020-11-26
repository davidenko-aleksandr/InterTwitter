using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace InterTwitter.Views
{
    public partial class HomePage : BaseContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        public double LastKnownY { get; set; }
        private void CollectionView_Scrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            Debug.WriteLine("Delta = " + e.VerticalDelta.ToString());
            Debug.WriteLine("OFFSet = " + e.VerticalOffset.ToString());

           
        }
    }
}
