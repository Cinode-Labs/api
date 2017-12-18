using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Cinode.Samples.Core.Abstractions;
using Cinode.Samples.Core.Models;
using Newtonsoft.Json;

namespace Cinode.Samples.Core
{
    public class CustomHttpClient : HttpClient, ICustomHttpClient
    {
        public async Task<ITokenResponse> GetAccessTokenAsync(string endpoint, string accessId, string accessSecret)
        {
            if (string.IsNullOrWhiteSpace(endpoint)) throw new ArgumentNullException(nameof(endpoint));
            if (string.IsNullOrWhiteSpace(accessId)) throw new ArgumentNullException(nameof(accessId));
            if (string.IsNullOrWhiteSpace(accessSecret)) throw new ArgumentNullException(nameof(accessSecret));
            try
            {
                var basicParameter = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{accessId}:{accessSecret}"));

                DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", basicParameter);

                var basicResponse = await GetAsync(endpoint);

                if (basicResponse.IsSuccessStatusCode)
                {
                    var basicResult = await basicResponse.Content.ReadAsStringAsync();

                    var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(basicResult);
                    return tokenResponse;
                }
            }
            catch (Exception)
            {
                // ignored
            }
            return null;
        }

        public async Task<ITokenResponse> ResfreshTokenAsync(string endpoint, string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(endpoint)) throw new ArgumentNullException(nameof(endpoint));
            if (string.IsNullOrWhiteSpace(refreshToken)) throw new ArgumentNullException(nameof(refreshToken));

            try
            {
                var response = await PostAsync(endpoint, new {refreshToken });
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(result);
                    return tokenResponse;
                }
            }
            catch (Exception)
            {
                //Ignore
            }
            return null;
        }

        public async Task<HttpResponseMessage> PostAsync(string url, object requestContent)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url))
            {
                Content = new StringContent(JsonConvert.SerializeObject(requestContent),
                    Encoding.UTF8,
                    //CONTENT-TYPE header;
                    "application/json")
            };

            return await SendAsync(request);
        }

        public Task<HttpResponseMessage> PostAsync(string url, ITokenResponse tokenResponse, object content)
        {
            if(string.IsNullOrWhiteSpace(url)) throw new ArgumentNullException(nameof(url));
            if(tokenResponse == null) throw new ArgumentNullException(nameof(tokenResponse));
            
            DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);
            DefaultRequestHeaders.Accept.Clear();
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return PostAsync(url, content);
        }

        public Task<HttpResponseMessage> GetAsync(string url, ITokenResponse tokenResponse, CancellationToken cancellationToken)
        {
            if(string.IsNullOrWhiteSpace(url)) throw new ArgumentNullException(nameof(url));
            if(tokenResponse == null) throw new ArgumentNullException(nameof(tokenResponse));
            DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);
            DefaultRequestHeaders.Accept.Clear();
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            return GetAsync(url, cancellationToken);
        }
    }
}
