using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SASMobileApp1.Models
{
   public class TokenResponseModel
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("issued")]
        public string IssuedAt { get; set; }

        [JsonProperty("expiresAt")]
        public string ExpiresAt { get; set; }
    }
}
