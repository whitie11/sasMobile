using Newtonsoft.Json;
using SASMobileApp1.Interfaces;
using SASMobileApp1.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace SASMobileApp1.Services
{
  public class PatientService : IPatientService
    {
        private AppDataService _appDataService;
        private TokenResponseModel trm;

        //    public HttpClientHandler Handler { get; set; } = new NativeMessageHandler();

        public PatientService(AppDataService appDataService)
        {
            _appDataService = appDataService;
            trm = _appDataService.GetTokenResponseModel();
        }
        public async Task<List<Patient>> GetAllPatientsWard()
        {
            string ward = _appDataService.getSelectedWard();
            string accessToken = trm.AccessToken;
            Uri baseURi = new Uri(Constants.API_Address);
            var subroute = "api/admission/getwardlist/" + ward;
            var uri = new Uri(baseURi, subroute);
            var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue
            {
                NoCache = true
            };

            client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
              
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    List<Patient> ptList = JsonConvert.DeserializeObject<List<Patient>>(content);

                    return ptList;
                }


                return null;

            }
            catch (Exception ex)
            {
                throw new SecurityException("Failed to load Patients", ex);
            }
        }
    }
}
