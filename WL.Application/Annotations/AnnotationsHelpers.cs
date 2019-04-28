using System;
using System.Collections.Generic;
using System.Text;
using WL.Domain;

namespace WL.Application.Annotations {

  public static class AnnotationsHelpers {

    public static AnnotationDto ToAnnotationDto(this Annotation an) {
      return new AnnotationDto {
        Id = an.Id,
        ToDocumentId = an.ToDocumentId,
        FromDocumentId = an.FromDocumentId,
        AnnotationTypeId = an.AnnotationTypeId,
        Description = an.Description
      };
    }
  }
}