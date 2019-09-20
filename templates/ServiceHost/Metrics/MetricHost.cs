using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Prometheus.Client.MetricServer;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceHost.Metrics
{
    public class MetricServiceOptions
    {
        public MetricServiceOptions()
        {
            Host = "localhost";
            Https = false;
            Port = 8900;
            Path = "/metrics";
        }

        public string Host { get; set; }

        public int Port { get; set; }

        public string Path { get; set; }

        public bool Https { get; set; }
    }

    public class MetricHostService : IHostedService
    {
        MetricServerOptions _options = new MetricServerOptions();
        private static IMetricServer _metricServer;
        private readonly IApplicationLifetime _appLifetime;
        ILogger _logger;

        public MetricHostService(ILogger<MetricHostService> logger, IApplicationLifetime appLifetime, IOptions<MetricServiceOptions> options)
        {
            var conf = options ?? throw new ArgumentNullException(nameof(options));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _appLifetime = appLifetime ?? throw new ArgumentNullException(nameof(appLifetime));

            _options = new MetricServerOptions
            {
                Host = conf.Value.Host,
                Port = conf.Value.Port,
                UseHttps = conf.Value.Https
            };

            if (!string.IsNullOrEmpty(conf.Value.Path))
            {
                _options.MapPath = conf.Value.Path;
            }


            _metricServer = new MetricServer(Prometheus.Client.Metrics.DefaultCollectorRegistry, _options);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // appLifetime allows the start/stop to be handled for you.
            _appLifetime.ApplicationStarted.Register(OnStarted);
            _appLifetime.ApplicationStopping.Register(OnStopping);
            _appLifetime.ApplicationStopped.Register(OnStopped);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }


        private void OnStarted()
        {
            _metricServer.Start();
            _logger.LogInformation("Started metrics listener, Listening on {0}:{1}{2}", _options.Host, _options.Port, _options.MapPath);

            // Perform post-startup activities here
        }

        private void OnStopping()
        {
            _logger.LogInformation("Stopping metrics listener.");

            // Perform on-stopping activities here
            _metricServer.Stop();
        }

        private void OnStopped()
        {
            _logger.LogInformation("OnStopped has been called.");

            // Perform post-stopped activities here
        }
    }
}
