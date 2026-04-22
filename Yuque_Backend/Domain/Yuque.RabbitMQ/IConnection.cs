using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yuque.RabbitMQ
{
    public interface IConnection
    {
        Task<global::RabbitMQ.Client.IConnection> CreateConnectionAsync();

        Task<IChannel> CreateChannelAsync(bool enablePublisherConfirmations = true);
    }
}
