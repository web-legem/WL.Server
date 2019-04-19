using LanguageExt;

using WL.Application.Common.Errors;
using WL.Application.Interfaces.Persistance;

using static WL.Application.Common.CommonValidations;
using static WL.Application.Entities.EntityValidations;

namespace WL.Application.Entities.Commands {

  public class CreateEntityCommandHandler {
    private readonly IEntityRepository _entityRepository;
    private readonly IEntityTypeRepository _teRepository;

    public CreateEntityCommandHandler(IEntityRepository repository,
        IEntityTypeRepository teRepository) {
      _entityRepository = repository;
      _teRepository = teRepository;
    }

    public Try<Validation<Error, EntityDto>> Execute(CreateEntityCommand msg)
       => ()
       => from x in ValidateCreateEntityMsg(msg)
          let y = x.ToEntity()
          let z = _entityRepository.Create(y)
          select z.ToEntityDto();

    private Validation<Error, CreateEntityCommand> ValidateCreateEntityMsg(CreateEntityCommand msg)
       => from x in ValidateNonNull(msg)
          from y in (
                ValidateEntityName(x.Name),
                ValidateEntityEmail(x.Email),
                ValidateEntityEntityTypeId(x.EntityTypeId))
             .Apply((name, email, eId) => msg)
          select y;
  }
}