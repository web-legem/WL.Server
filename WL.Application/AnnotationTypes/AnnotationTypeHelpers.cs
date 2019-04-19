using WL.Application.AnnotationTypes.Commands;
using WL.Domain;

namespace WL.Application.AnnotationTypes {

  public static class AnnotationTypeHelpers {

    public static AnnotationTypeDto ToAnnotationTypeDto(this AnnotationType annotationType)
      => new AnnotationTypeDto {
        Id = annotationType.AnnotationTypeId,
        Name = annotationType.Name,
        Root = annotationType.Root
      };

    public static AnnotationType ToAnnotationType(this CreateAnnotationTypeCommand cmd)
      => new AnnotationType {
        Name = cmd.Name,
        Root = cmd.Root
      };

    public static AnnotationType ToAnnotationType(this UpdateAnnotationTypeCommand cmd)
      => new AnnotationType {
        AnnotationTypeId = cmd.Id,
        Name = cmd.Name,
        Root = cmd.Root
      };
  }
}