using WL.Application.Roles.Commands;

namespace WL.Application.Roles {

  public static class RoleHelpers {

    public static Domain.User.Role ToRole(this CreateRoleCommand res)
       => new Domain.User.Role {
         Name = res.Name,
         ConfigSystem = res.ConfigSystem,
         CreateDocuments = res.CreateDocuments,
         DeleteDocuments = res.DeleteDocuments,
       };

    public static Domain.User.Role ToRole(this UpdateRoleCommand res)
       => new Domain.User.Role {
         Id = res.Id,
         Name = res.Name,
         ConfigSystem = res.ConfigSystem,
         CreateDocuments = res.CreateDocuments,
         DeleteDocuments = res.DeleteDocuments,
       };

    public static RoleDto ToRoleDTO(this Domain.User.Role res)
       => new RoleDto {
         Id = res.Id,
         Name = res.Name,
         ConfigSystem = res.ConfigSystem,
         CreateDocuments = res.CreateDocuments,
         DeleteDocuments = res.DeleteDocuments,
       };
  }
}