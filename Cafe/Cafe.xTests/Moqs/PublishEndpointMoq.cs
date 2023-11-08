using Moq;
using MassTransit;
using Stock.Application.DTO;

namespace Cafe.xTests.Moqs;

public static class PublishEndpointMoq
{
    public static IPublishEndpoint GetPublishEndpoint()
    {
        var result = new Mock<IPublishEndpoint>();
        result.Setup(o => o.Publish<TransactionCreationDTO>(It.IsAny<TransactionCreationDTO>(), It.IsAny<CancellationToken>()))
            .Returns((TransactionCreationDTO message, CancellationToken token) =>
            {
                return Task.FromResult(() => {
                    Console.WriteLine(message.ToString());
                });
            });

        return result.Object;
    }
}
