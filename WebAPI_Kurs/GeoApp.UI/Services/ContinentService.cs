using GeoApp.Shared.Entities;
using Newtonsoft.Json;
using System.Text;

namespace GeoApp.UI.Services
{
    public class ContinentService : IContinentService
    {
        private HttpClient _httpClient;

        public ContinentService(HttpClient httpClient) //Instanz bekommen wir via IHttpClientFactory 
        {
            _httpClient = httpClient;
        }

        public async Task<IList<Continent>> GetAllContinents()
        {
            //https://localhost:7195/api/Continent

            //HttpResponse erhält die Liste der Kontinente und Http-StatusCode (200) 

            HttpResponseMessage response =  await _httpClient.GetAsync("Continent");

            string jsonText = await response.Content.ReadAsStringAsync();

            List<Continent> continents = JsonConvert.DeserializeObject<List<Continent>>(jsonText);

            return continents;
        }

        public async Task<Continent> GetById(int id)
        {
            //https://localhost:7195/api/Continent/123
            HttpResponseMessage response = await _httpClient.GetAsync($"Continent/{id}");

            //Auslesen des JSON aus der Response
            string jsonText = await response.Content.ReadAsStringAsync();

            //https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/how-to?pivots=dotnet-7-0
            Continent continent = JsonConvert.DeserializeObject<Continent>(jsonText);


            return continent;

        }


        public async Task AddContinent(Continent continent)
        {
            //https://localhost:7195/api/Continent als POST

            continent.Countries = new List<Country>(); //in JSON wäre das  "Countries": [] 

            string jsonString = JsonConvert.SerializeObject(continent);

            StringContent data = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("Continent", data);


            //Wenn wir direkt mit dem neuen Continent weiterarbeite wollen und die ID gleich benötigen 
            string jsonText = await response.Content.ReadAsStringAsync();

            Continent continentWithId = JsonConvert.DeserializeObject<Continent>(jsonText);
        }

        public async Task DeleteContinent(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"Continent/{id}");
        }

        

        public async Task UpdateContinent(Continent modifiedContinent)
        {
            string jsonString = JsonConvert.SerializeObject(modifiedContinent);
            StringContent data = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync($"Continent/{modifiedContinent.Id}", data);
        }
    }
}
