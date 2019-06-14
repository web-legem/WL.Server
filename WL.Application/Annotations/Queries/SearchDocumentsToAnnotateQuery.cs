using LanguageExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WL.Application.Documents;
using WL.Application.Documents.Queries;
using WL.Application.Interfaces.Persistance;

using static LanguageExt.Prelude;

namespace WL.Application.Annotations.Queries {
  public class SearchDocumentsToAnnotateQuery {
    readonly IDocumentRepository repository;

    public SearchDocumentsToAnnotateQuery(IDocumentRepository repository) {
      this.repository = repository;
    }

    public Try<IQueryable<AnnotatedDocument>> Execute(SearchDocumentsMessage msg, string token) {
      Func<IQueryable<AnnotatedDocument>> query = () =>
        repository.SearchToAnnotate(msg, token);

      return Try(query);
    }
  }
}