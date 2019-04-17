using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using WL.Domain;

namespace WL.Persistance.DocumentTypes {

  internal class DocumentTypeConfig : IEntityTypeConfiguration<DocumentType> {

    public void Configure(EntityTypeBuilder<DocumentType> documentType) {
      documentType.HasAlternateKey(dt => dt.Name);
    }
  }
}