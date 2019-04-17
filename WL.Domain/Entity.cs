using System.ComponentModel.DataAnnotations;

namespace WL.Domain {

  public class Entity {
    public long EntityId { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    [Required]
    [MaxLength(255)]
    public string Email { get; set; }

    public long EntityTypeId { get; set; }
    public EntityType EntityType { get; set; }
  }
}