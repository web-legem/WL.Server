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
      return context.AnnotationTypes.Find(id);
    }

    public IQueryable<AnnotationType> GetAll() {
      return context.AnnotationTypes;
    }

    public AnnotationType Create(AnnotationType annotationType) {
      context.AnnotationTypes.Add(annotationType);
      context.SaveChanges();
      return annotationType;
    }

    public AnnotationType Update(AnnotationType updated) {
      var original = context.AnnotationTypes.Find(updated.Id);
      original.Name = updated.Name;
      original.Root = updated.Root;
      context.SaveChanges();
      return original;
    }

    public void Delete(long id) {
      var original = context.AnnotationTypes.Find(id);
      context.AnnotationTypes.Remove(original);
      context.SaveChanges();
    }
  }
}