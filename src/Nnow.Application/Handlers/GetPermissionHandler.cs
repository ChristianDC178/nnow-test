using MediatR;
using Microsoft.Extensions.Logging;
using Nnow.Application.Commands;
using Nnow.Database;
using Nnow.Application.Events.Messages;
using Nnow.Domain.Entities;
using Microsoft.Extensions.Options;
using Nnow.Settings;

namespace Nnow.Application.Handlers;

public class GetPermissionHandler : IRequestHandler<GetPermissionCommand, ApplicationResponse<Permission>>
{
    readonly UnitOfWork _unitOfWork;
    readonly IMediator _mediator;
    readonly ILogger<GetPermissionHandler> _logger;

    public GetPermissionHandler(
        UnitOfWork unitOfWork,
        IMediator mediator,
        ILogger<GetPermissionHandler> logger, 
        IOptions<NnowAppKeys> keys)
    {
        _unitOfWork = unitOfWork;
        _mediator = mediator;
        _logger = logger;
    }

    public async Task<ApplicationResponse<Permission>> Handle(GetPermissionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            ApplicationResponse<Permission> response = new();

            var permission = await _unitOfWork.PermissionRepo.GetByIdAsync(request.PermissionId);

            if (permission == null)
            {
                response.Errors.Add("Perssion not found");
            }

            if (response.Invalid)
                return response;

            _mediator.Publish(new PermissionMessage(permission, NotificationType.get));

            response.Entity = permission;

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError($"There is an error trying to get a permission {request.PermissionId}", ex);
            throw ex;
        }
    }

}