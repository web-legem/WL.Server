using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

using WL.Domain;

namespace WL.Persistance {

  public class WLDbContext : DbContext {
    public DbSet<DocumentType> DocumentTypes { get; set; }
    public DbSet<EntityType> EntityTypes { get; set; }
    public DbSet<AnnotationType> AnnotationTypes { get; set; }
    public DbSet<Entity> Entities { get; set; }
    public DbSet<Document> Documents { get; set; }

    public WLDbContext(DbContextOptions<WLDbContext> options)
      : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      modelBuilder.Entity<Document>().HasIndex(document => new {
        document.DocumentTypeId,
        document.EntityTypeId,
        document.Number,
        document.PublicationDate
      }).IsUnique();

      modelBuilder.Entity<EntityTypeDocumentType>()
        .HasKey(e => new { e.EntityTypeId, e.DocumentTypeId });
    }
  }
}