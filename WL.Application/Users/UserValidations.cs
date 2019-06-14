using LanguageExt;

using WL.Application.Common.Errors;
using static WL.Application.Common.FormValidations;
using static WL.Application.Common.LongValidations;

namespace WL.Application.Users {

  public static class UserValidations {

    public static Validation<Error, string> ValidateFirstName(string val)
    => from x in ValidateFieldNonNull(val, nameof(val))
       from y in ValidateFieldNonEmpty(val, nameof(val)) | ValidateFieldMaxLength(100)(val, nameof(val))
       select y;

    public static Validation<Error, string> ValidateLastName(string val)
    => from x in ValidateFieldNonNull(val, nameof(val))
       from y in ValidateFieldNonEmpty(val, nameof(val)) | ValidateFieldMaxLength(100)(val, nameof(val))
       select y;

    public static Validation<Error, string> ValidateDocument(string val)
   => from x in ValidateFieldNonNull(val, nameof(val))
      from y in ValidateFieldNonEmpty(val, nameof(val)) | ValidateFieldMaxLength(50)(val, nameof(val))
      select y;

    public static Validation<Error, string> ValidateNickname(string val)
    => from x in ValidateFieldNonNull(val, nameof(val))
       from y in ValidateFieldNonEmpty(val, nameof(val)) | ValidateFieldMaxLength(50)(val, nameof(val))
       select y;

    public static Validation<Error, string> validatePassword(string val)
    => from x in ValidateFieldNonNull(val, nameof(val))
       from y in ValidateFieldNonEmpty(val, nameof(val)) | ValidateFieldMaxLength(100)(val, nameof(val))
       select y;

    public static Validation<Error, string> ValidateEmail(string val)
    => from x in ValidateFieldNonNull(val, nameof(val))
       from y in ValidateFieldNonEmpty(val, nameof(val)) | ValidateFieldMaxLength(100)(val, nameof(val))
       select y;

    public static Validation<Error, long> ValidateRoleId(long val)
    => from y in ValidateMinValue(1)(val)
       select y;

    public static Validation<Error, long?> ValidateEntityId(long? val)
    => from y in ValidateMinValue((long?)1L)(val)
       select y;
  }
}