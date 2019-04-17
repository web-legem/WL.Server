using System.Collections.Generic;

namespace WL.Application.EntityTypes.Commands {

  public class CreateEntityTypeCommand {
    public string Name { get; set; }
    public List<long> SupportedDocumentTypesIds { get; set; }
  }
}