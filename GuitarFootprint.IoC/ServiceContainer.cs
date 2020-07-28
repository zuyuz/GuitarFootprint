using System;
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
        }
    }
}
