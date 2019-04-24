using LanguageExt;
using WL.Application.Interfaces.Persistance;

using static LanguageExt.Prelude;

namespace WL.Application.Users.Commands {

  public class DeleteUserCommandHandler {
    readonly IUserRepository _repository;

    public DeleteUserCommandHandler(IUserRepository repository) {
      _repository = repository;
    }

    public Try<Unit> Execute(long id) {
      UserHelpers.DeleteFile(id);
      return () => fun((long x) => _repository.Delete(x))(id);
    }
  }
}