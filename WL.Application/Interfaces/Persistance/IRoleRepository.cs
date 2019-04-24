using WL.Domain.User;

namespace WL.Application.Interfaces.Persistance {

  public interface IRoleRepository
      : IRepository<Role> {

    Role Get(string token);
  }
}