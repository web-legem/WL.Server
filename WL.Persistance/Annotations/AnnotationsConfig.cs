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
        .OnDelete(DeleteBehavior.Restrict);

      annotation.HasOne(a => a.To)
        .WithMany()
        .OnDelete(DeleteBehavior.Restrict);

      annotation.HasAlternateKey(a => new { a.FromDocumentId, a.ToDocumentId });
    }
  }
}