using System;
using System.Collections.Generic;
using System.Linq;
using WL.Application.Interfaces.Persistance;
using WL.Domain.User;

namespace WL.Persistance.Users {

  public class UserRepository : IUserRepository {
    readonly WLDbContext context;

    public UserRepository(WLDbContext context) {
      this.context = context;
    }

    public User Get(string nickname) {
      return context.Users.FirstOrDefault(u => u.Nickname == nickname);
    }

    public User Get(long id) {
      return context.Users.Find(id);
    }

    public IQueryable<User> GetAll() {
      return context.Users;
    }

    public User Create(User user) {
      context.Users.Add(user);
      context.SaveChanges();
      return user;
    }

    public User Update(User entity) {
      var original = context.Users.Find(entity.Id);
      original.Nickname = entity.Nickname;
      original.LastName = entity.LastName;
      original.Password = entity.Password;
      original.IDDocument = entity.IDDocument;
      original.RoleId = entity.RoleId;
      original.FirstName = entity.FirstName;
      original.State = entity.State;
      original.Email = entity.Email;
      context.SaveChanges();
      return original;
    }

    public void Delete(long id) {
      var original = context.Users.Find(id);
      context.Users.Remove(original);
      context.SaveChanges();
    }
  }
}