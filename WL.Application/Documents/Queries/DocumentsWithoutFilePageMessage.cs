using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Application.Documents.Queries {

  public class DocumentsWithoutFilePageMessage {
    public int? PageSize { get; set; }
    public int? Page { get; set; }
    public string OrderBy { get; set; }
    public bool? Descend { get; set; }
  }
}