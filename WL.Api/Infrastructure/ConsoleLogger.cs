using System;

namespace WL.Api.Infrastructure {

  public class ConsoleLogger {

    public void Log(string message) {
      Console.WriteLine($"ConsoleLogger: {message}");
    }
  }
}