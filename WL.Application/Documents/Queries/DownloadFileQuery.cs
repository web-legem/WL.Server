using LanguageExt;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WL.Application.Interfaces.Persistance;
using static WL.Application.Helpers.DirectoryHelpers;

namespace WL.Application.Documents.Queries {

  public class DownloadFileQuery {
    private readonly IDocumentRepository documentRepository;

    public DownloadFileQuery(IDocumentRepository documentRepository) {
      this.documentRepository = documentRepository;
    }

    public Try<FileStream> Execute(long documentId)
       => ()
       => {
         var fileName = documentRepository
           .Get(documentId).File.Name;
         var fullName = Path.Combine(GetDocumentsDirectory(), fileName);
         return new FileStream(fullName, FileMode.Open);
       };
  }
}