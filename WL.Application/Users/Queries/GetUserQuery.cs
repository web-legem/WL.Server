using LanguageExt;
using WL.Application.Interfaces.Persistance;

namespace WL.Application.Users.Queries {

   public class GetUserQuery {
      private readonly IUserRepository _repository;

      public GetUserQuery(IUserRepository repository) {
         _repository = repository;
      }

      public Try<UserDto> Execute(long id)
         => ()
         => _repository.Get(id).ToUserDTO();

   }
}