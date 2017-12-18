using System;
using Cinode.Samples.Abstractions;
using Cinode.Samples.Options;
using Microsoft.Extensions.Options;

namespace Cinode.Samples
{
    public class ApiUriHelper : IApiUriHelper
    {
        private readonly ApiOptions _apiOptions;

        public ApiUriHelper(IOptions<ApiOptions> apiOptions)
        {
            _apiOptions = apiOptions.Value ?? throw new ArgumentNullException(nameof(apiOptions));
        }

        public Uri GetFullUri(string endpoint, bool includeVersion)
        {
            var baseUrl = AppendSlash(_apiOptions.BaseUrl);
            return GetFullUri(new Uri(baseUrl), endpoint, includeVersion);
        }

        public Uri GetFullUri(Uri baseUri, string endpoint, bool includeVersion)
        {
            if (includeVersion)
            {
                if (endpoint.StartsWith("/"))
                {
                    endpoint = endpoint.Substring(1);
                }
                endpoint = $"v{_apiOptions.Version}/{endpoint}";
            }
            return new Uri(baseUri, endpoint);
        }

        public Uri GetFullUri(string baseUrl, string endpoint, bool includeVersion)
        {
            baseUrl = AppendSlash(baseUrl);
            return GetFullUri(new Uri(baseUrl), endpoint, includeVersion);
        }

        private string AppendSlash(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return string.Empty;

            if (url.EndsWith("/")) return url;

            return url + "/";
        }
    }
}
