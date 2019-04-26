using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using WL.Application.Common;
using WL.Application.Common.Errors;

namespace WL.Persistance.ExceptionsToValidations {

   public static class ExceptionsToValidations {

      public static Error WrapOracleExceptionsWithError(Exception e) {
         Console.WriteLine("------------------------------------------------------");
         Console.WriteLine(e);
         Console.WriteLine("------------------------------------------------------");
         var innerE = e.InnerException;
         switch (innerE) {
            case OracleException o when o.Number == 1403:
               return new FormFieldError("ORA1403", "NotFound");

            case OracleException o when o.Number == 1017:
               return new FormFieldError("ORA1407", "InvalidDbCredentials");

            case OracleException o when o.Number == 6503:
               return new FormFieldError("ORA6503", "FunctionReturnedWithoutValue");

            case OracleException o when o.Number == 1 || o.Number == 2292:
               return GetError(o.Message);

            default:
               Console.WriteLine(e);
               return new FormFieldError("WL_ERROR"); //error generico
         }
      }

      public static FormFieldError GetError(string msg) {
         string column = null;
         var regex = new Regex(@"\(WEBL.(.*?)\)");
         var match = regex.Match(msg);

         if (match.Success) {
            column = match.Groups[1].Value;
         }

         if (!string.IsNullOrEmpty(column)) {
            if (columnName.TryGetValue(column, out var mapValue)) {
               return mapValue;
            }
            return new FormFieldError("WL_ERROR");
         }
         return new FormFieldError("WL_ERROR");
      }

      static readonly Dictionary<string, FormFieldError> columnName = new Dictionary<string, FormFieldError> {

         { "AK_AT_N",      new FormFieldError("ORA1", "TA_NAME") },
         { "AK_AT_R",      new FormFieldError("ORA1", "TA_ROOT") },
         { "AK_ET_N",      new FormFieldError("ORA1", "TE_NAME") },
         { "AK_R_N",       new FormFieldError("ORA1",  "ROLE_NAME") },
         { "AK_E_N",       new FormFieldError("ORA1", "E_NAME") },
         { "AK_U_E",       new FormFieldError("ORA1", "U_EMAIL") },
         { "AK_U_IDD",     new FormFieldError("ORA1", "U_DOCUMENT") },
         { "AK_U_N",       new FormFieldError("ORA1", "E_NAME") },
         { "AKAK_D_DTI_EI_N_PY_U_N",   new FormFieldError("ORA1", "SUPERAK") },
         { "AK_C_T",       new FormFieldError("ORA1", "E_TOKEN") },

         //{ "FK_E_ET_ETI",      new FormFieldError("ORA1", "TD_NAME") },
         { "FK_ETDT_DT_DTI",     new FormFieldError("ORA6512", "DOCTYPE") },
         { "FK_ETDT_ET_ETI",     new FormFieldError("ORA6512", "ENTTYPE") },
         { "FK_U_R_RI",          new FormFieldError("ORA6512", "USR") },

         { "FK_D_DT_DTI",        new FormFieldError("ORA6512", "DOC") },
         { "FK_D_E_EI",          new FormFieldError("ORA6512", "DOC") },
         { "FK_D_F_FDI",         new FormFieldError("ORA6512", "DOC") },

         { "FK_A_AT_ATI",        new FormFieldError("ORA6512", "ANO") },
         { "FK_A_D_FI",          new FormFieldError("ORA6512", "ANO") },
         { "FK_A_D_TI",          new FormFieldError("ORA6512", "ANO") },

      };
   }
}