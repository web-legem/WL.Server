﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using WL.Api.Infrastructure;
using WL.Application.Annotations.Queries;
using WL.Application.Documents.Commands;
using WL.Application.Documents.Queries;
using WL.Application.DocumentTypes.Queries;
using WL.Application.Entities.Queries;
using static WL.Api.Infrastructure.PermissionsAttribute;

namespace WL.Api.Controllers {

  [Produces("application/json")]
  [Route("api/Document")]
  [Permissions(MapPerm.CreateDocument)]
  public class DocumentController : Controller {
    readonly CreateDocumentCommandHandler _createCommandHandler;
    readonly SearchDocumentsQuery searchDocumentsQuery;
    readonly SearchCountQuery searchCountQuery;
    readonly DownloadFileQuery downloadFileQuery;
    readonly GetOneDocumentQuery getOneQuery;
    readonly GetAllDocumentTypesQuery getDTsQuery;
    readonly GetAllEntitiesQuery getEsQuery;
    //readonly SendNotificationCommandHGandler sendNotificationCommand;

    public DocumentController(
      CreateDocumentCommandHandler createCommandHandler,
      SearchDocumentsQuery searchDocumentsQuery,
      SearchCountQuery searchCountQuery,
      DownloadFileQuery downloadFileQuery,
      GetOneDocumentQuery getOneQuery,
      GetAllDocumentTypesQuery getDTsQuery,
      GetAllEntitiesQuery getEsQuery
      //SendNotificationCommandHGandler sendNotificationCommand) {
      ) {
      _createCommandHandler = createCommandHandler;
      this.searchDocumentsQuery = searchDocumentsQuery;
      this.searchCountQuery = searchCountQuery;
      this.downloadFileQuery = downloadFileQuery;
      this.getOneQuery = getOneQuery;
      this.getDTsQuery = getDTsQuery;
      this.getEsQuery = getEsQuery;
      //this.sendNotificationCommand = sendNotificationCommand;
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

    //[HttpPost("notify/{documentId}")]
    //public IActionResult Notify([FromBody] string[] emails, long documentId)
    //    => sendNotificationCommand.Execute(emails, documentId).Match(
    //      Succ: _ => Ok() as IActionResult,
    //      Fail: err => StatusCode(500, err));
  }
}