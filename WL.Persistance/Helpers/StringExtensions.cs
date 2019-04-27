using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace WL.Persistance.MigrationHelpers {

  internal static class StringExtensions {

    public static string ApplyKeyConventions(this string key) {
      var regex = new Regex("[^A-Z_]");
      return regex.Replace(key, "");
    }
  }
}