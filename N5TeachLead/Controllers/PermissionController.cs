using AppServices.Command;
using AppServices.Query;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace N5TeachLead.Controllers;

/// <summary>
/// Manage Employee Permissions
/// </summary>
[ApiController]
[ApiVersion(1.0)]
[Route("[controller]/[action]")]
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

    /// <summary>
    /// Get all Employee permissions
    /// </summary>
    /// <param name="employeeId"></param>
    /// <returns></returns>
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
            _logger.LogError(ex, "Method: {method}. Error trying to get permission from employeeId {employeeId}", nameof(GetPermissions), employeeId);
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }

    /// <summary>
    /// Request Permission to Employee
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost(Name = "RequestPermissions")]
    public async Task<ActionResult> RequestPermission(RequestPermissionCommand command)
    {
        try
        {
            _logger.LogInformation("Adding permission {permission} to employeeId {employeeId}", command.Permission?.ToString(), command.EmployeeId);

            await _sender.Send(command);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Method: {method}: Error trying to add permission {permission} to employeeId {employeeId}", nameof(RequestPermission), command.Permission?.ToString(), command.EmployeeId);
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        return Ok();
    }

    /// <summary>
    /// Modify Permission to Employee
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost(Name = "ModifyPermission")]
    public async Task<ActionResult> ModifyPermission(ModifyPermissionCommand command)
    {
        try
        {
            var serialize = JsonSerializer.Serialize(command.Permissions);
            _logger.LogInformation("Adding permissions {permissions} to employeeId {employeeId}", serialize, command.EmployeeId);

            await _sender.Send(command);
        }
        catch (Exception ex)
        {
            var serialize = JsonSerializer.Serialize(command.Permissions);
            _logger.LogError(ex, "Method: {method}: Error trying to add permissions {permissions} to employeeId {employeeId}", nameof(RequestPermission), serialize, command.EmployeeId);
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        return Ok();
    }
}
