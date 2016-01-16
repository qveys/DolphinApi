using DolphinApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DolphinApp.DataAccess
{
    public class AccessApi : IAccessApi
    {
        public AccessApi()
        {

        }

        private async Task<string> GetResponseHttpAsync(string endUrl)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://dolphinapp.azurewebsites.net/api/" + endUrl);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else { throw new Exception(); }
        }

        public async Task<IEnumerable<Match>> GetListAllMatchsAsync()
        {
            try
            {
                String json = await GetResponseHttpAsync("match");
                return Newtonsoft.Json.JsonConvert.DeserializeObject<Match[]>(json);
            }
            catch { throw; }
        }

        public async Task<IEnumerable<Division>> GetListDivisionAsync()
        {
            try
            {
                String json = await GetResponseHttpAsync("division");
                return Newtonsoft.Json.JsonConvert.DeserializeObject<Division[]>(json);
            }
            catch { throw; }
        }

        public async Task<IEnumerable<Piscine>> GetListPiscineAsync()
        {
            try
            {
                String json = await GetResponseHttpAsync("piscine");
                return Newtonsoft.Json.JsonConvert.DeserializeObject<Piscine[]>(json);
            }
            catch { throw; }
        }

        public async Task DeleteMatchAsync(int idMatch)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.DeleteAsync("http://dolphinapp.azurewebsites.net/api/match/" + idMatch);

                if (!response.IsSuccessStatusCode)
                    throw new Exception();
            }
            catch { throw; }
        }
    }
}
