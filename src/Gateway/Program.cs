using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://*:5000");
                }).ConfigureAppConfiguration(builder =>
                {
                    var environmentName = Environment.GetEnvironmentVariable("ENVIRONMENT");
                    Console.WriteLine($"ENVIRONMENT {environmentName}");

                    builder.SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                        .AddJsonFile("appsettings.json", false)
                        .AddJsonFile($"appsettings.{environmentName}.json", true)
                        .AddEnvironmentVariables();
                });
        }
    }
}