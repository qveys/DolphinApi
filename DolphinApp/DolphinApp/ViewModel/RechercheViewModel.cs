using DolphinApp.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Navigation;

namespace DolphinApp.ViewModel
{
    class RechercheViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService _navigationService;

        public RechercheViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            EndDate = new DateTimeOffset(DateTime.Today.AddDays(1));
            StartDate = new DateTimeOffset(DateTime.Today);
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

        public Utilisateur User { get; set; }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            User = e.Parameter as Utilisateur;
        }

        public event EventHandler Msg_StartDateAfterEndDate;
        public event EventHandler Msg_EndDateBeforeStartDate;
        public event EventHandler Msg_NoResultSearch;

        private DateTimeOffset _startDate;
        public DateTimeOffset StartDate
        {
            get { return _startDate; }
            set
            {
                if (value >= EndDate)
                    Msg_StartDateAfterEndDate(this, new EventArgs());
                _startDate = value;
            }
        }

        private DateTimeOffset _endDate;
        public DateTimeOffset EndDate
        {
            get { return _endDate; }
            set
            {
                if (value <= StartDate)
                    Msg_EndDateBeforeStartDate(this, new EventArgs());
                _endDate = value;
            }
        }

        public IEnumerable<Match> ListResult { get; set; }

        private ICommand _goResultSearch;
        public ICommand GoResultSearch
        {
            get
            {
                if (_goResultSearch == null)
                {
                    _goResultSearch = new RelayCommand(() => IsResultSearch());
                }
                return _goResultSearch;
            }
        }

        private async void IsResultSearch()
        {
            if (StartDate < EndDate)
            {
                if (EndDate > StartDate)
                {
                    Chargement = true;
                    await SearchResults();
                    if (ListResult.Count() != 0)
                    {
                        NavToResultSearch();
                    }
                    else
                    {
                        Msg_NoResultSearch(this, new EventArgs());
                    }
                    Chargement = false;
                }
                else
                {
                    Msg_EndDateBeforeStartDate(this, new EventArgs());
                }
            }
            else
            {
                Msg_StartDateAfterEndDate(this, new EventArgs());
            }
        }

        private async Task SearchResults()
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync("http://dolphinapp.azurewebsites.net/api/match?idUtilisateur=" + User.ID_UTILISATEUR + "&startDate=" + StartDate.ToString("yyyy-MM-dd") + "&endDate=" + EndDate.ToString("yyyy-MM-dd"));
                if (response.IsSuccessStatusCode)
                {
                    String json = await response.Content.ReadAsStringAsync();
                    ListResult = Newtonsoft.Json.JsonConvert.DeserializeObject<Match[]>(json);
                }
                else { throw new Exception(); }
            }
            catch { throw; }
        }

        private void NavToResultSearch()
        {
            var parameters = new List<Object>();
            parameters.Add(User);
            parameters.Add(ListResult);
            _navigationService.NavigateTo("ResultSearchPage", parameters);
        }
    }
}
