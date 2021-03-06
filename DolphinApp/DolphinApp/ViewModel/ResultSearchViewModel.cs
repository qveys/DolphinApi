﻿using DolphinApp.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace DolphinApp.ViewModel
{
    public class ResultSearchViewModel : ViewModelBase, INotifyPropertyChanged
    {

        private INavigationService _navigationService;

        public ResultSearchViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public IEnumerable<Match> ListResult { get; set; }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            ListResult = e.Parameter as IEnumerable<Match>;
        }
    }
}
