using System;
using System.ComponentModel.DataAnnotations;

namespace WL.Domain.User {

  public class Credential {
    public long UserId { get; set; }
    public User User { get; set; }

    [Required]
    public string Token { get; set; }

    public DateTime Creation { get; set; }
  }
}