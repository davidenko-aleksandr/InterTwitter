using System;
using Prism.Navigation;

namespace InterTwitter.ViewModels
{
    public class MessagesPageViewModel : BaseViewModel
    {
        public MessagesPageViewModel(INavigationService navigationService)
                                    : base(navigationService)
        {
        }
    }
}
