using System.Linq;

using WL.Application.Interfaces.Persistance;
using WL.Domain;

namespace WL.Persistance.Entities {

  public class EntityRepository : IEntityRepository {
    readonly WLDbContext context;

    public EntityRepository(WLDbContext context) {
      this.context = context;
    }

    public Entity Get(long id) {
      return context.Entities.Find(id);
    }

    public IQueryable<Entity> GetAll() {
      return context.Entities;
    }

    public Entity Create(Entity entity) {
      context.Entities.Add(entity);
      context.SaveChanges();
      return entity;
    }

    public Entity Update(Entity entity) {
      var original = context.Entities.Find(entity.Id);
      original.Name = entity.Name;
      original.Email = entity.Email;
      original.EntityTypeId = entity.EntityTypeId;
      context.SaveChanges();
      return original;
    }

    public void Delete(long id) {
      var original = context.Entities.Find(id);
      context.Entities.Remove(original);
      context.SaveChanges();
    }
  }
}