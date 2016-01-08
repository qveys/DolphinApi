using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinApp.ViewModel
{
    class RechercheViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService _navigationService;

        public RechercheViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

    }
}
