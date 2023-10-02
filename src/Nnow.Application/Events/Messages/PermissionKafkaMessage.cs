namespace Nnow.Application.Events.Messages;

public class PermissionKafkaMessage
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Operation { get; set; } = "get";
}
