using Microsoft.AspNetCore.Mvc;

using WL.Application.DocumentTypes.Commands;
using WL.Application.DocumentTypes.Queries;

namespace WL.Api.Controllers {

  [Produces("application/json")]
  [Route("api/DocumentType")]
  public class DocumentTypeController : Controller {
    readonly CreateDocumentTypeCommandHandler createCommand;
    readonly GetAllDocumentTypesQuery getAllQuery;
    readonly GetDocumentTypeQuery getOneQuery;
    readonly UpdateDocumentTypeCommandHandler updateCommand;
    readonly DeleteDocumentTypeCommandHandler deleteCommand;

    public DocumentTypeController(
        CreateDocumentTypeCommandHandler createCommand,
        GetAllDocumentTypesQuery getAllQuery,
        GetDocumentTypeQuery getOneQuery,
        UpdateDocumentTypeCommandHandler updateCommand,
        DeleteDocumentTypeCommandHandler deleteCommand) {
      this.createCommand = createCommand;
      this.getAllQuery = getAllQuery;
      this.getOneQuery = getOneQuery;
      this.updateCommand = updateCommand;
      this.deleteCommand = deleteCommand;
    }

    [HttpGet("{id}")]
    public IActionResult Get(long id) => getOneQuery
       .Execute(id)
       .Match(
          Ok,
          ex => StatusCode(500, ex));

    [HttpGet]
    public IActionResult GetAll()
        => getAllQuery.Execute().Match(
           Ok,
           ex => StatusCode(500, ex));

    [HttpPost]
    public IActionResult Post([FromBody] CreateDocumentTypeCommand cmd)
        => createCommand.Execute(cmd).Match(
           x => x.Match<IActionResult>(
              Ok,
              BadRequest),
           ex => StatusCode(500, ex));

    [HttpPut]
    public IActionResult Put([FromBody] UpdateDocumentTypeCommand cmd)
       => updateCommand.Execute(cmd).Match(
          x => x.Match<IActionResult>(
             Ok,
             BadRequest),
          ex => StatusCode(500, ex));

    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
        => deleteCommand
          .Execute(id)
          .Match(
           Succ: _ => Ok() as IActionResult,
           Fail: err => StatusCode(500, err));
  }
}