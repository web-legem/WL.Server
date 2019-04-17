using LanguageExt;

using WL.Application.Common.Errors;
using WL.Application.Interfaces.Persistance;

using static WL.Application.Common.CommonValidations;
using static WL.Application.EntityTypes.EntityTypeHelpers;
using static WL.Application.EntityTypes.EntityTypeValidations;

namespace WL.Application.EntityTypes.Commands {

  public class CreateEntityTypeCommandHandler {
    readonly IEntityTypeRepository repository;

    public CreateEntityTypeCommandHandler(IEntityTypeRepository repository) {
      this.repository = repository;
    }

    public Try<Validation<Error, EntityTypeDto>> Execute(CreateEntityTypeCommand cmd)
      => ()
      => from x in ValidateCreateEntityTypeCommand(cmd)
         let y = CreateEntityTypeFrom(cmd)
         let z = repository.Create(y)
         select z.ToTipoEntidadDTO();

    public Validation<Error, CreateEntityTypeCommand> ValidateCreateEntityTypeCommand(CreateEntityTypeCommand cmd)
      => from x in ValidateNonNull(cmd)
         from y in (ValidateEntityTypeName(x.Name),
            ValidateEntityTypeSupportedDocumentsIds(x.SupportedDocumentTypesIds.ToArray()))
           .Apply((n, sdi) => cmd)
         select y;
  }
}