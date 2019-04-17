using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using WL.Domain;

namespace WL.Persistance.Entities {

  internal class EntityConfig : IEntityTypeConfiguration<Entity> {

    public void Configure(EntityTypeBuilder<Entity> entity) {
      entity.HasAlternateKey(e => e.Name);
    }
  }
}