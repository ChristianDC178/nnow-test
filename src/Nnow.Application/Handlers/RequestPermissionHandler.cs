using Azure;
using MediatR;
using Microsoft.Extensions.Logging;
using Nnow.Application.Commands;
using Nnow.Application.Events.Messages;
using Nnow.Database;
using Nnow.Domain.Entities;

namespace Nnow.Application.Handlers;

public class RequestPermissionHandler : IRequestHandler<RequestPermissionCommand, ApplicationResponse<Permission>>
{

    readonly UnitOfWork _unitOfWork;
    readonly IMediator _mediator;
    readonly ILogger<RequestPermissionHandler> _logger;

    public RequestPermissionHandler(
        UnitOfWork unitOfWork,
        IMediator mediator,
        ILogger<RequestPermissionHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mediator = mediator;
        _logger = logger;
    }

    public async Task<ApplicationResponse<Permission>> Handle(RequestPermissionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            ApplicationResponse<Permission> response = new();

            if (string.IsNullOrEmpty(request.Forename) || string.IsNullOrEmpty(request.Surname) || request.PermissionTypeId <= 0)
            {
                response.Errors.Add("All fields must be completed and with correct values");
            }

            var pType = await _unitOfWork.PermissionTypeRepo.GetByIdAsync(request.PermissionTypeId);

            if (pType == null)
            {
                response.Errors.Add("Permission Type cannot be null.");
            }

            if (response.Invalid)
                return response;

            Permission permission = new()
            {
                Forename = request.Forename,
                Surname = request.Surname,
                PermissionTypeId = request.PermissionTypeId,
                Date = DateTime.Now,
            };

            _unitOfWork.PermissionRepo.Create(permission);
            await _unitOfWork.SaveChangesAsync();

            _mediator.Publish(new PermissionMessage(permission, NotificationType.request));

            response.Entity = permission;

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError("It couldn't be possible to request a new permission.", ex);
            throw ex;
        }
    }

}

