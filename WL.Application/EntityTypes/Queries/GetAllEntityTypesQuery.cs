using LanguageExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WL.Application.Interfaces.Persistance;
using static LanguageExt.Prelude;

namespace WL.Application.EntityTypes.Queries {

  public class GetAllEntityTypesQuery {
    readonly IEntityTypeRepository repository;

    public GetAllEntityTypesQuery(IEntityTypeRepository repository) {
      this.repository = repository;
    }

    public Try<IQueryable<EntityTypeDto>> Execut() {
      Func<IQueryable<EntityTypeDto>> action =
        () => repository
        .GetAll()
        .Select(x => x.ToTipoEntidadDTO());

      return Try(action);
    }
  }
}