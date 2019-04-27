using System;
using System.Collections.Generic;
using System.Text;
using WL.Application.Common;

namespace WL.Persistance.Helpers {

  public static class DbHelpers {

    public static T NullVerifier<T>(Func<T> func) {
      var value = func();
      if (value == null)
        throw new FormFieldError(FormFieldError.notFound);

      return value;
    }
  }
}