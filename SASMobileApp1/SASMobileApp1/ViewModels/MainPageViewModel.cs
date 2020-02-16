using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SASMobileApp1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SASMobileApp1.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {

        IAppDataService AppDataService { get; }
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }
      private  string accessToken;

        public string AccessToken
        {
            get { return accessToken; }
            set { SetProperty(ref accessToken, value); }
        }


        public MainPageViewModel(INavigationService navigationService, IAppDataService appDataService)
            : base(navigationService)
        {
            Title = "Main Page";
            AppDataService = appDataService;
            UserName = AppDataService.GetTokenResponseModel().UserName;
            AccessToken = AppDataService.GetTokenResponseModel().AccessToken;
        }
    }
}
