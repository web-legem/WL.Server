using System.Collections.Generic;
using WL.Application.Common.Errors;

namespace WL.Application.Common {

   public class FormFieldError : Error {
      public IEnumerable<string> FieldNames { get; set; }
      public string ErrorType { get; set; }

      public FormFieldError(string errorType, params string[] fieldNames) {
         FieldNames = fieldNames;
         ErrorType = errorType;
      }
      public FormFieldError(string errorType) {
         ErrorType = errorType;
      }

      public FormFieldError(string errorType, string msg, bool generic) {
         ErrorType = errorType;
         Message = msg;
      }

   }
}