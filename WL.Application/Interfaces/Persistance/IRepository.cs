using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WL.Application.Interfaces.Persistance {

  public interface IRepository<T> {

    T Get(long id);

    IQueryable<T> GetAll();

    T Create(T entity);

    T Update(T entity);

    void Delete(long id);
  }
}