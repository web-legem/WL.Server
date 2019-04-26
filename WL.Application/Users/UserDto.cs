using System;

namespace WL.Application.Users {

  public class UserDto {
    public long Id { get; set; }
    public string Nickname { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string Document { get; set; }
    public string Email { get; set; }
    public long RoleId { get; set; }
    public string State { get; set; }
    public DateTime LastModification { get; set; }
  }
}