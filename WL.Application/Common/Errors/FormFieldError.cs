using System;
using System.Collections.Generic;
using System.Text;

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