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
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DolphinApp.ViewModel
{
    class MainPageViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService _navigationService;

        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private ICommand _isConnected;
        public ICommand IsConnected
        {
            get
            {
                if (_isConnected == null)
                {
                    _isConnected = new RelayCommand(() => IsUserConnected());
                }
                return _isConnected;
            }
        }

        private async void IsUserConnected()
        {
            var appData = Windows.Storage.ApplicationData.Current;
            var remember = appData.LocalSettings.Values["Remember"];

            if (remember != null)
            {
                try
                {
                    await readLocalUser(appData);
                }
                catch
                {
                    _navigationService.NavigateTo("ConnectionPage");
                }
            }
            else
            {
                _navigationService.NavigateTo("ConnectionPage");
            }
        }

        private async Task readLocalUser(Windows.Storage.ApplicationData appData)
        {
            var localFolder = appData.LocalFolder;
            var storageFile = await localFolder.GetFileAsync("user.json");
            var userString = await Windows.Storage.FileIO.ReadTextAsync(storageFile);
            var user = Newtonsoft.Json.JsonConvert.DeserializeObject<Utilisateur>(userString);

            if (user != null)
                _navigationService.NavigateTo("MenuPage", user);
            else
                _navigationService.NavigateTo("ConnectionPage");
        }
    }
}
