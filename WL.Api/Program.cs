using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging;
using WL.Api.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace WL.Api {

   public class Program {

      public static void Main(string[] args) {

         var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();

         var isService = !(Debugger.IsAttached || args.Contains("--console"));

         if (isService) {
            var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
            var pathToContentRoot = Path.GetDirectoryName(pathToExe);
            Directory.SetCurrentDirectory(pathToContentRoot);
         }

         var builder = CreateWebHostBuilder(
             args.Where(arg => arg != "--console").ToArray());

         var host = builder.UseConfiguration(config).Build().MigrateDatabase();

         if (isService) {
            // To run the app without the CustomWebHostService change the
            // next line to host.RunAsService();
            host.RunAsCustomService();
         } else {
            host.Run();
         }

         // CreateWebHostBuilder(args)
         //.Build()
         //.MigrateDatabase()
         //.Run();
      }

      public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
          WebHost.CreateDefaultBuilder(args)         
         .UseStartup<Startup>();
   }

   //.UseStartup<Startup>();

}