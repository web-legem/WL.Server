using LanguageExt;
using System;
using System.Linq;
using WL.Application.Interfaces.Persistance;

using static LanguageExt.Prelude;

namespace WL.Application.Documents.Queries {

  public class SearchDocumentsQuery {
    readonly IDocumentRepository repository;

    public SearchDocumentsQuery(IDocumentRepository repository) {
      this.repository = repository;
    }

    public Try<IQueryable<DocumentFileDto>> Execute(SearchDocumentsMessage msg) {
      Func<IQueryable<DocumentFileDto>> query = () =>
        repository.Search(msg).Select(x =>
            x.ToDocumentFileDto()
          );

      return Try(query);
    }
  }
}