using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WL.Domain {

  public class EntityType {
    public long Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    public ICollection<EntityTypeDocumentType> SupportedDocuments { set; get; }
  }
}