using Microsoft.AspNetCore.Mvc;
using WL.Application.Documents.Queries;
using WL.Application.DocumentTypes.Queries;
using WL.Application.EntityTypes.Queries;

namespace WL.Api.Controllers {

   [Produces("application/json")]
   [Route("api/Search")]
   public class SearchController : Controller {

      readonly GetAllDocumentTypesQuery getAllDTQuery;
      readonly GetAllEntityTypesQuery getAllETQuery;
      readonly SearchCountQuery searchCountQuery;
      readonly SearchDocumentsQuery searchDocumentsQuery;
      readonly DownloadFileQuery downloadFileQuery;


      public SearchController(
          GetAllDocumentTypesQuery getAllDTQuery,
          GetAllEntityTypesQuery getAllETQuery,
         SearchCountQuery searchCountQuery,
         SearchDocumentsQuery searchDocumentsQuery,
         DownloadFileQuery downloadFileQuery
         ) {
         this.getAllDTQuery = getAllDTQuery;
         this.getAllETQuery = getAllETQuery;
         this.searchCountQuery = searchCountQuery;
         this.searchDocumentsQuery = searchDocumentsQuery;
         this.downloadFileQuery = downloadFileQuery;

      }

      //tipos documentos   _____________________________________
      [HttpGet("documentTypes")]
      public IActionResult GetAllDT()
          => getAllDTQuery.Execute().Match(
            Succ: Ok,
            Fail: ex => StatusCode(500, ex));

      //tipos entidad      _____________________________________
      [HttpGet("entities")]
      public IActionResult GetAllET()
          => getAllETQuery.Execute().Match(
            Succ: Ok,
            Fail: ex => StatusCode(500, ex));

      //Numero de Paginas  _____________________________________

      [HttpGet("count")]
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


      //documentos
      [HttpGet]
      public IActionResult searchDocuments(long? page, long? pageSize, string wordsToSearch, long? publicationDate,
        string number, string orderBy = "DEFAULT", bool descend = false, long? entityId = null,
        long? documentTypeId = null) {
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

      //descarga de pdf    _____________________________________

      [HttpGet("file/download/{id}")]
      public IActionResult DownloadFile(long id) {
         return downloadFileQuery
               .Execute(id)
               .Match(
                  x => (IActionResult)File(x, "application/pdf"),
                  ex => StatusCode(500, ex));
      }

   }
}