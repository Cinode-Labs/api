
namespace Cinode.Samples.Core.Models
{
    public interface ITokenResponse
    {
        string AccessToken { get; set; }
        string RefreshToken { get; set; }
    }
}
