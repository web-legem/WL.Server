using WL.Domain;

namespace WL.Application.Interfaces.Persistance {

  public interface IDocumentRepository : IRepository<Document> {

    File CreateClassifiedDocument(Document document, string Issue, string fileName);
  }
}