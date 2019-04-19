using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WL.Application.Interfaces.Persistance;
using WL.Domain;

namespace WL.Persistance.Documents {

  public class DocumentRepository : IDocumentRepository {
    readonly WLDbContext context;

    public DocumentRepository(WLDbContext context) {
      this.context = context;
    }

    public File CreateClassifiedDocument(Document document, string Issue, string fileName) {
      var file = new File {
        Document = document,
        Issue = Issue,
        Name = fileName
      };

      using (var transaction = context.Database.BeginTransaction()) {
        try {
          context.Documents.Add(document);

          context.Files.Add(file);

          context.SaveChanges();

          context.Database.CommitTransaction();
        } catch (Exception ex) {
          Console.WriteLine(ex);
          transaction.Rollback();
          throw;
        }
      }

      return file;
    }

    public Document Get(long id) => throw new NotImplementedException();

    public IQueryable<Document> GetAll() => throw new NotImplementedException();

    public Document Create(Document entity) => throw new NotImplementedException();

    public Document Update(Document entity) => throw new NotImplementedException();

    public void Delete(long id) => throw new NotImplementedException();
  }
}