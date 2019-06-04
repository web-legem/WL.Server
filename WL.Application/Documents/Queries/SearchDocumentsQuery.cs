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

    public Try<IQueryable<AnnotatedDocument>> Execute(SearchDocumentsMessage msg) {
      Func<IQueryable<AnnotatedDocument>> query = () =>
        repository.Search(msg);

      return Try(query);
    }
  }
}