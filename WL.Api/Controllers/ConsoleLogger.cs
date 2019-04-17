namespace WL.Api.Controllers {

   public class ConsoleLogger {

      public void Log(string message) {
         System.Console.WriteLine($"ConsoleLogger: {message}");
      }
   }
}