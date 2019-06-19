using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WL.Api.Infrastructure;
using WL.Application.Annotations.Commands;
using WL.Application.Annotations.Queries;
using WL.Application.AnnotationTypes.Queries;
using WL.Application.Documents.Queries;
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
    readonly SearchDocumentsToAnnotateCountQuery searchCountQuery;
    readonly SearchDocumentsToAnnotateQuery searchQuery;

    public AnnotationController(
      CreateAnnotationCommandHandler createCommand,
      DeleteAnnotationCommandHandler deleteCommand,
      GetDocumentAnnotationsQuery documentAnnotationsQuery,
      GetAllDocumentTypesQuery getDTsQuery,
      GetAllEntitiesQuery getEsQuery,
      GetAllAnnotationTypesQuery getAATsQuery,
      SearchDocumentsToAnnotateCountQuery searchCountQuery,
      SearchDocumentsToAnnotateQuery searchQuery
      ) {
      this.createCommand = createCommand;
      this.deleteCommand = deleteCommand;
      this.documentAnnotationsQuery = documentAnnotationsQuery;
      this.getDTsQuery = getDTsQuery;
      this.getEsQuery = getEsQuery;
      this.getAATsQuery = getAATsQuery;
      this.searchCountQuery = searchCountQuery;
      this.searchQuery = searchQuery;
    }

    [HttpPost]
    public IActionResult Post([FromBody] CreateAnnotationCommand cmd)
     => createCommand.Execute(cmd).Match(
       Succ: x => x.Match<IActionResult>(Ok, BadRequest),
       Fail: ex => StatusCode(500, ex)
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

    [HttpGet("search/count")]
    public IActionResult searchCount(
      [FromQuery] string wordsToSearch,
      [FromQuery] long? publicationDate,
      [FromQuery] string number,
      [FromHeader(Name = "Authorization")] string token,
      [FromQuery] long? entityId = null,
      [FromQuery] long? documentTypeId = null
    ) {
      return searchCountQuery.Execute(
          new SearchDocumentsMessage {
            WordsToSearch = wordsToSearch,
            Year = publicationDate,
            Number = number,
            EntityId = entityId,
            DocumentTypeId = documentTypeId
          },
          token
        )
        .Match(
          x =>
            Ok(x),
          ex => StatusCode(500, ex));
    }

    //documentos
    [HttpGet("search")]
    public IActionResult searchDocuments(
      [FromHeader(Name = "Authorization")] string token,
      [FromQuery(Name = "page")] long? page,
      [FromQuery(Name = "pageSize")] long? pageSize,
      [FromQuery] string wordsToSearch,
      [FromQuery] long? publicationDate,
      [FromQuery] string number,
      [FromQuery] string orderBy = "DEFAULT",
      [FromQuery] bool descend = false,
      [FromQuery] long? entityId = null,
      [FromQuery] long? documentTypeId = null) {
      return searchQuery.Execute(
          new SearchDocumentsMessage {
            Page = page,
            PageSize = pageSize,
            WordsToSearch = wordsToSearch,
            Year = publicationDate,
            Number = number,
            OrderBy = orderBy,
            Descend = descend,
            EntityId = entityId,
            DocumentTypeId = documentTypeId
          },
          token
        )
        .Match(
          x =>
            Ok(x),
          ex => StatusCode(500, ex));
    }
  }
}