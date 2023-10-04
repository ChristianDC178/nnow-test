using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nnow.Application.Commands;
using AutoMapper;
using Elastic.Clients.Elasticsearch;
using System.Net;
using Nnow.Api.Dtos;
using System.Text.RegularExpressions;

namespace Nnow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PermissionsController : ControllerBase
{
    private readonly ILogger<PermissionsController> _logger;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;


    public PermissionsController(
        ILogger<PermissionsController> logger,
        IMediator mediator,
        IMapper mapper
        )
    {
        _logger = logger;
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost(Name = "Request Permission")]
    public async Task<IActionResult> RequestPermission([FromBody] RequestPermissionCommand reqCmd, CancellationToken cToken)
    {
        try
        {
            var appResponse = await _mediator.Send(reqCmd, cToken);
            return StatusCode(StatusCodes.Status200OK, _mapper.Map<ApplicationResponseDto<PermissionDto>>(appResponse));
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in RequestPermission method", ex);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut(template: "{id}", Name = "Modify Permission")]
    public async Task<IActionResult> ModifyPermission([FromRoute] int id, [FromBody] ModifyPermissionCommand reqCmd, CancellationToken cToken)
    {
        try
        {
            reqCmd.PermissionId = id;
            var appResponse = await _mediator.Send(reqCmd, cToken);
            return StatusCode(StatusCodes.Status200OK, _mapper.Map<ApplicationResponseDto<PermissionDto>>(appResponse));
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in ModifyPermission method", ex);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet(template: "{id}", Name = "GetPermissions")]
    public async Task<IActionResult> GetPermissions([FromRoute] int id, CancellationToken cToken)
    {
        try
        {
            GetPermissionCommand reqCmd = new() { PermissionId = id };
            var appResponse = await _mediator.Send(reqCmd, cToken);
            return StatusCode(StatusCodes.Status200OK, _mapper.Map<ApplicationResponseDto<PermissionDto>>(appResponse));
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in GetPermissions method", ex);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
