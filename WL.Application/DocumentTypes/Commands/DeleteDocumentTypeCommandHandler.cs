using LanguageExt;
using System;
using System.Collections.Generic;
using System.Text;
using WL.Application.Interfaces.Persistance;
using static LanguageExt.Prelude;

namespace WL.Application.DocumentTypes.Commands {

  public class DeleteDocumentTypeCommandHandler {
    readonly IDocumentTypeRepository repository;

    public DeleteDocumentTypeCommandHandler(IDocumentTypeRepository repository) {
      this.repository = repository;
    }

    public Try<Unit> Execute(long id)
      => ()
      => fun((long x) => repository.Delete(x))(id);
  }
}