using AppServices.Command;
using AppServices.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace N5TeachLead.Controllers;

[ApiController]
[Route("[controller]")]
public class PermissionController : ControllerBase
{
    private readonly ISender _sender;
    private readonly ILogger<PermissionController> _logger;

    public PermissionController(
        ILogger<PermissionController> logger, 
        ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }

    [HttpPost(Name = "AddPermissions")]
    public async Task<ActionResult> AddPermission(AddPermissionCommand command)
    {
        try
        {
            _logger.LogInformation("Adding permission {permission} to employeeId {employeeId}", command.Permission?.ToString(), command.EmployeeId);

            await _sender.Send(command);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error trying to add permission {permission} to employeeId {employeeId}", command.Permission?.ToString(), command.EmployeeId);
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        return Ok();
    }

    [HttpGet(Name = "GetPermissions")]
    public async Task<ActionResult> GetPermissions(int employeeId)
    {
        try
        {
            _logger.LogInformation("getting permission from employeeId {employeeId}", employeeId);

            var permissions = await _sender.Send(new GetPermissionQuery(employeeId));
            return Ok(permissions);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error trying to get permission from employeeId {employeeId}", employeeId);
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }        
    }

    //[HttpGet(Name = "GetPermissions")]
    //public async Task<ActionResult> UpdatePermissions(int employeeId)
    //{
    //    try
    //    {
    //        var permissions = await _sender.Send(new GetPermissionQuery(employeeId));
    //        return Ok(permissions);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error trying to get permission from employeeId {employeeId}", employeeId);
    //        return StatusCode((int)HttpStatusCode.InternalServerError);
    //    }
    //}
}
