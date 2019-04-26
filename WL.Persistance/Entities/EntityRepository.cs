using System;
using System.Linq;

using WL.Application.Interfaces.Persistance;
using WL.Domain;

namespace WL.Persistance.Entities {
   public class EntityRepository : IEntityRepository {
      readonly WLDbContext context;

      public EntityRepository(WLDbContext context) {
         this.context = context;
      }

      public Entity Get(long id) {
         try {
            return context.Entities.Find(id);
         }
         catch (Exception e) {
            throw ExceptionsToValidations.ExceptionsToValidations.WrapOracleExceptionsWithError(e);
         }
      }

      public IQueryable<Entity> GetAll() {
         try {
            return context.Entities;
         }
         catch (Exception e) {
            throw ExceptionsToValidations.ExceptionsToValidations.WrapOracleExceptionsWithError(e);
         }
      }

      public Entity Create(Entity entity) {
         try {
            context.Entities.Add(entity);
            context.SaveChanges();
            return entity;
         }
         catch (Exception e) {
            throw ExceptionsToValidations.ExceptionsToValidations.WrapOracleExceptionsWithError(e);
         }
      }

      public Entity Update(Entity entity) {
         try {
            var original = context.Entities.Find(entity.Id);
            original.Name = entity.Name;
            original.Email = entity.Email;
            original.EntityTypeId = entity.EntityTypeId;
            context.SaveChanges();
            return original;
         }
         catch (Exception e) {
            throw ExceptionsToValidations.ExceptionsToValidations.WrapOracleExceptionsWithError(e);
         }
      }

      public void Delete(long id) {
         try {
         }
         catch (Exception e) {
            throw ExceptionsToValidations.ExceptionsToValidations.WrapOracleExceptionsWithError(e);
         }
         var original = context.Entities.Find(id);
         context.Entities.Remove(original);
         context.SaveChanges();
      }
   }
}