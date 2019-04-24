using System;
using System.ComponentModel.DataAnnotations;

namespace WL.Domain.User {

  public class Role {
    public long RoleId { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; }

    public int ConfigSystem { get; set; }
    public int CreateDocuments { get; set; }
    public int DeleteDocuments { get; set; }
  }
}