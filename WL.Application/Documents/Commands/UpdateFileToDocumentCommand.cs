using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WL.Application.Documents.Commands {

  public class UpdateFileToDocumentCommand {
    public Stream File { get; set; }
    public long DocumentId { get; set; }
  }
}