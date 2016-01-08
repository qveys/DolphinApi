using DolphinApp.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Navigation;

namespace DolphinApp.ViewModel
{ 
    public class AjoutViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private static int ALLER_RETOUR = 2;
        private static decimal PRIX_AU_KM = 0.3456M;

        public event EventHandler Msg_ErreurChargementListes;
        public event EventHandler Msg_ErreurValidMatch;
        public event EventHandler Msg_ErreurDate;

        private INavigationService _navigationService;

        public AjoutViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Chargement = true;
            Today = new DateTimeOffset(DateTime.Today);
            DateMatch = Today;
        }

        public Utilisateur User { get; set; }

        public DateTimeOffset Today { get; set; }

        private DateTimeOffset _dateMatch;
        public DateTimeOffset DateMatch
        {
            get { return _dateMatch; }
            set
            {
                if (value < Today) Msg_ErreurDate(this, new EventArgs());
                _dateMatch = value;
            }
        }

        public bool SecondMatch { get; set; }

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

        private IEnumerable<Division> _listeDivision;
        public IEnumerable<Division> ListeDivision
        {
            get { return _listeDivision; }
            set
            {
                _listeDivision = value;
                RaisePropertyChanged("ListeDivision");
            }
        }

        private IEnumerable<Piscine> _listePiscine;
        public IEnumerable<Piscine> ListePiscine
        {
            get { return _listePiscine; }
            set
            {
                _listePiscine = value;
                RaisePropertyChanged("ListePiscine");
            }
        }

        private Piscine _piscineSelected;
        public Piscine PiscineSelected
        {
            get { return _piscineSelected; }
            set
            {
                _piscineSelected = value;
                RaisePropertyChanged("PiscineSelected");
            }
        }

        private Division _divisionSelected;
        public Division DivisionSelected
        {
            get { return _divisionSelected; }
            set
            {
                _divisionSelected = value;
                RaisePropertyChanged("DivisionSelected");
            }
        }

        private ICommand _validMatch;
        public ICommand ValidMatch
        {
            get
            {
                if (_validMatch == null)
                {
                    _validMatch = new RelayCommand(() => IsValidMatch());
                }
                return _validMatch;
            }
        }

        public async void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                User = e.Parameter as Utilisateur;
                await GetDivisionsAsync();
                await GetPiscinesAsync();
                PiscineSelected = ListePiscine.First();
                DivisionSelected = ListeDivision.First();
                Chargement = false;
            }
            catch
            {
                Msg_ErreurChargementListes(this, new EventArgs());
                _navigationService.NavigateTo("MenuPage", User);
            }
        }

        private async Task GetPiscinesAsync()
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync("http://dolphinapp.azurewebsites.net/api/piscine");
                if (response.IsSuccessStatusCode)
                {
                    String json = await response.Content.ReadAsStringAsync();
                    ListePiscine = Newtonsoft.Json.JsonConvert.DeserializeObject<Piscine[]>(json);
                }
                else { throw new Exception(); }
            }
            catch { throw; }
        }

        private async Task GetDivisionsAsync()
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync("http://dolphinapp.azurewebsites.net/api/division");
                if (response.IsSuccessStatusCode)
                {
                    String json = await response.Content.ReadAsStringAsync();
                    ListeDivision = Newtonsoft.Json.JsonConvert.DeserializeObject<Division[]>(json);
                }
                else { throw new Exception(); }
            }
            catch { throw; }
        }

        public async void IsValidMatch()
        {
            try
            {

                if (DateMatch < Today)
                {
                    Msg_ErreurDate(this, new EventArgs());
                    return;
                }

                Chargement = true;

                Match newMatch = await createMatch();

                await AddMatch(newMatch);
            }
            catch
            {
                Msg_ErreurValidMatch(this, new EventArgs());
                _navigationService.NavigateTo("MenuPage", User);
            }
        }

        private async Task<Match> createMatch()
        {
            try
            {
                var newMatch = new Match();
                newMatch.DATE_MATCH = DateMatch.DateTime;
                newMatch.ID_DIVISION = DivisionSelected.ID_DIVISION;
                newMatch.ID_PISCINE = PiscineSelected.ID_PISCINE;
                newMatch.ID_UTILISATEUR = User.ID_UTILISATEUR;
                newMatch.SECOND_MATCH = SecondMatch;
                newMatch.DISTANCE = await calculDistance();
                newMatch.COUT = calculCout(newMatch);
                return newMatch;
            }
            catch { throw; }
        }

        private async Task AddMatch(Match newMatch)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(newMatch);

            HttpClient client = new HttpClient();
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("http://dolphinapp.azurewebsites.net/api/match", content);

            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                newMatch.DIVISION = DivisionSelected;
                newMatch.PISCINE = PiscineSelected;
                Chargement = false;
                var parameters = new List<Object>();
                parameters.Add(User);
                parameters.Add(newMatch);
                _navigationService.NavigateTo("ResultAddPage", parameters);
            }
            else { throw new Exception(); }
        }

        private decimal calculCout(Match match)
        {
            decimal cout = (match.DISTANCE * ALLER_RETOUR) * PRIX_AU_KM;

            switch (match.ID_DIVISION)
            {
                case 1:
                case 2:
                case 20:
                    cout += 50;
                    break;
                case 7:
                case 9:
                case 11:
                case 13:
                    cout += 37.5M;
                    break;
                default:
                    cout += 25;
                    break;
            }
            return cout;
        }

        private async Task<decimal> calculDistance()
        {
            try
            {
                var url = new Uri("http://maps.google.com/maps/api/directions/json?origin=" + User.ADR_LATITUDE + "," + User.ADR_LONGITUDE + "&destination=" + PiscineSelected.ADR_LATITUDE + "," + PiscineSelected.ADR_LONGITUDE);

                HttpClient client = new HttpClient();
                var json = await client.GetStringAsync(url);
                var response = JObject.Parse(json);
                var status = (string)response.SelectToken("status");

                if (status.Equals("OK"))
                {
                    var distance = (double)response.SelectToken("routes").First.SelectToken("legs").First.SelectToken("distance").SelectToken("value");
                    distance = distance / 1000;
                    return (decimal)distance;
                }
                else { throw new Exception(); }
            }
            catch { throw; }
        }
    }
}
