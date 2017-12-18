using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Cinode.Samples.Abstractions;
using Cinode.Samples.Models;
using Cinode.Samples.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Cinode.Samples.Controllers
{
    public class ControllerBase : Controller
    {
        private const string TokenResponseCacheKey = nameof(TokenResponseCacheKey);
        private readonly TokenOptions _tokenOptions;
        private readonly IApiUriHelper _uriHelper;
        private readonly IMemoryCache _memoryCache;
        protected ControllerBase(IOptions<TokenOptions> tokenOptionsAccessor, IApiUriHelper uriHelper, IMemoryCache memoryCache)
        {
            _uriHelper = uriHelper;
            _memoryCache = memoryCache;
            _tokenOptions = tokenOptionsAccessor?.Value ?? throw new ArgumentNullException(nameof(tokenOptionsAccessor));
        }

        protected async Task<ITokenResponse> GetAccessToken(bool tryRefreshIfExpired)
        {
            if (_memoryCache.TryGetValue(TokenResponseCacheKey, out ITokenResponse tokenResponse) && TryGetJwtSecurityToken(tokenResponse, out var jwtSecurityToken))
            {
                if (CheckIfJwtSecurityTokenIsValid(jwtSecurityToken))
                {
                    return tokenResponse;
                }

                if (tryRefreshIfExpired)
                {
                    var refreshTokenUrl = _uriHelper.GetFullUri(_tokenOptions.BaseUrl, _tokenOptions.TokenRefreshEndpoint).ToString();
                    tokenResponse =
                        await CustomHttpClientWrapper.Instance.ResfreshTokenAsync(refreshTokenUrl,
                            tokenResponse.RefreshToken);
                }
            }

            //We didnt succeed to refresh
            if (tokenResponse == null)
            {
                var tokenUrl = _uriHelper.GetFullUri(_tokenOptions.BaseUrl, _tokenOptions.TokenEndpoint).ToString();
                tokenResponse = await CustomHttpClientWrapper.Instance.GetAccessTokenAsync(tokenUrl,
                    _tokenOptions.AccessId, _tokenOptions.AccessSecret);
            }

            if (tokenResponse == null) return null;

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                // Keep in cache for this time, reset time if accessed.
                .SetSlidingExpiration(TimeSpan.FromMinutes(30));

            // Save data in cache.
            _memoryCache.Set(TokenResponseCacheKey, tokenResponse, cacheEntryOptions);
            
            return tokenResponse;
        }

        private bool CheckIfJwtSecurityTokenIsValid(JwtSecurityToken jwtSecurityToken)
        {
            if (jwtSecurityToken == null) return false;

            var valid = jwtSecurityToken.ValidTo > DateTime.UtcNow;
            return valid;
        }

        private bool TryGetJwtSecurityToken(ITokenResponse tokenResponse, out JwtSecurityToken jwtSecurityToken)
        {
            jwtSecurityToken = null;
            if (tokenResponse == null) return false;

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            jwtSecurityToken = jwtSecurityTokenHandler.ReadJwtToken(tokenResponse.AccessToken);

            return jwtSecurityToken != null;
        }
    }
}
