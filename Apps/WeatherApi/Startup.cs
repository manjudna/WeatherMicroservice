using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenWeatherMapApi.Services;
using Microsoft.OpenApi.Models;
using OpenWeatherMapApi.Extensions;
using NLog;
using System.IO;
using LoggerService;


namespace OpenWeatherMapApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCors();
            services.AddMemoryCache();
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Audience = "WeatherApi";
                    // options.Authority = "https://localhost:5001";
                    options.Authority = Configuration.GetSection("AppSettings:LocalAuthorityUrl").Value;
                });
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddSingleton<ILoggerManager, LoggerManager>();

            services.AddTransient<IWeatherService, WeatherService>();
            services.AddTransient<IWeatherRepository, WeatherRepository>();
            services.AddTransient<IWeatherValidation, WeatherValidation>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WeatherApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerManager logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseCors(
            //    options => options.WithOrigins(Configuration.GetSection("AppSettings:CrossOriginURL").Value).AllowAnyMethod());

            app.UseCors(config => config
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                        );
            app.UseSwagger();

            app.ConfigureExceptionHandler(logger);

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
