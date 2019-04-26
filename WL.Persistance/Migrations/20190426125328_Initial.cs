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
            Id = table.Column<long>(nullable: false)
                  .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
            Name = table.Column<string>(maxLength: 255, nullable: false),
            Root = table.Column<string>(maxLength: 10, nullable: false)
          },
          constraints: table => {
            table.PrimaryKey("PK_AT", x => x.Id);
            table.UniqueConstraint("AK_AT_N", x => x.Name);
            table.UniqueConstraint("AK_AT_R", x => x.Root);
          });

      migrationBuilder.CreateTable(
          name: "DocumentTypes",
          columns: table => new {
            Id = table.Column<long>(nullable: false)
                  .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
            Name = table.Column<string>(maxLength: 255, nullable: false)
          },
          constraints: table => {
            table.PrimaryKey("PK_DT", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "EntityTypes",
          columns: table => new {
            Id = table.Column<long>(nullable: false)
                  .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
            Name = table.Column<string>(maxLength: 255, nullable: false)
          },
          constraints: table => {
            table.PrimaryKey("PK_ET", x => x.Id);
            table.UniqueConstraint("AK_ET_N", x => x.Name);
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
            table.PrimaryKey("PK_F", x => x.DocumentId);
          });

      migrationBuilder.CreateTable(
          name: "Roles",
          columns: table => new {
            Id = table.Column<long>(nullable: false)
                  .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
            Name = table.Column<string>(maxLength: 200, nullable: false),
            ConfigSystem = table.Column<int>(nullable: false),
            CreateDocuments = table.Column<int>(nullable: false),
            DeleteDocuments = table.Column<int>(nullable: false)
          },
          constraints: table => {
            table.PrimaryKey("PK_R", x => x.Id);
            table.UniqueConstraint("AK_R_N", x => x.Name);
          });

      migrationBuilder.CreateTable(
          name: "Entities",
          columns: table => new {
            Id = table.Column<long>(nullable: false)
                  .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
            Name = table.Column<string>(maxLength: 255, nullable: false),
            Email = table.Column<string>(maxLength: 255, nullable: false),
            EntityTypeId = table.Column<long>(nullable: false)
          },
          constraints: table => {
            table.PrimaryKey("PK_E", x => x.Id);
            table.UniqueConstraint("AK_E_N", x => x.Name);
            table.ForeignKey(
                      name: "FK_E_ET_ETI",
                      column: x => x.EntityTypeId,
                      principalTable: "EntityTypes",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "EntityTypeDocumentType",
          columns: table => new {
            EntityTypeId = table.Column<long>(nullable: false),
            DocumentTypeId = table.Column<long>(nullable: false)
          },
          constraints: table => {
            table.PrimaryKey("AK_ETDT_UQ", x => new { x.EntityTypeId, x.DocumentTypeId });
            table.ForeignKey(
                      name: "FK_ETDT_DT",
                      column: x => x.DocumentTypeId,
                      principalTable: "DocumentTypes",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Restrict);
            table.ForeignKey(
                      name: "FK_ETDT_ET",
                      column: x => x.EntityTypeId,
                      principalTable: "EntityTypes",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "Users",
          columns: table => new {
            Id = table.Column<long>(nullable: false)
                  .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
            Nickname = table.Column<string>(maxLength: 200, nullable: false),
            FirstName = table.Column<string>(maxLength: 200, nullable: false),
            LastName = table.Column<string>(maxLength: 200, nullable: false),
            IDDocument = table.Column<string>(maxLength: 30, nullable: false),
            Password = table.Column<string>(nullable: false),
            Email = table.Column<string>(nullable: false),
            State = table.Column<string>(nullable: false),
            RoleId = table.Column<long>(nullable: false)
          },
          constraints: table => {
            table.PrimaryKey("PK_U", x => x.Id);
            table.UniqueConstraint("AK_U_E", x => x.Email);
            table.UniqueConstraint("AK_U_IDD", x => x.IDDocument);
            table.UniqueConstraint("AK_U_N", x => x.Nickname);
            table.ForeignKey(
                      name: "FK_U_R_RI",
                      column: x => x.RoleId,
                      principalTable: "Roles",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "Documents",
          columns: table => new {
            Id = table.Column<long>(nullable: false)
                  .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
            DocumentTypeId = table.Column<long>(nullable: false),
            EntityId = table.Column<long>(nullable: false),
            Number = table.Column<string>(maxLength: 20, nullable: false),
            PublicationYear = table.Column<long>(nullable: false),
            PublicationDate = table.Column<DateTime>(nullable: false),
            FileDocumentId = table.Column<long>(nullable: false)
          },
          constraints: table => {
            table.PrimaryKey("PK_D", x => x.Id);
            table.UniqueConstraint("AK_D_DTI_EI_N_PY", x => new { x.DocumentTypeId, x.EntityId, x.Number, x.PublicationYear });
            table.ForeignKey(
                      name: "FK_D_DT_DTI",
                      column: x => x.DocumentTypeId,
                      principalTable: "DocumentTypes",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Restrict);
            table.ForeignKey(
                      name: "FK_D_E_EI",
                      column: x => x.EntityId,
                      principalTable: "Entities",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Restrict);
            table.ForeignKey(
                      name: "FK_D_F_FDI",
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
            table.PrimaryKey("PK_C", x => x.UserId);
            table.UniqueConstraint("AK_C_T", x => x.Token);
            table.ForeignKey(
                      name: "FK_C_U_UI",
                      column: x => x.UserId,
                      principalTable: "Users",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "RestoreCredentials",
          columns: table => new {
            UserId = table.Column<long>(nullable: false),
            Token = table.Column<string>(nullable: true)
          },
          constraints: table => {
            table.PrimaryKey("PK_RC", x => x.UserId);
            table.ForeignKey(
                      name: "FK_RC_U_UI",
                      column: x => x.UserId,
                      principalTable: "Users",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "Annotations",
          columns: table => new {
            Id = table.Column<long>(nullable: false)
                  .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
            FromDocumentId = table.Column<long>(nullable: false),
            FromId = table.Column<long>(nullable: true),
            ToDocumentId = table.Column<long>(nullable: false),
            ToId = table.Column<long>(nullable: true),
            AnnotationTypeId = table.Column<long>(nullable: false),
            Description = table.Column<string>(nullable: true)
          },
          constraints: table => {
            table.PrimaryKey("PK_A", x => x.Id);
            table.UniqueConstraint("AK_A_FDI_TDI", x => new { x.FromDocumentId, x.ToDocumentId });
            table.ForeignKey(
                      name: "FK_A_AT_ATI",
                      column: x => x.AnnotationTypeId,
                      principalTable: "AnnotationTypes",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_A_D_FI",
                      column: x => x.FromId,
                      principalTable: "Documents",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Restrict);
            table.ForeignKey(
                      name: "FK_A_D_TI",
                      column: x => x.ToId,
                      principalTable: "Documents",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Restrict);
          });

      migrationBuilder.CreateIndex(
          name: "IX_A_ATI",
          table: "Annotations",
          column: "AnnotationTypeId");

      migrationBuilder.CreateIndex(
          name: "IX_A_FI",
          table: "Annotations",
          column: "FromId");

      migrationBuilder.CreateIndex(
          name: "IX_A_TI",
          table: "Annotations",
          column: "ToId");

      migrationBuilder.CreateIndex(
          name: "IX_D_EI",
          table: "Documents",
          column: "EntityId");

      migrationBuilder.CreateIndex(
          name: "IX_D_FDI",
          table: "Documents",
          column: "FileDocumentId",
          unique: true);

      migrationBuilder.CreateIndex(
          name: "UQ_DT_N",
          table: "DocumentTypes",
          column: "Name",
          unique: true);

      migrationBuilder.CreateIndex(
          name: "IX_E_ETI",
          table: "Entities",
          column: "EntityTypeId");

      migrationBuilder.CreateIndex(
          name: "IX_ETDT_DTI",
          table: "EntityTypeDocumentType",
          column: "DocumentTypeId");

      migrationBuilder.CreateIndex(
          name: "IX_U_RI",
          table: "Users",
          column: "RoleId");

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
          name: "RestoreCredentials");

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