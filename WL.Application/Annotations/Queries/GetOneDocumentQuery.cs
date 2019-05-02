using LanguageExt;
using System;
using System.Collections.Generic;
using System.Text;
using WL.Application.Documents;
using WL.Application.Interfaces.Persistance;

namespace WL.Application.Annotations.Queries {

  public class GetOneDocumentQuery {
    readonly IDocumentRepository repository;

    public GetOneDocumentQuery(IDocumentRepository repository) {
      this.repository = repository;
    }

    public Try<DocumentFileDto> Execute(long id) {
      return () => repository.Get(id).ToDocumentFileDto();
    }
  }
}