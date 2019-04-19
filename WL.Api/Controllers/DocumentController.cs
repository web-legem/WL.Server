using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using WL.Application.Documents.Commands;

namespace WL.Api.Controllers {

  [Produces("application/json")]
  [Route("api/Document")]
  public class DocumentController : Controller {
    readonly CreateDocumentCommandHandler _createCommandHandler;

    public DocumentController(
       CreateDocumentCommandHandler createCommandHandler
       ) {
      _createCommandHandler = createCommandHandler;
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
  }
}