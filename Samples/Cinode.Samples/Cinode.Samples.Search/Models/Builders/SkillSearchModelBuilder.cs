
using System;
using System.Linq;
using System.Threading.Tasks;
using Cinode.Samples.Core.Abstractions;
using Cinode.Samples.Core.Models;
using Newtonsoft.Json;

namespace Cinode.Samples.Search.Models.Builders
{
    public class SkillSearchModelBuilder : IModelBuilder<SkillSearchModel>
    {
        private const string Endpoint = "/companies/1/skills/search/term";
        private readonly ICustomHttpClient _httpClient;
        private readonly ITokenResponse _tokenResponse;
        private readonly IApiUriHelper _uriHelper;
        private readonly string _term;
        public SkillSearchModelBuilder(ICustomHttpClient httpClient, IApiUriHelper uriHelper, ITokenResponse tokenResponse, string term)
        {
            _httpClient = httpClient;
            _tokenResponse = tokenResponse ?? throw new ArgumentNullException(nameof(tokenResponse));
            _uriHelper = uriHelper;
            _term = term;
        }

        public async Task<SkillSearchModel> BuildAsync()
        {
            if (string.IsNullOrWhiteSpace(_term)) return null;
            var content = new {term = _term};
            var url = _uriHelper.GetFullUri(Endpoint, true);
            var response = await _httpClient.PostAsync(url.ToString(), _tokenResponse, content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var serachSkillResultModel = JsonConvert.DeserializeObject<SearchSkillResultModel>(result);
                var model = new SkillSearchModel()
                {
                    Term = _term,
                    CompanyUsers =serachSkillResultModel.Hits?.ToList()
                };

                return model;
            }

            return null;
        }
    }
}
