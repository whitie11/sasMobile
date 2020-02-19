using Prism.AppModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using SASMobileApp1.Models;
using SASMobileApp1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SASMobileApp1.ViewModels
{


    public class LeavePageViewModel : ViewModelBase, IAutoInitialize, IInitialize
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

        private LeaveReg leaveData;
        public LeaveReg LeaveData
        {
            get { return leaveData; }
            set
            {
                SetProperty(ref leaveData, value);
            }
        }

        private TimeSpan leaveRetTime;
        public TimeSpan LeaveRetTime
        {
            get { return leaveRetTime; }
            set
            {
                SetProperty(ref leaveRetTime, value);
            }
        }

        private DateTime leaveRetDate;
        public DateTime LeaveRetDate
        {
            get { return leaveRetDate; }
            set
            {
                SetProperty(ref leaveRetDate, value);
            }
        }

        private string comments;
        public string Comments
        {
            get
            {
                return comments;
            }
            set
            {
                SetProperty(ref comments, value);
            }
        }

        private IList<LeaveType> leaveTypes;
        public IList<LeaveType> LeaveTypes
        {
            get { return leaveTypes; }
            set
            {
                SetProperty(ref leaveTypes, value);
            }
        }

        private LeaveType selectLeaveType;
        public LeaveType SelectLeaveType
        {
            get { return selectLeaveType; }
            set
            {
                SetProperty(ref selectLeaveType, value);
            }
        }

        bool DataSaved = false;

        private bool saving;
        public bool Saving
        {
            get
            {
                return saving;
            }
            set
            {
                SetProperty(ref saving, value);
            }
        }

        public DelegateCommand SubmitLeaveCommand { get; }

        public DelegateCommand CancelCommand { get; }

        IPageDialogService PageDialogService { get; }

        LeaveService ls;

        public LeavePageViewModel(INavigationService navigationService, LeaveService leaveService, IPageDialogService pageDialogService) : base(navigationService)
        {
            PageDialogService = pageDialogService;
            ls = leaveService;
            getLT();

            SubmitLeaveCommand = new DelegateCommand(SubmitLeaveCommandExecuted, SubmitLeaveCommandCanExecute)
                .ObservesProperty(() => SelectLeaveType)
                .ObservesProperty(() => LeaveRetTime)
                .ObservesProperty(() => LeaveRetDate)
                .ObservesProperty(() => Comments);

            CancelCommand = new DelegateCommand(Cancel);
        }

        private async void getLT()
        {
            LeaveTypes = await ls.GetLeaveTypes();
        }

        private void Cancel()
        {
            navigationService.GoBackAsync();
        }

        public void Initialize(INavigationParameters parameters)
        {
            Pt = parameters.GetValue<PatientListItem>("patient");
            LeaveRetTime = DateTime.UtcNow.TimeOfDay.Subtract(new TimeSpan(0, 0, 1));
            LeaveRetDate = DateTime.UtcNow.Date;
        }

        public async void SubmitLeaveCommandExecuted()
        {
            if (Saving)
            {
                return;
            }
            if (leaveRetDate.Add(leaveRetTime) < DateTime.UtcNow)
            {
                errorMessage("Return time must be in the future!");
                return;
            }

            
            Device.BeginInvokeOnMainThread(() => { Saving = true; });

            await Task.Run(async () =>
                  {
                      var DTO = new LeaveReg();
                      DTO.PatientId = Pt.PatientId;
                      DTO.LeaveTypeId = selectLeaveType.LeaveTypeId;
                      DTO.Description = comments;
                      DTO.IsCurrent = true;
                      DTO.TimeOut = DateTime.UtcNow;
                      DTO.TimeRetDue = leaveRetDate.Add(leaveRetTime);

                      DataSaved = await ls.SaveLeave(DTO);
                  });

            Device.BeginInvokeOnMainThread(async () =>
            {
                if (DataSaved)
                {
                    await navigationService.NavigateAsync("../");
                }
                else
                {
                    //   Saving = false;
                }
            });
        }

        private bool SubmitLeaveCommandCanExecute()
        {

            if (Saving)
            {
                return false;
            }
            var LeaveRetDateTime = LeaveRetDate.Add(LeaveRetTime);
            //if (LeaveRetDateTime < DateTime.UtcNow)
            //{
            //    return false;
            //}
            if (SelectLeaveType == null) return false;
            return !string.IsNullOrWhiteSpace(SelectLeaveType.Text) && !string.IsNullOrWhiteSpace(Comments);
        }


        async void errorMessage(string message)
        {
            await PageDialogService.DisplayAlertAsync("ERROR", message, "Close");

        }

    }




}
