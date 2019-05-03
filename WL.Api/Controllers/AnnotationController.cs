using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WL.Api.Infrastructure;
using WL.Application.Annotations.Commands;
using WL.Application.Annotations.Queries;
using WL.Application.AnnotationTypes.Queries;
using WL.Application.DocumentTypes.Queries;
using WL.Application.Entities.Queries;
using static WL.Api.Infrastructure.PermissionsAttribute;

namespace WL.Api.Controllers {

  [Produces("application/json")]
  [Route("api/Annotation")]
  [Permissions(MapPerm.CreateDocument)]
  public class AnnotationController : Controller {
    readonly CreateAnnotationCommandHandler createCommand;
    readonly DeleteAnnotationCommandHandler deleteCommand;
    readonly GetDocumentAnnotationsQuery documentAnnotationsQuery;
    readonly GetAllDocumentTypesQuery getDTsQuery;
    readonly GetAllEntitiesQuery getEsQuery;
    readonly GetAllAnnotationTypesQuery getAATsQuery;

    public AnnotationController(
      CreateAnnotationCommandHandler createCommand,
      DeleteAnnotationCommandHandler deleteCommand,
      GetDocumentAnnotationsQuery documentAnnotationsQuery,
      GetAllDocumentTypesQuery getDTsQuery,
      GetAllEntitiesQuery getEsQuery,
      GetAllAnnotationTypesQuery getAATsQuery) {
      this.createCommand = createCommand;
      this.deleteCommand = deleteCommand;
      this.documentAnnotationsQuery = documentAnnotationsQuery;
      this.getDTsQuery = getDTsQuery;
      this.getEsQuery = getEsQuery;
      this.getAATsQuery = getAATsQuery;
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

    [HttpGet("entities")]
    public IActionResult GetEntities() {
      return getEsQuery
            .Execute()
            .Match(
               x => Ok(x),
               ex => StatusCode(500, ex));
    }

    [HttpGet("documentTypes")]
    public IActionResult GetDocumentTypes() {
      return getDTsQuery
            .Execute()
            .Match(
               x => Ok(x),
               ex => StatusCode(500, ex));
    }

    [HttpGet("annotationTypes")]
    public IActionResult GetAnnotationTypes() {
      return getAATsQuery
            .Execute()
            .Match(
               x => Ok(x),
               ex => StatusCode(500, ex));
    }
  }
}