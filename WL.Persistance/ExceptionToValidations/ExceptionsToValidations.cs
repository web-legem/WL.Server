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

         if (e is FormFieldError) {
            return (FormFieldError)e;
         }

         var innerE = e.InnerException;
         switch (innerE) {
            case OracleException o when o.Number == 1403:
               return new FormFieldError(FormFieldError.notFound, "ORA1403");

            case OracleException o when o.Number == 1017:
               return new FormFieldError(FormFieldError.invalidDbCredentials, "ORA1407");

            case OracleException o when o.Number == 6503:
               return new FormFieldError(FormFieldError.functionReturnedWithoutValue, "ORA6503");

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

         { "IX_AT_N",      new FormFieldError(FormFieldError.uniqueConstraint, "TA_NAME") },
         { "IX_AT_R",      new FormFieldError(FormFieldError.uniqueConstraint, "TA_ROOT") },
         { "IX_ET_N",      new FormFieldError(FormFieldError.uniqueConstraint, "TE_NAME") },
         { "IX_E_N",       new FormFieldError(FormFieldError.uniqueConstraint, "E_NAME") },

         { "IX_U_E",       new FormFieldError(FormFieldError.uniqueConstraint, "U_EMAIL") },
         { "IX_U_IDD",     new FormFieldError(FormFieldError.uniqueConstraint, "U_DOCUMENT") },
         { "IX_U_N",       new FormFieldError(FormFieldError.uniqueConstraint, "U_NICKNAME") },
         { "IX_R_N",       new FormFieldError(FormFieldError.uniqueConstraint, "ROLE_NAME") },

         { "IX_C_T",       new FormFieldError(FormFieldError.uniqueConstraint, "E_TOKEN") },
         { "IX_D_DTI_EI_N_PY",   new FormFieldError(FormFieldError.uniqueConstraint, "SUPERAK") },
         { "IX_DT_N",      new FormFieldError(FormFieldError.uniqueConstraint, "TD_NAME") },


         { "FK_E_ET_ETI",     new FormFieldError(FormFieldError.integrityConstraint, "ENT") },
         { "FK_ETDT_DT_DTI",  new FormFieldError(FormFieldError.integrityConstraint, "DOCTYP") },
         { "FK_ETDT_ET_ETI",  new FormFieldError(FormFieldError.integrityConstraint, "ENTTYP") },
         { "FK_U_R_RI",       new FormFieldError(FormFieldError.integrityConstraint, "USR") },

         { "FK_D_DT_DTI",     new FormFieldError(FormFieldError.integrityConstraint, "DOCTYP") },
         { "FK_D_E_EI",       new FormFieldError(FormFieldError.integrityConstraint, "ENT") },
         { "FK_D_F_FDI",      new FormFieldError(FormFieldError.integrityConstraint, "DOC") },

         { "FK_A_AT_ATI",     new FormFieldError(FormFieldError.integrityConstraint, "ANOTYP") },
         { "FK_A_D_FI",       new FormFieldError(FormFieldError.integrityConstraint, "DOC") },
         { "FK_A_D_TI",       new FormFieldError(FormFieldError.integrityConstraint, "DOC") },

      };
   }
}