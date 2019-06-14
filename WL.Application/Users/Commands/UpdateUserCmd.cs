using System;

namespace WL.Application.Users.Commands {

  public class UpdateUserCmd {
    public long Id { get; set; }
    public string Nickname { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Document { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string State { get; set; }
    public int RoleId { get; set; }
    public DateTime LastModification { get; set; }
    public long? EntityId { get; set; }
  }
}