using DolphinApp.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DolphinApp.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Recherche : Page
    {
        public Recherche()
        {
            this.InitializeComponent();
        }

        #region ChangeOrientation
        //Forcing Phone 4"
        int x = 320; int y = 569;

        //Forcing Phone 5"
        //int x = 360; int y = 640;
        private void ChangeOrientation(object sender, RoutedEventArgs e)
        {
            var view = ApplicationView.GetForCurrentView();
            if (view.Orientation.Equals(ApplicationViewOrientation.Landscape))
                view.TryResizeView(new Size { Width = x, Height = y });
            else
                view.TryResizeView(new Size { Width = y, Height = x });
        }
        #endregion ChangeOrientation

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var viewModel = ((RechercheViewModel)DataContext);
            viewModel.Msg_StartDateAfterEndDate += Msg_StartDateAfterEndDate;
            viewModel.Msg_EndDateBeforeStartDate += Msg_EndDateBeforeStartDate;
            viewModel.Msg_NoResultSearch += Msg_NoResultSearch;
            viewModel.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            var viewModel = ((RechercheViewModel)DataContext);
            viewModel.Msg_StartDateAfterEndDate -= Msg_StartDateAfterEndDate;
            viewModel.Msg_EndDateBeforeStartDate -= Msg_EndDateBeforeStartDate;
            viewModel.Msg_NoResultSearch += Msg_NoResultSearch;
        }

        private async void Msg_EndDateBeforeStartDate(object sender, EventArgs e)
        {
            MessageDialog msgDialog = new MessageDialog("La date de fin de la recherche doit être situé après la date de début", "Oooops...");
            await msgDialog.ShowAsync();
        }

        private async void Msg_StartDateAfterEndDate(object sender, EventArgs e)
        {
            MessageDialog msgDialog = new MessageDialog("La date de début de la recherche doit être situé avant la date de fin", "Oooops...");
            await msgDialog.ShowAsync();
        }

        private async void Msg_NoResultSearch(object sender, EventArgs e)
        {
            MessageDialog msgDialog = new MessageDialog("L'intervalle de dates proposé ne contient aucun match..", "Oooops...");
            await msgDialog.ShowAsync();
        }
    }
}
