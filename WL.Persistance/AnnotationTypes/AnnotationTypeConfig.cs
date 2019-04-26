using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using WL.Domain;

namespace WL.Persistance.AnnotationTypes {

  internal class AnnotationTypeConfig : IEntityTypeConfiguration<AnnotationType> {

    public void Configure(EntityTypeBuilder<AnnotationType> builder) {
      builder.HasIndex(at => at.Name).IsUnique();
      builder.HasIndex(at => at.Root).IsUnique();

      SeedData(builder);
      builder.Property(dt => dt.Id)
        .HasDefaultValueSql("\"AnnotationTypesSeq\".NEXTVAL");
    }

    private void SeedData(EntityTypeBuilder<AnnotationType> builder) {
      builder.HasData(
        new { Id = 1L, Name = "Deroga", Root = "Derog" },
        new { Id = 2L, Name = "Aclara", Root = "Acl" },
        new { Id = 3L, Name = "Reglamenta", Root = "Regl" }
        );
    }
  }
}