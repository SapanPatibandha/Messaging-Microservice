using EventBus.Messages.Events;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace Service1.EventBusConsumer
{
    public class EventOneConsumer : IConsumer<EventOne>
    {
        public async Task Consume(ConsumeContext<EventOne> context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            Console.WriteLine(context.Message.Message);
        }
    }
}
