using LanguageExt;
using System;
using System.Collections.Generic;
using System.Text;
using WL.Application.Common.Errors;
using static WL.Application.Common.CommonValidations;
using static WL.Application.Common.FormValidations;

namespace WL.Application.EntityTypes {

  public static class EntityTypeValidations {

    public static Validation<Error, long> ValidateEntityTypeId(long id)
      => ValidateId(id);

    public static Validation<Error, string> ValidateEntityTypeName(string name)
      => from x in ValidateFieldNonNull(name, nameof(name))
         from y in ValidateFieldNonEmpty(name, nameof(name))
            | ValidateFieldMaxLength(50)(name, nameof(name))
         select y;

    public static Validation<Error, long[]> ValidateEntityTypeSupportedDocumentsIds(long[] supportedDocumentsIds)
      => from x in ValidateFieldNonNull(
            supportedDocumentsIds, nameof(supportedDocumentsIds))
         from y in ValidateArrayFieldNonEmpty(
            supportedDocumentsIds, nameof(supportedDocumentsIds))
         select y;
  }
}