using Cafe.Application.Interfaces;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;

namespace Cafe.Infrastructure.MessageBroker;

public class RabbitMqService : IMessageBrokerService
{
    private readonly RabbitMQSettings _rabbitMqSettings;

    public RabbitMqService(IOptions<RabbitMQSettings> settings)
    {
        _rabbitMqSettings = settings.Value;
    }

    public void SendMessage(object obj)
	{
		var message = JsonSerializer.Serialize(obj);
		SendMessage(message);
	}

	public void SendMessage(string message)
	{
		var factory = new ConnectionFactory();
        factory.UserName = _rabbitMqSettings.User;
        factory.Password = _rabbitMqSettings.Password;
        factory.VirtualHost = _rabbitMqSettings.VHost;
        factory.HostName = _rabbitMqSettings.HostName;

		using (var connection = factory.CreateConnection())
		using (var channel = connection.CreateModel())
		{
			var body = Encoding.UTF8.GetBytes(message);

			channel.BasicPublish(exchange: "",
                           routingKey: _rabbitMqSettings.RoutingKey,
                           basicProperties: null,
                           body: body);
		}
	}
}
