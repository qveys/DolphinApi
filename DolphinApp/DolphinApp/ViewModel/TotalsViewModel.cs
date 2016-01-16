using DolphinApp.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections;
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
    class TotalsViewModel : ViewModelBase, INotifyPropertyChanged
    {

        private INavigationService _navigationService;

        public TotalsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Chargement = false;
            Year = true;
            Today = new DateTimeOffset(DateTime.Today);
            StartingYear = new DateTimeOffset(new DateTime(DateTime.Today.Year, 1, 1));
            StartingMonth = new DateTimeOffset(new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1));
        }

        public DateTimeOffset Today { get; set; }
        public DateTimeOffset StartingYear { get; set; }
        public DateTimeOffset StartingMonth { get; set; }

        public Utilisateur User { get; set; }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            User = e.Parameter as Utilisateur;
        }

        public event EventHandler Msg_NoResultSearch;
        public event EventHandler Msg_ErreurInternet;

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

        private bool _year;
        public bool Year
        {
            get { return _year; }
            set
            {
                if (value)
                {
                    Month = false;
                    Season = false; 
                }
                _year = value;
                RaisePropertyChanged("Year");
            }
        }

        private bool _month;
        public bool Month
        {
            get { return _month; }
            set
            {
                if (value)
                {
                    Year = false;
                    Season = false; 
                }
                _month = value;
                RaisePropertyChanged("Month");
            }
        }

        private bool _season;
        public bool Season
        {
            get { return _season; }
            set
            {
                if (value)
                {
                    Year = false;
                    Month = false; 
                }
                _season = value;
                RaisePropertyChanged("Season");
            }
        }

        public IEnumerable<Match> ListResultJson { get; set; }

        public IEnumerable<Totaux> ListResultCalculate { get; set; }

        private ICommand _goResultTotals;
        public ICommand GoResultTotals
        {
            get
            {
                if (_goResultTotals == null)
                {
                    _goResultTotals = new RelayCommand(() => IsGoResultPage());
                }
                return _goResultTotals;
            }
        }

        private async void IsGoResultPage()
        {
            try
            {
                Chargement = true;
                await SearchResults();
                if (ListResultJson.Count() != 0)
                {
                    ProcessingResult();
                    NavToResultTotals();
                }
                else
                {
                    Msg_NoResultSearch(this, new EventArgs());
                }
            }
            catch
            {
                Msg_ErreurInternet(this, new EventArgs());
            }
            finally
            {
                Chargement = false;
            }
        }

        private async Task SearchResults()
        {
            try
            {
                HttpClient client = new HttpClient();
                var startDate = ((Year) ? StartingYear : ((Month) ? StartingMonth : new DateTimeOffset(new DateTime(2000, 1, 1))));
                HttpResponseMessage response = await client.GetAsync("http://dolphinapp.azurewebsites.net/api/match?idUtilisateur=" + User.ID_UTILISATEUR + "&startDate=" + startDate.ToString("yyyy-MM-dd") + "&endDate=" + Today.ToString("yyyy-MM-dd"));
                if (response.IsSuccessStatusCode)
                {
                    String json = await response.Content.ReadAsStringAsync();
                    ListResultJson = Newtonsoft.Json.JsonConvert.DeserializeObject<Match[]>(json);
                }
                else { throw new Exception(); }
            }
            catch { throw; }
        }

        private void ProcessingResult()
        {
            if (Year)
            {
                Calculate("yyyy");
            }
            else if (Month)
            {
                Calculate("MMMM yyyy");
            }
            else
            {
                CalculateSeason();
            }
        }

        private void Calculate(string dateFormat)
        {
            var listCalculate = new List<Totaux>();
            var saveDateFormat = ListResultJson.First().DATE_MATCH.ToString(dateFormat);
            var result = initTotalsObject(saveDateFormat);

            foreach (var listResult in ListResultJson)
            {
                if (!saveDateFormat.Equals(listResult.DATE_MATCH.ToString(dateFormat)))
                {
                    listCalculate.Add(result);
                    saveDateFormat = listResult.DATE_MATCH.ToString(dateFormat);
                    result = initTotalsObject(saveDateFormat);
                }

                result.COUT += listResult.COUT;
                result.DISTANCE += listResult.DISTANCE;
            }

            listCalculate.Add(result);
            ListResultCalculate = listCalculate;
        }

        private void CalculateSeason()
        {
            var listCalculate = new List<Totaux>();

            var dateFirstMatch = ListResultJson.First().DATE_MATCH;
            var startYear = (dateFirstMatch.Month < 9) ? dateFirstMatch.Year - 1 : dateFirstMatch.Year;
            var endSeason = new DateTime(startYear + 1, 9, 1);

            string saveDateFormat = startYear.ToString() + " - " + endSeason.Year.ToString();
            var result = initTotalsObject(saveDateFormat);

            foreach (var listResult in ListResultJson)
            {
                if (listResult.DATE_MATCH >= endSeason)
                {
                    listCalculate.Add(result);
                    saveDateFormat = endSeason.Year.ToString();
                    endSeason.AddYears(1);
                    saveDateFormat += " - " + endSeason.Year.ToString();
                    result = initTotalsObject(saveDateFormat);
                }

                result.COUT += listResult.COUT;
                result.DISTANCE += listResult.DISTANCE;
            }

            listCalculate.Add(result);
            ListResultCalculate = listCalculate;
        }

        private static Totaux initTotalsObject(string dateFormat)
        {
            var totaux = new Totaux();
            totaux.COUT = 0;
            totaux.DISTANCE = 0;
            totaux.DATEFORMAT = dateFormat;
            return totaux;
        }

        private void NavToResultTotals()
        {
            _navigationService.NavigateTo("ResultTotalsPage", ListResultCalculate);
        }
    }
}
