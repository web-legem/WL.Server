using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WL.Api.Infrastructure;
using WL.Application.Annotations.Commands;
using WL.Application.Annotations.Queries;
using static WL.Api.Infrastructure.PermissionsAttribute;

namespace WL.Api.Controllers {

  [Produces("application/json")]
  [Route("api/Annotation")]
  [Permissions(MapPerm.CreateDocument)]
  public class AnnotationController : Controller {
    readonly CreateAnnotationCommandHandler createCommand;
    readonly DeleteAnnotationCommandHandler deleteCommand;
    readonly GetDocumentAnnotationsQuery documentAnnotationsQuery;

    public AnnotationController(
      CreateAnnotationCommandHandler createCommand,
      DeleteAnnotationCommandHandler deleteCommand,
      GetDocumentAnnotationsQuery documentAnnotationsQuery
    ) {
      this.createCommand = createCommand;
      this.deleteCommand = deleteCommand;
      this.documentAnnotationsQuery = documentAnnotationsQuery;
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

    [HttpGet("document/{id}")]
    public IActionResult GetDocumentAnnotations(long id)
      => documentAnnotationsQuery
        .Execute(id)
        .Match(
          Succ: x =>
          Ok(x),
          Fail: err =>
         StatusCode(500, err)
        );
  }
}