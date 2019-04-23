using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Application.Documents.Queries {

  public class SearchDocumentsMessage {
    public long? Page { get; set; }
    public long? PageSize { get; set; }
    public string WordsToSearch { get; set; }
    public long? EntityId { get; set; }
    public long? DocumentTypeId { get; set; }
    public string Number { get; set; }
    public long? Year { get; set; }
    public string OrderBy { get; set; }
    public bool? Descend { get; set; }
  }
}