using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace Service1
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
            //MassTransit configuration is created hear. -----------------
            services.AddMassTransit(config =>
            {
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(Configuration["EventBussSetting:HostAddress"]);

                    //-----------------------------------------------------------------
                    //cfg.Host("localhost", 5672, "vhost", h =>
                    //  {
                    //      h.Username("username");
                    //      h.Password("password");
                    //      h.UseSsl(s =>
                    //      {
                    //          s.Protocol = SslProtocols.Ssl2;
                    //      });
                    //  });
                    //-----------------------------------------------------------------
                    //cfg.Host(new Uri("amqps://b-12345678-1234-1234-1234-123456789012.mq.us-east-2.amazonaws.com:5671"), h =>
                    //{
                    //    h.Username("username");
                    //    h.Password("password");
                    //});
                    //-----------------------------------------------------------------
                });
            });
            services.AddMassTransitHostedService();
            //-----------------------------------------------------------

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Service1", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Service1 v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
