using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Assessment.Api.Models;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using AutoMapper;
using Assessment.Api.Services;

namespace Assessment.Api
{
    public class Startup
    {

        private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // todo: get this from somewhere secure
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        private readonly IHostingEnvironment _env;
        private readonly ILoggerFactory _loggerFactory;

        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        public Startup(IHostingEnvironment env, IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            //Startup configuration setup for running it in different configuration mode
            _env = env;
            var builder = new ConfigurationBuilder().SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            _loggerFactory = loggerFactory;


        }


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Configure database
            var dbContextOptions = new DbContextOptionsBuilder<AssessmentDbContext>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            services.AddDbContext<AssessmentDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            //inject scoped StudentService
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IDatabaseService, DatabaseService>();

            //wire Automapper
            services.AddAutoMapper();

            services.AddCors(options => {
                options.AddPolicy("AllowAnyOrigin",
                    builders => builders
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                    );

            });

            //Add MVC configuration and JsonOptions 
            services.AddMvc().AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.Formatting = Formatting.None;
            }).AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>()); ;

            //just for the demonstration porpose we added api versioning
            services.AddApiVersioning(o => {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1,0);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
                          ILoggerFactory loggerFactory)
        {
            //setup a rolling log appender
            _loggerFactory.AddFile("Logs/Assessment-{Date}.txt");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Add centralize exception handler for quick setup we are using .net core provided exception handler
            app.UseExceptionHandler(
                builder =>
                {
                    builder.Run(
                        async context =>
                        {
                            var feature = context.Features.Get<IExceptionHandlerPathFeature>();
                            var exception = feature.Error;

                            var result = JsonConvert.SerializeObject(new { error = exception.Message });
                            context.Response.ContentType = "application/json";
                            await context.Response.WriteAsync(result);
                        });
                });

            app.UseCors("AllowAnyOrigin");
            app.UseMvc();
        }
    }
}
