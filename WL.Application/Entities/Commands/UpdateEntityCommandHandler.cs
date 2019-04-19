using LanguageExt;

using WL.Application.Common.Errors;
using WL.Application.Interfaces.Persistance;

using static WL.Application.Common.CommonValidations;
using static WL.Application.Entities.EntityValidations;

namespace WL.Application.Entities.Commands {

  public class UpdateEntityCommandHandler {
    readonly IEntityRepository _repository;

    public UpdateEntityCommandHandler(IEntityRepository repository) {
      _repository = repository;
    }

    public Try<Validation<Error, EntityDto>> Execute(UpdateEntityCommand msg)
       => ()
       => from x in ValidationUpdateEntityMsg(msg)
          let y = x.ToEntity()
          let z = _repository.Update(y)
          select z.ToEntityDto();

    private Validation<Error, UpdateEntityCommand> ValidationUpdateEntityMsg(UpdateEntityCommand msg)
       => from x in ValidateNonNull(msg)
          from y in (
                ValidateEntityId(x.Id),
                ValidateEntityName(x.Name),
                ValidateEntityEmail(x.Email),
                ValidateEntityEntityTypeId(x.EntityType))
             .Apply((id, name, email, entityTypeId) => msg)
          select y;
  }
}