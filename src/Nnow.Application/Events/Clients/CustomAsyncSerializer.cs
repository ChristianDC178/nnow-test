using Confluent.Kafka;
using Nnow.Application.Events.Messages;

namespace Nnow.Application.Events.Clients;

internal class CustomAsyncSerializer : IAsyncSerializer<PermissionKafkaMessage>
{
    async Task<byte[]> IAsyncSerializer<PermissionKafkaMessage>.SerializeAsync(PermissionKafkaMessage data, SerializationContext context)
    {
        MemoryStream memoryStream = new MemoryStream();
        await System.Text.Json.JsonSerializer.SerializeAsync(memoryStream, data);
        return memoryStream.ToArray();
    }
}

