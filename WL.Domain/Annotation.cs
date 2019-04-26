using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WL.Domain {

  public class Annotation {
    public long Id { get; set; }

    [Required]
    public long FromDocumentId { get; set; }

    public Document From { get; set; }

    [Required]
    public long ToDocumentId { get; set; }

    public Document To { get; set; }

    public long AnnotationTypeId { get; set; }
    public AnnotationType AnnotationType { get; set; }

    public string Description { get; set; }
  }
}