using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Formatting.Compact;

using System.ServiceProcess;
using ServiceHost.Metrics;
using System.Linq;

namespace ServiceHost
{
    class Program
    {
        private const string ServiceName = "ServiceHost";

        public static LoggingLevelSwitch levelSwitch = new LoggingLevelSwitch();

        private static IHostBuilder _hostBuilder;
        private static IHost _host;


        public static async Task Main(string[] args)
        {

            if (args.Contains("--debug"))
            {

                levelSwitch.MinimumLevel = Serilog.Events.LogEventLevel.Verbose;
            }

            Init(args);
            if (args.Contains("--service"))
            {

                using (var service = new HostService(ServiceName))
                {
                    ServiceBase.Run(service);
                }
            }
            else
            {
                await Start(args);
            }

        }

        internal static void Init(string[] args)
        {

            var logfile = string.Format("{0}.log", ServiceName);

            var logFilepath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                "Logs",
               logfile
                );



            var StartUpLog = new LoggerConfiguration()
             .MinimumLevel.ControlledBy(levelSwitch)
             .Enrich.FromLogContext()
             .WriteTo.Console().WriteTo.File(new RenderedCompactJsonFormatter(), logFilepath, rollingInterval: RollingInterval.Day)
             .CreateLogger();

            Log.Logger = StartUpLog;

            StartUpLog.Information("Starting {0}.", ServiceName);
            StartUpLog.Verbose("Current Directory = {0}", Directory.GetCurrentDirectory());
            StartUpLog.Information("Logging to: {logfile}", logFilepath);

            _hostBuilder = new HostBuilder()
               .ConfigureHostConfiguration(configHost =>
               {
                   //configHost.SetBasePath(Directory.GetCurrentDirectory()); - docs are wrong, this is asp only

                   configHost.AddJsonFile("hostsettings.json", optional: true);
                   configHost.AddEnvironmentVariables(prefix: "PREFIX_");
                   configHost.AddCommandLine(args);
               })
               .ConfigureAppConfiguration((hostContext, configApp) =>
               {
                   configApp.AddJsonFile("appsettings.json", optional: true);
                   configApp.AddJsonFile(
                       $"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json",
                       optional: true);
                   configApp.AddEnvironmentVariables(prefix: "PREFIX_");
                   configApp.AddCommandLine(args);
               })
               .ConfigureServices((hostContext, services) =>
               {
                   //services.AddHostedService<LifetimeEventsHostedService>();
                   //services.AddHostedService<TimedHostedService>();
                   services.UsePrometheusMetricServer(hostContext.Configuration.GetSection("metrics"));
                   services.AddLogging();
                   services.AddOptions();
               })
               .UseSerilog(StartUpLog);


            if (args.Contains("--service"))
            {
                Log.Information("Running as a service.");
                _host = _hostBuilder.Build();
            }
            else
            {
                Log.Information("Running as an application.");
                _host = _hostBuilder
                      .UseConsoleLifetime()
                      .Build();
            }
        }

        internal static async Task Start(string[] args)
        {
            if (args.Contains("--service"))
            {
                Log.Information("Running as a service.");
                await _host.StartAsync();
            }
            else
            {
                await _host.RunAsync();
            }
        }
        internal static void Stop()
        {
            _host.StopAsync();
        }

    }

    internal class HostService : ServiceBase
    {
        public HostService(string serviceName)
        {
            ServiceName = serviceName;
        }


        protected override void OnStart(string[] args)
        {
            Program.Init(args);
            Program.Start(args).Wait();
        }

        protected override void OnStop()
        {
            Program.Stop();
            Log.Information("Stopping...");
        }
    }


}
