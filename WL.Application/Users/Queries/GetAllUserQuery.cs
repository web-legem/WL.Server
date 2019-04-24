using LanguageExt;

using System.Collections.Generic;
using System.Linq;

using WL.Application.Interfaces.Persistance;

namespace WL.Application.Users.Queries {

   public class GetAllUsersQuery {
      private readonly IUserRepository _repository;

      public GetAllUsersQuery(IUserRepository repository) {
         _repository = repository;
      }

      public Try<IEnumerable<UserDto>> Execute()
         => ()
         => _repository.GetAll().Select(x => x.ToUserDTO()).ToSeq();
   }
}