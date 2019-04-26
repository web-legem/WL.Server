using System.Linq;

using WL.Application.Interfaces.Persistance;
using WL.Domain;

namespace WL.Persistance.DocumentTypes {

  public class DocumentTypeRepository : IDocumentTypeRepository {
    readonly WLDbContext context;

    public DocumentTypeRepository(WLDbContext context) {
      this.context = context;
    }

    public DocumentType Get(long id) {
      return context.DocumentTypes.Find(id);
    }

    public IQueryable<DocumentType> GetAll() {
      return context.DocumentTypes;
    }

    public DocumentType Create(DocumentType documentType) {
      context.DocumentTypes.Add(documentType);
      context.SaveChanges();
      return documentType;
    }

    public DocumentType Update(DocumentType updated) {
      var original = Get(updated.Id);
      original.Name = updated.Name;
      context.SaveChanges();
      return original;
    }

    public void Delete(long id) {
      var original = Get(id);
      context.DocumentTypes.Remove(original);
      context.SaveChanges();
    }
  }
}