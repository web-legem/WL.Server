using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using WL.Application.Roles;
using WL.Application.Roles.Queries;
using static WL.Api.Infrastructure.PermissionsAttribute;

namespace WL.Api.Infrastructure {

  public class AuthFilter : IActionFilter {
    readonly GetRoleQuery roleQuery;

    public AuthFilter(GetRoleQuery roleQuery) {
      this.roleQuery = roleQuery;
    }

    public void OnActionExecuted(ActionExecutedContext context) {
    }

    public void OnActionExecuting(ActionExecutingContext context) {
      var attributes = context.ActionDescriptor.EndpointMetadata.FirstOrDefault(x => x is PermissionsAttribute);

      if (attributes != null) {
        if (!context.HttpContext.Request.Headers.ContainsKey("Authorization")) {
          context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
        } else {
          context.HttpContext.Request.Headers.TryGetValue("Authorization", out var values);
          var token = values.FirstOrDefault();
          if (!string.IsNullOrEmpty(token)) {
            RoleDto userRole = null;
            roleQuery.Execute(token).Match(
               Succ: x => userRole = x,
               Fail: _ => context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized)
               );

            if (userRole == null) {
              context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
              return;
            }

            MapPerm[] perms = ((PermissionsAttribute)attributes).Perms;
            for (var i = 0; i < perms.Length(); i++) {
              switch (perms[i].ToString()) {
                case ConfigSystem: {
                  if (userRole.ConfigSystem == 0) {
                    context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
                    return;
                  }
                  break;
                }
                case CreateDocument: {
                  if (userRole.CreateDocuments == 0) {
                    context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
                    return;
                  }
                  break;
                }
                case DeleteDocument: {
                  if (userRole.DeleteDocuments == 0) {
                    context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
                    return;
                  }
                  break;
                }
              }
            }
          } else {
            context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
          }
        }
      }
    }
  }
}