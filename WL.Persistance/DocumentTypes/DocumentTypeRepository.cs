using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

    public DocumentType Update(DocumentType updatedDocumetType) {
      var original = Get(updatedDocumetType.DocumentTypeId);
      original.Name = updatedDocumetType.Name;
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