using ChatClient.BuisnessLogic.Library;
using ChatClient.BuisnessLogic.Library.Communication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Net.Sockets;

namespace ChatClient.Gui.ConsoleApplication
{
    static class Startup
    {
        public static void Start()
        {
            // Create a new instance of Chat Controller and call its Run method
            ChatController chat = ActivatorUtilities.CreateInstance<ChatController>(GetServices());
            chat.Run();
        }

        private static IConfigurationRoot GetConfiguration()
        {
            // Create a configuration builder
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();

            // Add default configuration file
            configurationBuilder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            // Build the configuration and return it
            return configurationBuilder.Build();
        }

        private static IServiceProvider GetServices()
        {
            // Create a list of dependencies
            IServiceCollection services = new ServiceCollection();

            // Get the appsettings configuration
            IConfigurationRoot configuration = GetConfiguration();

            // Add dependencies
            services.AddSingleton<IConfiguration>(configuration);
            //services.AddSingleton<TcpClient, TcpClient>();
            services.AddScoped<ICommunicationHandler, StringCommunicationHandler>();

            // Build the service provider and return it
            return services.BuildServiceProvider();
        }
    }
}
