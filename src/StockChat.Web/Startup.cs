using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StockChat.Application;
using StockChat.Application.Options;
using StockChat.Domain.Entity;
using StockChat.Infrastructure;
using StockChat.Interface.Application;
using StockChat.Interface.Infrastructure;
using StockChat.Interface.Repository;
using StockChat.Interface.Service;
using StockChat.Repository;
using StockChat.Repository.Infrastructure;
using StockChat.SignalR.Hubs;
using StockChat.SignalR.Service;

namespace StockChat.Web
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config) => _config = config;
        public void ConfigureServices(IServiceCollection services)
        {
            #region Components

            services.AddMvc(opt => {
                opt.EnableEndpointRouting = false;
            });

            services.AddDbContext<StockChatDbContext>(options =>
            {
                options.UseSqlServer("Data source=localhost,1433;Database=StockChat;User Id=sa;Password=yourStrong(!)Password;");
            });

            services.AddIdentity<User, IdentityRole>(opt =>
            {
                opt.Password.RequiredLength = 6;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<StockChatDbContext>()
            .AddDefaultTokenProviders();

            services.AddSignalR();

            #endregion

            #region DI
            services.Configure<BotConfigurationOptions>(_config.GetSection("BotConfiguration"));
            services.AddTransient<IChatRepository, ChatRepository>();
            services.AddTransient<IChatApplication, ChatApplication>();
            services.AddTransient<IUserChatApplication, UserChatApplication>();
            services.AddTransient<IChatHubApplication, ChatHubApplication>();
            services.AddTransient<IMessageRepository, MessageRepository>();
            services.AddTransient<IMessageApplication, MessageApplication>();
            services.AddTransient<IUserChatRepository, UserChatRepository>();
            services.AddTransient<IChatHubService, ChatHubService>();
            services.AddTransient<IRabbitMqService, RabbitMqService>();
            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("/chatHub");
            });

            app.UseMvcWithDefaultRoute();
        }
    }
}
