using MediatR;
using Nnow.Domain.Entities;

namespace Nnow.Application.Commands;

public class ApplicationResponse<TEntity>
{
    public bool Valid { get { return Errors.Count == 0; } }
    public bool Invalid { get { return Errors.Count > 0; } }
    public List<string> Errors { get; private set; } = new List<string>();
    public TEntity Entity { get; set; }
}

public class GetPermissionCommand : IRequest<ApplicationResponse<Permission>>
{
    public int PermissionId { get; set; }
}
