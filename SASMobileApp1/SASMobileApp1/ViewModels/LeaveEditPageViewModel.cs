using Prism.AppModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SASMobileApp1.Models;
using SASMobileApp1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SASMobileApp1.ViewModels
{
    public class LeaveEditPageViewModel : ViewModelBase, IAutoInitialize, IInitialize
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

        private int leaveCode;
        public int LeaveCode
        {
            get
            {
                return leaveCode;
            }
            set
            {
                SetProperty(ref leaveCode, value);
            }
        }

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

        LeaveService LeaveService;
        bool DataSaved = false;
        private object syncLock = new object();
        bool isInCall = false;


        public DelegateCommand SubmitLeaveCommand { get; }
        public DelegateCommand CancelCommand { get; }

        public LeaveEditPageViewModel(INavigationService navigationService, LeaveService leaveService) : base(navigationService)
        {
            SubmitLeaveCommand = new DelegateCommand(SubmitLeaveCommandExecuted, SubmitLeaveCommandCanExecute);
            CancelCommand = new DelegateCommand(Cancel);
            LeaveService = leaveService;
            Saving = false;
        }

        private void Cancel()
        {
            navigationService.GoBackAsync();
        }

        private bool SubmitLeaveCommandCanExecute()
        {

            return !Saving;
        }

        private async void SubmitLeaveCommandExecuted()
        {
            if (Saving)
            {
                return;
            }
            Device.BeginInvokeOnMainThread(() => { Saving = true; });

                await Task.Run(async () =>
                {
                    LeaveRegDTO data = new LeaveRegDTO
                    {
                        LeaveId = pt.Leave.LeaveId,
                        LeaveTypeId = pt.Leave.LeaveTypeId,
                        Description = pt.Leave.Description,
                        IsCurrent = false,
                        PatientId = pt.Leave.PatientId,
                        TimeOut = pt.Leave.TimeOut,
                        TimeRetActual = this.leaveRetDate.Add(LeaveRetTime),
                        TimeRetDue = pt.Leave.TimeRetDue
                    };
                    DataSaved = await LeaveService.EditLeave(data);

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

        public void Initialize(INavigationParameters parameters)
        {

            Pt = parameters.GetValue<PatientListItem>("patient");
            LeaveRetTime = DateTime.UtcNow.TimeOfDay;
            LeaveRetDate = DateTime.UtcNow.Date;

        }
    }
}
