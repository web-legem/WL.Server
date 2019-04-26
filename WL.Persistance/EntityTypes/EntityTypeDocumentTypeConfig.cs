using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using WL.Domain;

namespace WL.Persistance.EntityTypes {

  internal class EntityTypeDocumentTypeConfig : IEntityTypeConfiguration<EntityTypeDocumentType> {

    public void Configure(EntityTypeBuilder<EntityTypeDocumentType> entityTypeDocumentType) {
      entityTypeDocumentType
        .HasKey(etdt => new { etdt.EntityTypeId, etdt.DocumentTypeId });
      //.HasName("AK_ETDT_UQ"); // Para los nombres de los indices unicos

      entityTypeDocumentType
        .HasOne(etdt => etdt.DocumentType)
        .WithMany()
        .HasForeignKey(etdt => etdt.DocumentTypeId)
        //.HasConstraintName("FK_ETDT_DT") // Para los nombres de las llaves foraneas
        .OnDelete(DeleteBehavior.Restrict);

      entityTypeDocumentType
        .HasOne(etdt => etdt.EntityType)
        .WithMany(et => et.SupportedDocuments)
        .HasForeignKey(etdt => etdt.EntityTypeId)
        //.HasConstraintName("FK_ETDT_ET")
        .OnDelete(DeleteBehavior.Cascade);

      SeedData(entityTypeDocumentType);
    }

    public void SeedData(EntityTypeBuilder<EntityTypeDocumentType> entityTypeDocumentType) {
      entityTypeDocumentType.HasData(
        new { DocumentTypeId = 1L, EntityTypeId = 1L },
        new { DocumentTypeId = 2L, EntityTypeId = 1L },
        new { DocumentTypeId = 3L, EntityTypeId = 1L },
        new { DocumentTypeId = 3L, EntityTypeId = 2L },
        new { DocumentTypeId = 5L, EntityTypeId = 2L },
        new { DocumentTypeId = 1L, EntityTypeId = 2L },
        new { DocumentTypeId = 4L, EntityTypeId = 3L },
        new { DocumentTypeId = 2L, EntityTypeId = 3L }
        );
    }
  }
}