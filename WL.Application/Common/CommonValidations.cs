using LanguageExt;

using System;

using WL.Application.Common.Errors;

using static LanguageExt.Prelude;

namespace WL.Application.Common {

  public static class CommonValidations {

    public static Validation<Error, T> ValidateNonNull<T>(T obj) where T : class
       => obj != null
          ? Success<Error, T>(obj)
          : Fail<Error, T>(new Error { Message = "NullObject" });

    public static Validation<Error, T[]> ValidateArrayNonEmpty<T>(T[] obj)
       => obj.Length > 0
          ? Success<Error, T[]>(obj)
          : Fail<Error, T[]>(new Error());

    public static Validation<Error, DateTime> ValidateDateBeforeToday(DateTime date)
       => date < DateTime.Today
          ? Success<Error, DateTime>(date)
          : Fail<Error, DateTime>(new Error());

    public static Func<DateTime, Validation<Error, DateTime>> ValidateDateAfter(DateTime milestone)
       => (DateTime date) => date > milestone
          ? Success<Error, DateTime>(date)
          : Fail<Error, DateTime>(new Error());
  }
}