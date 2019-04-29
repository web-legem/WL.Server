using System;
using System.Collections.Generic;
using System.Text;
using WL.Application.Documents;

namespace WL.Application.Annotations.Queries {

  public class DocumentAnnotationsDto {
    public long Id { get; set; }
    public DocumentDto From { get; set; }
    public DocumentDto To { get; set; }
    public long AnnotationTypeId { get; set; }
    public string Description { get; set; }
  }
}