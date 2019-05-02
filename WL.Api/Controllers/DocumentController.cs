using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using WL.Application.Annotations.Queries;
using WL.Application.Documents.Commands;
using WL.Application.Documents.Queries;

namespace WL.Api.Controllers {

  [Produces("application/json")]
  [Route("api/Document")]
  public class DocumentController : Controller {
    readonly CreateDocumentCommandHandler _createCommandHandler;
    readonly SearchDocumentsQuery searchDocumentsQuery;
    readonly SearchCountQuery searchCountQuery;
    readonly DownloadFileQuery downloadFileQuery;
    readonly GetOneDocumentQuery getOneQuery;

    public DocumentController(
      CreateDocumentCommandHandler createCommandHandler,
      SearchDocumentsQuery searchDocumentsQuery,
      SearchCountQuery searchCountQuery,
      DownloadFileQuery downloadFileQuery,
      GetOneDocumentQuery getOneQuery
    ) {
      _createCommandHandler = createCommandHandler;
      this.searchDocumentsQuery = searchDocumentsQuery;
      this.searchCountQuery = searchCountQuery;
      this.downloadFileQuery = downloadFileQuery;
      this.getOneQuery = getOneQuery;
    }

    [HttpPost]
    public IActionResult CreateDocument(
       [ModelBinder(BinderType = typeof(JsonModelBinder))] CreateDocumentCommand value,
       IList<IFormFile> files) {
      Stream stream = null;

      if (files != null && files.Length() > 0) {
        var file = files[0];
        stream = file.OpenReadStream();
        value.File = stream;
      }

      return _createCommandHandler.Execute(value)
         .Match(x => x.Match<IActionResult>(Ok, BadRequest),
            ex => StatusCode(500, ex));
    }

    [HttpGet("{id}")]
    public IActionResult GetOne(long id)
      => getOneQuery.Execute(id).Match(
        Succ: x =>
        Ok(x),
        Fail: e =>
        StatusCode(StatusCodes.Status500InternalServerError, e)
        );

    [HttpGet("search")]
    public IActionResult searchDocuments(long? page, long? pageSize, string wordsToSearch, long? publicationDate,
      string number, string orderBy = "DEFAULT", bool descend = false, long? entityId = null,
      long? documentTypeId = null
    ) {
      return searchDocumentsQuery.Execute(
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
          }
        )
        .Match(
          x =>
            Ok(x),
          ex => StatusCode(500, ex));
    }

    [HttpGet("search/count")]
    public IActionResult searchCount(
     string wordsToSearch,
     long? publicationDate,
     string number,
     long? entityId = null,
     long? documentTypeId = null
  ) {
      return searchCountQuery.Execute(
          new SearchDocumentsMessage {
            WordsToSearch = wordsToSearch,
            Year = publicationDate,
            Number = number,
            EntityId = entityId,
            DocumentTypeId = documentTypeId
          }
        )
        .Match(
          x =>
            Ok(x),
          ex => StatusCode(500, ex));
    }

    [HttpGet("download/{id}")]
    public IActionResult DownloadFile(long id) {
      return downloadFileQuery
            .Execute(id)
            .Match(
               x => (IActionResult)File(x, "application/pdf"),
               ex => StatusCode(500, ex));
    }
  }
}