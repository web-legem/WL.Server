using System;
using System.Collections.Generic;
using System.Text;
using WL.Domain;

namespace WL.Application.Interfaces.Persistance {

  public interface IEntityRepository : IRepository<Entity> {
  }
}