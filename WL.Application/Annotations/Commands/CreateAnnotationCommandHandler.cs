using LanguageExt;
using WL.Application.Common.Errors;
using WL.Application.Interfaces.Persistance;
using static WL.Application.Common.CommonValidations;
using static WL.Application.Annotations.AnnotationValidations;

namespace WL.Application.Annotations.Commands {

  public class CreateAnnotationCommandHandler {
    readonly IAnnotationRepository repository;

    public CreateAnnotationCommandHandler(IAnnotationRepository repository) {
      this.repository = repository;
    }

    public Try<Validation<Error, AnnotationDto>> Execute(CreateAnnotationCommand cmd)
      => ()
      => from x in ValidateCreateAnnoationCommand(cmd)
         let y = repository.Create(x)
         select y.ToAnnotationDto();

    private Validation<Error, CreateAnnotationCommand> ValidateCreateAnnoationCommand(CreateAnnotationCommand cmd)
      => from x in ValidateNonNull(cmd)
         from y in ValidateFromDocumentId(x.FromDocumentId)
         from z in ValidateToDocumentTypeId(x.ToDocumentTypeId)
         from a in ValidateToEntityId(x.ToEntityId)
         from b in ValidateToNumber(x.ToNumber)
         from c in ValidateToPublicationYear(x.ToPublicationYear)
         from d in ValidateAnnotationTypeId(x.AnnotationTypeId)
         select x;
  }
}