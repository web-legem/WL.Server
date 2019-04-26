using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WL.Application.Interfaces.Persistance;
using WL.Domain.User;

namespace WL.Persistance.Roles {

  public class RoleRepository : IRoleRepository {
    readonly WLDbContext context;

    public RoleRepository(WLDbContext context) {
      this.context = context;
    }

    public Role Get(string token) {
      // TODO - first lanzara error si no hay un token como el recivido
      var credential = context.Credentials
        .Include(c => c.User)
        .First(c => c.Token == token);

      var role = context.Roles.First(r => credential.User.RoleId == r.Id);
      return role;
    }

    public Role Get(long id) {
      return context.Roles.Find(id);
    }

    public IQueryable<Role> GetAll() {
      return context.Roles;
    }

    public Role Create(Role entity) {
      context.Roles.Add(entity);
      context.SaveChanges();
      return entity;
    }

    public Role Update(Role entity) {
      var original = context.Roles.Find(entity.Id);
      original.CreateDocuments = entity.CreateDocuments;
      original.DeleteDocuments = entity.DeleteDocuments;
      original.ConfigSystem = entity.ConfigSystem;
      context.SaveChanges();
      return original;
    }

    public void Delete(long id) {
      var original = context.Roles.Find(id);
      context.Roles.Remove(original);
      context.SaveChanges();
    }
  }
}