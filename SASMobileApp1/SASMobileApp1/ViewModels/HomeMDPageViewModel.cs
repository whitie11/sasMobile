using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SASMobileApp1.ViewModels
{
    public class HomeMDPageViewModel : ViewModelBase
    {
        private DelegateCommand<string> _navigateCommand { get; set; }

        public DelegateCommand<string> NavigateCommand => _navigateCommand
            ?? (_navigateCommand = new DelegateCommand<string>(Navigate, CanNavigate));

        public HomeMDPageViewModel(INavigationService navigationService) : base(navigationService)
        {

        }
        private bool CanNavigate(string arg)
        {
            return true;
        }

        private void Navigate(string obj)
        {
            navigationService.NavigateAsync("NavigationPage/" + obj);
        }

        private bool CanNavigate()
        {
            return true;
        }
    }
}
