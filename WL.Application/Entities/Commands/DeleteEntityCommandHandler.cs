using LanguageExt;

using WL.Application.Interfaces.Persistance;

using static LanguageExt.Prelude;

namespace WL.Application.Entities.Commands {

  public class DeleteEntityCommandHandler {
    readonly IEntityRepository _repository;

    public DeleteEntityCommandHandler(IEntityRepository repository) {
      _repository = repository;
    }

    public Try<Unit> Execute(long id)
       => ()
       => fun((long x) => _repository.Delete(x))(id);
  }
}