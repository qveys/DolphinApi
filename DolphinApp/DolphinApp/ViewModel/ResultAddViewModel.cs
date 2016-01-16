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
using Windows.UI.Xaml.Navigation;

namespace DolphinApp.ViewModel
{
    public class ResultAddViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService _navigationService;

        public ResultAddViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            var parameters = e.Parameter as List<Object>;
            User = parameters.First() as Utilisateur;
            Match m = parameters.Last() as Match;

            Piscine = m.PISCINE.NOM_PISCINE;
            Kilometre = m.DISTANCE.ToString("#.##") + " km";
            Prix = m.COUT.ToString("#.##") + " €";
        }

        public Utilisateur User { get; set; }

        private string _piscine;
        public string Piscine
        {
            get { return _piscine; }
            set
            {
                _piscine = value;
                RaisePropertyChanged("Piscine");
            }
        }

        private string _kilometre;
        public string Kilometre
        {
            get { return _kilometre; }
            set
            {
                _kilometre = value;
                RaisePropertyChanged("Kilometre");
            }
        }

        private string _prix;
        public string Prix
        {
            get { return _prix; }
            set
            {
                _prix = value;
                RaisePropertyChanged("Prix");
            }
        }

        private ICommand _goMenuPage;
        public ICommand GoMenuPage
        {
            get
            {
                if (_goMenuPage == null)
                {
                    _goMenuPage = new RelayCommand(() => NavToMenuPage());
                }
                return _goMenuPage;
            }
        }

        private void NavToMenuPage()
        {
            _navigationService.NavigateTo("MenuPage", User);
        }

    }
}
