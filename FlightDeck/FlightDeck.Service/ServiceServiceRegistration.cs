using FlightDeck.Application.Infrastructure.Service.Email;
using FlightDeck.Application.Infrastructure.Service.Helper;
using FlightDeck.Application.Infrastructure.Service.RandomToken;
using FlightDeck.Application.JsonModels;
using FlightDeck.Service.Email;
using FlightDeck.Service.Helper;
using FlightDeck.Service.RandomToken;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightDeck.Service
{
    public static class ServiceServiceRegistration
    {
        public static void AddServiceServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region Configuring appsetting.json key to JsonModal class   
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));            
            #endregion

            #region Dependency registration region for services            
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<ITokenGenerator, TokenGenerator>();
            services.AddTransient<IStringHelper, StringHelper>();
            #endregion
        }
    }
}
