using Microsoft.Extensions.Logging;
using NewTemplate.Configuration;

namespace NewTemplate.Commands
{
    public class HelloWorldCommand : ICommand
    {
        private ILogger<HelloWorldCommand> Logger { get; }
        private IConfig Configuration { get; }

        public HelloWorldCommand(ILogger<HelloWorldCommand> logger, IConfig configuration)
        {
            Logger = logger;
            Configuration = configuration;
        }

        public void Run()
        {
            string text = "world";
            if (!string.IsNullOrEmpty(Configuration.HelloWorldText))
                text = Configuration.HelloWorldText;
            Logger.LogInformation($"Hello {text}!");
        }
    }
}