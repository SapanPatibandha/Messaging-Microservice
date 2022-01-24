using EventBus.Messages.Events;
using GreenPipes;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace Service2.EventBusConsumer
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

    public class EventOneConsumerFault : IConsumerFactory<EventOne>
    {
        public void Probe(ProbeContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            Console.WriteLine(context.ToString());
        }

        public async Task Send<T>(ConsumeContext<T> context, IPipe<ConsumerConsumeContext<EventOne, T>> next) where T : class
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            Console.WriteLine(context.Message.ToString());
        }
    }
}
