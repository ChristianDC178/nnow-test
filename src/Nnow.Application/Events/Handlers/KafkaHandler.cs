using MediatR;
using Microsoft.Extensions.Logging;
using Nnow.Application.Events.Clients;
using Nnow.Application.Events.Messages;

namespace Nnow.Application.Events.Handlers;

internal class KafkaHandler : INotificationHandler<PermissionMessage>
{
    readonly KafkaClientWrapper _kafkaClient;
    readonly ILogger<KafkaHandler> _logger;

    public KafkaHandler(
        KafkaClientWrapper kafkaClient,
        ILogger<KafkaHandler> logger)
    {
        _kafkaClient = kafkaClient;
        _logger = logger;
    }

    public async Task Handle(PermissionMessage message, CancellationToken cancellationToken)
    {
        var kafkaMessage = new PermissionKafkaMessage()
        {
            Operation = message.Operation
        };

        await _kafkaClient.SendMessage(kafkaMessage, cancellationToken);
    }
}

