using System;
using System.IO;
using System.Reflection;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Response.Handlers;

namespace Response
{
    internal class Program
    {
        static IConfigurationRoot _configuration;

        public static void Main(string[] args)
        {
            Console.WriteLine($"starting {Assembly.GetCallingAssembly().GetName().Name}");
            ServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            CreateHostBuilder(args).Build().Run();
        }

        private static void ConfigureServices(ServiceCollection serviceCollection)
        {
            var environmentName = Environment.GetEnvironmentVariable("ENVIRONMENT");
            Console.WriteLine($"ENVIRONMENT {environmentName}");
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .AddJsonFile($"appsettings.{environmentName}.json", true)
                .AddEnvironmentVariables()
                .Build();

            serviceCollection.AddSingleton(_configuration);
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMediatR(typeof(GetToDoListHandler));
                    services.AddHostedService<QueryListener>();
                });
        }
    }
}