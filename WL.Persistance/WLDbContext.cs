using Microsoft.EntityFrameworkCore;
using System;
using WL.Domain;
using WL.Domain.User;
using WL.Persistance.Annotations;
using WL.Persistance.AnnotationTypes;
using WL.Persistance.Documents;
using WL.Persistance.Documents.Files;
using WL.Persistance.DocumentTypes;
using WL.Persistance.Entities;
using WL.Persistance.EntityTypes;
using WL.Persistance.Roles;
using WL.Persistance.Users;

namespace WL.Persistance {

  public class WLDbContext : DbContext {
    public DbSet<DocumentType> DocumentTypes { get; set; }
    public DbSet<EntityType> EntityTypes { get; set; }
    public DbSet<AnnotationType> AnnotationTypes { get; set; }
    public DbSet<Entity> Entities { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<File> Files { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Credential> Credentials { get; set; }
    public DbSet<Restore> Restores { get; set; }
    public DbSet<Annotation> Annotations { get; set; }

    public DbQuery<AuxCount> Counts { get; set; }

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
      modelBuilder.ApplyConfiguration(new RolesConfig());
      modelBuilder.ApplyConfiguration(new UsersConfig());
      modelBuilder.ApplyConfiguration(new CredentialsConfig());
      modelBuilder.ApplyConfiguration(new RestoresConfig());
      modelBuilder.ApplyConfiguration(new AnnotationsConfig());
    }
  }
}