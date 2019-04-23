using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Oracle.EntityFrameworkCore.Metadata;
using WL.Persistance.MigrationHelpers;

namespace WL.Persistance.Migrations {

  public partial class Initial : Migration {

    protected override void Up(MigrationBuilder migrationBuilder) {
      migrationBuilder.CreateTable(
          name: "AnnotationTypes",
          columns: table => new {
            AnnotationTypeId = table.Column<long>(nullable: false)
                  .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
            Name = table.Column<string>(maxLength: 255, nullable: false),
            Root = table.Column<string>(maxLength: 10, nullable: false)
          },
          constraints: table => {
            table.PrimaryKey("PK_AnnotationTypes", x => x.AnnotationTypeId);
            table.UniqueConstraint("AK_AnnotationTypes_Name", x => x.Name);
            table.UniqueConstraint("AK_AnnotationTypes_Root", x => x.Root);
          });

      migrationBuilder.CreateTable(
          name: "DocumentTypes",
          columns: table => new {
            DocumentTypeId = table.Column<long>(nullable: false)
                  .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
            Name = table.Column<string>(maxLength: 255, nullable: false)
          },
          constraints: table => {
            table.PrimaryKey("PK_DocumentTypes", x => x.DocumentTypeId);
            table.UniqueConstraint("AK_DocumentTypes_Name", x => x.Name);
          });

      migrationBuilder.CreateTable(
          name: "EntityTypes",
          columns: table => new {
            EntityTypeId = table.Column<long>(nullable: false)
                  .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
            Name = table.Column<string>(maxLength: 255, nullable: false)
          },
          constraints: table => {
            table.PrimaryKey("PK_EntityTypes", x => x.EntityTypeId);
            table.UniqueConstraint("AK_EntityTypes_Name", x => x.Name);
          });

      migrationBuilder.CreateTable(
          name: "Files",
          columns: table => new {
            DocumentId = table.Column<long>(nullable: false)
                  .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
            Name = table.Column<string>(nullable: false),
            Issue = table.Column<string>(nullable: true)
          },
          constraints: table => {
            table.PrimaryKey("PK_Files", x => x.DocumentId);
          });

      migrationBuilder.CreateTable(
          name: "Entities",
          columns: table => new {
            EntityId = table.Column<long>(nullable: false)
                  .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
            Name = table.Column<string>(maxLength: 255, nullable: false),
            Email = table.Column<string>(maxLength: 255, nullable: false),
            EntityTypeId = table.Column<long>(nullable: false)
          },
          constraints: table => {
            table.PrimaryKey("PK_Entities", x => x.EntityId);
            table.UniqueConstraint("AK_Entities_Name", x => x.Name);
            table.ForeignKey(
                      name: "FK_Entities_EntityTypes_EntityTypeId",
                      column: x => x.EntityTypeId,
                      principalTable: "EntityTypes",
                      principalColumn: "EntityTypeId",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "EntityTypeDocumentType",
          columns: table => new {
            EntityTypeId = table.Column<long>(nullable: false),
            DocumentTypeId = table.Column<long>(nullable: false)
          },
          constraints: table => {
            table.PrimaryKey("PK_EntityTypeDocumentType", x => new { x.EntityTypeId, x.DocumentTypeId });
            table.ForeignKey(
                      name: "FK_EntityTypeDocumentType_DocumentTypes_DocumentTypeId",
                      column: x => x.DocumentTypeId,
                      principalTable: "DocumentTypes",
                      principalColumn: "DocumentTypeId",
                      onDelete: ReferentialAction.Restrict);
            table.ForeignKey(
                      name: "FK_EntityTypeDocumentType_EntityTypes_EntityTypeId",
                      column: x => x.EntityTypeId,
                      principalTable: "EntityTypes",
                      principalColumn: "EntityTypeId",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "Documents",
          columns: table => new {
            DocumentId = table.Column<long>(nullable: false)
                  .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
            DocumentTypeId = table.Column<long>(nullable: false),
            EntityId = table.Column<long>(nullable: false),
            Number = table.Column<string>(maxLength: 20, nullable: false),
            PublicationYear = table.Column<long>(nullable: false),
            PublicationDate = table.Column<DateTime>(nullable: false),
            FileDocumentId = table.Column<long>(nullable: false)
          },
          constraints: table => {
            table.PrimaryKey("PK_Documents", x => x.DocumentId);
            table.UniqueConstraint("AK_Documents_DocumentTypeId_EntityId_Number_PublicationYear", x => new { x.DocumentTypeId, x.EntityId, x.Number, x.PublicationYear });
            table.ForeignKey(
                      name: "FK_Documents_DocumentTypes_DocumentTypeId",
                      column: x => x.DocumentTypeId,
                      principalTable: "DocumentTypes",
                      principalColumn: "DocumentTypeId",
                      onDelete: ReferentialAction.Restrict);
            table.ForeignKey(
                      name: "FK_Documents_Entities_EntityId",
                      column: x => x.EntityId,
                      principalTable: "Entities",
                      principalColumn: "EntityId",
                      onDelete: ReferentialAction.Restrict);
            table.ForeignKey(
                      name: "FK_Documents_Files_FileDocumentId",
                      column: x => x.FileDocumentId,
                      principalTable: "Files",
                      principalColumn: "DocumentId",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateIndex(
          name: "IX_Documents_EntityId",
          table: "Documents",
          column: "EntityId");

      migrationBuilder.CreateIndex(
          name: "IX_Documents_FileDocumentId",
          table: "Documents",
          column: "FileDocumentId",
          unique: true);

      migrationBuilder.CreateIndex(
          name: "IX_Entities_EntityTypeId",
          table: "Entities",
          column: "EntityTypeId");

      migrationBuilder.CreateIndex(
          name: "IX_EntityTypeDocumentType_DocumentTypeId",
          table: "EntityTypeDocumentType",
          column: "DocumentTypeId");

      migrationBuilder.UpdateDirectory();
    }

    protected override void Down(MigrationBuilder migrationBuilder) {
      migrationBuilder.DropTable(
          name: "AnnotationTypes");

      migrationBuilder.DropTable(
          name: "Documents");

      migrationBuilder.DropTable(
          name: "EntityTypeDocumentType");

      migrationBuilder.DropTable(
          name: "Entities");

      migrationBuilder.DropTable(
          name: "Files");

      migrationBuilder.DropTable(
          name: "DocumentTypes");

      migrationBuilder.DropTable(
          name: "EntityTypes");
    }
  }
}