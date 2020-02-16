using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Prism.Navigation;
using SASMobileApp1.Interfaces;
using SASMobileApp1.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SASMobileApp1.Services
{
   public class AuthenticationService : IAuthenticationService
    {
        INavigationService NavigationService { get; }
        AppDataService AppDataService { get; }

        public AuthenticationService(INavigationService navigationService, AppDataService appDataService)
        {
            NavigationService = navigationService;
            AppDataService = appDataService;
        }

        public bool Login(string username, string password)
        {
            if (password.Equals("prismrocks", StringComparison.OrdinalIgnoreCase))
            {
                //  Settings.Current.UserName = username;
                return true;
            }

            return false;
        }


        public async Task<bool> LoginAsync(string userName, string pw, string selectedWard)
        {

            var keyValues = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("username", userName),
        new KeyValuePair<string, string>("password", pw),
        new KeyValuePair<string, string>("grant_type", "password")
    };
            var request = new HttpRequestMessage(HttpMethod.Post, "https://sasdatamanager.azurewebsites.net/token");
            request.Content = new FormUrlEncodedContent(keyValues);
            request.Headers.Add("content_type", "application/x-www-form-urlencoded");
            var client = new HttpClient();
            try
            {
                var response = client.SendAsync(request).Result;

                if (response.IsSuccessStatusCode)
                {
                    TokenResponseModel trm = new TokenResponseModel();

                    var content = await response.Content.ReadAsStringAsync();
                    JObject jwtData = JsonConvert.DeserializeObject<dynamic>(content);
                    trm.AccessToken = jwtData.Value<string>("access_token");
                    trm.TokenType = jwtData.Value<string>("token_type");
                    trm.ExpiresIn = jwtData.Value<int>("expires_in");
                    trm.UserName = jwtData.Value<string>("userName");
                    trm.IssuedAt = jwtData.Value<string>(".issued");
                    trm.ExpiresAt = jwtData.Value<string>(".expires");

                    AppDataService.SaveTokenResponseModel(trm);
                    AppDataService.setSelectedWard(selectedWard);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }





        public void Logout()
        {
            // Settings.Current.UserName = string.Empty;
            NavigationService.NavigateAsync("/Login");
        }

    }
}
