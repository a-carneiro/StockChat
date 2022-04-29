using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StockChat.Infrastructure;

namespace StockChat.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).Build();
            var modelApplication = ActivatorUtilities.CreateInstance<ReadEvent>(host.Services);
            InitializeRabbitMqReader(modelApplication, host);
        }

        private static void InitializeRabbitMqReader(ReadEvent modelApplication, IHost host)
        {
            modelApplication.Read(host);

        }
    }
}