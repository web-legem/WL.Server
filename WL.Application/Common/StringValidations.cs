using LanguageExt;

using System;
using System.Text.RegularExpressions;

using WL.Application.Common.Errors;

using static LanguageExt.Prelude;

namespace WL.Application.Common {

  public static class StringValidations {

    public static Func<T, string, Validation<Error, T>> ValidateField<T>(Func<T, Validation<Error, T>> validation, string error)
       => (T value, string fieldName)
       => validation(value).IsSuccess
          ? Success<Error, T>(value)
          : Fail<Error, T>(new FormFieldError(error, fieldName));

    public static Validation<Error, string> ValidateNonEmpty(string str)
       => str.Length > 0
          ? Success<Error, string>(str)
          : Fail<Error, string>(new Error());

    public static Func<string, Validation<Error, string>> ValidateMinLength(int minLength)
       => (string str)
       => str.Length >= minLength
          ? Success<Error, string>(str)
          : Fail<Error, string>(new Error());

    public static Func<string, Validation<Error, string>> ValidateMaxLength(int maxLength)
       => (string str)
       => str.Length <= maxLength
          ? Success<Error, string>(str)
          : Fail<Error, string>(new Error());

    public static Validation<Error, string> ValidateEmail(string str)
       => ValidateRegex(
             @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))"
             + @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
          RegexOptions.IgnoreCase)(str);

    public static Func<string, Validation<Error, string>> ValidateRegex(string regex, RegexOptions opts)
       => (string str)
       => Regex.IsMatch(str, regex, opts)
          ? Success<Error, string>(str)
          : Fail<Error, string>(new Error());
  }
}