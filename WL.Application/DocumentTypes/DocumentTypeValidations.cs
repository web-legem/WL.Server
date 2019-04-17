using LanguageExt;
using System;
using System.Collections.Generic;
using System.Text;
using WL.Application.Common.Errors;
using static WL.Application.Common.FormValidations;

namespace WL.Application.DocumentTypes {

  public class DocumentTypeValidations {

    public static Validation<Error, string> ValidateDocumentTypeName(string name)
      => from x in ValidateFieldNonNull(name, nameof(name))
         from y in ValidateFieldNonEmpty(name, nameof(name))
           | ValidateFieldMaxLength(50)(name, nameof(name))
         select y;

    public static Validation<Error, long> ValidateDocumentTypeId(long id)
      => ValidateId(id);
  }
}