using System.Linq;

namespace WL.Application.Interfaces.Persistance {

  public interface IRepository<T> {

    T Get(long id);

    IQueryable<T> GetAll();

    T Create(T entity);

    T Update(T entity);

    void Delete(long id);
  }
}