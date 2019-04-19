using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace WL.Persistance.MigrationHelpers {

  public static class MigrationExtensions {

    public static void UpdateDirectory(this MigrationBuilder migrationBuilder) {
      var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

      var configuration = builder.Build();
      var baseDirctory = configuration["BaseDirectory"];
      var textDirectoryPath = Path.Combine(baseDirctory, "text");

      var createDirectory = $@"CREATE OR REPLACE DIRECTORY wl_text_dir AS '{textDirectoryPath}'";
      migrationBuilder.Sql(createDirectory);
      var addContentsColumn = @"ALTER TABLE ""Files"" ADD ""CONTENTS"" BFILE NOT NULL";
      migrationBuilder.Sql(addContentsColumn);

      var contextIndex = @"
CREATE INDEX ""CONTENTS_IDX""
    ON ""Files""( ""CONTENTS"" )
    INDEXTYPE is CTXSYS.CONTEXT
    PARAMETERS('replace metadata sync (on commit)');
";
      migrationBuilder.Sql(contextIndex);

      var trigger = @"
CREATE OR REPLACE TRIGGER CREATE_CONTENT
BEFORE INSERT OR UPDATE ON ""Files""
FOR EACH ROW
BEGIN
  :NEW.CONTENTS := BFILENAME('WL_TEXT_DIR', :NEW.""Name"" || '.txt');
END;";
      migrationBuilder.Sql(trigger);
    }
  }
}