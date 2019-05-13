using System;

namespace WL.Application.Users {

   public class CredentialDto {      
      public bool newPasswordRequired { get; set; }
      public long id { get; set; }
      public string firstName { get; set; }
      public string lastName { get; set; }
      public string document { get; set; }
      public string token { get; set; }
      public Object permissions { get; set; }
      public String photo { get; set; }

   }
}