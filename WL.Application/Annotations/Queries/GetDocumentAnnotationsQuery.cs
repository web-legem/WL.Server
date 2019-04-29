using LanguageExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WL.Application.Interfaces.Persistance;

using static LanguageExt.Prelude;

namespace WL.Application.Annotations.Queries {

  public class GetDocumentAnnotationsQuery {
    readonly IAnnotationRepository repository;

    public GetDocumentAnnotationsQuery(IAnnotationRepository repository) {
      this.repository = repository;
    }

    public Try<IQueryable<DocumentAnnotationsDto>> Execute(long documentId) {
      Func<IQueryable<DocumentAnnotationsDto>> query =
        () => repository
        .GetDocumentAnnotations(documentId)
        .Select(x => x.ToDocumentAnnotations());

      return Try(query);
    }
  }
}