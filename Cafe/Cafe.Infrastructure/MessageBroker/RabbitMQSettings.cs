namespace Cafe.Infrastructure.MessageBroker;

public class RabbitMQSettings
{
    public string HostName { get; set; } = null!;
    public string User { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string VHost { get; set; } = null!;
    public string Queue { get; set; } = null!;
    public string RoutingKey { get; set; } = null!;
}
