namespace Cafe.Application.Interfaces;

public interface IMessageBrokerService
{
    void SendMessage(object obj);
	void SendMessage(string message);
}
