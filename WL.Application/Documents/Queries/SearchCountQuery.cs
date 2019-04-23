using LanguageExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WL.Application.Interfaces.Persistance;

using static LanguageExt.Prelude;

namespace WL.Application.Documents.Queries {

  public class SearchCountQuery {
    readonly IDocumentRepository repository;

    public SearchCountQuery(IDocumentRepository repository) {
      this.repository = repository;
    }

    public Try<long?> Execute(SearchDocumentsMessage msg) {
      Func<long?> query = () =>
        repository.SearchCount(
          msg.WordsToSearch,
          msg.EntityId,
          msg.DocumentTypeId,
          msg.Number,
          msg.Year);

      return Try(query);
    }
  }
}