using LanguageExt;

using WL.Application.Interfaces.Persistance;

namespace WL.Application.AnnotationTypes.Queries {

  public class GetAnnotationTypeQuery {
    private readonly IAnnotationTypeRepository _repository;

    public GetAnnotationTypeQuery(IAnnotationTypeRepository repository) {
      _repository = repository;
    }

    public Try<AnnotationTypeDto> Execute(long id)
       => ()
       => _repository
          .Get(id)
          .ToAnnotationTypeDto();
  }
}