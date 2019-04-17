using LanguageExt;

using WL.Application.Interfaces.Persistance;

using static LanguageExt.Prelude;

namespace WL.Application.EntityTypes.Commands {

  public class DeleteEntityTypeCommandHandler {
    readonly IEntityTypeRepository repository;

    public DeleteEntityTypeCommandHandler(IEntityTypeRepository repository) {
      this.repository = repository;
    }

    public Try<Unit> Execute(long id)
      => ()
      => fun((long x) => repository.Delete(x))(id);
  }
}