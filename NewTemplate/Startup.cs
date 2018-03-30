using System;
using System.IO;
using NewTemplate.Commands;
using NewTemplate.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using NLog.Extensions.Logging;

namespace NewTemplate
{
    public class Startup
    {
        public IServiceProvider ServiceProvider { get; private set; }

        private IConfigurationRoot Configuration { get; }

        public Startup()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();
        }

        public void Initialize()
        {
            IServiceCollection services = new ServiceCollection();

            ConfigureServices(services);
        }
        public void ConfigureServices(IServiceCollection services)
        {
            try
            {
                // Singleton
                services.AddSingleton(Configuration);
                services.AddSingleton<IConfig, LocalConfig>();
                services.AddSingleton<ICommandsProvider, CommandsProvider>();
                services.AddSingleton<ICommand, HelloWorldCommand>();

                // Logger
                services.AddSingleton<ILoggerFactory, LoggerFactory>();
                services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
                services.AddLogging((builder) => builder.SetMinimumLevel(LogLevel.Trace));
                
                // Database
                // services.AddDbContext<BarsContext>(options =>
                //     options.EnableSensitiveDataLogging(true)
                //         .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
                //         .UseLoggerFactory(MyLoggerFactory)
                //         .UseMySql(
                //             Configuration.GetConnectionString("ProductionDb")));

                ServiceProvider = services.BuildServiceProvider();

                var loggerFactory = ServiceProvider.GetRequiredService<ILoggerFactory>();
                loggerFactory.AddNLog(new NLogProviderOptions { CaptureMessageTemplates = true, CaptureMessageProperties = true });
                loggerFactory.ConfigureNLog("nlog.config");

            }
            catch (Exception e)
            {
                string logMessage = "An error occurred while setting up IoC container.";
                throw new Exception(logMessage, e);
            }
        }

        public static readonly LoggerFactory MyLoggerFactory
            = new LoggerFactory(new[] { new ConsoleLoggerProvider((_, __) => true, true) });
    }
}