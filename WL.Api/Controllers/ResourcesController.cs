using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using WL.Application.AnnotationTypes.Queries;

namespace WL.Api.Controllers {

  [Produces("application/json")]
  [Route("api/Resources")]
  public class ResourcesController : Controller {
    readonly IHostingEnvironment hostingEnvironment;

    public ResourcesController(IHostingEnvironment hostingEnvironment) {
      this.hostingEnvironment = hostingEnvironment;
    }

    [HttpGet("{fileName}")]
    public IActionResult Get(String fileName) {
      var rootPath = hostingEnvironment.WebRootPath;
      // TODO - cuadrar la carpeta correcta
      var fullPath = new FileInfo(Path.Combine(rootPath, "recursos", fileName));
      FileStream fileStream = null;

      if (System.IO.File.Exists(fullPath.FullName)) {
        fileStream = new FileStream(fullPath.FullName, FileMode.Open);
      }

      return File(fileStream, "image/jpeg");
    }
  }
}