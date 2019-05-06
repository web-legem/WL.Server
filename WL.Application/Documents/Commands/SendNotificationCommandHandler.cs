using System;
using System.Collections.Generic;
using System.Text;
using WL.Application.Interfaces.Persistance;

namespace WL.Application.Documents.Commands {

  public class SendNotificationCommandHandler {
    readonly IDocumentRepository repository;

    public SendNotificationCommandHandler(IDocumentRepository repository) {
      this.repository = repository;
    }
  }
}