using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WL.Domain;

namespace WL.Persistance.Annotations {

  public class AnnotationsConfig : IEntityTypeConfiguration<Annotation> {

    public void Configure(EntityTypeBuilder<Annotation> annotation) {
      annotation.HasOne(a => a.From)
        .WithMany()
        .HasForeignKey(a => a.FromDocumentId)
        .OnDelete(DeleteBehavior.Restrict);

      annotation.HasOne(a => a.To)
        .WithMany()
        .HasForeignKey(a => a.ToDocumentId)
        .OnDelete(DeleteBehavior.Restrict);

      annotation.HasIndex(a => new { a.FromDocumentId, a.ToDocumentId })
        .IsUnique();
    }
  }
}