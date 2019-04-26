using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using WL.Domain;

namespace WL.Persistance.Entities {

  internal class EntityConfig : IEntityTypeConfiguration<Entity> {

    public void Configure(EntityTypeBuilder<Entity> entity) {
      entity.HasIndex(e => e.Name).IsUnique();
      SeedData(entity);
    }

    private void SeedData(EntityTypeBuilder<Entity> entities) {
      entities.HasData(
        new { Id = 1L, Name = "Rectoría", Email = "rectoria@udenar.edu.co", EntityTypeId = 1L },
        new { Id = 2L, Name = "Facultad de Ingeniería", Email = "ingenieria@udenar.edu.co", EntityTypeId = 2L },
        new { Id = 3L, Name = "Departamento de Sistemas", Email = "sistemas@udenar.edu.co", EntityTypeId = 3L },
        new { Id = 4L, Name = "Facultad de Medicina", Email = "medicina@udenar.edu.co", EntityTypeId = 2L },
        new { Id = 5L, Name = "Depatamento de Enfermería", Email = "enfermeria@udenar.edu.co", EntityTypeId = 3L }
        );
    }
  }
}