using System.ComponentModel.DataAnnotations;

namespace WL.Domain {

  public class AnnotationType {
    public long Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    [Required]
    [MaxLength(10)]
    public string Root { get; set; }
  }
}