using System.Linq;
using WL.Domain;

namespace WL.Application.Interfaces.Persistance {

  public interface IEntityRepository : IRepository<Entity> {

    IQueryable<Entity> GetEntitiesIn(long[] ids);
  }
}