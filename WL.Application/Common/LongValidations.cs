using LanguageExt;

using System;
using WL.Application.Common.Errors;
using static LanguageExt.Prelude;

namespace WL.Application.Common {

  public static class LongValidations {

    public static Func<long, Validation<Error, long>>
       ValidateMaxValue(long maxValue)
       => (long value)
       => value <= maxValue
          ? Success<Error, long>(value)
          : Fail<Error, long>(new Error());

    public static Func<long, Validation<Error, long>>
       ValidateMinValue(long minValue)
       => (long value)
       => value >= minValue
          ? Success<Error, long>(value)
          : Fail<Error, long>(new Error());

    public static Validation<Error, long>
       ValidateIsPositive(long value)
       => ValidateMinValue(1)(value);
  }
}