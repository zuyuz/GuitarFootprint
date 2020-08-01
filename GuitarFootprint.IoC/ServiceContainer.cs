﻿using System;
using GuitarFootprint.Data.Abstraction.Interfaces;
using GuitarFootprint.Data.PostgreSQL;
using GuitarFootprint.Data.PostgreSQL.Repositories;
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
            var assembly = AppDomain.CurrentDomain.Load("TransactionData.Service");
            services.AddMediatR(assembly);

            services.AddDbContext<ApplicationContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            #region Repositories

            services.AddTransient<IAudioRepository, AudioRepository>();

            #endregion

            #region Unit of work

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            #endregion
        }
    }
}