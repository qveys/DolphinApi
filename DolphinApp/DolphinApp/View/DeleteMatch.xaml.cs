using DolphinApp.Model;
using DolphinApp.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public sealed partial class DeleteMatch : Page
    {
        public DeleteMatch()
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
            var viewModel = ((DeleteMatchViewModel)DataContext);
            viewModel.Msg_ErreurChargementListe += Msg_ErreurChargementListe;
            viewModel.Msg_SureToDelete += Msg_SureToDelete;
            viewModel.Msg_ValidDeleteMatch += Msg_ValidDeleteMatch;
            viewModel.Msg_ErreurInternet += Msg_ErreurInternet;
            viewModel.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            var viewModel = ((DeleteMatchViewModel)DataContext);
            viewModel.Msg_ErreurChargementListe -= Msg_ErreurChargementListe;
            viewModel.Msg_SureToDelete -= Msg_SureToDelete;
            viewModel.Msg_ValidDeleteMatch -= Msg_ValidDeleteMatch;
            viewModel.Msg_ErreurInternet -= Msg_ErreurInternet;
        }

        private async Task<bool> Msg_SureToDelete(object sender, DeleteEventArgs e)
        {
            MessageDialog msgDialog = new MessageDialog("Etes-vous sur de vouloir supprimer le match " + e.Division + " du " + e.MatchDate + " à la piscine de " + e.Piscine);
            msgDialog.Commands.Add(new UICommand("Yes") { Id = 1 });
            msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("No") { Id = 0 });
            msgDialog.DefaultCommandIndex = 1;
            msgDialog.CancelCommandIndex = 0;
            var result = await msgDialog.ShowAsync();
            return ((Convert.ToInt32(result.Id).Equals(1)) ? true : false);
        }

        private async void Msg_ErreurChargementListe(object sender, EventArgs e)
        {
            MessageDialog msgDialog = new MessageDialog("Une erreur est survenue lors de la récupération de la liste des matchs. \nVérifier votre connection internet!", "Oooops...");
            await msgDialog.ShowAsync();
        }

        private async void Msg_ValidDeleteMatch(object sender, EventArgs e)
        {
            MessageDialog msgDialog = new MessageDialog("Le Match a correctement été supprimé! :)", "OK");
            await msgDialog.ShowAsync();
        }

        private async void Msg_ErreurInternet(object sender, EventArgs e)
        {
            MessageDialog msgDialog = new MessageDialog("Vérifier votre connection internet!", "Oooops...");
            await msgDialog.ShowAsync();
        }
    }
}
