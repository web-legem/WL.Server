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
          name: "Roles",
          columns: table => new {
            RoleId = table.Column<long>(nullable: false)
                  .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
            Name = table.Column<string>(maxLength: 200, nullable: false),
            ConfigSystem = table.Column<int>(nullable: false),
            CreateDocuments = table.Column<int>(nullable: false),
            DeleteDocuments = table.Column<int>(nullable: false)
          },
          constraints: table => {
            table.PrimaryKey("PK_Roles", x => x.RoleId);
            table.UniqueConstraint("AK_Roles_Name", x => x.Name);
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
                      name: "FK_ETDT_DT",
                      column: x => x.DocumentTypeId,
                      principalTable: "DocumentTypes",
                      principalColumn: "DocumentTypeId",
                      onDelete: ReferentialAction.Restrict);
            table.ForeignKey(
                      name: "FK_ETDT_ET",
                      column: x => x.EntityTypeId,
                      principalTable: "EntityTypes",
                      principalColumn: "EntityTypeId",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "Users",
          columns: table => new {
            UserId = table.Column<long>(nullable: false)
                  .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
            Nickname = table.Column<string>(maxLength: 200, nullable: false),
            FirstName = table.Column<string>(maxLength: 200, nullable: false),
            LastName = table.Column<string>(maxLength: 200, nullable: false),
            IDDocument = table.Column<string>(maxLength: 30, nullable: false),
            Password = table.Column<string>(nullable: false),
            Email = table.Column<string>(nullable: false),
            State = table.Column<string>(nullable: false),
            RoleId = table.Column<int>(nullable: false),
            RoleId1 = table.Column<long>(nullable: true)
          },
          constraints: table => {
            table.PrimaryKey("PK_Users", x => x.UserId);
            table.UniqueConstraint("AK_Users_Email", x => x.Email);
            table.UniqueConstraint("AK_Users_IDDocument", x => x.IDDocument);
            table.UniqueConstraint("AK_Users_Nickname", x => x.Nickname);
            table.ForeignKey(
                      name: "FK_Users_Roles_RoleId1",
                      column: x => x.RoleId1,
                      principalTable: "Roles",
                      principalColumn: "RoleId",
                      onDelete: ReferentialAction.Restrict);
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

      migrationBuilder.CreateTable(
          name: "Credentials",
          columns: table => new {
            UserId = table.Column<long>(nullable: false),
            Token = table.Column<string>(nullable: false),
            Creation = table.Column<DateTime>(nullable: false)
          },
          constraints: table => {
            table.PrimaryKey("PK_Credentials", x => x.UserId);
            table.UniqueConstraint("AK_Credentials_Token", x => x.Token);
            table.ForeignKey(
                      name: "FK_Credentials_Users_UserId",
                      column: x => x.UserId,
                      principalTable: "Users",
                      principalColumn: "UserId",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "Restores",
          columns: table => new {
            UserId = table.Column<long>(nullable: false),
            Token = table.Column<string>(nullable: true)
          },
          constraints: table => {
            table.PrimaryKey("PK_Restores", x => x.UserId);
            table.ForeignKey(
                      name: "FK_Restores_Users_UserId",
                      column: x => x.UserId,
                      principalTable: "Users",
                      principalColumn: "UserId",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "Annotations",
          columns: table => new {
            AnnotationId = table.Column<long>(nullable: false)
                  .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
            FromDocumentId = table.Column<long>(nullable: false),
            ToDocumentId = table.Column<long>(nullable: false),
            AnnotationTypeId = table.Column<long>(nullable: false),
            Description = table.Column<string>(nullable: true)
          },
          constraints: table => {
            table.PrimaryKey("PK_Annotations", x => x.AnnotationId);
            table.UniqueConstraint("AK_Annotations_FromDocumentId_ToDocumentId", x => new { x.FromDocumentId, x.ToDocumentId });
            table.ForeignKey(
                      name: "FK_Annotations_AnnotationTypes_AnnotationTypeId",
                      column: x => x.AnnotationTypeId,
                      principalTable: "AnnotationTypes",
                      principalColumn: "AnnotationTypeId",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_Annotations_Documents_FromDocumentId",
                      column: x => x.FromDocumentId,
                      principalTable: "Documents",
                      principalColumn: "DocumentId",
                      onDelete: ReferentialAction.Restrict);
            table.ForeignKey(
                      name: "FK_Annotations_Documents_ToDocumentId",
                      column: x => x.ToDocumentId,
                      principalTable: "Documents",
                      principalColumn: "DocumentId",
                      onDelete: ReferentialAction.Restrict);
          });

      migrationBuilder.CreateIndex(
          name: "IX_Annotations_AnnotationTypeId",
          table: "Annotations",
          column: "AnnotationTypeId");

      migrationBuilder.CreateIndex(
          name: "IX_Annotations_ToDocumentId",
          table: "Annotations",
          column: "ToDocumentId");

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

      migrationBuilder.CreateIndex(
          name: "IX_Users_RoleId1",
          table: "Users",
          column: "RoleId1");

      migrationBuilder.UpdateDirectory();
    }

    protected override void Down(MigrationBuilder migrationBuilder) {
      migrationBuilder.DropTable(
          name: "Annotations");

      migrationBuilder.DropTable(
          name: "Credentials");

      migrationBuilder.DropTable(
          name: "EntityTypeDocumentType");

      migrationBuilder.DropTable(
          name: "Restores");

      migrationBuilder.DropTable(
          name: "AnnotationTypes");

      migrationBuilder.DropTable(
          name: "Documents");

      migrationBuilder.DropTable(
          name: "Users");

      migrationBuilder.DropTable(
          name: "DocumentTypes");

      migrationBuilder.DropTable(
          name: "Entities");

      migrationBuilder.DropTable(
          name: "Files");

      migrationBuilder.DropTable(
          name: "Roles");

      migrationBuilder.DropTable(
          name: "EntityTypes");
    }
  }
}