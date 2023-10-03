using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nnow.Application.Events.Messages;
using Nnow.Settings;

namespace Nnow.Application.Events.Clients;

public class KafkaClientWrapper
{

    ProducerBuilder<string, PermissionKafkaMessage> builder;
    readonly IOptions<NnowAppKeys> _keys;
    readonly ILogger<KafkaClientWrapper> _logger;

    public KafkaClientWrapper(
        IOptions<NnowAppKeys> keys,
        ILogger<KafkaClientWrapper> logger)
    {
        _keys = keys;
        var config = new ProducerConfig()
        {
            BootstrapServers = _keys.Value.KafkaServer,
            SaslUsername = _keys.Value.KafkaSaslUsername,
            SaslPassword = _keys.Value.KafkaSaslPassword,
            SaslMechanism = SaslMechanism.Plain,
            SecurityProtocol = SecurityProtocol.SaslSsl,
        };

        _logger = logger;

        builder = new ProducerBuilder<string, PermissionKafkaMessage>(config)
            .SetValueSerializer(new CustomAsyncSerializer());
    }

    public async Task SendMessage(PermissionKafkaMessage message, CancellationToken cancellationToken)
    {
        try
        {

            _logger.LogInformation($"Trying to permisos Message {message.Id} {message.Operation} to Kakfa.Topic: {_keys.Value.KafkaTopic}");

            using (var producer = builder.Build())
            {
                var t = await producer.ProduceAsync(_keys.Value.KafkaTopic,
                                                    new Message<string, PermissionKafkaMessage> { Key = message.Id, Value = message },
                                                    cancellationToken);

                if (t.Status == PersistenceStatus.Persisted)
                {
                    _logger.LogInformation($"Message {message.Id} {message.Operation} persisted to Kakfa");
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error trying to publish to Kafka" , ex);
        }
    }

}

