using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using WL.Domain;

namespace WL.Persistance.EntityTypes {

  internal class EntityTypeConfig : IEntityTypeConfiguration<EntityType> {

    public void Configure(EntityTypeBuilder<EntityType> builder) {
      builder.HasAlternateKey(et => et.Name);
    }
  }
}