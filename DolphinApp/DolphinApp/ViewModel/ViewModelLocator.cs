using DolphinApp.View;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinApp.ViewModel
{
    class ViewModelLocator
    {

        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainPageViewModel>();
            SimpleIoc.Default.Register<ConnectionViewModel>();
            SimpleIoc.Default.Register<MenuViewModel>();
            SimpleIoc.Default.Register<AjoutViewModel>();
            SimpleIoc.Default.Register<ResultAddViewModel>();

            NavigationService navigationService = new NavigationService();
            SimpleIoc.Default.Register<INavigationService>(() => navigationService);
            navigationService.Configure("MainPage", typeof(MainPage));
            navigationService.Configure("ConnectionPage", typeof(Connection));
            navigationService.Configure("MenuPage", typeof(Menu));
            navigationService.Configure("AjoutPage", typeof(Ajout));
            navigationService.Configure("ResultAddPage", typeof(ResultAdd));
        }

        public MainPageViewModel Main
        {
            get { return ServiceLocator.Current.GetInstance<MainPageViewModel>(); }
        }

        public ConnectionViewModel Connection
        {
            get { return ServiceLocator.Current.GetInstance<ConnectionViewModel>(); }
        }

        public MenuViewModel Menu
        {
            get { return ServiceLocator.Current.GetInstance<MenuViewModel>(); }
        }

        public AjoutViewModel Ajout
        {
            get { return ServiceLocator.Current.GetInstance<AjoutViewModel>(); }
        }

        public ResultAddViewModel ResultAdd
        {
            get { return ServiceLocator.Current.GetInstance<ResultAddViewModel>(); }
        }
    }
}
