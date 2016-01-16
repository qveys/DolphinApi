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
            SimpleIoc.Default.Register<AddMatchViewModel>();
            SimpleIoc.Default.Register<ResultAddViewModel>();
            SimpleIoc.Default.Register<RechercheViewModel>();
            SimpleIoc.Default.Register<ResultSearchViewModel>();
            SimpleIoc.Default.Register<TotalsViewModel>();
            SimpleIoc.Default.Register<ResultTotalsViewModel>();
            SimpleIoc.Default.Register<DeleteMatchViewModel>();

            NavigationService navigationService = new NavigationService();
            navigationService.Configure("MainPage", typeof(MainPage));
            navigationService.Configure("ConnectionPage", typeof(Connection));
            navigationService.Configure("MenuPage", typeof(Menu));
            navigationService.Configure("AddMatchPage", typeof(AddMatch));
            navigationService.Configure("ResultAddPage", typeof(ResultAdd));
            navigationService.Configure("RecherchePage", typeof(Recherche));
            navigationService.Configure("ResultSearchPage", typeof(ResultSearch));
            navigationService.Configure("TotalsPage", typeof(Totals));
            navigationService.Configure("ResultTotalsPage", typeof(ResultTotals));
            navigationService.Configure("DeleteMatchPage", typeof(DeleteMatch));
            SimpleIoc.Default.Register<INavigationService>(() => navigationService);
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

        public AddMatchViewModel AddMatch
        {
            get { return ServiceLocator.Current.GetInstance<AddMatchViewModel>(); }
        }

        public ResultAddViewModel ResultAdd
        {
            get { return ServiceLocator.Current.GetInstance<ResultAddViewModel>(); }
        }

        public RechercheViewModel Recherche
        {
            get { return ServiceLocator.Current.GetInstance<RechercheViewModel>(); }
        }

        public ResultSearchViewModel ResultSearch
        {
            get { return ServiceLocator.Current.GetInstance<ResultSearchViewModel>(); }
        }

        public TotalsViewModel Totals
        {
            get { return ServiceLocator.Current.GetInstance<TotalsViewModel>(); }
        }

        public ResultTotalsViewModel ResultTotals
        {
            get { return ServiceLocator.Current.GetInstance<ResultTotalsViewModel>(); }
        }

        public DeleteMatchViewModel DeleteMatch
        {
            get { return ServiceLocator.Current.GetInstance<DeleteMatchViewModel>(); }
        }
    }
}
