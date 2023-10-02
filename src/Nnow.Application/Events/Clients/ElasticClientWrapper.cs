using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Microsoft.Extensions.Options;
using Nnow.Application.Events.Messages;
using Nnow.Settings;

namespace Nnow.Application.Events.Clients;

internal class ElasticSearchClientWrapper
{
    ElasticsearchClient client = null;
    readonly IOptions<NnowAppKeys> _keys;

    public ElasticSearchClientWrapper(IOptions<NnowAppKeys> keys )
    {
        var apiKey = keys.Value.ElasticApiKey;
        var cloudId = keys.Value.ElasticCloudId;
        _keys = keys;

        client = new ElasticsearchClient(cloudId, new ApiKey(apiKey));
    }

    public async Task<IndexResponse> IndexPermission(PermissionMessage permissionNotification, CancellationToken cancellationToken)
    {
        var response = await client.IndexAsync(permissionNotification,_keys.Value.ElasticIndex ,cancellationToken );
        return response;
    }

}

