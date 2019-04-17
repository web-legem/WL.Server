//using Microsoft.AspNetCore.Mvc;

//using WL.Application.Entities.Commands;
//using WL.Application.Entities.Queries;

//namespace WL.Api.Controllers {
//   [Produces("application/json")]
//   [Route("api/Entity")]
//   public class EntityController : Controller {
//      readonly CreateEntityCommandHandler createCommand;
//      readonly GetAllEntitiesQuery getAllQuery;
//      readonly GetEntityQuery getOneQuery;
//      readonly UpdateEntityCommandHandler updateCommand;
//      readonly DeleteEntityCommandHandler deleteCommand;

//      public EntityController(CreateEntityCommandHandler createCommand,
//          GetAllEntitiesQuery getAllQuery,
//          GetEntityQuery getOneQuery,
//          UpdateEntityCommandHandler updateCommand,
//          DeleteEntityCommandHandler deleteCommand) {
//         this.createCommand = createCommand;
//         this.getAllQuery = getAllQuery;
//         this.getOneQuery = getOneQuery;
//         this.updateCommand = updateCommand;
//         this.deleteCommand = deleteCommand;
//      }

//      [HttpGet("{id}")]
//      public IActionResult Get(long id)
//         => getOneQuery.Execute(id).Match(
//            Succ: Ok,
//            Fail: ex => StatusCode(500, ex));

//      [HttpGet]
//      public IActionResult GetAll()
//          => getAllQuery.Execute().Match(
//            Succ: Ok,
//            Fail: ex => StatusCode(500, ex));

//      [HttpPost]
//      public IActionResult Post([FromBody] CreateEntityCmd msg)
//          => createCommand.Execute(msg).Match(
//            Succ: x => x.Match<IActionResult>(Ok, BadRequest),
//            Fail: ex => StatusCode(500, ex));

//      [HttpPut]
//      public IActionResult Put([FromBody] UpdateEntityCmd msg)
//          => updateCommand.Execute(msg).Match(
//            Succ: x => x.Match<IActionResult>(Ok, BadRequest),
//            Fail: ex => StatusCode(500, ex));

//      [HttpDelete("{id}")]
//      public IActionResult Delete(long id)
//          => deleteCommand.Execute(id).Match(
//            Succ: _ => Ok() as IActionResult,
//            Fail: err => StatusCode(500, err));
//   }
//}