using System;
using WL.Domain.User;

namespace WL.Application.Interfaces.Persistance {

  public interface IRestoreRepository : IRepository<Restore> {

    long CreateRestoreToken(string email, string token);

    bool IsValidToken(long userId, string token);
  }
}