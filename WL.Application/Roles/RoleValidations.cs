using LanguageExt;
using WL.Application.Common.Errors;
using static WL.Application.Common.FormValidations;
using static WL.Application.Common.LongValidations;

namespace WL.Application.Roles {

  public static class RoleValidations {

    public static Validation<Error, string> ValidateName(string val)
    => from x in ValidateFieldNonNull(val, nameof(val))
       from y in ValidateFieldNonEmpty(val, nameof(val)) | ValidateFieldMaxLength(100)(val, nameof(val))
       select y;

    public static Validation<Error, long> ValidateConfigSystem(long val)
    => from x in ValidateMaxValue(1)(val)
       from y in ValidateMinValue(0)(val)
       select y;

    public static Validation<Error, long> ValidateCreateDocuments(long val)
    => from x in ValidateMaxValue(1)(val)
       from y in ValidateMinValue(0)(val)
       select y;

    public static Validation<Error, long> ValidateDeleteDocuments(long val)
    => from x in ValidateMaxValue(1)(val)
       from y in ValidateMinValue(0)(val)
       select y;
  }
}