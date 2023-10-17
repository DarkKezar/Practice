using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Options;
using System.Text;
using System.Diagnostics;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Stock.Application.DTO;
using Stock.Application.IServices;

namespace Stock.Web.BackgroundServices;

public class RabbitMqListener : BackgroundService
{
    private readonly ITransactionService _transactionService;
    private readonly RabbitMQSettings _rabbitMqSettings;
	private readonly IConnection _connection;
	private readonly IModel _channel;

	public RabbitMqListener(IOptions<RabbitMQSettings> settings, ITransactionService transactionService)
	{
        _transactionService = transactionService;
		_rabbitMqSettings = settings.Value;

		var factory = new ConnectionFactory();
        factory.UserName = _rabbitMqSettings.User;
        factory.Password = _rabbitMqSettings.Password;
        factory.VirtualHost = _rabbitMqSettings.VHost;
        factory.HostName = _rabbitMqSettings.HostName;

		_connection = factory.CreateConnection();
		_channel = _connection.CreateModel();
		//_channel.QueueDeclare(queue: _rabbitMqSettings.Queue, durable: false, exclusive: false, autoDelete: false, arguments: null);
	}

	protected async override Task ExecuteAsync(CancellationToken stoppingToken)
	{
		stoppingToken.ThrowIfCancellationRequested();
		var consumer = new EventingBasicConsumer(_channel);
		consumer.Received += async (ch, ea) =>
		{
			var content = JsonSerializer.Deserialize<TransactionCreationDTO>(Encoding.UTF8.GetString(ea.Body.ToArray()));
            var result = await _transactionService.InsertTransactionAsync(content, stoppingToken);

			_channel.BasicAck(ea.DeliveryTag, false);
		};
		_channel.BasicConsume(_rabbitMqSettings.Queue, false, consumer);
	}
	
	public override void Dispose()
	{
		_channel.Close();
		_connection.Close();
		base.Dispose();
	}
}
