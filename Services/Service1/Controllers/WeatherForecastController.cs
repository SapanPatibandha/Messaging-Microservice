using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service1.EventBusConsumer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(
            IPublishEndpoint publishEndpoint, 
            ILogger<WeatherForecastController> logger)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            //------------------------------------------------------------------
            // ---- Send message from hear to RabbitMQ. 
            var eventMsg = new EventOne()
            {
                EventName = "TestEvet",
                Message = $"This is test message from service 1: {DateTime.Now.Ticks}",
                ReferenceID = DateTime.Now.Ticks
            };
            _publishEndpoint.Publish(eventMsg);
            //_publishEndpoint.Publish<EventOne>(eventMsg);

            //------------------------------------------------------------------

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
