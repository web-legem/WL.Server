using System.ComponentModel.DataAnnotations;

namespace WL.Domain {

  public class File {
    public long DocumentId { get; set; }
    public Document Document { get; set; }

    [Required]
    public string Name { get; set; }

    public string Issue { get; set; }
  }
}