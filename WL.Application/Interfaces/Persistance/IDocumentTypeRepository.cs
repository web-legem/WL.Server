using System.Linq;
using WL.Domain;

namespace WL.Application.Interfaces.Persistance {

  public interface IDocumentTypeRepository : IRepository<DocumentType> {

    IQueryable<DocumentType> GetAllByUser(string token);
  }
}