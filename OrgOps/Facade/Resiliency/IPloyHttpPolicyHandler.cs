using Polly;
using System.Net.Http;

namespace Facade
{
    public interface IPloyHttpPolicyHandler
    {
        IAsyncPolicy<HttpResponseMessage> GetRetryPolicy();
        IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy();
    }
}
