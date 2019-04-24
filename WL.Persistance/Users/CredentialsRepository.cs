using System.Linq;
using WL.Application.Interfaces.Persistance;
using WL.Domain.User;

namespace WL.Persistance.Users {

  public class CredentialsRepository : ICredentialRepository {
    readonly WLDbContext context;

    public CredentialsRepository(WLDbContext context) {
      this.context = context;
    }

    public Credential Get(long id) {
      return context.Credentials.Find(id);
    }

    public IQueryable<Credential> GetAll() {
      return context.Credentials;
    }

    public Credential Create(Credential entity) {
      var original = context.Credentials.FirstOrDefault(c => c.UserId == entity.UserId);
      if (original != null) {
        return Update(entity);
      }
      context.Credentials.Add(entity);
      context.SaveChanges();
      return entity;
    }

    public Credential Update(Credential entity) {
      var original = context.Credentials.Find(entity.UserId);
      original.Token = entity.Token;
      original.Creation = entity.Creation;
      context.SaveChanges();
      return original;
    }

    public void Delete(long id) {
      var original = context.Credentials.Find(id);
      context.Credentials.Remove(original);
      context.SaveChanges();
    }
  }
}