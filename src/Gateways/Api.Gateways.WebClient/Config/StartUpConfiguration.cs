using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Api.Gateways.WebClient.Config
{
    public static class StartUpConfiguration
    {
        public static IServiceCollection AddAppSettingBinding(this ServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApiUrls>(opts => configuration.GetSection("ApiUrls").Bind(opts));
            return services;
        }

        public static IServiceCollection AddProxiesRegistration(this ServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();

            services.AddHttpClient<ICatalogProxy, CatalogProxy>();
            services.AddHttpClient<IOrderProxy, OrderProxy>();
            services.AddHttpClient<ICustomerProxy, CustomerProxy>();

            return services;
        }
    }
}
