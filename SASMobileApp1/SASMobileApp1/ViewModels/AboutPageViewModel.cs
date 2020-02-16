using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SASMobileApp1.ViewModels
{
    public class AboutPageViewModel : ViewModelBase
    {
        public AboutPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "About Page";
        }
    }
}
