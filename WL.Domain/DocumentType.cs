using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WL.Domain {

  public class DocumentType {
    public long DocumentTypeId { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; }
  }
}