using System.Collections.Generic;

namespace WL.Application.EntityTypes {

  public class EntityTypeDto {
    public long Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<long> SupportedDocumentTypesIds { get; set; }
  }
}