using Microsoft.EntityFrameworkCore;

using WL.Domain;
using WL.Persistance.AnnotationTypes;
using WL.Persistance.Documents;
using WL.Persistance.Documents.Files;
using WL.Persistance.DocumentTypes;
using WL.Persistance.Entities;
using WL.Persistance.EntityTypes;

namespace WL.Persistance {

  public class WLDbContext : DbContext {
    public DbSet<DocumentType> DocumentTypes { get; set; }
    public DbSet<EntityType> EntityTypes { get; set; }
    public DbSet<AnnotationType> AnnotationTypes { get; set; }
    public DbSet<Entity> Entities { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<File> Files { get; set; }

    public WLDbContext(DbContextOptions<WLDbContext> options)
      : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      modelBuilder.ApplyConfiguration(new DocumentTypeConfig());
      modelBuilder.ApplyConfiguration(new EntityTypeConfig());
      modelBuilder.ApplyConfiguration(new EntityTypeDocumentTypeConfig());
      modelBuilder.ApplyConfiguration(new EntityConfig());
      modelBuilder.ApplyConfiguration(new AnnotationTypeConfig());
      modelBuilder.ApplyConfiguration(new DocumentConfig());
      modelBuilder.ApplyConfiguration(new FilesConfig());
    }
  }
}