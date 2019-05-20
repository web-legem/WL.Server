using LanguageExt;
using WL.Application.Interfaces.Persistance;
using static LanguageExt.Prelude;

namespace WL.Application.Documents.Commands {

  public class DeleteDocumentCommandHandler {
    readonly IDocumentRepository repository;

    public DeleteDocumentCommandHandler(IDocumentRepository repository) {
      this.repository = repository;
    }

    public Try<Unit> Execute(long id)
       => ()
       => fun((long x) => repository.Delete(x))(id);
  }
}