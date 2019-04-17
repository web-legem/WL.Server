using LanguageExt;

using WL.Application.Common.Errors;
using WL.Application.Interfaces.Persistance;

using static WL.Application.Common.CommonValidations;
using static WL.Application.DocumentTypes.DocumentTypeHelpers;
using static WL.Application.DocumentTypes.DocumentTypeValidations;

namespace WL.Application.DocumentTypes.Commands {

  public class UpdateDocumentTypeCommandHandler {
    private readonly IDocumentTypeRepository repository;

    public UpdateDocumentTypeCommandHandler(IDocumentTypeRepository repository) {
      this.repository = repository;
    }

    public Try<Validation<Error, DocumentTypeDto>>
      Execute(UpdateDocumentTypeCommand cmd)
      => ()
      => from x in ValidateUpdateDocumentTypeCommand(cmd)
         let y = CreateDocumentTypeFrom(cmd)
         let z = repository.Update(y)
         select z.ToDocumentTypeDto();

    private Validation<Error, UpdateDocumentTypeCommand>
      ValidateUpdateDocumentTypeCommand(UpdateDocumentTypeCommand cmd)
      => from x in ValidateNonNull(cmd)
         from y in (ValidateDocumentTypeId(x.Id), ValidateDocumentTypeName(x.Name))
          .Apply((id, name) => cmd)
         select y;
  }
}