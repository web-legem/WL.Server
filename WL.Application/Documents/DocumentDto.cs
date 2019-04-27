using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Application.Documents {

  public class DocumentDto {
    public long Id { get; set; }
    public long EntityId { get; set; }
    public long DocumentTypeId { get; set; }
    public string Number { get; set; }
    public string FileName { get; set; }
    public string Issue { get; set; }
    public DateTime? PublicationDate { get; set; }
  }
}