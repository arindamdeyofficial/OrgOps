using Businessmodel.Common;
using Polly;
using Polly.CircuitBreaker;
using Polly.Contrib.WaitAndRetry;
using Polly.Extensions.Http;
using Polly.Retry;
using Polly.Timeout;
using Polly.Wrap;
using System;
using System.Net;
using System.Net.Http;

namespace Facade
{
    public class PloyPolicyHandler : IPolyDbPolicyHandler, IPloyHttpPolicyHandler
    {
        /// <summary>
        /// For log and exception handling
        /// </summary>
        private readonly IApiRequestHandler _reqHandler;

        /// <summary>
        /// AsyncPolicyWrap
        /// </summary>
        private readonly AsyncPolicyWrap _policyWrapDb;

        public PloyPolicyHandler(IApiRequestHandler reqHandler)
        {
            _reqHandler = reqHandler;
            AsyncRetryPolicy retry = GetDbRetryPolicy();
            AsyncCircuitBreakerPolicy breaker = GetDbCircuitBreakerPolicy();
            AsyncTimeoutPolicy timeout = GetDbTimeOutPolicy();
            IAsyncPolicy[] policies = new AsyncPolicy[] { retry, breaker, timeout };
            _policyWrapDb = Policy.WrapAsync(policies);
        }

        public AsyncPolicyWrap GetPollyPolicyConfiguration()
        {
            return _policyWrapDb;
        }

        public AsyncRetryPolicy GetDbRetryPolicy()
        {
            return Policy.Handle<Exception>(ex => ex.GetType().Name.Contains("EntityException"))
                       .RetryAsync(2);
        }

        public AsyncCircuitBreakerPolicy GetDbCircuitBreakerPolicy()
        {
            return Policy.Handle<Exception>(ex => ex.GetType().Name.Contains("EntityException"))
                                            .AdvancedCircuitBreakerAsync(
                                                 failureThreshold: 0.5,
                                                samplingDuration: TimeSpan.FromSeconds(10),
                                                minimumThroughput: 2,
                                                durationOfBreak: TimeSpan.FromSeconds(30)
                                                 );
        }

        public AsyncTimeoutPolicy GetDbTimeOutPolicy()
        {
            return Policy.TimeoutAsync(30, TimeoutStrategy.Optimistic);
        }

        public IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            //Wait and Retry with Exponential Back-off
            //https://github.com/Polly-Contrib/Polly.Contrib.WaitAndRetry#wait-and-retry-with-jittered-back-off
            var delay = Backoff.ExponentialBackoff(TimeSpan.FromMilliseconds(100), retryCount: 5);
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
                .WaitAndRetryAsync(delay);
        }

        public IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
        }
    }
}
