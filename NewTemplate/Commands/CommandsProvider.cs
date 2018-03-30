using System;
using System.IO;
using NewTemplate.Configuration;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace NewTemplate.Commands
{
    public class CommandsProvider : ICommandsProvider
    {
        private const string HELP_ARGS = "-h | --help";
        private const string HELP = "help";
        private readonly ILogger<CommandsProvider> logger;
        private IConfig Configuration { get; set; }
        private IServiceProvider ServiceProvider { get; set; }

        public CommandsProvider(ILogger<CommandsProvider> logger, IConfig configuration,
            IServiceProvider serviceProvider)
        {
            this.logger = logger;
            Configuration = configuration;
            ServiceProvider = serviceProvider;
        }

        /// <summary>
        /// https://blog.terribledev.io/Parsing-cli-arguments-in-dotnet-core-Console-App/
        /// https://gist.github.com/iamarcel/8047384bfbe9941e52817cf14a79dc34
        /// </summary>
        /// <param name="args">Console arguments.</param>
        public void Execute(string[] args)
        {
            try
            {
                CommandLineApplication app = new CommandLineApplication();

                Version(app);
                HelloWorld(app);
                
                app.HelpOption(HELP_ARGS);
                app.Execute(args);
            }
            catch (Exception e)
            {
                string logMessage = $"An unknown problem occured while executing command {args.ToString()}. Exception: ";
                logger.LogError(logMessage + Environment.NewLine + e);
                throw new Exception(logMessage, e);
            }
        }

        public void HelloWorld(CommandLineApplication app)
        {
            const string DESCRIPTION = "Hello world command.";

            CommandLineApplication pkoCsv = app.Command("helloworld", config =>
            {
                config.HelpOption(HELP_ARGS);
                config.Description = DESCRIPTION;

                CommandOption source =
                    config.Option("-t | --text <display_text>", "Text will be displayed in console.", CommandOptionType.SingleValue);

                config.OnExecute(() =>
                {
                    // https://stackoverflow.com/questions/39174989/how-to-register-multiple-implementations-of-the-same-interface-in-asp-net-core
                    ICommand helloWorldCommand = 
                        ServiceProvider.GetServices<ICommand>().First(
                            x => x.GetType() == typeof(HelloWorldCommand));
                    helloWorldCommand.Run();

                    return 0;
                });
            });
            pkoCsv.Command(HELP, config =>
            {
                config.Description = "Help about 'helloworld' command.";
                config.OnExecute(() =>
                {
                    pkoCsv.ShowHelp(DESCRIPTION);
                    return 1;
                });
            });
        }

        private void Version(CommandLineApplication app)
        {
            const string DESCRIPTION = "Print NewTemplate version.";

            CommandLineApplication version = app.Command("version", config =>
            {
                config.Description = DESCRIPTION;
                config.HelpOption(HELP_ARGS);

                config.OnExecute(() =>
                    {
                        logger.LogInformation($"NewTemplate version: {System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString()})");

                        return 0;
                    });
            });
            version.Command(HELP, config =>
            {
                config.Description = "Help about 'version' command.";
                config.OnExecute(() =>
                {
                    version.ShowHelp(DESCRIPTION);
                    return 1;
                });
            });
        }
    }
}