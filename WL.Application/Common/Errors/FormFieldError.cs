using System.Collections.Generic;

namespace WL.Application.Common.Errors {

  public class FormFieldError : Error {
    public IEnumerable<string> FieldNames { get; set; }
    public string ErrorType { get; set; }

    public FormFieldError(string errorType, params string[] fieldNames) {
      FieldNames = fieldNames;
      ErrorType = errorType;
    }
  }
}