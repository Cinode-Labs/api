
using System;

namespace Cinode.Samples.Core.Abstractions
{
    public interface IApiUriHelper
    {
        Uri GetFullUri(string endpoint, bool includeVersion=false);
        Uri GetFullUri(Uri baseUri, string endpoint, bool includeVersion=false);
        Uri GetFullUri(string baseUrl, string endpoint, bool includeVersion=false);
    }
}
