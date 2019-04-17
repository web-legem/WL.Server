using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WL.Persistance {

  public class WlDbContextFactory : IDesignTimeDbContextFactory<WLDbContext> {

    public WLDbContext CreateDbContext(string[] args) {
      // Get environment
      string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

      // Build config
      var config = new ConfigurationBuilder()
          .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../WL.Api"))
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
          .AddJsonFile($"appsettings.{environment}.json", optional: true)
          .AddEnvironmentVariables()
          .Build();

      // Get connection string
      var optionsBuilder = new DbContextOptionsBuilder<WLDbContext>();
      var connectionString = config["ConnectionStrings:DefaultConnection"];
      optionsBuilder.UseOracle(connectionString, b => b.MigrationsAssembly("WL.Persistance"));
      return new WLDbContext(optionsBuilder.Options);
    }
  }
}