using System;
using System.ComponentModel.DataAnnotations;

namespace WL.Domain {

  public class Document {
    public long DocumentId { get; set; }

    public long DocumentTypeId { get; set; }
    public DocumentType DocumentType { get; set; }

    public long EntityTypeId { get; set; }
    public EntityType EntityType { get; set; }

    [Required]
    [MaxLength(20)]
    public string Number { get; set; }

    public long PublicationYear { get; set; }

    public DateTime PublicationDate { get; set; }
  }
}