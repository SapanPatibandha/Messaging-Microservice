﻿using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service1.Observer
{
    public class ConsumeMessageObserver<T> : IConsumeMessageObserver<T> where T : class
    {
        public async Task ConsumeFault(ConsumeContext<T> context, Exception exception)
        {
            Console.WriteLine($"IConsumeMessageObserver -- ConsumeFault : {context.Message} : {exception.Message}");
            await Task.FromResult(0);
        }

        public async Task PostConsume(ConsumeContext<T> context)
        {
            Console.WriteLine($"IConsumeMessageObserver -- PostConsume : {context.Message} ");
            await Task.FromResult(0);
        }

        public async Task PreConsume(ConsumeContext<T> context)
        {
            Console.WriteLine($"IConsumeMessageObserver -- PreConsume : {context.Message}");
            await Task.FromResult(0);
        }
    }
}
