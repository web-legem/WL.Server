using LanguageExt;

using WL.Application.Common.Errors;

using static WL.Application.Common.FormValidations;

namespace WL.Application.AnnotationTypes {

  public static class AnnotationTypeValidations {

    public static Validation<Error, string> ValidateEntityTypeName(string name)
      => from x in ValidateFieldNonNull(name, nameof(name))
         from y in ValidateFieldNonEmpty(name, nameof(name))
            | ValidateFieldMaxLength(50)(name, nameof(name))
         select y;

    public static Validation<Error, string> ValidateEntityTypeRoot(string root)
       => from x in ValidateFieldNonNull(root, nameof(root))
          from y in ValidateFieldNonEmpty(root, nameof(root))
             | ValidateFieldMaxLength(5)(root, nameof(root))
          select y;
  }
}