using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using WL.Application.Interfaces.Persistance;
using WL.Domain;
using static WL.Persistance.ExceptionsToValidations.ExceptionsToValidations;
using static WL.Persistance.Helpers.DbHelpers;


namespace WL.Persistance.EntityTypes {
   public class EntityTypeRepository : IEntityTypeRepository {
      readonly WLDbContext context;


      public EntityTypeRepository(WLDbContext context) {
         this.context = context;
      }

      public EntityType Get(long id) {
         try {
            return NullVerifier(() => context.EntityTypes
           .AsNoTracking()
           .Include(et => et.SupportedDocuments)
           .ThenInclude(sd => sd.DocumentType)
           .First(et => et.Id == id));
         }
         catch (Exception e) {
            throw WrapOracleException(e);
         }
      }

      public IQueryable<EntityType> GetAll() {
         try {
            return context.EntityTypes
           .AsNoTracking()
           .Include(et => et.SupportedDocuments)
           .ThenInclude(sd => sd.DocumentType);
         }
         catch (Exception e) {
            throw WrapOracleException(e);
         }
      }

      public EntityType Create(EntityType entity) {
         try {
            context.EntityTypes.Add(entity);
            context.SaveChanges();
            return entity;
         }
         catch (Exception e) {
            throw WrapOracleException(e);
         }
      }

      public EntityType Update(EntityType entity) {
         try {
            var original = Get(entity.Id);
            original.Name = entity.Name;
            original.SupportedDocuments = entity.SupportedDocuments;

            context.SaveChanges();

            return original;
         }
         catch (Exception e) {
            throw WrapOracleException(e);
         }
      }

      public void Delete(long id) {
         try {
            var original = Get(id);
            context.EntityTypes.Remove(original);
            context.SaveChanges();
         }
         catch (Exception e) {
            throw WrapOracleException(e);
         }
      }
   }
}