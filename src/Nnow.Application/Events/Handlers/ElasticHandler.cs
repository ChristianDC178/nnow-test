using MediatR;
using Microsoft.Extensions.Logging;
using Nnow.Application.Events.Clients;
using Nnow.Application.Events.Messages;

namespace Nnow.Application.Events.Handlers;

internal class ElasticHandler : INotificationHandler<PermissionMessage>
{

    readonly ElasticSearchClientWrapper _elasticClient;
    readonly ILogger<ElasticHandler> _logger;


    public ElasticHandler(
        ElasticSearchClientWrapper elasticClient,
        ILogger<ElasticHandler> logger)
    {
        _elasticClient = elasticClient;
        _logger = logger;
    }

    public async Task Handle(PermissionMessage messsage, CancellationToken cancellationToken)
    {
        await _elasticClient.IndexPermission(messsage, cancellationToken);
    }
}
