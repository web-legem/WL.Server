using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace WL.Persistance.MigrationHelpers {

  public static class MigrationExtensions {

    public static string LoadStringFromFile(string fileName) {
      var currentDirectory = Directory.GetCurrentDirectory();
      return File.ReadAllText(Path.Combine(currentDirectory, "Sql", fileName));
    }

    public static string GetBaseDirectory() {
      var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build();

      return configuration["BaseDirectory"];
    }

    public static void UpdateDirectory(this MigrationBuilder migrationBuilder) {
      var baseDirctory = GetBaseDirectory();
      var textDirectoryPath = Path.Combine(baseDirctory, "text");

      var createDirectory = $@"CREATE OR REPLACE DIRECTORY wl_text_dir AS '{textDirectoryPath}'";
      migrationBuilder.Sql(createDirectory);

      var addContentsColumn = LoadStringFromFile("contents_column.sql");
      migrationBuilder.Sql(addContentsColumn);

      var contextIndex = LoadStringFromFile("context_index.sql");
      migrationBuilder.Sql(contextIndex);

      var createContentsTrigger = LoadStringFromFile("create_contents_trigger.sql");
      migrationBuilder.Sql(createContentsTrigger);
    }
  }
}