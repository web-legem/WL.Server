using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WL.Application.Interfaces.Persistance;
using WL.Domain;

namespace WL.Persistance.EntityTypes {

  public class EntityTypeRepository : IEntityTypeRepository {
    readonly WLDbContext context;

    public EntityTypeRepository(WLDbContext context) {
      this.context = context;
    }

    public EntityType Get(long id) {
      return context.EntityTypes
        .AsNoTracking()
        .Include(et => et.SupportedDocuments)
        .ThenInclude(sd => sd.DocumentType)
        .First(et => et.EntityTypeId == id);
    }

    public IQueryable<EntityType> GetAll() {
      return context.EntityTypes
        .AsNoTracking()
        .Include(et => et.SupportedDocuments)
        .ThenInclude(sd => sd.DocumentType);
    }

    public EntityType Create(EntityType entity) {
      context.EntityTypes.Add(entity);

      context.SaveChanges();

      return entity;
    }

    public EntityType Update(EntityType entity) {
      var original = context.EntityTypes
        .Include(et => et.SupportedDocuments)
        .First(et => et.EntityTypeId == entity.EntityTypeId);

      original.Name = entity.Name;
      original.SupportedDocuments = entity.SupportedDocuments;

      context.SaveChanges();

      return original;
    }

    public void Delete(long id) {
      var original = context.EntityTypes.Find(id);

      context.EntityTypes.Remove(original);

      context.SaveChanges();
    }
  }
}