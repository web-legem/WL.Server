using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Application.EntityTypes.Commands {

  public class CreateEntityTypeCommand {
    public string Name { get; set; }
    public IEnumerable<long> SupportedDocumentTypesIds { get; set; }
  }
}