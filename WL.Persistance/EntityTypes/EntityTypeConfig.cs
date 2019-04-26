using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using WL.Domain;

namespace WL.Persistance.EntityTypes {

  internal class EntityTypeConfig : IEntityTypeConfiguration<EntityType> {

    public void Configure(EntityTypeBuilder<EntityType> builder) {
      builder.HasAlternateKey(et => et.Name);
      SeedData(builder);
      builder.Property(dt => dt.Id)
        .HasDefaultValueSql("\"EntityTypesSeq\".NEXTVAL");
    }

    public void SeedData(EntityTypeBuilder<EntityType> builder) {
      builder.HasData(
        new { Id = 1L, Name = "Rectoría" },
        new { Id = 2L, Name = "Facultad" },
        new { Id = 3L, Name = "Departamento" }
        );
    }
  }
}