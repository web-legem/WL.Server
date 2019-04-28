using LanguageExt;
using System;
using System.Collections.Generic;
using System.Text;
using WL.Application.Interfaces.Persistance;
using static LanguageExt.Prelude;

namespace WL.Application.Annotations.Commands {

  public class DeleteAnnotationCommandHandler {
    readonly IAnnotationRepository repository;

    public DeleteAnnotationCommandHandler(IAnnotationRepository repository) {
      this.repository = repository;
    }

    public Try<Unit> Execute(long id)
      => ()
      => fun((long x) => repository.Delete(x))(id);
  }
}