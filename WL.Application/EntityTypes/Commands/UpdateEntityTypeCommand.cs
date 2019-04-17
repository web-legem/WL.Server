using System.Collections.Generic;

namespace WL.Application.EntityTypes.Commands {

  public class UpdateEntityTypeCommand {
    public long Id { get; set; }
    public string Name { get; set; }
    public List<long> SupportedDocumentTypesIds { get; set; }
  }
}