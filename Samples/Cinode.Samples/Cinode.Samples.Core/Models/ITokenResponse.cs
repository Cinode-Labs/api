
namespace Cinode.Samples.Models
{
    public interface ITokenResponse
    {
        string AccessToken { get; set; }
        string RefreshToken { get; set; }
    }
}
