using MediatR;
using Nnow.Domain.Entities;

namespace Nnow.Application.Events.Messages;

internal class PermissionMessage : INotification
{
    public int Id { get; set; }
    public string Forename { get; set; }
    public string Surname { get; set; }
    public int PermissionTypeId { get; set; }
    public DateTime Date { get; set; }
    public string Operation { get; set; }

    public PermissionMessage(Permission permission, NotificationType notificationType)
    {
        Id = permission.Id;
        Forename = permission.Forename;
        Surname = permission.Surname;
        PermissionTypeId = permission.PermissionTypeId;
        Date = permission.Date;
        Operation = notificationType.ToString();
    }

}
