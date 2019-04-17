//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.IO;
//using WL.Application.AnnotationTypes.Queries;

//namespace WL.Api.Controllers {
//   [Produces("application/json")]
//   [Route("api/Resources")]
//   public class ResourcesController : Controller {
//      readonly GetAnnotationTypeQuery getOneQuery;
//      readonly IHostingEnvironment hostingEnvironment;

//      public ResourcesController(GetAnnotationTypeQuery getOneQuery, IHostingEnvironment hostingEnvironment) {
//         this.getOneQuery = getOneQuery;
//         this.hostingEnvironment = hostingEnvironment;

//      }

//      [HttpGet("{fileName}")]
//      public IActionResult Get(String fileName) {
//         var rootPath = hostingEnvironment.WebRootPath;
//         FileInfo fullPath = new FileInfo(Path.Combine(rootPath, "recursos", fileName));
//         DirectoryInfo fileInfo = new DirectoryInfo(fullPath.FullName);
//         FileStream fileStream = null;
//         if (System.IO.File.Exists(fullPath.FullName)) {
//            fileStream = new FileStream(fullPath.FullName, FileMode.Open);
//         }

//         return File(fileStream, "image/jpeg");

//      }
//   }
//}