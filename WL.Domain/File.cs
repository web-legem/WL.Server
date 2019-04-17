using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Domain {

  public class File {
    public long FileId { get; set; }

    [Requierd]
    public string Name { get; set; }

    public string Issue { get; set; }

    public Document Document { get; set; }
  }
}