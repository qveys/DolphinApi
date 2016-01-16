using DolphinApp.Model;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace DolphinApp.ViewModel
{
    public class ResultTotalsViewModel
    {
        private INavigationService _navigationService;

        public ResultTotalsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public IEnumerable<Totaux> ListResult { get; set; }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            ListResult = e.Parameter as IEnumerable<Totaux>;
        }

    }
}
