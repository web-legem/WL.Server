using LanguageExt;

using WL.Application.Common.Errors;
using WL.Application.Interfaces.Persistance;

using static WL.Application.Common.CommonValidations;
using static WL.Application.DocumentTypes.DocumentTypeHelpers;
using static WL.Application.DocumentTypes.DocumentTypeValidations;

namespace WL.Application.DocumentTypes.Commands {

  public class CreateDocumentTypeCommandHandler {
    readonly IDocumentTypeRepository repository;

    public CreateDocumentTypeCommandHandler(IDocumentTypeRepository repository) {
      this.repository = repository;
    }

    public Try<Validation<Error, DocumentTypeDto>> Execute(CreateDocumentTypeCommand cmd)
      => ()
      => from x in ValidateCreateDocumentTypeCommand(cmd)
         let y = CreateDocumentTypeFrom(x)
         let z = repository.Create(y)
         select z.ToDocumentTypeDto();

    private Validation<Error, CreateDocumentTypeCommand>
      ValidateCreateDocumentTypeCommand(CreateDocumentTypeCommand cmd)
      => from x in ValidateNonNull(cmd)
         from y in ValidateDocumentTypeName(x.Name)
         select cmd;
  }
}