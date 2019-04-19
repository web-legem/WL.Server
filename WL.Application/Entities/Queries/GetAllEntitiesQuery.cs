using LanguageExt;

using System;
using System.Linq;

using WL.Application.Interfaces.Persistance;

using static LanguageExt.Prelude;

namespace WL.Application.Entities.Queries {

  public class GetAllEntitiesQuery {
    readonly IEntityRepository _repository;

    public GetAllEntitiesQuery(IEntityRepository repository) {
      _repository = repository;
    }

    public Try<IQueryable<EntityDto>> Execute() {
      Func<IQueryable<EntityDto>> action = ()
        => _repository
        .GetAll()
        .Select(x => x.ToEntityDto());

      return Try(action);
    }
  }
}