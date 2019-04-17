using LanguageExt;

using WL.Application.Interfaces.Persistance;

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