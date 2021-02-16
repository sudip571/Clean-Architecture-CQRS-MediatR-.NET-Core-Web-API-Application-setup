using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightDeck.Api.Extensions;
using FlightDeck.Api.Services;
using FlightDeck.Application;
using FlightDeck.Application.Api;
using FlightDeck.DataAccess;
using FlightDeck.Identity;
using FlightDeck.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FlightDeck.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerExtension();
            services.AddApiVersioningExtension();
            services.AddApplicationServices();
            services.AddDataAccessServices(Configuration);
            services.AddIdentityServices(Configuration);
            services.AddServiceServices(Configuration);


            #region Dependency registration region for services within this Api project           
            services.AddScoped<ILoggedInUserService, LoggedInUserService>();
            #endregion

            services.AddCors(options =>
            {
                options.AddPolicy("FlightDeckOpen", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwaggerExtension();
            app.UseExceptionHandlerMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
