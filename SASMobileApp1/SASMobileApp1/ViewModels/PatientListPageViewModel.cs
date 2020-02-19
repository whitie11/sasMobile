using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SASMobileApp1.Interfaces;
using SASMobileApp1.Models;
using SASMobileApp1.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;



namespace SASMobileApp1.ViewModels
{

    public class PatientListPageViewModel : ViewModelBase
    {
        AppDataService AppDataService { get; }
        PatientService PatientService { get; }

        private ObservableCollection<PatientListItem> pts = new ObservableCollection<PatientListItem>();

        public ObservableCollection<PatientListItem> Pts
        {
            get { return pts; }
            set { SetProperty(ref pts, value); }
        }

        private string _wardName;
        public string WardName
        {
            get { return _wardName; }
            set { SetProperty(ref _wardName, value); }
        }

        private PatientListItem _selectedItem;
        public PatientListItem SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }



        private PatientListItem _oldPt;

        private DelegateCommand<PatientListItem> _navigateToObsCommand { get; set; }
        public DelegateCommand<PatientListItem> NavigateToObsCommand => _navigateToObsCommand
            ?? (_navigateToObsCommand = new DelegateCommand<PatientListItem>(NavigateToObs, CanNavigate));

        private DelegateCommand<PatientListItem> _navigateToLeaveCommand { get; set; }
        public DelegateCommand<PatientListItem> NavigateToLeaveCommand => _navigateToLeaveCommand
            ?? (_navigateToLeaveCommand = new DelegateCommand<PatientListItem>(NavigateToLeave, CanNavigate));

        private DelegateCommand<PatientListItem> _navigateToLeaveEditCommand { get; set; }
        public DelegateCommand<PatientListItem> NavigateToLeaveEditCommand => _navigateToLeaveEditCommand
            ?? (_navigateToLeaveEditCommand = new DelegateCommand<PatientListItem>(NavigateToLeaveEdit, CanNavigate));


        private DelegateCommand<PatientListItem> _navigateToDetailsCommand { get; set; }
        public DelegateCommand<PatientListItem> NavigateToDetailsCommand => _navigateToDetailsCommand
            ?? (_navigateToDetailsCommand = new DelegateCommand<PatientListItem>(NavigateToDetails, CanNavigate));

     //   public DelegateCommand RefreshListCommand { get; }

        public DelegateCommand UpdateListCommand { get; }

        public DelegateCommand<PatientListItem> ItemTappedCommand { get; }

        public DelegateCommand WardChangeCommand { get; }

        private void OnItemTappedCommandExecuted(PatientListItem pt) =>
         //  ShowOrHideButtons(pt);
         NavigateToDetails(pt);

        private bool CanNavigate(PatientListItem arg)
        {
            return true;
        }

        private bool CanNavigate()
        {
            return true;
        }

        public void NavigateToObs(PatientListItem obj)
        {
            var navigationParams = new NavigationParameters();
            navigationParams.Add("patient", obj);
            navigationService.NavigateAsync("ObsPage", navigationParams);
        }

        public void NavigateToLeave(PatientListItem obj)
        {
            var navigationParams = new NavigationParameters();
            navigationParams.Add("patient", obj);
            navigationService.NavigateAsync("LeavePage", navigationParams);
        }

        public void NavigateToLeaveEdit(PatientListItem obj)
        {
            var navigationParams = new NavigationParameters();
            navigationParams.Add("patient", obj);
            navigationService.NavigateAsync("LeaveEditPage", navigationParams);
        }

        public void NavigateToDetails(PatientListItem obj)
        {
            var navigationParams = new NavigationParameters();
            navigationParams.Add("patient", obj);
            navigationService.NavigateAsync("PatientDetailsPage", navigationParams);
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }


        protected override void OnIsActive()
        {

            initialiseList();
        }

        //public override void OnNavigatedTo(INavigationParameters parameters)
        //{
        //    WardName = AppDataService.getSelectedWard();
        //    initialiseList();
        //}

        //   protected virtual void OnIsNotActive() { }


        public PatientListPageViewModel(INavigationService navigationService,
            AppDataService appDataService,
            PatientService patientService) : base(navigationService)
        {
            AppDataService = appDataService;
           
            PatientService = patientService;
           // RefreshListCommand = new DelegateCommand(refreshList, CanNavigate);
            UpdateListCommand = new DelegateCommand(initialiseList, CanNavigate);
            WardChangeCommand = new DelegateCommand(changeWard);
           // WardName = AppDataService.getSelectedWard();
            ItemTappedCommand = new DelegateCommand<PatientListItem>(OnItemTappedCommandExecuted);
            initialiseList();
        }

        private void changeWard()
        {
            navigationService.NavigateAsync("WardChanger");
        }

        public async void initialiseList()
        {
            Device.BeginInvokeOnMainThread(() => { IsBusy = true; });

            await Task.Run(async () => {

                WardName = AppDataService.getSelectedWard();
                Pts.Clear();
                DateTime timeCheck = DateTime.UtcNow.AddMinutes(-30);

                List<Patient> result = await PatientService.GetAllPatientsWard();
                foreach (Patient pt in result)
                {
                    PatientListItem pli = new PatientListItem(pt);
                    if (pli.LastSeen == null || pli.LastSeen < timeCheck)
                    {
                        pli.ObsTimeColour = "LightPink";
                    }
                    if (pli.Leave.TimeRetDue == null || pli.Leave.TimeRetDue < timeCheck)
                    {
                        pli.ObsRetColour = "LightPink";
                    }


                    if (pli.Leave == null)
                    {
                        pli.Leave = new LeaveReg();
                        pli.Leave.IsCurrent = false;
                    }


                    Pts.Add(pli);
                }
            });

            Device.BeginInvokeOnMainThread(() =>
            {
                IsBusy = false;

            });


            
        }




        public void ShowOrHideButtons(PatientListItem pt)
        {
            if (_oldPt == pt)
            {
                // click twice on the same item will hide it
                pt.IsSelected = !pt.IsSelected;
                UpdatePt(pt);
            }
            else
            {
                if (_oldPt != null)
                {
                    // hide previous selected item
                    _oldPt.IsSelected = false;
                    UpdatePt(_oldPt);
                }
                // show selected item
                pt.IsSelected = true;
                UpdatePt(pt);
            }

            _oldPt = pt;
        }

        private void UpdatePt(PatientListItem pt)
        {

            var index = Pts.IndexOf(pt);
            if (index > -1)
            {
                Pts.Remove(pt);
                Pts.Insert(index, pt);
                SelectedItem = pt;
            }
        }


    }
}
