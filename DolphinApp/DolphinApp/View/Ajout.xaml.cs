using DolphinApp.ViewModel;
using System;
using Windows.Foundation;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.Data.Xml.Dom;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DolphinApp.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Ajout : Page
    {

        public Ajout()
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
            ((AjoutViewModel)DataContext).Msg_ErreurChargementListes += Msg_ErreurChargementListes;
            ((AjoutViewModel)DataContext).Msg_ErreurValidMatch += Msg_ErreurValidMatch;
            ((AjoutViewModel)DataContext).Msg_ErreurDate += Msg_ErreurDate;
            ((AjoutViewModel)DataContext).OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            ((AjoutViewModel)DataContext).Msg_ErreurChargementListes -= Msg_ErreurChargementListes;
            ((AjoutViewModel)DataContext).Msg_ErreurValidMatch -= Msg_ErreurValidMatch;
            ((AjoutViewModel)DataContext).Msg_ErreurDate -= Msg_ErreurDate;
        }

        private async void Msg_ErreurChargementListes(object sender, EventArgs e)
        {
            MessageDialog msgDialog = new MessageDialog("Une erreur est survenue lors de la récupération des listes. \nVérifier votre connection internet!", "Oooops...");
            await msgDialog.ShowAsync();
        }

        private async void Msg_ErreurValidMatch(object sender, EventArgs e)
        {
            MessageDialog msgDialog = new MessageDialog("Une erreur est survenue lors de l'ajout d'un nouveau match. \nVérifier votre connection internet!", "Oooops...");
            await msgDialog.ShowAsync();
        }

        private async void Msg_ErreurDate(object sender, EventArgs e)
        {
            MessageDialog msgDialog = new MessageDialog("La date doit être égale ou supérieur à celle du jour", "Attention...");
            await msgDialog.ShowAsync();
        }

    }
}
