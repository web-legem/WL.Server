using LanguageExt;
using System;
using System.Collections.Generic;
using System.Text;
using WL.Application.Interfaces.Persistance;
using static WL.Application.EntityTypes.EntityTypeHelpers;

namespace WL.Application.EntityTypes.Queries {

  public class GetEntityTypeQuery {
    readonly IEntityTypeRepository repository;

    public GetEntityTypeQuery(IEntityTypeRepository repository) {
      this.repository = repository;
    }

    public Try<EntityTypeDto> Execute(long id)
      => ()
      => repository
        .Get(id)
        .ToTipoEntidadDTO();
  }
}