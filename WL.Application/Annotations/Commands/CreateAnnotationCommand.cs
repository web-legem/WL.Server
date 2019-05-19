using System;

namespace WL.Application.Annotations.Commands {

  public class CreateAnnotationCommand {
    public long FromDocumentId { get; set; }
    public long ToDocumentTypeId { get; set; }
    public long ToEntityId { get; set; }
    public long ToPublicationYear { get; set; }
    public DateTime ToPublicationDate { get; set; }
    public string ToNumber { get; set; }
    public long AnnotationTypeId { get; set; }
    public string Description { get; set; }
  }
}