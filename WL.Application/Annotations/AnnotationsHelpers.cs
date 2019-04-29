using System;
using WL.Application.Annotations.Queries;
using WL.Application.Documents;
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

    public static DocumentAnnotationsDto ToDocumentAnnotations(this Annotation an) {
      return new DocumentAnnotationsDto {
        Id = an.Id,
        To = new DocumentDto {
          Id = an.To.Id,
          DocumentTypeId = an.To.DocumentTypeId,
          EntityId = an.To.EntityId,
          Number = an.To.Number,
          PublicationDate = an.To.PublicationDate.HasValue ? an.To.PublicationDate : new DateTime((int)an.To.PublicationYear, 1, 1)
        },
        From = new DocumentDto {
          Id = an.From.Id,
          DocumentTypeId = an.From.DocumentTypeId,
          EntityId = an.From.EntityId,
          Number = an.From.Number,
          PublicationDate = an.From.PublicationDate
        },
        AnnotationTypeId = an.AnnotationTypeId,
        Description = an.Description
      };
    }
  }
}