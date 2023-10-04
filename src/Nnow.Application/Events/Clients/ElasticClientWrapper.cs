using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nnow.Application.Events.Messages;
using Nnow.Settings;

namespace Nnow.Application.Events.Clients;

internal class ElasticSearchClientWrapper
{
    ElasticsearchClient client = null;
    readonly IOptions<NnowAppKeys> _keys;
    readonly ILogger<ElasticSearchClientWrapper> _logger;

    public ElasticSearchClientWrapper(
        IOptions<NnowAppKeys> keys,
        ILogger<ElasticSearchClientWrapper> logger)
    {
        _keys = keys;
        client = new ElasticsearchClient(_keys.Value.ElasticCloudId, new ApiKey(_keys.Value.ElasticApiKey));
        _logger = logger;
    }

    public async Task IndexPermission(PermissionMessage permissionNotification, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"Sending messafe to Elastic. Index: { _keys.Value.ElasticIndex}");
            var response = await client.IndexAsync(permissionNotification, _keys.Value.ElasticIndex, cancellationToken);

            if (response.IsValidResponse && response.IsSuccess())
                _logger.LogInformation($"Message to Elastic sent successfully: {response.Id}");
        }
        catch (Exception ex)
        {
            if(cancellationToken.IsCancellationRequested)
                _logger.LogError($"Elastic Search Client - The thread was cancelled", ex);

            _logger.LogError($"Error trying to put to Elastic", ex);
        }
    }

}

