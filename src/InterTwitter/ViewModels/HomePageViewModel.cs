using System.Collections.ObjectModel;
using System.Threading.Tasks;
using InterTwitter.Services.Owl;
using InterTwitter.Helpers;
using Prism.Navigation;
using System.Windows.Input;
using Xamarin.Forms;
using InterTwitter.ViewModels.OwlItems;

namespace InterTwitter.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        private readonly IOwlService _owlService;

        public HomePageViewModel(
                                INavigationService navigationService,
                                IOwlService owlService)
                                : base(navigationService)
        {
            _owlService = owlService;

            //ProfileAvatar = "pic_profile_small.png";
        }

        #region -- Public properties --

        private ObservableCollection<OwlViewModel> _owls;
        public ObservableCollection<OwlViewModel> Owls
        {
            get => _owls;
            set => SetProperty(ref _owls, value);
        }

        private string _icon = "ic_home_gray";
        public string Icon
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        private string _profileAvatar;
        public string ProfileAvatar
        {
            get => _profileAvatar;
            set => SetProperty(ref _profileAvatar, value);
        }

        public ICommand OpenMenuCommand => SingleExecutionCommand.FromFunc(OnOpenMenuCommandAsync);

        #endregion       

        #region -- Overrides --
       
        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            Icon = "ic_home_blue";

            var owls = await _owlService.GetAllOwlsAsync();

            Owls = new ObservableCollection<OwlViewModel>(owls.Result);
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            Icon = "ic_home_gray";
        }

        #endregion

        #region -- Private helpers --

        private async Task OnOpenMenuCommandAsync()
        {
            MessagingCenter.Send<object>(this, Constants.OpenMenuMessage);
        }

        #endregion
    }
}