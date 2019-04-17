using LanguageExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WL.Application.Interfaces.Persistance;
using static LanguageExt.Prelude;

namespace WL.Application.DocumentTypes.Queries {

  public class GetAllDocumentTypesQuery {
    readonly IDocumentTypeRepository repository;

    public GetAllDocumentTypesQuery(IDocumentTypeRepository repository) {
      this.repository = repository;
    }

    public Try<IQueryable<DocumentTypeDto>> Execute() {
      Func<IQueryable<DocumentTypeDto>> query =
        () => repository
        .GetAll()
        .Select(x => x.ToDocumentTypeDto());

      return Try(query);
    }
  }
}