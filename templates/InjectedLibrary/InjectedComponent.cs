using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Prometheus.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace InjectedLibrary
{
    public class InjectedLibrary : IInjectedLibrary
    {
        IOptions<InjectedLibraryOptions> _options;
        ILogger _logger;
        private static Counter _counter = Metrics.CreateCounter("StuffDoneCount", "Count of times DoStuff was called.", "name");

        public InjectedLibrary(IOptions<InjectedLibraryOptions> options, ILogger<InjectedLibrary> logger)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void DoStuff()
        {
            _logger.LogInformation("{0} is doing stuff!", _options.Value.Name);
            _counter.Labels(_options.Value.Name).Inc();
        }
    }
}
