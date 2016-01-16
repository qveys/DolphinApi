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
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;

namespace DolphinApp.ViewModel
{
    public class ConnectionViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService _navigationService;

        public ConnectionViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Chargement = false;
        }

        public event EventHandler Msg_ErreurInternet;
        public event EventHandler Msg_ErreurUser;

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

        public bool RememberMe { get; set; }

        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value.ToUpper(); }
        }

        public string UserPassword { get; set; }

        public Utilisateur User { get; set; }

        private ICommand _goMenuPage;
        public ICommand GoMenu
        {
            get
            {
                if (_goMenuPage == null)
                {
                    _goMenuPage = new RelayCommand(() => IsNavToMenuPage());
                }
                return _goMenuPage;
            }
        }

        private async void IsNavToMenuPage()
        {
            try
            {
                Chargement = true;
                User = await IsUserExist();
                if (User != null && UserPassword != null && IsPasswordRight())
                {
                    if (RememberMe)
                        await RememberUser(); 
                    _navigationService.NavigateTo("MenuPage", User);
                }
                else
                {
                    Msg_ErreurUser(this, new EventArgs());
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

        private async Task<Utilisateur> IsUserExist()
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync("http://dolphinapp.azurewebsites.net/api/utilisateur?login=" + Username);

                if (response.IsSuccessStatusCode)
                {
                    String json = await response.Content.ReadAsStringAsync();
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<Utilisateur>(json);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound) { return null; }
                else { throw new Exception(); }
            }
            catch { throw; }
        }

        private bool IsPasswordRight()
        {
            return cryptPassword(UserPassword).Equals(User.MOTDEPASSE);
        }

        private string cryptPassword (string passwordEncryption)
        {
            // Create a string that contains the name of the hashing algorithm to use.
            String strAlgName = HashAlgorithmNames.Sha512;

            // Create a HashAlgorithmProvider object.
            HashAlgorithmProvider objAlgProv = HashAlgorithmProvider.OpenAlgorithm(strAlgName);

            // Create a CryptographicHash object. This object can be reused to continually
            // hash new messages.
            CryptographicHash objHash = objAlgProv.CreateHash();

            // Hash message 1.
            IBuffer buffMsg1 = CryptographicBuffer.ConvertStringToBinary(passwordEncryption, BinaryStringEncoding.Utf16BE);
            objHash.Append(buffMsg1);
            IBuffer buffHash1 = objHash.GetValueAndReset();

            return CryptographicBuffer.EncodeToBase64String(buffHash1);
        }

        private async Task RememberUser()
        {
            try
            {
                var appData = Windows.Storage.ApplicationData.Current;
                var localFolder = appData.LocalFolder;
                var storageFile = await localFolder.CreateFileAsync("user.json", Windows.Storage.CreationCollisionOption.ReplaceExisting);
                var userString = Newtonsoft.Json.JsonConvert.SerializeObject(User);
                await Windows.Storage.FileIO.WriteTextAsync(storageFile, userString);
                appData.LocalSettings.Values["Remember"] = "true";
            }
            catch { throw; }
        }
    }
}
