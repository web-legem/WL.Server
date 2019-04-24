using LanguageExt;
using WL.Application.Interfaces.Persistance;
using static LanguageExt.Prelude;

namespace WL.Application.Roles.Commands {

   public class DeleteRoleCommandHandler {
      readonly IRoleRepository _repository;

      public DeleteRoleCommandHandler(IRoleRepository repository) {
         _repository = repository;
      }

      public Try<Unit> Execute(long id) {
         return () => fun((long x) => _repository.Delete(x))(id);
      }
      
   }
}