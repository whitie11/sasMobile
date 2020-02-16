using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SASMobileApp1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace SASMobileApp1.ViewModels
{
    public class LogoutPageViewModel : ViewModelBase
    {
        AuthenticationService AuthenticationService { get; }

        public LogoutPageViewModel(INavigationService navigationService, AuthenticationService authenticationService)
               : base(navigationService)
        {
            AuthenticationService = authenticationService;
        }

        //public override void OnNavigatedTo(INavigationParameters parameters)
        //{
        //    base.OnNavigatedTo(parameters); 
        //}

        protected override void OnIsActive()
        {
            Application.Current.Properties.Clear();
            navigationService.NavigateAsync("/LoginPage");
        }
    }
}
