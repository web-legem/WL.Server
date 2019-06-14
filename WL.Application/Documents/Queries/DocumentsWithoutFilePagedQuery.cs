using LanguageExt;
using System.Linq;
using WL.Application.Common;
using WL.Application.Interfaces.Persistance;

namespace WL.Application.Documents.Queries {

  public class DocumentsWithoutFilePagedQuery {
    readonly IDocumentRepository repository;

    public DocumentsWithoutFilePagedQuery(IDocumentRepository repository) {
      this.repository = repository;
    }

    public Try<PagedResult<DocumentWithoutFileDto>> Execute(DocumentsWithoutFilePageMessage msg, string token)
      => ()
      => {
        var pagedDocuments = repository.GetPageOfDocumentsWithoutFile(msg, token);
        return new PagedResult<DocumentWithoutFileDto> {
          Count = pagedDocuments.Count,
          Page = pagedDocuments.Page.Select(x => x.ToDocumentWithoutFileDto())
        };
      };
  }
}