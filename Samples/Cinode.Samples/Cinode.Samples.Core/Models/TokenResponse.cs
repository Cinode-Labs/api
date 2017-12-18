using Newtonsoft.Json;

namespace Cinode.Samples.Core.Models
{
    public class TokenResponse : ITokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
