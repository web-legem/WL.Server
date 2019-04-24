using Microsoft.AspNetCore.Mvc;
using WL.Application.Users.Commands;
using WL.Application.Users.Queries;
using System.IO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System;
using static WL.Api.Infrastructure.PermissionsAttribute;
using WL.Api.Infrastructure;

namespace WL.Api.Controllers {

  [Produces("application/json")]
  [Route("api/User")]
  public class UserController : Controller {
    readonly CreateUserCommandHandler createCommand;
    readonly GetAllUsersQuery getAllQuery;
    readonly GetUserQuery getOneQuery;
    readonly DownloadPhotoQuery getPhotoQuery;
    readonly SessionQuery sessionQuery;
    readonly UpdateUserCommandHandler updateCommand;
    readonly DeleteUserCommandHandler deleteCommand;
    readonly IHostingEnvironment _hostingEnvironment;

    public UserController(
        CreateUserCommandHandler createCommand,
        GetAllUsersQuery getAllQuery,
        GetUserQuery getOneQuery,
        DownloadPhotoQuery getPhotoQuery,
        SessionQuery sessionQuery,
        UpdateUserCommandHandler updateCommand,
        DeleteUserCommandHandler deleteCommand,
        IHostingEnvironment environment) {
      this.createCommand = createCommand;
      this.getAllQuery = getAllQuery;
      this.getOneQuery = getOneQuery;
      this.getPhotoQuery = getPhotoQuery;
      this.sessionQuery = sessionQuery;
      this.updateCommand = updateCommand;
      this.deleteCommand = deleteCommand;
      _hostingEnvironment = environment;
    }

    [HttpGet("{id}")]
    [Permissions(MapPerm.ConfigSystem)]
    public IActionResult Get(long id) {
      return getOneQuery.Execute(id).Match(
         Succ: Ok,
         Fail: ex => StatusCode(500, ex));
    }

    [HttpPost("Login")]
    public IActionResult Login([FromBody] UserCredentialCmd data) {
      string address = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
      return sessionQuery.Execute(data, address).Match(
         Succ: x => x.Match<IActionResult>(Ok, BadRequest),
         Fail: ex => StatusCode(500, ex));
    }

    [HttpDelete("Logout/{id}")]
    public IActionResult Logout(long id) {
      return sessionQuery.Execute(id).Match(
         _ => Ok() as IActionResult,
         err => StatusCode(500, err));
    }

    [HttpGet]
    [Permissions(MapPerm.ConfigSystem)]
    public IActionResult GetAll()
        => getAllQuery.Execute().Match(
          Succ: Ok,
          Fail: ex => StatusCode(500, ex));

    [HttpGet("Photo")]
    public IActionResult DownloadFile([FromQuery]long id, [FromQuery]string mode) {
      //min = minuatura
      return getPhotoQuery.Execute(id, (!string.IsNullOrEmpty(mode) && mode.Equals("min"))).Match(
            x => (IActionResult)File(x, "image/jpeg"),
            ex => StatusCode(500, ex));
    }

    [HttpPost]
    [Permissions(MapPerm.ConfigSystem)]
    public IActionResult Post(
       [ModelBinder(BinderType = typeof(JsonModelBinder))] CreateUserCmd value,
       IList<IFormFile> files) {
      Stream stream = null;
      if (files != null && files.Length() > 0) {
        IFormFile file = files[0];
        stream = file.OpenReadStream();
      }

      return createCommand.Execute(value, stream).Match(
         Succ: x => x.Match<IActionResult>(Ok, BadRequest),
         Fail: ex => StatusCode(500, ex));
    }

    [HttpPut("UpdatePassword")]
    public IActionResult UpdatePassword([FromBody] UpdatePasswordCmd data, [FromQuery]String token) {
      return updateCommand.Execute(data, token).Match(
         Succ: x => x.Match<IActionResult>(Ok, BadRequest),
         Fail: ex => StatusCode(500, ex));
    }

    [HttpPut]
    [Permissions(MapPerm.ConfigSystem)]
    public IActionResult Put(
       [ModelBinder(
         BinderType = typeof(JsonModelBinder))] UpdateUserCmd value,
       IList<IFormFile> files,
       Boolean fileWasChange,
       Boolean restorePass
       ) {
      Stream stream = null;
      if (files != null && files.Length() > 0) {
        IFormFile file = files[0];
        stream = file.OpenReadStream();
      }

      return updateCommand.Execute(value, stream, fileWasChange, restorePass).Match(
         Succ: x => x.Match<IActionResult>(Ok, BadRequest),
         Fail: ex => StatusCode(500, ex));
    }

    [HttpDelete("{id}")]
    [Permissions(MapPerm.ConfigSystem)]
    public IActionResult Delete(long id) {
      return deleteCommand.Execute(id).Match(
         Succ: _ => Ok() as IActionResult,
         Fail: err => StatusCode(500, err));
    }

    [HttpGet("RestoreRequest")]
    public IActionResult RestoreRequest([FromQuery]string email, [FromHeader] string origin) {
      string address = $"{origin}/ingreso/actualizar-contrasena";
      return sessionQuery.ExecuteRestorePassword(email, address).Match(
         Succ: x => x ? Ok(x) : StatusCode(400, new Exception("INVALID")),
         Fail: ex => StatusCode(500, ex));
    }

    [HttpGet("VerifyToken")]
    public IActionResult VerifyToken([FromQuery]long id, [FromQuery]String token) {
      return sessionQuery.VerifyToken(id, token).Match(
         Succ: x => x ? Ok(x) : StatusCode(400, new Exception("INVALID")),
         Fail: ex => StatusCode(500, ex));
    }
  }
}