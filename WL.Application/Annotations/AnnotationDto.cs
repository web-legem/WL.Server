using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Application.Annotations {

  public class AnnotationDto {
    public long Id { get; set; }
    public long ToDocumentId { get; set; }
    public long FromDocumentId { get; set; }
    public long AnnotationTypeId { get; set; }
    public string Description { get; set; }
  }
}