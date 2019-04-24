using System;

namespace WL.Api.Infrastructure {

  public class PermissionsAttribute : Attribute {
    public const string CreateDocument = "createDocument";
    public const string DeleteDocument = "deleteDocument";
    public const string ConfigSystem = "configSystem";

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