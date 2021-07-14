using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AltaMiraDatabase.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using NLog.Extensions.Logging;
using NLog.Web;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using Nest;
using AltaMiraDatabase.DataAccess.Abstract;
using AltaMiraDatabase.DataAccess.Concreate;
using AltaMiraDatabase.DataAccess.Connections;
using AltaMiraDatabase.Business.Abstract;
using AltaMiraDatabase.Business.Concreate;

namespace AltaMiraDatabase.API
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
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddLogging(builder => builder.AddNLog());
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddControllers();
            services.AddDbContext<UserDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("SqlServer")));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AltaMiraDatabase.API", Version = "v1" });
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(x => x.LoginPath = "/swagger/index.html");
            services.AddStackExchangeRedisCache(action =>
            {
                action.Configuration = Configuration.GetConnectionString("Redis");
            });
            services.AddScoped<IElasticConnection, ElasticConnection>();
            services.AddScoped<IElasticClient, ElasticClient>();
            services.AddScoped<ILoggerRepository, LoggerRepository>();
            services.AddScoped<ILoggerService, LoggerService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AltaMiraDatabase.API v1"));
            }

            //app.UseHttpsRedirection();

            var logger = NLogBuilder.ConfigureNLog("Nlog.config").GetCurrentClassLogger();

            app.UseExceptionHandler(opt =>
            {
                opt.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.Error($"Sometings is wrong ! {contextFeature.Error}");
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new { message = "Oops bir hata oldu :(" }));
                    }
                });
            });

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
