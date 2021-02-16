using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Text;
using FlightDeck.DataAccess.Repositories;
using FlightDeck.Application.Infrastructure.Repository;
using FlightDeck.Application.JsonModels;
using FlightDeck.DataAccess.Repositories.References;
using Microsoft.Extensions.Configuration;
using FlightDeck.DataAccess.Repositories.LogUserLogins;

namespace FlightDeck.DataAccess
{
   public static class DataAccessServiceRegistration
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
        {

            #region Configuring appsetting.json key to JsonModal class             
            services.Configure<ConnectionStrings>(configuration.GetSection("ConnectionStrings"));
            #endregion

            #region Dependency registration region for repositories
            services.AddScoped<IAsyncReferenceRepository, ReferenceRepository>();
            services.AddScoped<IAsyncLogUserLoginRepository, LogUserLoginRepository>();

            #endregion

            return services;
        }
    }
}
