using LanguageExt;
using System;
using System.Collections.Generic;
using System.Text;
using WL.Application.Documents.Queries;
using WL.Application.Interfaces.Persistance;

using static LanguageExt.Prelude;

namespace WL.Application.Annotations.Queries {

  public class SearchDocumentsToAnnotateCountQuery {
    readonly IDocumentRepository repository;

    public SearchDocumentsToAnnotateCountQuery(IDocumentRepository repository) {
      this.repository = repository;
    }

    public Try<long?> Execute(SearchDocumentsMessage msg, string token) {
      Func<long?> query = () =>
        repository.SearchCountToAnnotate(
          msg.WordsToSearch,
          msg.EntityId,
          msg.DocumentTypeId,
          msg.Number,
          msg.Year,
          token);

      return Try(query);
    }
  }
}