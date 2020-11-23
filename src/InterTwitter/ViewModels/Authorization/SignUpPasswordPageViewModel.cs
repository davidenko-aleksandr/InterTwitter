using System;
using System.Threading.Tasks;
using System.Windows.Input;
using InterTwitter.Helpers;
using InterTwitter.Services.Authorization;
using InterTwitter.Validators;
using InterTwitter.Views.Authorization;
using Prism.Navigation;
using Xamarin.Forms;

namespace InterTwitter.ViewModels.Authorization
{
    public class SignUpPasswordPageViewModel : BaseViewModel
    {
        private readonly IAuthorizationService _authorizationService;
        private string _email;
        private string _name;

        public SignUpPasswordPageViewModel(INavigationService navigationService,
                                            IAuthorizationService authorizationService)
                                          : base(navigationService)
        {
            _authorizationService = authorizationService;
        }

        #region -- Public Properties --

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _confirm;
        public string Confirm
        {
            get => _confirm;
            set => SetProperty(ref _confirm, value);
        }

        private ICommand _confirmCommand;
        public ICommand ConfirmCommand => _confirmCommand ??= SingleExecutionCommand.FromFunc(OnConfirmCommand);

        #endregion

        #region -- OnCommand Handlers --

        private async Task OnConfirmCommand()
        {
            var canContinue = PasswordIsValidated();

            if(canContinue)
            {
                await _authorizationService.SignUpAsync(_email, _name, Password);
               // await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(LogInPage)}");
            }
            else
            {
            //Wrong password data
            }
        }

        #endregion

        #region -- Overrides --

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.TryGetValue<string>(Constants.Navigation.Name, out string Name))
            {
                _name = Name;
            }
            else
            { 
            // obtain next parameter
            }            
            if (parameters.TryGetValue<string>(Constants.Navigation.Email, out string Email))
            {
                _email = Email;
            }
            else
            { 
            //
            }
        }

        #endregion

        #region -- Private Heplers --

        private bool PasswordIsValidated()
        {
            return Validator.IsMatch(Password, Validator.RegexPassword) && Password == Confirm;
        }

        #endregion

    }
}
