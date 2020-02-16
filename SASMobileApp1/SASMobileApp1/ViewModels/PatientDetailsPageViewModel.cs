using Prism.AppModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SASMobileApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SASMobileApp1.ViewModels
{
    public class PatientDetailsPageViewModel : ViewModelBase, IAutoInitialize, IInitialize
    {
        private PatientListItem pt;
        public PatientListItem Pt
        {
            get { return pt; }
            set
            {
                SetProperty(ref pt, value);
            }
        }

        private DelegateCommand<string> _navigateToTaskCommand { get; set; }
        public DelegateCommand<string> NavigateToTaskCommand => _navigateToTaskCommand
            ?? (_navigateToTaskCommand = new DelegateCommand<string>(Navigate, CanNavigate));

        private bool CanNavigate(string arg)
        {
            return true;
        }

        private void Navigate(string obj)
        {
            if (obj == "LeavePage" && Pt.Leave.IsCurrent)
            {
                obj = "LeaveEditPage";
            }
            var navigationParams = new NavigationParameters();
            navigationParams.Add("patient", Pt);
            navigationService.NavigateAsync(obj, navigationParams);
        }

        public PatientDetailsPageViewModel(INavigationService navigationService) : base(navigationService)
        {

        }

        public void Initialize(INavigationParameters parameters)
        {
            Pt = parameters.GetValue<PatientListItem>("patient");
           
        }
    }
}
