using Polly.CircuitBreaker;
using Polly.Retry;
using Polly.Timeout;
using Polly.Wrap;

namespace Facade
{
    public interface IPolyDbPolicyHandler
    {
        AsyncPolicyWrap GetPollyPolicyConfiguration();
        AsyncTimeoutPolicy GetDbTimeOutPolicy();
        AsyncCircuitBreakerPolicy GetDbCircuitBreakerPolicy();
        AsyncRetryPolicy GetDbRetryPolicy();
    }
}
