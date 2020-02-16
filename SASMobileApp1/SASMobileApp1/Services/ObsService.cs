using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SASMobileApp1.Interfaces;
using SASMobileApp1.Models;
using SASMobileApp1.Services;

namespace SASMobileApp1.Services
{
    public class ObsService : IObsService
    {
        private AppDataService _appDataService;
        private TokenResponseModel trm;

        public ObsService(AppDataService appDataService)
        {
            _appDataService = appDataService;
            trm = _appDataService.GetTokenResponseModel();
        }



        public async Task<ObservationDTO> SaveObs(ObservationDTO dto)
        {
            

            string url = String.Format("{0}api/observation", Constants.API_Address);
            //{
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                      new AuthenticationHeaderValue("Bearer", trm.AccessToken);

            var json = JsonConvert.SerializeObject(dto);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();

                var json2 = await response.Content.ReadAsStringAsync();
                ObservationDTO savedObs = JsonConvert.DeserializeObject<ObservationDTO>(json2);
                var Id = savedObs.ObsId;
                return savedObs;

            }
            catch (Exception ex)
            {
                throw new SecurityException("Failed to save observation.", ex);
            }
        }




    }
}
