using System;
using Prism.Mvvm;
using Prism.Navigation;

namespace InterTwitter.ViewModels
{
    public class BaseViewModel : BindableBase, INavigationAware, IInitialize, IDestructible
    {
        public BaseViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        #region -- Properties --

        protected INavigationService NavigationService { get; private set; }

        #endregion

        #region -- IDestructible Implementation --

        public void Destroy()
        {
        }

        #endregion

        #region -- IInitialize Implementation --

        public void Initialize(INavigationParameters parameters)
        {
        }

        #endregion

        #region -- INavigationAware Implementation --

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        #endregion
    }
}
