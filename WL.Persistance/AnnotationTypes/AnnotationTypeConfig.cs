using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WL.Domain;

namespace WL.Persistance.AnnotationTypes {

  internal class AnnotationTypeConfig : IEntityTypeConfiguration<AnnotationType> {

    public void Configure(EntityTypeBuilder<AnnotationType> builder) {
      builder.HasAlternateKey(at => at.Name);
      builder.HasAlternateKey(at => at.Root);
    }
  }
}