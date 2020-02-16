using Prism;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SASMobileApp1.ViewModels
{
    public class ViewModelBase : BindableBase, IActiveAware, INavigationAware, IDestructible
    {
        protected INavigationService navigationService { get; private set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        #region IActiveAware

        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set { SetProperty(ref _isActive, value, RaiseIsActiveChanged); }
        }
        public event EventHandler IsActiveChanged;

        private void RaiseIsActiveChanged()
        {
            IsActiveChanged?.Invoke(this, EventArgs.Empty);

            if (IsActive)
            {
                OnIsActive();
            }
            else
            {
                OnIsNotActive();
            }
        }

        protected virtual void OnIsActive()
        {
        }

        protected virtual void OnIsNotActive()

        {
        }

        #endregion IActiveAware


        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value, () => RaisePropertyChanged(nameof(IsNotBusy))); }
        }

        public bool IsNotBusy
        {
            get { return !IsBusy; }
        }

        public ViewModelBase(INavigationService navigationService)
        {
            this.navigationService = navigationService;

        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatingTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }
    }
}
