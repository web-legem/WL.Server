using LanguageExt;

using System;

using WL.Application.Common.Errors;

using static WL.Application.Common.CommonValidations;
using static WL.Application.Common.LongValidations;
using static WL.Application.Common.StringValidations;

namespace WL.Application.Common {

  public static class FormValidations {

    public static Validation<Error, string>
       ValidateEmailField(string email, string fieldName)
       => ValidateField<string>(ValidateEmail, "ValidEmail")(email, fieldName);

    public static Validation<Error, string>
       ValidateFieldNonEmpty(string str, string fieldName)
       => ValidateField<string>(ValidateNonEmpty, "EmptyField")(str, fieldName);

    public static Validation<Error, T> ValidateFieldNonNull<T>(T value, string fieldName)
       where T : class
       => ValidateField<T>(ValidateNonNull, "NullValue")(value, fieldName);

    public static Validation<Error, T[]> ValidateArrayFieldNonEmpty<T>(T[] value, string fieldName)
       => ValidateField<T[]>(ValidateArrayNonEmpty, "EmptyArray")(value, fieldName);

    public static Validation<Error, string>
       ValidateFieldNonNull(string str, string fieldName)
       => ValidateFieldNonNull<string>(str, fieldName);

    public static Func<string, string, Validation<Error, string>>
       ValidateFieldMaxLength(int maxLength)
       => (string str, string fieldName)
       => ValidateField(ValidateMaxLength(maxLength), "MaxLength")(str, fieldName);

    public static Validation<Error, long> ValidateId(long id)
       => ValidateField(ValidateMinValue(1), "InvalidId")(id, nameof(id));

    public static Validation<Error, long> ValidateId(long id, string fieldName)
       => ValidateField(ValidateMinValue(1), "InvalidId")(id, fieldName);

    public static Validation<Error, DateTime> ValidateBeforeToday(DateTime date, string fieldName)
       => ValidateField<DateTime>(ValidateDateBeforeToday, "DateMustHavePassed")(date, fieldName);

    public static Validation<Error, DateTime> ValidateAfter(DateTime milestone, DateTime date, string fieldName)
       => ValidateField(ValidateDateAfter(milestone), "DateMustBeAfter")(date, fieldName);
  }
}