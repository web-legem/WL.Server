using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WL.Application.Documents.Commands {

  public class CreateDocumentCommand {
    public long EntityId { get; set; }
    public long DocumentTypeId { get; set; }
    public string Number { get; set; }
    public DateTime PublicationDate { get; set; }
    public Stream File { get; set; }
  }
}