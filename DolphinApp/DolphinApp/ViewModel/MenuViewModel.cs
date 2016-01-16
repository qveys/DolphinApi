using DolphinApp.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace DolphinApp.ViewModel
{
    class MenuViewModel : ViewModelBase, INotifyPropertyChanged
    {

        private INavigationService _navigationService;

        public MenuViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public Utilisateur User { get; set; }

        public void OnNavigateTo(NavigationEventArgs e)
        {
            User = e.Parameter as Utilisateur;
        }

        private void NavToPage(string page)
        {
            _navigationService.NavigateTo(page, User);
        }

        private ICommand _goAddMatch;
        public ICommand GoAddMatch
        {
            get
            {
                if (_goAddMatch == null)
                    _goAddMatch = new RelayCommand(() => NavToPage("AddMatchPage"));
                return _goAddMatch;
            }
        }


        private ICommand _goDeleteMatch;
        public ICommand GoDeleteMatch
        {
            get
            {
                if (_goDeleteMatch == null)
                    _goDeleteMatch = new RelayCommand(() => NavToPage("DeleteMatchPage"));
                return _goDeleteMatch;
            }
        }

        private ICommand _goSearch;
        public ICommand GoSearch
        {
            get
            {
                if (_goSearch == null)
                    _goSearch = new RelayCommand(() => NavToPage("RecherchePage"));
                return _goSearch;
            }
        }

        private ICommand _goTotals;
        public ICommand GoTotals
        {
            get
            {
                if (_goTotals == null)
                    _goTotals = new RelayCommand(() => NavToPage("TotalsPage"));
                return _goTotals;
            }
        }

        private ICommand _goDeconnection;
        public ICommand GoDeconnection
        {
            get
            {
                if (_goDeconnection == null)
                    _goDeconnection = new RelayCommand(() => Deconnection());
                return _goDeconnection;
            }
        }
        private void Deconnection()
        {
            var appData = Windows.Storage.ApplicationData.Current;
            var remember = appData.LocalSettings.Values.Remove("Remember");
            _navigationService.NavigateTo("MainPage");
        }

    }
}
