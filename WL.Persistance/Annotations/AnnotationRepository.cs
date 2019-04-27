using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WL.Application.Annotations.Commands;
using WL.Application.Interfaces.Persistance;
using WL.Domain;

namespace WL.Persistance.Annotations {

  public class AnnotationRepository : IAnnotationRepository {
    readonly WLDbContext context;

    public AnnotationRepository(WLDbContext context) {
      this.context = context;
    }

    public Annotation Create(CreateAnnotationCommand cmd) {
      context.Documents.Find(cmd.FromDocumentId);
      throw new Exception();
    }

    public Annotation Get(long id) => throw new NotImplementedException();

    public IQueryable<Annotation> GetAll() => throw new NotImplementedException();

    public Annotation Create(Annotation entity) => throw new NotImplementedException();

    public Annotation Update(Annotation entity) => throw new NotImplementedException();

    public void Delete(long id) => throw new NotImplementedException();
  }
}