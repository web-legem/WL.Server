using System.Collections.Generic;
using WL.Application.Common.Errors;

namespace WL.Application.Common {

   public class FormFieldError : Error {

      public static readonly string notFound = "NotFound";
      public static readonly string invalidDbCredentials = "InvalidDbCredentials";
      public static readonly string invalidUserCredentials = "InvalidUserCredentials";
      public static readonly string functionReturnedWithoutValue = "FunctionReturnedWithoutValue";
      public static readonly string notAllowedEdit = "NotAllowedEdit";
      public static readonly string uniqueConstraint = "uniqueConstraintViolated";
      public static readonly string integrityConstraint = "IntegrityConstraintViolated";


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