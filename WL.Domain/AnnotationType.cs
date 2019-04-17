using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WL.Domain {

  public class AnnotationType {
    public long AnnotationTypeId { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    [Required]
    [MaxLength(10)]
    public string Root { get; set; }
  }
}