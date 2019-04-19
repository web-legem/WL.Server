using Microsoft.AspNetCore.Mvc;

using WL.Application.AnnotationTypes.Commands;
using WL.Application.AnnotationTypes.Queries;

namespace WL.Api.Controllers {

  [Produces("application/json")]
  [Route("api/AnnotationType")]
  public class AnnotationTypeController : Controller {
    readonly CreateAnnotationTypeCommandHandler createCommand;
    readonly GetAllAnnotationTypesQuery getAllQuery;
    readonly GetAnnotationTypeQuery getOneQuery;
    readonly UpdateAnnotationTypeCommandHandler updateCommand;
    readonly DeleteAnnotationTypeCommandHandler deleteCommand;

    public AnnotationTypeController(
        CreateAnnotationTypeCommandHandler createCommand,
        GetAllAnnotationTypesQuery getAllQuery,
        GetAnnotationTypeQuery getOneQuery,
        UpdateAnnotationTypeCommandHandler updateCommand,
        DeleteAnnotationTypeCommandHandler deleteCommand) {
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
    public IActionResult Post([FromBody] CreateAnnotationTypeCommand cmd)
       => createCommand.Execute(cmd).Match(
          Succ: x => x.Match<IActionResult>(Ok, BadRequest),
          Fail: ex => StatusCode(500, ex));

    [HttpPut]
    public IActionResult Put([FromBody] UpdateAnnotationTypeCommand cmd)
        => updateCommand.Execute(cmd).Match(
          Succ: x => x.Match<IActionResult>(Ok, BadRequest),
          Fail: ex => StatusCode(500, ex));

    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
        => deleteCommand.Execute(id).Match(
          Succ: _ => Ok() as IActionResult,
          Fail: err => StatusCode(500, err));
  }
}