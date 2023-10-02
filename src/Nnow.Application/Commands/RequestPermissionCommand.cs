using MediatR;
using Nnow.Domain.Entities;

namespace Nnow.Application.Commands;

public class RequestPermissionCommand : IRequest<ApplicationResponse<Permission>>
{
    public string Forename { get; set; }
    public string Surname { get; set; }
    public int PermissionTypeId { get; set; }
}
