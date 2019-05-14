using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Application.Documents {

  public class DocumentWithoutFileDto {
    public long Id { get; set; }
    public long DocumentTypeId { get; set; }
    public long EntityId { get; set; }
    public string Number { get; set; }
    public long PublicationYear { get; set; }
  }
}