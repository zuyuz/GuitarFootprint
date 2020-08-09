using System;
using GuitarFootprint.Data.Abstraction.Interfaces;
using GuitarFootprint.Data.Entities;
using GuitarFootprint.Data.PostgreSQL;
using GuitarFootprint.Data.PostgreSQL.Repositories;
using GuitarFootprint.Service.Abstraction.Dxos;
using GuitarFootprint.Service.Dxos;
using MediatR;
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

            services.AddDbContext<ApplicationContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, ApplicationRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationContext>();

            #region Repositories

            services.AddTransient<IAudioRepository, AudioRepository>();

            #endregion

            #region Unit of work

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            #endregion

            #region Dxos

            services.AddTransient<IAudioDxo, AudioDxo>();

            #endregion
        }
    }
}
