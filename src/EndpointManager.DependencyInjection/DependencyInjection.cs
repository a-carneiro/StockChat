using EndpointManager.Application;
using EndpointManager.Interfece.Application;
using EndpointManager.Interfece.Repository;
using EndpointManager.Repository;
using EndpointManager.Repository.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.IO;

namespace EndpointManager.DependencyInjection
{
    public static class DependencyInjection
    {

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
            #region DbContext
            services.AddDbContext<EndpointManagerContext>(opt => opt.UseInMemoryDatabase("EndpointManager"));
            #endregion

            #region Repository
            services.AddScoped<IEndPointRepository, EndPointRepository>();
            services.AddScoped<IModelRepository, ModelRepository>();
            services.AddScoped<IDbContext, EndpointManagerContext>();
            #endregion

            #region Application
            services.AddScoped<IEndPointApplication, EndPointApplication>();
            services.AddScoped<IModelApplication, ModelApplication>();
            #endregion
        }
    }
}
