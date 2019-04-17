using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using WL.Domain;

namespace WL.Persistance.EntityTypes {

  internal class EntityTypeDocumentTypeConfig : IEntityTypeConfiguration<EntityTypeDocumentType> {

    public void Configure(EntityTypeBuilder<EntityTypeDocumentType> entityTypeDocumentType) {
      entityTypeDocumentType
        .HasKey(etdt => new { etdt.EntityTypeId, etdt.DocumentTypeId });

      entityTypeDocumentType
        .HasOne(etdt => etdt.DocumentType)
        .WithMany()
        .OnDelete(DeleteBehavior.Restrict);

      entityTypeDocumentType
        .HasOne(etdt => etdt.EntityType)
        .WithMany(et => et.SupportedDocuments)
        .OnDelete(DeleteBehavior.Cascade);
    }
  }
}