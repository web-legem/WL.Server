//using Microsoft.AspNetCore.Mvc;

//using WL.Application.EntityTypes.Commands;
//using WL.Application.EntityTypes.Queries;

//namespace WL.Api.Controllers {
//   [Produces("application/json")]
//   [Route("api/EntityType")]
//   public class EntityTypeController : Controller {
//      readonly CreateEntityTypeCommandHandler createCommand;
//      readonly GetAllEntityTypesQuery getAllQuery;
//      readonly GetEntityTypeQuery getOneQuery;
//      readonly UpdateEntityTypeCommandHandler updateCommand;
//      readonly DeleteEntityTypeCommandHandler deleteCommand;

//      public EntityTypeController(
//          CreateEntityTypeCommandHandler createCommand,
//          GetAllEntityTypesQuery getAllQuery,
//          GetEntityTypeQuery getOneQuery,
//          UpdateEntityTypeCommandHandler updateCommand,
//          DeleteEntityTypeCommandHandler deleteCommand) {
//         this.createCommand = createCommand;
//         this.getAllQuery = getAllQuery;
//         this.getOneQuery = getOneQuery;
//         this.updateCommand = updateCommand;
//         this.deleteCommand = deleteCommand;
//      }

//      [HttpGet("{id}")]
//      public ActionResult Get(long id)
//         => getOneQuery.Execute(id).Match(
//            Succ: Ok,
//            Fail: ex => StatusCode(500, ex));

//      [HttpGet]
//      public IActionResult GetAll()
//          => getAllQuery.Execute().Match(
//             Succ: Ok,
//             Fail: ex => StatusCode(500, ex));

//      [HttpPost]
//      public IActionResult Post([FromBody] CreateEntityTypeCmd msg)
//          => createCommand.Execute(msg).Match(
//             Succ: x => x.Match<IActionResult>(Ok, BadRequest),
//             Fail: ex => StatusCode(500, ex));

//      [HttpPut]
//      public IActionResult Put([FromBody] UpdateTipoEntidadCmd msg)
//          => updateCommand.Execute(msg).Match(
//             Succ: x => x.Match<IActionResult>(Ok, BadRequest),
//             Fail: ex => StatusCode(500, ex));

//      [HttpDelete("{id}")]
//      public IActionResult Delete(long id)
//          => deleteCommand.Execute(id).Match(
//             Succ: _ => Ok() as IActionResult,
//             Fail: ex => StatusCode(500, ex));
//   }
//}