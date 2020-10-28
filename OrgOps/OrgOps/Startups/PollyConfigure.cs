using Businessmodel.Common;
using Facade;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace OrgOps
{
    public static class PollyConfigure
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            var prov = services.BuildServiceProvider();
            var apiRequestHandlerProvider = prov.GetService<IApiRequestHandler>();
            var polyHttpPolicyHandlerProvider = prov.GetService<IPloyHttpPolicyHandler>();

            services.AddSingleton<IPolyDbPolicyHandler>(new PloyPolicyHandler(apiRequestHandlerProvider));

            services.AddHttpClient("appHttpClient")
               .SetHandlerLifetime(TimeSpan.FromMinutes(5))  //Set lifetime to five minutes
               .AddPolicyHandler(polyHttpPolicyHandlerProvider.GetRetryPolicy()) //Retry
               .AddPolicyHandler(polyHttpPolicyHandlerProvider.GetCircuitBreakerPolicy()) //Circuit Breaker
               ;
        }
    }
}
