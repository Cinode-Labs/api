using Cinode.Samples.Abstractions;

namespace Cinode.Samples
{
    public class CustomHttpClientWrapper
    {
        public static ICustomHttpClient Instance => NestedCustomHttpClientWrapper.Instance;

        class NestedCustomHttpClientWrapper
        {
            internal static readonly ICustomHttpClient Instance  = new CustomHttpClient();
        }
    }
}
