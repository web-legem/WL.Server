using System.ComponentModel.DataAnnotations;

namespace WL.Domain.User {

  public class User {
    public long Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Nickname { get; set; }

    [Required]
    [MaxLength(200)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(200)]
    public string LastName { get; set; }

    [Required]
    [MaxLength(30)]
    public string IDDocument { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string State { get; set; }

    public long RoleId { get; set; }
    public Role Role { get; set; }

    public Credential Credential { get; set; }

    public long? EntityId { get; set; }
    public Entity Entity { get; set; }
  }
}