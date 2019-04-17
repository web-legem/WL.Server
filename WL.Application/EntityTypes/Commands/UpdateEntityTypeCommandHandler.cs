using LanguageExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WL.Application.Common.Errors;
using WL.Application.Interfaces.Persistance;
using static WL.Application.Common.CommonValidations;
using static WL.Application.EntityTypes.EntityTypeValidations;
using static WL.Application.EntityTypes.EntityTypeHelpers;

namespace WL.Application.EntityTypes.Commands {

  public class UpdateEntityTypeCommandHandler {
    readonly IEntityTypeRepository repository;
    readonly IDocumentTypeRepository tiposDocumentRepository;

    public UpdateEntityTypeCommandHandler(IEntityTypeRepository repository) {
      this.repository = repository;
    }

    public Try<Validation<Error, EntityTypeDto>> Execute(UpdateEntityTypeCommand cmd)
      => ()
      => from x in ValidateUpdateEntityTypeCommand(cmd)
         let y = CreateEntityTypeFrom(x)
         let z = repository.Update(y)
         select z.ToTipoEntidadDTO();

    private Validation<Error, UpdateEntityTypeCommand> ValidateUpdateEntityTypeCommand(UpdateEntityTypeCommand cmd)
      => from x in ValidateNonNull(cmd)
         from y in (
              ValidateEntityTypeId(x.Id),
              ValidateEntityTypeName(x.Name),
              ValidateEntityTypeSupportedDocumentsIds(x.SupportedDocumentTypesIds.ToArray()))
           .Apply((id, name, sdt) => cmd)
         select y;
  }
}