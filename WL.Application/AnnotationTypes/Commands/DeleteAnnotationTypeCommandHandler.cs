using LanguageExt;

using WL.Application.Interfaces.Persistance;

using static LanguageExt.Prelude;

namespace WL.Application.AnnotationTypes.Commands {

  public class DeleteAnnotationTypeCommandHandler {
    readonly IAnnotationTypeRepository annotationTypeRepository;

    public DeleteAnnotationTypeCommandHandler(IAnnotationTypeRepository repository) {
      annotationTypeRepository = repository;
    }

    public Try<Unit> Execute(long id)
       => ()
       => fun((long x) => annotationTypeRepository.Delete(x))(id);
  }
}