using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using WL.Domain;

namespace WL.Persistance.DocumentTypes {

  internal class DocumentTypeConfig : IEntityTypeConfiguration<DocumentType> {

    public void Configure(EntityTypeBuilder<DocumentType> documentType) {
      documentType.HasIndex(dt => dt.Name)
        .IsUnique()
        .HasName("UQ_DT_Name"); // Indice unico, diferente de AlternateKey
    }
  }
}