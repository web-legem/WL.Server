using LanguageExt;
using System;
using System.Collections.Generic;
using System.Text;
using WL.Application.Common.Errors;

using static WL.Application.Common.FormValidations;
using static WL.Application.Common.CommonValidations;
using static WL.Application.Common.StringValidations;
using static WL.Application.Common.LongValidations;

namespace WL.Application.Annotations {

  public class AnnotationValidations {

    public static Validation<Error, long> ValidateAnnotationTypeId(long annotationTypeId)
      => ValidateField(ValidateMinValue(1), "InvalidId")(annotationTypeId, nameof(annotationTypeId));

    public static Validation<Error, long> ValidateFromDocumentId(long fromDocumentId)
      => ValidateField(ValidateMinValue(1), "InvalidId")(fromDocumentId, nameof(fromDocumentId));

    public static Validation<Error, long> ValidateToDocumentTypeId(long toDocumentTypeId)
      => ValidateField(ValidateMinValue(1), "InvalidId")(toDocumentTypeId, nameof(toDocumentTypeId));

    public static Validation<Error, long> ValidateToEntityId(long toEntityId)
      => ValidateField(ValidateMinValue(1), "InvalidId")(toEntityId, nameof(toEntityId));

    public static Validation<Error, string> ValidateToNumber(string toNumber)
      => from x in ValidateFieldNonNull(toNumber, nameof(toNumber))
         from y in ValidateFieldNonEmpty(toNumber, nameof(toNumber))
         select y;

    public static Validation<Error, long> ValidateToPublicationYear(long toPublicationYear)
      => from x in ValidateField(ValidateMinValue(1900), "MinValue")(toPublicationYear, nameof(toPublicationYear))
         from y in ValidateField(ValidateMaxValue(DateTime.Now.Year), "MaxValue")(toPublicationYear, nameof(toPublicationYear))
         select y;
  }
}