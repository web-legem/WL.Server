using System;
using System.Linq;
using WL.Application.Interfaces.Persistance;
using WL.Domain;
using static WL.Persistance.ExceptionsToValidations.ExceptionsToValidations;
using static WL.Persistance.Helpers.DbHelpers;

namespace WL.Persistance.AnnotationTypes {

  public class AnnotationTypeRepository : IAnnotationTypeRepository {
    readonly WLDbContext context;

    public AnnotationTypeRepository(WLDbContext context) {
      this.context = context;
    }

    public AnnotationType Get(long id) {
      try {
        return NullVerifier(() => context.AnnotationTypes.Find(id));
      } catch (Exception e) {
        throw WrapOracleException(e);
      }
    }

    public IQueryable<AnnotationType> GetAll() {
      try {
        return context.AnnotationTypes;
      } catch (Exception e) {
        throw WrapOracleException(e);
      }
    }

    public AnnotationType Create(AnnotationType annotationType) {
      try {
        context.AnnotationTypes.Add(annotationType);
        context.SaveChanges();
        return annotationType;
      } catch (Exception e) {
        throw WrapOracleException(e);
      }
    }

    public AnnotationType Update(AnnotationType updated) {
      try {
        var original = Get(updated.Id);
        original.Name = updated.Name;
        original.Root = updated.Root;
        context.SaveChanges();
        return original;
      } catch (Exception e) {
        throw WrapOracleException(e);
      }
    }

    public void Delete(long id) {
      try {
        var original = Get(id);
        context.AnnotationTypes.Remove(original);
        context.SaveChanges();
      } catch (Exception e) {
        throw WrapOracleException(e);
      }
    }
  }
}