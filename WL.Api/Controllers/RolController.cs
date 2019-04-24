using Microsoft.AspNetCore.Mvc;
using WL.Api.Infrastructure;
using WL.Application.Roles.Commands;
using WL.Application.Roles.Queries;
using static WL.Api.Infrastructure.PermissionsAttribute;

namespace WL.Api.Controllers {

  [Produces("application/json")]
  [Route("api/Role")]
  [Permissions(MapPerm.ConfigSystem)]
  public class RolController : Controller {
    readonly CreateRoleCommandHandler createCommand;
    readonly GetAllRolesQuery getAllQuery;
    readonly GetRoleQuery getOneQuery;
    readonly UpdateRoleCommandHandler updateCommand;
    readonly DeleteRoleCommandHandler deleteCommand;

    public RolController(
        CreateRoleCommandHandler createCommand,
        GetAllRolesQuery getAllQuery,
        GetRoleQuery getOneQuery,
        UpdateRoleCommandHandler updateCommand,
        DeleteRoleCommandHandler deleteCommand) {
      this.createCommand = createCommand;
      this.getAllQuery = getAllQuery;
      this.getOneQuery = getOneQuery;
      this.updateCommand = updateCommand;
      this.deleteCommand = deleteCommand;
    }

    [HttpGet("{id}")]
    public IActionResult Get(long id)
       => getOneQuery.Execute(id).Match(
          Succ: Ok,
          Fail: ex => StatusCode(500, ex));

    [HttpGet]
    public IActionResult GetAll()
        => getAllQuery.Execute().Match(
          Succ: Ok,
          Fail: ex => StatusCode(500, ex));

    [HttpPost]
    public IActionResult Post([FromBody] CreateRoleCommand msg)
       => createCommand.Execute(msg).Match(
          Succ: x => x.Match<IActionResult>(Ok, BadRequest),
          Fail: ex => StatusCode(500, ex));

    [HttpPut]
    public IActionResult Put([FromBody] UpdateRoleCommand msg)
        => updateCommand.Execute(msg).Match(
          Succ: x => x.Match<IActionResult>(Ok, BadRequest),
          Fail: ex => StatusCode(500, ex));

    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
        => deleteCommand.Execute(id).Match(
          Succ: _ => Ok() as IActionResult,
          Fail: err => StatusCode(500, err));
  }
}