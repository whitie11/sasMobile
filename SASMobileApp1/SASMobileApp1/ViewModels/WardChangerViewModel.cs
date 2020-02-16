using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using SASMobileApp1.Services;
using System;
using System.Collections.Generic;

namespace SASMobileApp1.ViewModels
{
    public class WardChangerViewModel : ViewModelBase
    {
     
        AppDataService AppDataService { get; }

        public WardChangerViewModel(INavigationService navigationService, AppDataService appDataService1, IPageDialogService pageDialogService)
            : base(navigationService)
        {
            AppDataService = appDataService1;

            Title = "Change Ward";

            ChangeWardCommand = new DelegateCommand(ChangeWard, WardChangeCanExecute)
                .ObservesProperty(() => SelectedWard);
        }

        private void ChangeWard()
        {
           AppDataService.setSelectedWard(SelectedWard);
            navigationService.NavigateAsync("../");
        }

       
        List<string> wardList = new List<string>
        {
            "Keats",
            "Byron",
            "Churchill"
        };

        public List<string> WardList => wardList;

        private string _selectedWard;
        public string SelectedWard
        {
            get { return _selectedWard; }
            set {
              //  AppDataService.setSelectedWard(value);
                SetProperty(ref _selectedWard, value);
                //ChangeWard();

            }
        }


        public DelegateCommand ChangeWardCommand { get; }

        private bool WardChangeCanExecute() =>
            !string.IsNullOrWhiteSpace(SelectedWard);

    }
}
