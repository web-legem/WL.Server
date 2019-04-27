using System;
using System.Linq;
using WL.Application.Interfaces.Persistance;
using WL.Domain.User;
using static WL.Persistance.ExceptionsToValidations.ExceptionsToValidations;

namespace WL.Persistance.Users {

  public class CredentialsRepository : ICredentialRepository {
    readonly WLDbContext context;

    public CredentialsRepository(WLDbContext context) {
      this.context = context;
    }

    public Credential Get(long id) {
      try {
        return context.Credentials.Find(id);
      } catch (Exception e) {
        throw WrapOracleException(e);
      }
    }

    public IQueryable<Credential> GetAll() {
      try {
        return context.Credentials;
      } catch (Exception e) {
        throw WrapOracleException(e);
      }
    }

    public Credential Create(Credential entity) {
      try {
        var original = context.Credentials.FirstOrDefault(c => c.UserId == entity.UserId);
        if (original != null) {
          return Update(entity);
        }
        context.Credentials.Add(entity);
        context.SaveChanges();
        return entity;
      } catch (Exception e) {
        throw WrapOracleException(e);
      }
    }

    public Credential Update(Credential entity) {
      try {
        var original = context.Credentials.Find(entity.UserId);
        original.Token = entity.Token;
        original.Creation = entity.Creation;
        context.SaveChanges();
        return original;
      } catch (Exception e) {
        throw WrapOracleException(e);
      }
    }

    public void Delete(long id) {
      try {
        var original = context.Credentials.Find(id);
        context.Credentials.Remove(original);
        context.SaveChanges();
      } catch (Exception e) {
        throw WrapOracleException(e);
      }
    }
  }
}