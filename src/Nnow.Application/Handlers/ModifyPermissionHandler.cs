using MediatR;
using Microsoft.Extensions.Logging;
using Nnow.Application.Commands;
using Nnow.Application.Events.Messages;
using Nnow.Database;
using Nnow.Domain.Entities;

namespace Nnow.Application.Handlers;

public class ModifyPermissionHandler : IRequestHandler<ModifyPermissionCommand, ApplicationResponse<Permission>>
{

    readonly UnitOfWork _unitOfWork;
    readonly IMediator _mediator;
    readonly ILogger<ModifyPermissionHandler> _logger;

    public ModifyPermissionHandler(
        UnitOfWork unitOfWork,
        IMediator mediator,
        ILogger<ModifyPermissionHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mediator = mediator;
        _logger = logger;
    }

    public async Task<ApplicationResponse<Permission>> Handle(ModifyPermissionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            ApplicationResponse<Permission> response = new();

            if (string.IsNullOrEmpty(request.Forename) || string.IsNullOrEmpty(request.Surname) || request.PermissionTypeId <= 0)
            {
                response.Errors.Add("All fields must be completed and with correct values");
            }

            var pType = await _unitOfWork.PermissionTypeRepo.GetByIdAsync(request.PermissionTypeId);
            var permission = await _unitOfWork.PermissionRepo.GetByIdAsync(request.PermissionId);

            if (pType == null || permission == null)
            {
                response.Errors.Add("Permission and Permission type cannot be null.");
            }

            if (response.Invalid)
                return response;

            permission.Surname = request.Surname;
            permission.Forename = request.Forename;
            permission.PermissionTypeId = request.PermissionTypeId;
            permission.Date = DateTime.Now;

            _unitOfWork.PermissionRepo.Update(permission);
            await _unitOfWork.SaveChangesAsync();

            _mediator.Publish(new PermissionMessage(permission, NotificationType.modify));

            response.Entity = permission;

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError("It couldn't be possible to modify the permission.", ex);
            throw ex;
        }
    }

}