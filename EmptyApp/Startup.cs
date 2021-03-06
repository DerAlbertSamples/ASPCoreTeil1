﻿using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StructureMap;

namespace EmptyApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            services.Configure<ReinRausOptions>(Configuration.GetSection("ReinRaus"));

            var container = new Container();
            container.Configure(c =>
            {
                c.Scan(a =>
                {
                    a.AssemblyContainingType<Startup>();
                    a.WithDefaultConventions();
                });
            });
            container.Populate(services);
            return container.GetInstance<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            var logger = loggerFactory.CreateLogger<Startup>();
            logger.LogWarning("Hello my {Name}", Environment.MachineName);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use((context, next) =>
            {
                context.Response.ContentType = "text/html";
                return next();
            });

            app.UseMiddleware<ReinRausMiddleware>();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!<br />");
            });
        }
    }


}
