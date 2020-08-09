using System;
using GuitarFootprint.Data.Abstraction.Interfaces;
using GuitarFootprint.Data.Entities;
using GuitarFootprint.Data.PostgreSQL;
using GuitarFootprint.Data.PostgreSQL.Repositories;
using GuitarFootprint.Service.Abstraction.Dxos;
using GuitarFootprint.Service.Abstraction.Manager;
using GuitarFootprint.Service.Abstraction.Services;
using GuitarFootprint.Service.Dxos;
using GuitarFootprint.Service.Managers;
using GuitarFootprint.Service.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GuitarFootprint.IoC
{
    public static class ServiceContainer
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = AppDomain.CurrentDomain.Load("GuitarFootprint.Service");
            services.AddMediatR(assembly);

            #region DbContext

            services.AddDbContext<ApplicationContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, ApplicationRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationContext>();
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 1;
            });

            #endregion

            #region Repositories

            services.AddTransient<IAudioRepository, AudioRepository>();

            #endregion

            #region Unit of work

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            #endregion

            #region Dxos

            services.AddTransient<IAudioDxo, AudioDxo>();
            services.AddTransient<IApplicationUserDxo, ApplicationUserDxo>();
            services.AddTransient<ITokenDxo, TokenDxo>();

            #endregion

            #region Services

            services.AddTransient<IJwtService, JwtService>();
            services.AddTransient<IHubNotificationService, HubNotificationService>();

            #endregion

            #region Managers

            services.AddTransient<IConnectionManager, HubConnectionManager>();

            #endregion
        }
    }
}
