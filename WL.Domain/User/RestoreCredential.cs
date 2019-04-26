using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Domain.User {

  public class RestoreCredential {
    public long UserId { get; set; }
    public User User { get; set; }

    public string Token { get; set; }
  }
}