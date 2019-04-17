using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace WL.Domain {

  public class EntityType {
    public long EntityTypeId { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    public IQueryable<EntityTypeDocumentType> SupportedDocuments { set; get; }
  }
}