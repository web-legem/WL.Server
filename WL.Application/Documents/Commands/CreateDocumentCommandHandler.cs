using LanguageExt;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using WL.Application.Common.Errors;
using WL.Application.Documents.Ocr;
using WL.Application.Interfaces.Persistance;

using static WL.Application.Common.CommonValidations;
using static WL.Application.Documents.DocumentValidations;

namespace WL.Application.Documents.Commands {

  public class CreateDocumentCommandHandler {
    readonly IDocumentRepository _repository;

    public CreateDocumentCommandHandler(
      IDocumentRepository repository
      ) {
      _repository = repository;
    }

    public Try<Validation<Error, DocumentDto>>
       Execute(CreateDocumentCommand cmd)
        => ()
        => from x in ValidateCreateDocumentCmd(cmd)
           select PerformSideEffect(x);

    public DocumentDto PerformSideEffect(CreateDocumentCommand cmd) {
      var test = from file in SaveFile(cmd.File)
                 from text in new OcrPdfToText().GetText(file) // TODO - Use OCR as interface and apply with Dependency Injection
                 select CreateDocument(cmd, file, text);
      return test.Try().Match(x => x, e => throw e);
    }

    Validation<Error, CreateDocumentCommand> ValidateCreateDocumentCmd(
       CreateDocumentCommand cmd)
       => from x in ValidateNonNull(cmd)
          from y in ValidateDocument(x)
          select cmd;

    DocumentDto CreateDocument(CreateDocumentCommand x, FileInfo fileInfo, string text) {
      SaveTextFile(fileInfo, text);
      var document = x.ToDocument();

      return _repository
        .CreateClassifiedDocument(document, IssueAnalizer.ObtenerAsunto(text), fileInfo.Name)
        .ToDocumentDto();
    }

    void SaveTextFile(FileInfo fileInfo, string text) {
      File.WriteAllText(Path.Combine(GetBaseDirectoryPath(), "text", fileInfo.Name + ".txt"), text);
    }

    Try<FileInfo> SaveFile(Stream stream) {
      var fileInfo = new FileInfo(Path.Combine(GetBaseDirectoryPath(), "documents", Path.GetRandomFileName() + ".pdf"));

      if (fileInfo.Exists)
        fileInfo.Delete();

      using (var output = new FileStream(fileInfo.FullName, FileMode.Create))
        stream.CopyTo(output);

      return () => fileInfo;
    }

    string GetBaseDirectoryPath() {
      // Get environment
      var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

      // Build config
      var config = new ConfigurationBuilder()
          .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../WL.Api"))
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
          .AddJsonFile($"appsettings.{environment}.json", optional: true)
          .AddEnvironmentVariables()
          .Build();

      return config["BaseDirectory"];
    }
  }
}