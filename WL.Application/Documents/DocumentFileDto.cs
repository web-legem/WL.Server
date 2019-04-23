using System;

namespace WL.Application.Documents {

  public class DocumentFileDto {
    public long Id { get; set; }
    public long DocumentTypeId { get; set; }
    public long EntityId { get; set; }
    public string Number { get; set; }
    public long PublicationYear { get; set; }
    public DateTime PublicationDate { get; set; }
    public long FileId { get; set; }
    public string Issue { get; set; }
    public string FileName { get; set; }
  }
}