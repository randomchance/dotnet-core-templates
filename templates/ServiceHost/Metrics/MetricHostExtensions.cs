using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prometheus.Client.MetricServer;
using System;

namespace ServiceHost.Metrics
{
    public static class MetricHostExtensions
    {
        public static IServiceCollection UsePrometheusMetricServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHostedService<MetricHostService>();
            services.Configure<MetricServiceOptions>(configuration);

            return services;
        }

        public static IServiceCollection HostMetrics(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHostedService<MetricHostService>();
            services.Configure<MetricServiceOptions>(configuration);

            return services;
        }

        public static IServiceCollection UsePrometheusMetricServer(this IServiceCollection services, string host, int port)
        {
            services.AddHostedService<MetricHostService>();
            services.Configure<MetricServiceOptions>(options =>
            {
                options.Port = port;
                options.Host = host;
            });

            return services;
        }

    }
}
