using LanguageExt;

using WL.Application.Common.Errors;
using WL.Application.Interfaces.Persistance;

using static WL.Application.AnnotationTypes.AnnotationTypeValidations;
using static WL.Application.Common.CommonValidations;
using static WL.Application.Common.FormValidations;

namespace WL.Application.AnnotationTypes.Commands {

  public class UpdateAnnotationTypeCommandHandler {
    readonly IAnnotationTypeRepository _repository;

    public UpdateAnnotationTypeCommandHandler(IAnnotationTypeRepository repository) {
      _repository = repository;
    }

    public Try<Validation<Error, AnnotationTypeDto>> Execute(UpdateAnnotationTypeCommand cmd)
       => ()
       => from x in ValidateUpdateAnnotationTypeCommand(cmd)
          let y = x.ToAnnotationType()
          let z = _repository.Update(y)
          select z.ToAnnotationTypeDto();

    Validation<Error, UpdateAnnotationTypeCommand> ValidateUpdateAnnotationTypeCommand(UpdateAnnotationTypeCommand cmd)
       => from x in ValidateNonNull(cmd)
          from y in (
              ValidateId(x.Id),
              ValidateEntityTypeName(x.Name),
              ValidateEntityTypeRoot(x.Root))
            .Apply((id, name, root) => cmd)
          select y;
  }
}