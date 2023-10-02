using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Nnow.Application.Events.Messages;
using Nnow.Settings;

namespace Nnow.Application.Events.Clients;

public class KafkaClientWrapper
{

    ProducerConfig config = null;
    ProducerBuilder<string, PermissionKafkaMessage> builder;
    readonly IOptions<NnowAppKeys> _keys;

    public KafkaClientWrapper(IOptions<NnowAppKeys> keys)
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

        builder = new ProducerBuilder<string, PermissionKafkaMessage>(config)
            .SetValueSerializer(new CustomAsyncSerializer());
    }

    public async Task SendMessage(PermissionKafkaMessage message, CancellationToken cancellationToken)
    {

        try
        {

            using (var producer = builder.Build())
            {
                var t = await producer.ProduceAsync(_keys.Value.KafkaTopic,
                                                    new Message<string, PermissionKafkaMessage> { Key = message.Id, Value = message },
                                                    cancellationToken);
                var s = t.Status;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

}

