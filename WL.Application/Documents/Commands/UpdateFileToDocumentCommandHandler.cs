using LanguageExt;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WL.Application.Common.Errors;
using WL.Application.Documents.Ocr;
using WL.Application.Interfaces.Persistance;
using WL.Domain;
using static WL.Application.Common.CommonValidations;
using static WL.Application.Documents.DocumentValidations;
using static WL.Application.Helpers.DirectoryHelpers;

namespace WL.Application.Documents.Commands {

  public class UpdateFileToDocumentCommandHandler {
    readonly IDocumentRepository repository;

    public UpdateFileToDocumentCommandHandler(IDocumentRepository repository) {
      this.repository = repository;
    }

    public Try<Validation<Error, DocumentDto>> Execute(UpdateFileToDocumentCommand cmd)
      => ()
      => from x in ValidateUpdateFileToDocumentCommand(cmd)
         select PerformSideEffect(x);

    private Validation<Error, UpdateFileToDocumentCommand> ValidateUpdateFileToDocumentCommand(UpdateFileToDocumentCommand cmd)
      => from x in ValidateNonNull(cmd)
         from y in ValidateFile(x)
         select cmd;

    private DocumentDto PerformSideEffect(UpdateFileToDocumentCommand cmd) {
      // eliminar el archivo anterior
      var oldFile = repository.GetFileIfExist(cmd.DocumentId);
      if (oldFile != null) {
        DeleteFilesInOs(oldFile);
      }
      // crear el archivo nuevo
      var test = from file in SaveFile(cmd.File)
                 from text in new OcrPdfToText().GetText(file)
                 select UpdateDocumentFile(cmd, file, text);

      return test.Try().Match(x => x, e => throw e);
    }

    private DocumentDto UpdateDocumentFile(UpdateFileToDocumentCommand cmd, FileInfo file, string text) {
      SaveTextFile(file, text);
      return repository
        .UpdateDocumentFile(cmd.DocumentId, file.Name, IssueAnalizer.ObtenerAsunto(text))
        .ToDocumentDto();
    }

    void SaveTextFile(FileInfo fileInfo, string text) {
      System.IO.File.WriteAllText(Path.Combine(GetTextDirectory(), fileInfo.Name + ".txt"), text);
    }

    private void DeleteFilesInOs(Domain.File oldFile) {
      var fileInfo = new FileInfo(Path.Combine(GetDocumentsDirectory(), oldFile.Name));
      if (fileInfo.Exists)
        fileInfo.Delete();

      var fileInfoText = new FileInfo(Path.Combine(GetTextDirectory(), oldFile.Name + ".txt"));
      if (fileInfoText.Exists)
        fileInfoText.Delete();
    }

    Try<FileInfo> SaveFile(Stream stream) {
      return () => {
        var fileInfo = new FileInfo(Path.Combine(GetDocumentsDirectory(), Path.GetRandomFileName() + ".pdf"));

        if (fileInfo.Exists)
          fileInfo.Delete();

        using (var output = new FileStream(fileInfo.FullName, FileMode.Create))
          stream.CopyTo(output);

        return fileInfo;
      };
    }
  }
}