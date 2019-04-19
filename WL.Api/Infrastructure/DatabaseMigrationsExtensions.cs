using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using WL.Persistance;

namespace WL.Api.Infrastructure {

  public static class DatabaseMigrationsExtensions {

    public static IWebHost MigrateDatabase
      (this IWebHost webHost) {
      using (var scope = webHost.Services.CreateScope()) {
        var services = scope.ServiceProvider;

        using (var context = services.GetRequiredService<WLDbContext>()) {
          try {
            context.Database.Migrate();
          } catch (Exception ex) {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while migrating the database");

            throw;
          }
        }
      }

      return webHost;
    }
  }
}