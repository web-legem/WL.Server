﻿using WL.Application.DocumentTypes.Commands;
using WL.Domain;

namespace WL.Application.DocumentTypes {

  public static class DocumentTypeHelpers {

    public static DocumentTypeDto ToDocumentTypeDto(this DocumentType documentType)
      => new DocumentTypeDto {
        Id = documentType.Id,
        Name = documentType.Name
      };

    public static DocumentType CreateDocumentTypeFrom(CreateDocumentTypeCommand cmd) {
      return new DocumentType {
        Name = cmd.Name
      };
    }

    public static DocumentType CreateDocumentTypeFrom(UpdateDocumentTypeCommand cmd) {
      return new DocumentType {
        Id = cmd.Id,
        Name = cmd.Name
      };
    }
  }
}