using DolphinApp.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
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
    public sealed partial class Connection : Page
    {

        public Connection()
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
            ((ConnectionViewModel)DataContext).Msg_ErreurInternet += Msg_ErreurInternet;
            ((ConnectionViewModel)DataContext).Msg_ErreurUser += Msg_ErreurUser;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            ((ConnectionViewModel)DataContext).Msg_ErreurInternet -= Msg_ErreurInternet;
            ((ConnectionViewModel)DataContext).Msg_ErreurUser -= Msg_ErreurUser;
        }

        private async void Msg_ErreurUser(object sender, EventArgs e)
        {
            MessageDialog msgDialog = new MessageDialog("Le Login et/ou Mot de Passe inséré est incorrect !", "Oooops...");
            await msgDialog.ShowAsync();
        }

        private async void Msg_ErreurInternet(object sender, EventArgs e)
        {
            MessageDialog msgDialog = new MessageDialog("Vérifier votre connection internet!", "Oooops...");
            await msgDialog.ShowAsync();
        }

    }
}