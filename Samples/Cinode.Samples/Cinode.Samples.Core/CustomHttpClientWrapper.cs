using Cinode.Samples.Core.Abstractions;

namespace Cinode.Samples.Core
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
