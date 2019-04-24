using LanguageExt;
using WL.Application.Interfaces.Persistance;

namespace WL.Application.Roles.Queries {

   public class GetRoleQuery {
      private readonly IRoleRepository _repository;

      public GetRoleQuery(IRoleRepository repository) {
         _repository = repository;
      }

      public Try<RoleDto> Execute(long id)
         => ()
         => _repository.Get(id).ToRoleDTO();

      public Try<RoleDto> Execute(string token)
         => ()
         => _repository.Get(token).ToRoleDTO();

   }
}