using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nnow.Application.Commands;
using Nnow.Application.Events.Messages;
using Nnow.Database;
using Nnow.Domain.Entities;
using Nnow.Settings;

namespace Nnow.Application.Handlers;

public class GetPermissionHandler : IRequestHandler<GetPermissionCommand, ApplicationResponse<Permission>>
{
    readonly UnitOfWork _unitOfWork;
    readonly IMediator _mediator;
    readonly ILogger<GetPermissionHandler> _logger;
    readonly CancellationTokenSource source = new CancellationTokenSource();


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

    public async Task<ApplicationResponse<Permission>> Handle(GetPermissionCommand request, CancellationToken cToken = default(CancellationToken))
    {
        try
        {
            
            ApplicationResponse<Permission> response = new();

            var permission = await _unitOfWork.PermissionRepo.GetByIdAsync(request.PermissionId, cToken);

            if (permission == null)
            {
                response.Errors.Add("Perssion not found");
            }

            if (response.Invalid)
                return response;

            _mediator.Publish(new PermissionMessage(permission, NotificationType.get), cToken);

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