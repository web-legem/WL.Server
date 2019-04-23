﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using WL.Application.Documents.Commands;
using WL.Application.Documents.Queries;

namespace WL.Api.Controllers {

  [Produces("application/json")]
  [Route("api/Document")]
  public class DocumentController : Controller {
    readonly CreateDocumentCommandHandler _createCommandHandler;
    readonly SearchDocumentsQuery searchDocumentsQuery;
    readonly SearchCountQuery searchCountQuery;

    public DocumentController(
      CreateDocumentCommandHandler createCommandHandler,
      SearchDocumentsQuery searchDocumentsQuery,
      SearchCountQuery searchCountQuery
      ) {
      _createCommandHandler = createCommandHandler;
      this.searchDocumentsQuery = searchDocumentsQuery;
      this.searchCountQuery = searchCountQuery;
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
  }
}