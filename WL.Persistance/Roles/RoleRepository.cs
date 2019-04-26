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
      try {
        // TODO - first lanzara error si no hay un token como el recivido
        var credential = context.Credentials
       .Include(c => c.User)
       .First(c => c.Token == token);

        var role = context.Roles.First(r => credential.User.RoleId == r.Id);
        return role;
      } catch (Exception e) {
        throw ExceptionsToValidations.ExceptionsToValidations.WrapOracleExceptionsWithError(e);
      }
    }

    public Role Get(long id) {
      try {
        return context.Roles.Find(id);
      } catch (Exception e) {
        throw ExceptionsToValidations.ExceptionsToValidations.WrapOracleExceptionsWithError(e);
      }
    }

    public IQueryable<Role> GetAll() {
      try {
      } catch (Exception e) {
        throw ExceptionsToValidations.ExceptionsToValidations.WrapOracleExceptionsWithError(e);
      }
      return context.Roles;
    }

    public Role Create(Role entity) {
      try {
        context.Roles.Add(entity);
        context.SaveChanges();
        return entity;
      } catch (Exception e) {
        throw ExceptionsToValidations.ExceptionsToValidations.WrapOracleExceptionsWithError(e);
      }
    }

    public Role Update(Role entity) {
      try {
        var original = context.Roles.Find(entity.Id);
        original.Name = entity.Name;
        original.CreateDocuments = entity.CreateDocuments;
        original.DeleteDocuments = entity.DeleteDocuments;
        original.ConfigSystem = entity.ConfigSystem;
        context.SaveChanges();
        return original;
      } catch (Exception e) {
        throw ExceptionsToValidations.ExceptionsToValidations.WrapOracleExceptionsWithError(e);
      }
    }

    public void Delete(long id) {
      try {
        var original = context.Roles.Find(id);
        context.Roles.Remove(original);
        context.SaveChanges();
      } catch (Exception e) {
        throw ExceptionsToValidations.ExceptionsToValidations.WrapOracleExceptionsWithError(e);
      }
    }
  }
}