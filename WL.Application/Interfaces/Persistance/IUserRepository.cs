using WL.Domain.User;

namespace WL.Application.Interfaces.Persistance {

  public interface IUserRepository
      : IRepository<User> {

    User Get(string nickname);
  }
}