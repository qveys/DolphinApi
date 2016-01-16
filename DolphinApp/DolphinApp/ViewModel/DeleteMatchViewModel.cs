using DolphinApp.DataAccess;
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
using Windows.UI.Xaml.Navigation;

namespace DolphinApp.ViewModel
{
    public class DeleteMatchViewModel : ViewModelBase, INotifyPropertyChanged
    {

        private IAccessApi ApiAccess { get; set; }

        private INavigationService _navigationService;
        public DeleteMatchViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            ApiAccess = new AccessApi();
        }

        public Utilisateur User { get; set; }

        public void OnNavigatedTo(NavigationEventArgs e)
        { 
            User = e.Parameter as Utilisateur;
            ChargementListMatchsAsync();
        }

        private async void ChargementListMatchsAsync()
        {
            try
            {
                Chargement = true;
                ListMatchs = await ApiAccess.GetListAllMatchsAsync();
                SelectedMatch = ListMatchs.First();
            }
            catch
            {
                Msg_ErreurChargementListe(this, new EventArgs());
            }
            finally
            {
                Chargement = false;
            }
            
        }

        private bool _chargement;
        public bool Chargement
        {
            get { return _chargement; }
            set
            {
                _chargement = value;
                RaisePropertyChanged("Chargement");
            }
        }

        private IEnumerable<Match> _listMatchs;
        public IEnumerable<Match> ListMatchs
        {
            get { return _listMatchs; }
            set
            {
                _listMatchs = value;
                RaisePropertyChanged("ListMatchs");
            }
        }

        private Match _selectedMatch;
        public Match SelectedMatch
        {
            get { return _selectedMatch; }
            set
            {
                _selectedMatch = value;
                RaisePropertyChanged("SelectedMatch");
            }
        }

        public event EventHandler Msg_ErreurChargementListe;
        public event EventHandler Msg_ValidDeleteMatch;
        public event EventHandler Msg_ErreurInternet;
        public delegate Task<bool> SaveEventHandler(object sender, DeleteEventArgs e);
        public event SaveEventHandler Msg_SureToDelete;

        private ICommand _deleteMatch;
        public ICommand DeleteMatch
        {
            get
            {
                if (_deleteMatch == null)
                {
                    _deleteMatch = new RelayCommand(() => IsSureDeleteMatch());
                }
                return _deleteMatch;
            }
        }

        private async void IsSureDeleteMatch()
        {
            var result = await Msg_SureToDelete(this, new DeleteEventArgs(SelectedMatch.DATE_MATCH.ToString("dd/MM/yyyy"), SelectedMatch.DIVISION.LIBELLE_DIVISION, SelectedMatch.PISCINE.NOM_PISCINE));
            if (result)
                ProcessingDeleteMatch();
        }

        private async void ProcessingDeleteMatch()
        {

            try
            {
                Chargement = true;
                await ApiAccess.DeleteMatchAsync(SelectedMatch.ID_MATCH);
                Msg_ValidDeleteMatch(this, new EventArgs());
            }
            catch
            {
                Msg_ErreurInternet(this, new EventArgs());
            }
            finally
            {
                Chargement = false;
                _navigationService.NavigateTo("MenuPage", User);
            }
        }
    }
}
