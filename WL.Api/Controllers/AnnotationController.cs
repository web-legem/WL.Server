using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WL.Api.Infrastructure;
using WL.Application.Annotations.Commands;
using static WL.Api.Infrastructure.PermissionsAttribute;

namespace WL.Api.Controllers {

  [Produces("application/json")]
  [Route("api/Annotation")]
  [Permissions(MapPerm.CreateDocument)]
  public class AnnotationController : Controller {
    readonly CreateAnnotationCommandHandler createCommand;
    readonly DeleteAnnotationCommandHandler deleteCommand;

    public AnnotationController(
      CreateAnnotationCommandHandler createCommand,
      DeleteAnnotationCommandHandler deleteCommand
    ) {
      this.createCommand = createCommand;
      this.deleteCommand = deleteCommand;
    }

    [HttpPost]
    public IActionResult Post([FromBody] CreateAnnotationCommand cmd)
     => createCommand.Execute(cmd).Match(
       Succ: x => x.Match<IActionResult>(
         y =>
         Ok(y),
         y =>
         BadRequest(y)
       ),
       Fail: ex =>
       StatusCode(500, ex)
       );

    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
        => deleteCommand
          .Execute(id)
          .Match(
           Succ: _ =>
           Ok() as IActionResult,
           Fail: err =>
           StatusCode(500, err)
           );
  }
}