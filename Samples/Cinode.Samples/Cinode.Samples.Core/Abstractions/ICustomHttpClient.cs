
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Cinode.Samples.Core.Models;

namespace Cinode.Samples.Core.Abstractions
{
    public interface ICustomHttpClient
    {
        Task<ITokenResponse> GetAccessTokenAsync(string endpoint, string accessId, string accessSecret);
        Task<ITokenResponse> ResfreshTokenAsync(string endpoint, string refreshToken);
        Task<HttpResponseMessage> PostAsync(string url, object content);
        Task<HttpResponseMessage> PostAsync(string url, ITokenResponse tokenResponse, object content);

        Task<HttpResponseMessage> GetAsync(string url, ITokenResponse tokenResponse, CancellationToken cancellationToken);
    }
}
