using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service2.Observer
{
    public class BusObserver : IBusObserver
    {
        //private readonly ILogger<BusObserver> _logger;

        //public BusObserver(ILogger<BusObserver> logger)
        //{
        //    _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        //}

        public async Task CreateFaulted(Exception exception)
        {
            Console.WriteLine($"IBusObserver: -- CreateFaulted {exception.Message}");
            await Task.FromResult(0);
        }

        public async Task PostCreate(IBus bus)
        {
            Console.WriteLine($"IBusObserver: -- PostCreate {bus.Address}");
            await Task.FromResult(0);
        }

        public async Task PostStart(IBus bus, Task<BusReady> busReady)
        {
            Console.WriteLine($"IBusObserver: -- PostStart {bus.Address} : {busReady.Status}");
            await Task.FromResult(0);
        }

        public async Task PostStop(IBus bus)
        {
            Console.WriteLine($"IBusObserver: -- PostStop {bus.Address}");
            await Task.FromResult(0);
        }

        public async Task PreStart(IBus bus)
        {
            Console.WriteLine($"IBusObserver: -- PreStart {bus.Address}");
            await Task.FromResult(0);
        }

        public async Task PreStop(IBus bus)
        {
            Console.WriteLine($"IBusObserver: -- PreStop {bus.Address}");
            await Task.FromResult(0);
        }

        public async Task StartFaulted(IBus bus, Exception exception)
        {
            Console.WriteLine($"IBusObserver: -- StartFaulted {bus.Address} : {exception.Message}");
            await Task.FromResult(0);
        }

        public async Task StopFaulted(IBus bus, Exception exception)
        {
            Console.WriteLine($"IBusObserver: -- StopFaulted {bus.Address} : {exception.Message}");
            await Task.FromResult(0);
        }
    }
}
