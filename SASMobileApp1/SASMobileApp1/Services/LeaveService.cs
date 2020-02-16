using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SASMobileApp1.Interfaces;
using SASMobileApp1.Models;

namespace SASMobileApp1.Services
{
    public class LeaveService : ILeaveService
    {
        private AppDataService _appDataService;
        private TokenResponseModel trm;

        public LeaveService()
        {
            _appDataService = new AppDataService();
            trm = _appDataService.GetTokenResponseModel();
        }

        public async Task<IList<LeaveType>> GetLeaveTypes()
        {
            string accessToken = trm.AccessToken;
            Uri baseURi = new Uri(Constants.API_Address);
            var subroute = "api/leave/getleavetypes";
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
                    var LeaveTypeList = JsonConvert.DeserializeObject<IList<LeaveType>>(content);


                    return LeaveTypeList;
                }


                return null;

            }
            catch (Exception ex)
            {
                throw new SecurityException("Failed to load Leave Types", ex);
            }
        }

        public async Task<bool> SaveLeave(LeaveReg dto)
        {
            string url = String.Format("{0}api/leave", Constants.API_Address);
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

                return true; 

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> EditLeave(LeaveRegDTO dto)
        {
            string url = String.Format("{0}api/leave", Constants.API_Address);
            //{
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                      new AuthenticationHeaderValue("Bearer", trm.AccessToken);

            var json = JsonConvert.SerializeObject(dto);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PutAsync(url, content);
                response.EnsureSuccessStatusCode();

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
