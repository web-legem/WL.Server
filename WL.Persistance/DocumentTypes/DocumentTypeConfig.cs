using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using WL.Domain;

namespace WL.Persistance.DocumentTypes {

  internal class DocumentTypeConfig : IEntityTypeConfiguration<DocumentType> {

    public void Configure(EntityTypeBuilder<DocumentType> documentType) {
      documentType.HasIndex(dt => dt.Name)
        .IsUnique();
      //.HasName("UQ_DT_Name"); // Indice unico, diferente de AlternateKey

      AddSeedData(documentType);
      documentType.Property(dt => dt.Id)
        .HasDefaultValueSql("\"DocumentTypesSeq\".NEXTVAL");
    }

    private void AddSeedData(EntityTypeBuilder<DocumentType> documentTypes) {
      documentTypes.HasData(
        new { Id = 1L, Name = "Acuerdo" },
        new { Id = 2L, Name = "Circular" },
        new { Id = 3L, Name = "Resolución" },
        new { Id = 4L, Name = "Ley" },
        new { Id = 5L, Name = "Decreto" }
        );
    }
  }
}