using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Application.EntityTypes {

  public class EntityTypeDto {
    public long Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<long> SupportedDocumentTypesIds { get; set; }
  }
}