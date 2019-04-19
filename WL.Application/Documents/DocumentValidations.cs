using LanguageExt;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WL.Application.Common.Errors;
using WL.Application.Documents.Commands;

using static WL.Application.Common.FormValidations;

namespace WL.Application.Documents {

  public static class DocumentValidations {

    public static Validation<Error, CreateDocumentCommand>
   ValidateDocument(CreateDocumentCommand document)
   => from x in ValidateFieldNonNull(document, nameof(document))
      from y in (
            ValidateEntityId(x.EntityId),
            ValidateDocumentTypeId(x.DocumentTypeId),
            ValidateNumber(x.Number),
            ValidatePublicationDate(x.PublicationDate),
            ValidateFile(x.File)
         )
         .Apply((entityId, documentTypeId, number, publicationDate, file)
            => document)
      select y;

    public static Validation<Error, long> ValidateEntityId(long entityId)
       => from x in ValidateId(entityId, nameof(entityId))
          select x;

    public static Validation<Error, long>
       ValidateDocumentTypeId(long documentTypeId)
       => from x in ValidateId(documentTypeId, nameof(documentTypeId))
          select x;

    public static Validation<Error, string> ValidateNumber(string number)
       => from x in ValidateFieldNonNull(number, nameof(number))
          from y in ValidateFieldNonEmpty(number, nameof(number))
          select y;

    public static Validation<Error, DateTime>
       ValidatePublicationDate(DateTime publicationDate)
       => from x in ValidateBeforeToday(publicationDate, nameof(publicationDate))
          from y in ValidateAfter(new DateTime(1900, 1, 1), x, nameof(publicationDate))
          select y;

    public static Validation<Error, Stream>
       ValidateFile(Stream file)
       => from x in ValidateFieldNonNull(file, nameof(file))
          select x;
  }
}