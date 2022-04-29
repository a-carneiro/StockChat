using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using StockChatBot.Interface;
using StockChatBot.Service;
using System;
using System.IO;

namespace StockChatBot
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.BuildConfig();

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.ConfigureServices();
                })
                //.UseSerilog()
                .Build();

            Log.Information("Application Starting");
            var modelApplication = ActivatorUtilities.CreateInstance<RabbitMqReader>(host.Services);
            InitializeRabbitMqReader(modelApplication);
        }
        public static void BuildConfig(this IConfigurationBuilder builder)
        {
            builder.ConfigureOptions();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();
        }
        private static void ConfigureOptions(this IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "prod"}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

        }
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IProcessMessageService, ProcessMessageService>();
            services.AddScoped<ISendMessageService, SendMessageService>();
            services.AddScoped<IRabbitMqService, RabbitMqService>();
        }

        private static void InitializeRabbitMqReader(RabbitMqReader modelApplication)
        {
            modelApplication.Read();
        }
    }
}
