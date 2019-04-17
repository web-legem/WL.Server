using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Domain {

  public class EntityTypeDocumentType {
    public long EntityTypeId { get; set; }
    public EntityType EntityType { get; set; }

    public long DocumentTypeId { get; set; }
    public DocumentType DocumentType { get; set; }
  }
}