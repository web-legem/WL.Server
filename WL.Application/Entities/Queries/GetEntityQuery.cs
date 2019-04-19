using LanguageExt;

using WL.Application.Interfaces.Persistance;

namespace WL.Application.Entities.Queries {

  public class GetEntityQuery {
    readonly IEntityRepository _repository;

    public GetEntityQuery(IEntityRepository repository) {
      _repository = repository;
    }

    public Try<EntityDto> Execute(long id)
       => ()
       => _repository
        .Get(id)
        .ToEntityDto();
  }
}