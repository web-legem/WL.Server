using LanguageExt;
using System;
using System.Linq;
using WL.Application.Interfaces.Persistance;
using static LanguageExt.Prelude;

namespace WL.Application.DocumentTypes.Queries {

  public class GetAllDocumentTypesByUserQuery {
    readonly IDocumentTypeRepository repository;

    public GetAllDocumentTypesByUserQuery(IDocumentTypeRepository repository) {
      this.repository = repository;
    }

    public Try<IQueryable<DocumentTypeDto>> Execute(string credentialToken) {
      Func<IQueryable<DocumentTypeDto>> query =
        () => repository
        .GetAllByUser(credentialToken)
        .Select(x => x.ToDocumentTypeDto());

      return Try(query);
    }
  }
}