using LanguageExt;

using WL.Application.Common.Errors;
using WL.Application.Interfaces.Persistance;

using static WL.Application.AnnotationTypes.AnnotationTypeValidations;
using static WL.Application.Common.CommonValidations;

namespace WL.Application.AnnotationTypes.Commands {

  public class CreateAnnotationTypeCommandHandler {
    private readonly IAnnotationTypeRepository repository;

    public CreateAnnotationTypeCommandHandler(IAnnotationTypeRepository repository) {
      this.repository = repository;
    }

    public Try<Validation<Error, AnnotationTypeDto>> Execute(CreateAnnotationTypeCommand cmd)
       => ()
       => from x in ValidateCreateAnnotationTypeCommand(cmd)
          let y = x.ToAnnotationType()
          let z = repository.Create(y)
          select z.ToAnnotationTypeDto();

    private Validation<Error, CreateAnnotationTypeCommand>
       ValidateCreateAnnotationTypeCommand(CreateAnnotationTypeCommand cmd)
       => from x in ValidateNonNull(cmd)
          from y in (ValidateEntityTypeName(x.Name), ValidateEntityTypeRoot(x.Root))
             .Apply((name, root) => cmd)
          select y;
  }
}