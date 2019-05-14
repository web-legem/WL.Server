using System;
using System.Collections.Generic;
using System.Text;
using WL.Application.Documents.Commands;
using WL.Domain;

namespace WL.Application.Documents {

  public static class DocumentHelpers {

    public static Document ToDocument(this CreateDocumentCommand cmd) {
      return new Document {
        DocumentTypeId = cmd.DocumentTypeId,
        EntityId = cmd.EntityId,
        PublicationDate = cmd.PublicationDate,
        PublicationYear = cmd.PublicationDate.Year,
        Number = cmd.Number
      };
    }

    public static DocumentDto ToDocumentDto(this Document document) => new DocumentDto {
      Id = document.Id,
      EntityId = document.EntityId,
      DocumentTypeId = document.DocumentTypeId,
      Number = document.Number,
      PublicationDate = document.PublicationDate
    };

    public static DocumentWithoutFileDto ToDocumentWithoutFileDto(this Document document) => new DocumentWithoutFileDto {
      Id = document.Id,
      EntityId = document.EntityId,
      DocumentTypeId = document.DocumentTypeId,
      Number = document.Number,
      PublicationYear = document.PublicationYear
    };

    public static DocumentDto ToDocumentDto(this File file) {
      var document = file.Document.ToDocumentDto();
      document.Issue = file.Issue;
      document.FileName = file.Name;

      return document;
    }

    public static DocumentFileDto ToDocumentFileDto(this Document document) {
      return new DocumentFileDto {
        Id = document.Id,
        DocumentTypeId = document.DocumentTypeId,
        EntityId = document.EntityId,
        Number = document.Number,
        PublicationYear = document.PublicationYear,
        PublicationDate = document.PublicationDate,
        FileId = document.File?.DocumentId,
        Issue = document.File?.Issue,
        FileName = document.File?.Name
      };
    }
  }
}