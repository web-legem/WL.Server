using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WL.Application.EntityTypes.Commands {

  public class UpdateEntityTypeCommand {
    public long Id { get; set; }
    public string Name { get; set; }
    public IQueryable<long> SupportedDocumentTypesIds { get; set; }
  }
}