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

        private ICommand _goAjout;
        public ICommand GoAjout
        {
            get
            {
                if (_goAjout == null)
                    _goAjout = new RelayCommand(() => NavToAjoutPage());
                return _goAjout;
            }
        }

        private void NavToAjoutPage()
        {
            _navigationService.NavigateTo("AjoutPage", User);
        }

        private ICommand _goRecherche;
        public ICommand GoRecherche
        {
            get
            {
                if (_goRecherche == null)
                    _goRecherche = new RelayCommand(() => NavToRecherchePage());
                return _goRecherche;
            }
        }

        private void NavToRecherchePage()
        {
            _navigationService.NavigateTo("RecherchePage", User);
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

        public void OnNavigateTo(NavigationEventArgs e)
        {
            User = e.Parameter as Utilisateur;
        }
    }
}
