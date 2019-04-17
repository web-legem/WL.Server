using System.ComponentModel.DataAnnotations;

namespace WL.Domain {

  public class DocumentType {
    public long DocumentTypeId { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; }
  }
}