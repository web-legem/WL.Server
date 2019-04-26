using System;
using System.Linq;
using WL.Application.Interfaces.Persistance;
using WL.Domain;

namespace WL.Persistance.AnnotationTypes {

   public class AnnotationTypeRepository : IAnnotationTypeRepository {
      readonly WLDbContext context;

      public AnnotationTypeRepository(WLDbContext context) {
         this.context = context;
      }

      public AnnotationType Get(long id) {
         try {
            return context.AnnotationTypes.Find(id);
         }
         catch (Exception e) {
            throw ExceptionsToValidations.ExceptionsToValidations.WrapOracleExceptionsWithError(e);
         }
      }

      public IQueryable<AnnotationType> GetAll() {
         try {
            return context.AnnotationTypes;
         }
         catch (Exception e) {
            throw ExceptionsToValidations.ExceptionsToValidations.WrapOracleExceptionsWithError(e);
         }
      }

      public AnnotationType Create(AnnotationType annotationType) {
         try {
            context.AnnotationTypes.Add(annotationType);
            context.SaveChanges();
            return annotationType;
         }
         catch (Exception e) {
            throw ExceptionsToValidations.ExceptionsToValidations.WrapOracleExceptionsWithError(e);
         }
      }

      public AnnotationType Update(AnnotationType updated) {
         try {
            var original = context.AnnotationTypes.Find(updated.Id);
            original.Name = updated.Name;
            original.Root = updated.Root;
            context.SaveChanges();
            return original;
         }
         catch (Exception e) {
            throw ExceptionsToValidations.ExceptionsToValidations.WrapOracleExceptionsWithError(e);
         }
      }

      public void Delete(long id) {
         try {
            var original = context.AnnotationTypes.Find(id);
            context.AnnotationTypes.Remove(original);
            context.SaveChanges();
         }
         catch (Exception e) {
            throw ExceptionsToValidations.ExceptionsToValidations.WrapOracleExceptionsWithError(e);
         }
      }
   }
}