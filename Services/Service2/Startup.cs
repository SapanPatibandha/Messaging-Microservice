using EventBus.Messages.Common;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Service2.EventBusConsumer;
using Service2.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service2
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
                config.AddConsumer<EventOneConsumer>();

                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(Configuration["EventBussSetting:HostAddress"]);

                    cfg.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue, c => {
                        c.ConfigureConsumer<EventOneConsumer>(ctx);
                        //c.ConfigureConsumer<EventOneConsumerFault>(ctx);
                        //c.UseMessageRetry(r =>
                        //{
                        //    r.Handle<ArgumentNullException>();
                        //    r.Ignore(typeof(InvalidOperationException), typeof(InvalidCastException));
                        //    r.Ignore<ArgumentException>(t => t.ParamName == "orderTotal");
                        //});
                    });


                    cfg.ConnectBusObserver(new BusObserver());
                    cfg.ConnectReceiveObserver(new ReceiveObserver());
                    cfg.ConnectConsumeObserver(new ConsumeObserver());
                    //cfg.ConnectSendObserver(new SendObserver());
                    //cfg.ConnectPublishObserver(new PublishObserver());
                });
            });
            services.AddMassTransitHostedService();
            services.AddScoped<EventOneConsumer>();
            //-----------------------------------------------------------

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Service2", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Service2 v1"));
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
