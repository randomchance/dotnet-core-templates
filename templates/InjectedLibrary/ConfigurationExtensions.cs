using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace InjectedLibrary
{

    public static class ConfigurationExtensions
    {
        public static IServiceCollection UseInjectedLibrary(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IInjectedLibrary, InjectedLibrary>();
            services.Configure<InjectedLibraryOptions>(configuration);
            return services;
        }

        public static IServiceCollection UseExampleComponent(this IServiceCollection services, string name)
        {
            services.AddSingleton<IInjectedLibrary, InjectedLibrary>();
            services.Configure<InjectedLibraryOptions>(options => { options.Name = name; });
            return services;
        }

        public static IServiceCollection UseExampleComponent(this IServiceCollection services)
        {
            services.AddSingleton<IInjectedLibrary, InjectedLibrary>();
            return services;
        }
    }
}
