using Microsoft.AspNetCore.Mvc;
using WL.Application.Annotations.Queries;
using WL.Application.AnnotationTypes.Queries;
using WL.Application.Documents.Queries;
using WL.Application.DocumentTypes.Queries;
using WL.Application.Entities.Queries;
using WL.Application.EntityTypes.Queries;

namespace WL.Api.Controllers {

   [Produces("application/json")]
   [Route("api/Search")]
   public class SearchController : Controller {
      readonly GetAllDocumentTypesQuery getAllDTQuery;
      readonly GetAllEntitiesQuery getAllEQuery;
      readonly SearchCountQuery searchCountQuery;
      readonly SearchDocumentsQuery searchDocumentsQuery;
      readonly DownloadFileQuery downloadFileQuery;
      readonly GetAllAnnotationTypesQuery getAllATQuery;
      readonly GetDocumentAnnotationsQuery getDocAnnotationsQuery;
      readonly GetOneDocumentQuery getOneDocumentQuery;

      public SearchController(
        GetAllDocumentTypesQuery getAllDTQuery,
        GetAllEntitiesQuery getAllETQuery,
       SearchCountQuery searchCountQuery,
       SearchDocumentsQuery searchDocumentsQuery,
       DownloadFileQuery downloadFileQuery,
       GetAllAnnotationTypesQuery getAllATQuery,
       GetDocumentAnnotationsQuery getDocAnnotationsQuery,
       GetOneDocumentQuery getOneDocumentQuery
       ) {
         this.getAllDTQuery = getAllDTQuery;
         this.getAllEQuery = getAllETQuery;
         this.searchCountQuery = searchCountQuery;
         this.searchDocumentsQuery = searchDocumentsQuery;
         this.downloadFileQuery = downloadFileQuery;
         this.getAllATQuery = getAllATQuery;
         this.getDocAnnotationsQuery = getDocAnnotationsQuery;
         this.getOneDocumentQuery = getOneDocumentQuery;
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
          => getAllEQuery.Execute().Match(
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


      //consultar las anotaciones ______________________________

      [HttpGet("docAnnotations/{id}")]
      public IActionResult GetDocumentAnnotations(long id)
     => getDocAnnotationsQuery
       .Execute(id)
       .Match(
         Succ: x =>
         Ok(x),
         Fail: err =>
        StatusCode(500, err)
       );

      //Tipos de anotacion  _____________________________________

      [HttpGet("annotationTypes")]
      public IActionResult annotationTypes() {
         return getAllATQuery
               .Execute()
               .Match(
                  x => Ok(x),
                  ex => StatusCode(500, ex));
      }


      [HttpGet("documentInfo/{id}")]
      public IActionResult GetOneDocumene(long id)
      => getOneDocumentQuery.Execute(id).Match(
        Succ: x => Ok(x),
        Fail: e => StatusCode(500, e));

   }
}