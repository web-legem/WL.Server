using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WL.Application.Interfaces.Persistance;
using WL.Domain.User;

namespace WL.Persistance.Users {

  public class RestoreRepository : IRestoreRepository {
    readonly WLDbContext context;

    public RestoreRepository(WLDbContext context) {
      this.context = context;
    }

    public long CreateRestoreToken(string email, string token) {
      var user = context.Users.FirstOrDefault(x => x.Email == email);
      if (user != null) {
        var original = context.Restores.FirstOrDefault(r => r.UserId == user.UserId);
        if (original != null) {
          original.Token = token;
          context.SaveChanges();
        } else {
          var restore = new Restore { UserId = user.UserId, User = user, Token = token };
          context.Restores.Add(restore);
          context.SaveChanges();
        }
        return user.UserId;
      }

      // TODO - que pasa si el usuario no existe?
      return 0;
    }

    public bool IsValidToken(long userId, string token) {
      // TODO - que hacer si no lo encuentra, validar
      var restore = context.Restores.Find(userId);
      return restore.Token == token;
    }

    public Restore Get(long id) => throw new NotImplementedException();

    public IQueryable<Restore> GetAll() => throw new NotImplementedException();

    public Restore Create(Restore entity) => throw new NotImplementedException();

    public Restore Update(Restore entity) => throw new NotImplementedException();

    public void Delete(long id) => throw new NotImplementedException();
  }
}