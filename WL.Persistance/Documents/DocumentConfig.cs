using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using WL.Domain;

namespace WL.Persistance.Documents {

  internal class DocumentConfig : IEntityTypeConfiguration<Document> {

    public void Configure(EntityTypeBuilder<Document> document) {
      document.HasAlternateKey(d => new {
        d.DocumentTypeId,
        d.EntityId,
        d.Number,
        d.PublicationYear
      });

      document.HasOne(d => d.DocumentType)
        .WithMany()
        .OnDelete(DeleteBehavior.Restrict);

      document.HasOne(d => d.Entity)
        .WithMany()
        .OnDelete(DeleteBehavior.Restrict);
    }
  }
}