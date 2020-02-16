using Prism.AppModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SASMobileApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using SASMobileApp1.Services;

namespace SASMobileApp1.ViewModels
{
    public class ObsPageViewModel : ViewModelBase, IAutoInitialize, IInitialize
    {
        private ObsService ObsService;

        private PatientListItem pt;
        public PatientListItem Pt
        {
            get { return pt; }
            set
            {
                SetProperty(ref pt, value);
            }
        }


        private string _location;
        public string Location
        {
            get { return _location; }
            set
            {
                SetProperty(ref _location, value);
            }
        }


        public IList<string> LocationList => Constants.Locations;

        private string _status;
        public string Status
        {
            get { return _status; }
            set
            {
                SetProperty(ref _status, value);
            }
        }

        private string _notes;
        public string Notes
        {
            get { return _notes; }
            set
            {
                SetProperty(ref _notes, value);
            }
        }

        private bool _locPIckerEnabled;
        public bool LocPIckerEnabled
        {
            get { return _locPIckerEnabled; }
            set
            {
                SetProperty(ref _locPIckerEnabled, value);
                if (value)
                {
                    LocPIckerColour = "Aqua";
                }
                else
                {
                    LocPIckerColour = "Azure";
                }
            }
        }

        private string _locPIckerColour;
        public string LocPIckerColour
        {
            get
            {
                return _locPIckerColour;
            }
            set
            {
                SetProperty(ref _locPIckerColour, value);
            }
        }


        private bool _statusPIckerEnabled;
        public bool StatusPIckerEnabled
        {
            get { return _statusPIckerEnabled; }
            set
            {
                SetProperty(ref _statusPIckerEnabled, value);
                if (value)
                {
                    StatusPIckerColour = "Aqua";
                }
                else
                {
                    StatusPIckerColour = "Azure";
                }
            }
        }

        private string _stausPIckerColour;
        public string StatusPIckerColour
        {
            get
            {
                return _stausPIckerColour;
            }
            set
            {
                SetProperty(ref _stausPIckerColour, value);
            }
        }


        public IList<string> StatusList => Constants.Status;

        public DelegateCommand SaveObsCommand { get; }
        public DelegateCommand CancelCommand { get; }

        public ObsPageViewModel(INavigationService navigationService, ObsService obsService) : base(navigationService)
        {
            Title = "Obsevations";
            ObsService = obsService;

            SaveObsCommand = new DelegateCommand(SaveObsCommandExecuted, SaveObsCommandCanExecute)
    .ObservesProperty(() => Location)
    .ObservesProperty(() => Status)
    .ObservesProperty(() => Notes);

            CancelCommand = new DelegateCommand(Cancel);
        }

        private void Cancel()
        {
            navigationService.GoBackAsync();
        }

        private async void SaveObsCommandExecuted()
        {
            var DTO = new ObservationDTO();
            DTO.PatientId = Pt.PatientId;
            DTO.ObsTime = DateTime.UtcNow;
            DTO.ObsLocation = Location;
            DTO.Status = Status;

            if (string.IsNullOrEmpty(Notes))
            {
                DTO.Notes = "No comments made!";
            }else
            {
                DTO.Notes = Notes;
            }

            if (Application.Current.Properties.ContainsKey("UserName"))
            {
                DTO.SeenBy = Application.Current.Properties["UserName"] as string;
            }
            else
            {
                DTO.SeenBy = "";
            }

            var result = await ObsService.SaveObs(DTO);

            await navigationService.NavigateAsync("../");
        }

        private bool SaveObsCommandCanExecute() =>
            !string.IsNullOrWhiteSpace(Location)
            && !string.IsNullOrWhiteSpace(Status)
            ;




        public void Initialize(INavigationParameters parameters)
        {
            Pt = parameters.GetValue<PatientListItem>("patient");
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }
    }
}
