using FlightDeck.Application.Infrastructure.Identity;
using FlightDeck.Application.JsonModels;
using FlightDeck.Application.Responses;
using FlightDeck.Identity.Contexts;
using FlightDeck.Identity.Models;
using FlightDeck.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightDeck.Identity
{
    public static class IdentityServiceRegistration
    {
        public static void AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region EntityFramework and IdentityCore configuration          

            services.AddDbContext<FlightDeckIdentityContext>(options => options.UseSqlServer(configuration.GetConnectionString("FlightDeckAPI"),
               b => b.MigrationsAssembly(typeof(FlightDeckIdentityContext).Assembly.FullName)));
            services.AddIdentity<ApplicationUser, IdentityRole>()
               .AddEntityFrameworkStores<FlightDeckIdentityContext>().AddDefaultTokenProviders();

            //the below codes configure different options in Identity
            services.Configure<IdentityOptions>(opts =>
            {

                #region password configuration
                //    opts.Password.RequiredLength = 8;
                //    opts.Password.RequireNonAlphanumeric = true;
                //    opts.Password.RequireLowercase = false;
                //    opts.Password.RequireUppercase = true;
                //    opts.Password.RequireDigit = true;
                #endregion

                #region Userlocking configuration
                opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(365 * 5);
                opts.Lockout.MaxFailedAccessAttempts = 3;
                opts.Lockout.AllowedForNewUsers = true;
                #endregion

                #region User configuration
                
                //opts.User.AllowedUserNameCharacters =@"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._$+";
                opts.User.RequireUniqueEmail = true;

                #endregion

            });
            #endregion


            #region Configuring appsetting.json key to JsonModal class   
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            #endregion

            #region Dependency registration region for services            
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            #endregion

            #region JWT setup
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = configuration["JwtSettings:Issuer"],
                        ValidAudience = configuration["JwtSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
                    };
                    o.Events = new JwtBearerEvents()
                    {
                        OnAuthenticationFailed = c =>
                        {
                            c.NoResult();
                            c.Response.StatusCode = 500;
                            c.Response.ContentType = "text/plain";
                            return c.Response.WriteAsync(c.Exception.ToString());
                        },
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";
                            var result = JsonConvert.SerializeObject(new Response<string>("you are  not authorized", false, "Not Authorized"));
                            return context.Response.WriteAsync(result);
                        },
                        OnForbidden = context =>
                        {
                            context.Response.StatusCode = 403;
                            context.Response.ContentType = "application/json";
                            var result = JsonConvert.SerializeObject(new Response<string>("You are not authorized to access this resource", false, "Forbidden"));
                            return context.Response.WriteAsync(result);
                        },
                    };
                });
            #endregion
        }
    }
}
