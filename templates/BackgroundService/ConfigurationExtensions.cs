using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackgroundService
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection UseBackgroundService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHostedService<BackgroundService>();
            services.Configure<BackgroundServiceOptions>(configuration);
            return services;
        }

        public static IServiceCollection UseBackgroundService(this IServiceCollection services, string name)
        {
            services.AddHostedService<BackgroundService>();
            services.Configure<BackgroundServiceOptions>(options => { options.Name = name; });
            return services;
        }
    }
}
