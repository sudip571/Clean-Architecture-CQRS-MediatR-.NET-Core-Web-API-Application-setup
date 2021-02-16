using AutoMapper;
using FlightDeck.Application.LogUserLogins;
using FlightDeck.Application.PipelineBehaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FlightDeck.Application
{
    /// <summary>
    /// ServiceCollection Extension class where we register different things/service and
    /// later we register this Extension class in Startup.cs class from API project.
    /// </summary>
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            #region Dependency registration region for services  
            
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient<ILogUserLoginService, LogUserLoginService>();

            #endregion

            return services;
        }
    }
}
