using LanguageExt;

using System;
using System.Linq;

using WL.Application.Interfaces.Persistance;

using static LanguageExt.Prelude;

namespace WL.Application.AnnotationTypes.Queries {

  public class GetAllAnnotationTypesQuery {
    readonly IAnnotationTypeRepository repository;

    public GetAllAnnotationTypesQuery(IAnnotationTypeRepository repository) {
      this.repository = repository;
    }

    public Try<IQueryable<AnnotationTypeDto>> Execute() {
      Func<IQueryable<AnnotationTypeDto>> query =
        () => repository
        .GetAll()
        .Select(x => x.ToAnnotationTypeDto());

      return Try(query);
    }
  }
}