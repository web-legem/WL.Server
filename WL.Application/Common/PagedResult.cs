using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WL.Application.Common {

  public class PagedResult<T> {
    public long Count { get; set; }
    public IQueryable<T> Page { get; set; }
  }
}