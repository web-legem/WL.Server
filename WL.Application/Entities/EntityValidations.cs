using LanguageExt;

using WL.Application.Common.Errors;

using static WL.Application.Common.FormValidations;

namespace WL.Application.Entities {

  public static class EntityValidations {

    public static Validation<Error, long> ValidateEntityId(long id)
      => ValidateId(id);

    public static Validation<Error, string> ValidateEntityName(string name)
      => from x in ValidateFieldNonNull(name, nameof(name))
         from y in ValidateFieldNonEmpty(name, nameof(name))
           | ValidateFieldMaxLength(50)(name, nameof(name))
         select y;

    public static Validation<Error, string> ValidateEntityEmail(string email)
      => from x in ValidateFieldNonNull(email, nameof(email))
         from y in ValidateFieldNonEmpty(email, nameof(email))
            | ValidateEmailField(email, nameof(email))
         select y;

    public static Validation<Error, long> ValidateEntityEntityTypeId(long entityTypeId)
      => ValidateId(entityTypeId, nameof(entityTypeId));
  }
}