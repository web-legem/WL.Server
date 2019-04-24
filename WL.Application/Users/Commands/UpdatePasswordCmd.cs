using System;

namespace WL.Application.Users.Commands {

    public class UpdatePasswordCmd {

        public long UserId { get; set; }
        public string Password { get; set; }

    }
}