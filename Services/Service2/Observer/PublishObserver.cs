using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service2.Observer
{
    public class PublishObserver : IPublishObserver
    {
        public async Task PostPublish<T>(PublishContext<T> context) where T : class
        {
            Console.WriteLine($"IPublishObserver  -- PostPublish : {context.Message}");
            await Task.FromResult(0);
        }

        public async Task PrePublish<T>(PublishContext<T> context) where T : class
        {
            Console.WriteLine($"IPublishObserver  -- PrePublish : {context.Message}");
            await Task.FromResult(0);
        }

        public async Task PublishFault<T>(PublishContext<T> context, Exception exception) where T : class
        {
            Console.WriteLine($"IPublishObserver  -- PublishFault {context.Message} : {exception.Message}");
            await Task.FromResult(0);
        }
    }
}
