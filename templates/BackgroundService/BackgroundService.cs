using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundService
{
    public class BackgroundService : IHostedService
    {

        private BackgroundServiceOptions _options;
        private ILogger<BackgroundService> _logger;

        public BackgroundService(IOptions<BackgroundServiceOptions> options, ILogger<BackgroundService> logger)
        {
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private void DoStuff()
        {
            using (_logger.BeginScope("Service = {0}", _options.Name))
            {
                _logger.LogInformation("Starting stuff!");


                _logger.LogWarning("Out of STUFF to do!");
            }
        }




        public Task StartAsync(CancellationToken cancellationToken)
        {
            DoStuff();
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }







    }
}
