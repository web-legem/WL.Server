using System;

namespace WL.Application.Common.Errors {

  public class Error : Exception {
    new public string Message { get; set; }
  }
}