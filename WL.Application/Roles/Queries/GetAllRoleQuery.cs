using LanguageExt;

using System.Collections.Generic;
using System.Linq;

using WL.Application.Interfaces.Persistance;

namespace WL.Application.Roles.Queries {

   public class GetAllRolesQuery {
      private readonly IRoleRepository _repository;

      public GetAllRolesQuery(IRoleRepository repository) {
         _repository = repository;
      }

      public Try<IEnumerable<RoleDto>> Execute()
         => ()
         => _repository.GetAll().Select(x => x.ToRoleDTO()).ToSeq();
   }
}