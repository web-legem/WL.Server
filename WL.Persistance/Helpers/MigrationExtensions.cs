using Microsoft.EntityFrameworkCore.Migrations;
using System.IO;
using static WL.Application.Helpers.DirectoryHelpers;

namespace WL.Persistance.MigrationHelpers {

  internal static class MigrationExtensions {

    public static string LoadStringFromFile(string fileName) {
      var currentDirectory = Directory.GetCurrentDirectory();
      return File.ReadAllText(Path.Combine(currentDirectory, "Sql", fileName));
    }

    public static void UpdateDirectory(this MigrationBuilder migrationBuilder) {
      var baseDirctory = GetBaseDirectory();
      var textDirectoryPath = Path.Combine(baseDirctory, "text");

      var createDirectory = $@"CREATE OR REPLACE DIRECTORY wl_text_dir AS '{textDirectoryPath}'";
      migrationBuilder.Sql(createDirectory);

      var addContentsColumn = LoadStringFromFile("contents_column.sql");
      migrationBuilder.Sql(addContentsColumn);

      var contextIndex = LoadStringFromFile("context_idx.sql");
      migrationBuilder.Sql(contextIndex);

      var createContentsTrigger = LoadStringFromFile("create_contents_trigger.sql");
      migrationBuilder.Sql(createContentsTrigger);

      var documentUdt = LoadStringFromFile("document_typ.sql");
      migrationBuilder.Sql(documentUdt);

      var documentTableUdt = LoadStringFromFile("document_tbl.sql");
      migrationBuilder.Sql(documentTableUdt);

      var searchFunc = LoadStringFromFile("search_dt.sql");
      migrationBuilder.Sql(searchFunc);

      var searchCountFunc = LoadStringFromFile("search_count.sql");
      migrationBuilder.Sql(searchCountFunc);
    }
  }
}