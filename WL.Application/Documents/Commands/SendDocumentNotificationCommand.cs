using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Application.Documents.Commands {

  public class SendDocumentNotificationCommand {
    public long DocumentId { get; set; }
    public long[] RecipientEntitiesIds { get; set; }
  }
}