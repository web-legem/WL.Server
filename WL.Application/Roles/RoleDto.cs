using System;

namespace WL.Application.Roles {

   public class RoleDto {

      public long Id { get; set; }
      public string Name { get; set; }
      public int ConfigSystem { get; set; }
      public int CreateDocuments { get; set; }
      public int DeleteDocuments { get; set; }
      public DateTime LastModification { get; set; }

   }
}