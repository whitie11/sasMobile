using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using SASMobileApp1.Interfaces;
using SASMobileApp1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SASMobileApp1.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        AuthenticationService AuthenticationService { get; }
        IPageDialogService PageDialogService { get; }

        public LoginPageViewModel(INavigationService navigationService, AuthenticationService authenticationService, IPageDialogService pageDialogService)
            : base(navigationService)
        {
            AuthenticationService = authenticationService;
            PageDialogService = pageDialogService;

            Title = "Login";
            UserName = "Ian.White";
            Password = "P@ssw0rd1";
            SelectedWard = "Keats";
    

            LoginCommand = new DelegateCommand(OnLoginCommandExecuted, LoginCommandCanExecute)
                .ObservesProperty(() => UserName)
                .ObservesProperty(() => Password)
                .ObservesProperty(() => SelectedWard);
        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public List<string> WardList { get; } = new List<string>
        {
            "Keats",
            "Byron",
            "Churchill"
        };

        private string _selectedWard;
        private bool isloggedIn;

        public string SelectedWard
        {
            get { return _selectedWard; }
            set { SetProperty(ref _selectedWard, value); }
        }

        //private Boolean IsBusyX = false;
        //public new Boolean IsBusy
        //{
        //    get { return IsBusyX; }
        //    set { SetProperty(ref IsBusyX, value); }
        //}



        public DelegateCommand LoginCommand { get; }




        private async void OnLoginCommandExecuted()
        {
            Device.BeginInvokeOnMainThread(() => { IsBusy= true; });

            await Task.Run(async () => {
                isloggedIn = await AuthenticationService.LoginAsync(UserName, Password, SelectedWard);
            });

            Device.BeginInvokeOnMainThread(async () => {
                IsBusy = false;
                if (isloggedIn)
                {
                    await navigationService.NavigateAsync("/NavigationPage/HomeTabPage");
                }
                else
                {
                    await PageDialogService.DisplayAlertAsync("ERROR", "Username or Password not recognised!", "Continue");
                }

            });

        }

        private bool LoginCommandCanExecute() =>
            !string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(SelectedWard) && IsNotBusy;

    }
}
