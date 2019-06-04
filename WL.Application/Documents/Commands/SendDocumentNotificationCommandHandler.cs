using LanguageExt;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WL.Application.Helpers;
using WL.Application.Interfaces.Persistance;
using WL.Domain;
using System.Linq;

namespace WL.Application.Documents.Commands {

  public class SendDocumentNotificationCommandHandler {
    private IEntityRepository entityRepository;
    private IDocumentRepository documentRepository;

    public SendDocumentNotificationCommandHandler(
      IEntityRepository entityRepository,
      IDocumentRepository documentRepository) {
      this.entityRepository = entityRepository;
      this.documentRepository = documentRepository;
    }

    public Unit Execute(SendDocumentNotificationCommand cmd) {
      var entities = entityRepository.GetEntitiesIn(cmd.RecipientEntitiesIds);

      // TODO - get document and prepare message
      var document = documentRepository.GetIncludingRelationsById(cmd.DocumentId);

      if (entities.Length() == 0 || document == null)
        return Unit.Default;

      var emails = entities.Map(x => x.Email);
      var emailContent = GetDocumentTitle(document);

      var message = $"El documento es {emailContent}, para verlo puedes hacer click ";

      var configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .Build();

      var clientUrl = configuration["clientUrl"];

      message += $"<a href=\"{clientUrl}/{cmd.DocumentId}\" >aquí</a>";

      SendMail.Send(emails.ToArray(), "Un nuevo documento ha sido cargado al sistema", message);

      return Unit.Default;
    }

    public string GetDocumentTitle(Document document) {
      return $"{document.DocumentType.Name} No. {document.Number} de {document.PublicationYear}";
    }
  }
}