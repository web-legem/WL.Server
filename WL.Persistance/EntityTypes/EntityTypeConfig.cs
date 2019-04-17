using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Persistance.EntityTypes {

  internal class EntityTypeConfig : IEntityTypeConfiguration<EntityType> {

    public void Configure(EntityTypeBuilder<EntityType> builder) {
      builder.HasAlternateKey(et => et.Name);
    }
  }
}