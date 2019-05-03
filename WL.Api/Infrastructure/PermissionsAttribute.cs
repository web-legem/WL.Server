using System;

namespace WL.Api.Infrastructure {

  public class PermissionsAttribute : Attribute {
    public const string CreateDocument = "CreateDocument";
    public const string DeleteDocument = "DeleteDocument";
    public const string ConfigSystem = "ConfigSystem";

    public enum MapPerm {
      CreateDocument,
      DeleteDocument,
      ConfigSystem
    }

    public MapPerm[] Perms { get; set; }

    public PermissionsAttribute(params MapPerm[] Perms) {
      this.Perms = Perms;
    }
  }
}