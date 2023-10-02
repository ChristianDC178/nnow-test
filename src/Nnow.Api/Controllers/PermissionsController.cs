using Elastic.Clients.Elasticsearch.Aggregations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nnow.Application.Commands;
using Nnow.Api.Dtos;
using Azure;

namespace Nnow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PermissionsController : ControllerBase
{
    private readonly ILogger<PermissionsController> _logger;
    private readonly IMediator _mediator;

    public PermissionsController(
        ILogger<PermissionsController> logger,
        IMediator mediator
        )
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost(Name = "Request Permission")]
    public async Task<IActionResult> RequestPermission([FromBody] RequestPermissionCommand reqCmd, CancellationToken cancellationToken)
    {
        try
        {
            var appResponse = await _mediator.Send(reqCmd, cancellationToken);
            return Ok( AdapterHelper.Adapt(appResponse));
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in RequestPermission method", ex);
            return BadRequest();
        }
    }

    [HttpPut(template: "{id}", Name = "Modify Permission")]
    public async Task<IActionResult> ModifyPermission([FromRoute] int id, [FromBody] ModifyPermissionCommand reqCmd, CancellationToken cancellationToken)
    {
        try
        {
            reqCmd.PermissionId = id;
            var appResponse =   await _mediator.Send(reqCmd, cancellationToken);
            return Ok(AdapterHelper.Adapt(appResponse));
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in ModifyPermission method", ex);
            return BadRequest();
        }
    }

    [HttpGet(template: "{id}", Name = "GetPermissions")]
    public async Task<IActionResult> GetPermissions([FromRoute] int id, CancellationToken cancellationToken)
    {
        try
        {
            GetPermissionCommand reqCmd = new() { PermissionId = id };
            var appResponse =  await _mediator.Send(reqCmd);
            return Ok(AdapterHelper.Adapt(appResponse));
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in GetPermissions method", ex);
            return BadRequest();
        }
    }

}
