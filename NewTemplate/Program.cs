using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Runtime.InteropServices;
using System.Reflection;
using Microsoft.Extensions.CommandLineUtils;
using NewTemplate.Configuration;
using NewTemplate.Commands;

namespace NewTemplate
{
    class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        private static ILogger<Program> logger;

        static void Main(string[] args)
        {
            Console.WriteLine("### NewTemplate ###");
            Console.WriteLine("Application started");
            Console.WriteLine("Building IoC container...");

            Startup startup = new Startup();
            startup.Initialize();
            ServiceProvider = startup.ServiceProvider;

            Console.WriteLine("IoC container set up successfully.");

            ILoggerFactory loggerFactory = ServiceProvider.GetRequiredService<ILoggerFactory>();

            logger = loggerFactory.CreateLogger<Program>();

            logger.LogDebug("Logger set up successfully.");

            logger.LogDebug("Reading command line arguments...");

            ICommandsProvider commandsProvider = ServiceProvider.GetRequiredService<ICommandsProvider>();
            commandsProvider.Execute(args);
        }
    }
}
