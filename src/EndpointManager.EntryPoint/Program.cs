using EndpointManager.Application;
using EndpointManager.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Threading.Tasks;

namespace EndpointManager.EntryPoint
{
    class Program
    {
        static async Task Main(string[] args)
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

            var modelApplication = ActivatorUtilities.CreateInstance<ModelApplication>(host.Services);
            await InitializeModels(modelApplication);

            await ManagerUI.Run(ActivatorUtilities.CreateInstance<EndPointApplication>(host.Services), modelApplication);
        }
        private static async Task InitializeModels(ModelApplication modelApplication)
        {
            await modelApplication.SetInitialModels();
        }
    }
}
