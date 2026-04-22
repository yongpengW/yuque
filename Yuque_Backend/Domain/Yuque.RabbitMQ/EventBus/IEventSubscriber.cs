using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Yuque.RabbitMQ.EventBus
{
    /// <summary>
    /// RabbitMQ订阅者接口
    /// </summary>
    public interface IEventSubscriber : IDisposable
    {
        Task SubscribeAsync(Type eventType, Type eventHandlerType);
    }
}
