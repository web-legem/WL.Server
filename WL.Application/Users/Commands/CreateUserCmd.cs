namespace WL.Application.Users.Commands {

   public class CreateUserCmd {
      public string Nickname { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public string Document { get; set; }
      public string Password { get; set; }
      public string Email { get; set; }
      public int RoleId { get; set; }
      public string State { get; set; }
   }
}