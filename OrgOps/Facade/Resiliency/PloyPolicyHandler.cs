using Businessmodel.Common;
using BusinessModel.Common;
using Microsoft.Extensions.Configuration;
using Polly;
using Polly.Bulkhead;
using Polly.CircuitBreaker;
using Polly.Contrib.WaitAndRetry;
using Polly.Extensions.Http;
using Polly.Fallback;
using Polly.Retry;
using Polly.Timeout;
using Polly.Wrap;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

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

        /// <summary>
        /// Configuration
        /// </summary>
        private readonly IConfiguration _configuration;

        public PloyPolicyHandler(IApiRequestHandler reqHandler, IConfiguration configuration)
        {
            _reqHandler = reqHandler;
            _configuration = configuration;
            AsyncRetryPolicy retry = GetDbRetryPolicy();

            AsyncCircuitBreakerPolicy breaker = GetDbCircuitBreakerPolicy();

            AsyncTimeoutPolicy timeout = GetDbTimeOutPolicy();

            AsyncBulkheadPolicy bulkhead = GetDbBulkheadPolicy();

            AsyncFallbackPolicy fallback = GetDbFallbackPolicy();

            //https://github.com/App-vNext/Polly/wiki/cache
            //https://github.com/App-vNext/Polly/wiki/PolicyWrap/
            var enablementConfig = configuration.GetSection("ResiliencyConfigs").Get<ResiliencyConfigs>();

            //if (enablementConfig.ResiliencyEnabled == 1)
            //{
                //if ((enablementConfig.RetryEnabled == 1) 
                //    && (enablementConfig.CircuitBreakerEnabled == 1))
                //{
                _policyWrapDb = Policy.WrapAsync(retry, timeout);
                //}
                //if (enablementConfig.BulkHeadEnabled == 1)
                //{
                //    _policyWrapDb.WrapAsync(bulkhead);
                //}
                //if (enablementConfig.FallBackEnabled == 1)
                //{
                //    _policyWrapDb.WrapAsync(fallback);
                //}
                //if (enablementConfig.TimeOutEnabled == 1)
                //{
                //    _policyWrapDb.WrapAsync(timeout);
                //}
            //}
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
            return Policy.TimeoutAsync(300, TimeoutStrategy.Pessimistic);
        }


        public AsyncBulkheadPolicy GetDbBulkheadPolicy()
        {
            return Policy.BulkheadAsync(
               // Restrict executions through the policy to a maximum of twelve concurrent actions
               maxParallelization: 3,
               // with up to two actions waiting for an execution slot in the bulkhead if all slots are taken
               maxQueuingActions: 2,
              // Restrict concurrent executions, calling an action if an execution is rejected
              context => Task.Run(() => _reqHandler.LogInfo("BaseFacade", "BaseFacade", "Reliency Error: BulkHead Rejected")));
        }

        public AsyncFallbackPolicy GetDbFallbackPolicy()
        {
            return Policy.Handle<Exception>(ex => ex.GetType().Name.Contains("EntityException"))
                .FallbackAsync(fallbackAction: (cancleTkn, task) =>
                Task.Run(() => _reqHandler.RaiseBusinessException("BaseFacade", "BaseFacade", "Reliency Error: fallback activated"))
                , onFallbackAsync: (result, context) =>
                Task.Run(() => _reqHandler.RaiseBusinessException
                ("BaseFacade", "BaseFacade"
                , $"Fallback of Fallback {context.PolicyKey} at {context.OperationKey}: fallback value substituted, due to: {result.Message}.")));
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
