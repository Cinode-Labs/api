using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cinode.Samples.Abstractions;
using Cinode.Samples.Models;
using Newtonsoft.Json;

namespace Cinode.Samples.Search.Models.Builders
{
    public class KeywordSearchModelBuilder : IModelBuilder<KeywordSearchModel>
    {
        private const string Endpoint = "/companies/1/keywords/search";
        private readonly ICustomHttpClient _httpClient;
        private readonly ITokenResponse _tokenResponse;
        private readonly IApiUriHelper _uriHelper;
        private readonly string _term;
        public KeywordSearchModelBuilder(ICustomHttpClient httpClient, IApiUriHelper uriHelper, ITokenResponse tokenResponse, string term)
        {
            _httpClient = httpClient;
            _term = term;
            _uriHelper = uriHelper;
            _tokenResponse = tokenResponse ?? throw new ArgumentNullException(nameof(tokenResponse));
        }

        public async Task<KeywordSearchModel> BuildAsync()
        {
            if (string.IsNullOrWhiteSpace(_term)) return null;

            var apiUri = _uriHelper.GetFullUri(Endpoint, true);

            var url = apiUri +"/" + _term;
            var response = await _httpClient.GetAsync(url, _tokenResponse, new CancellationToken(false));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var model = new KeywordSearchModel
                {
                    Term = _term,
                    Keywords = JsonConvert.DeserializeObject<List<KeywordModel>>(result)
                };

                return model;
            }
            return null;
        }
    }
}
