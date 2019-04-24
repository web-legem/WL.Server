using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Linq;
using WL.Application.Documents.Queries;
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

    public IQueryable<Document> Search(
      SearchDocumentsMessage msg
      ) {
      var pagePrm = new OracleParameter(
        "page", OracleDbType.Int64, System.Data.ParameterDirection.Input) {
        Value = msg.Page.HasValue ? msg.Page.Value : (object)null
      };

      var pageSizePrm = new OracleParameter(
        "page_size", OracleDbType.Int64, System.Data.ParameterDirection.Input);
      pageSizePrm.Value = msg.PageSize.HasValue ? msg.PageSize.Value : (object)null;

      var wordsToSearchPrm = new OracleParameter(
        "words_to_search", OracleDbType.Varchar2, System.Data.ParameterDirection.Input) {
        Value = msg.WordsToSearch
      };

      var entityIdPrm = new OracleParameter(
        "entity", OracleDbType.Int64, System.Data.ParameterDirection.Input) {
        Value = msg.EntityId.HasValue ? msg.EntityId.Value : (object)null
      };

      var documentTypeIdPrm = new OracleParameter(
        "document_type", OracleDbType.Int64, System.Data.ParameterDirection.Input) {
        Value = msg.DocumentTypeId.HasValue ? msg.DocumentTypeId.Value : (object)null
      };

      var numberPrm = new OracleParameter(
        "doc_number", OracleDbType.Varchar2, System.Data.ParameterDirection.Input) {
        Value = msg.Number
      };

      var publicationYearPrm = new OracleParameter(
        "pub_year", OracleDbType.Int64, System.Data.ParameterDirection.Input) {
        Value = msg.Year.HasValue ? msg.Year.Value : (object)null
      };

      var orderByPrm = new OracleParameter(
        "order_by", OracleDbType.Varchar2, System.Data.ParameterDirection.Input) {
        Value = msg.OrderBy
      };

      var descendPrm = new OracleParameter(
        "descend", OracleDbType.Boolean, System.Data.ParameterDirection.Input) {
        Value = msg.Descend
      };

      return context.Documents
        .Include(d => d.File)

        .FromSql("SELECT * FROM search_dt(:page, :page_size, :words_to_search, :entity, :document_type, :doc_number, :pub_year, :order_by, :descend)",
          pagePrm,
          pageSizePrm,
          wordsToSearchPrm,
          entityIdPrm,
          documentTypeIdPrm,
          numberPrm,
          publicationYearPrm,
          orderByPrm,
          descendPrm
          );
    }

    public long? SearchCount(
      string wordsToSearch,
      long? entityId,
      long? documentTypeId,
      string number,
      long? publicationYear) {
      var res = new OracleParameter {
        Direction = System.Data.ParameterDirection.ReturnValue,
        OracleDbType = OracleDbType.Int64
      };

      var words = new OracleParameter("words_to_search", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
      words.Value = wordsToSearch;

      var entity = new OracleParameter("entity", OracleDbType.Int64, System.Data.ParameterDirection.Input);
      entity.Value = entityId.HasValue ? entityId.Value : (object)null;

      var documentType = new OracleParameter("document_type", OracleDbType.Int64, System.Data.ParameterDirection.Input);
      documentType.Value = documentTypeId.HasValue ? documentTypeId.Value : (object)null;

      var docNumber = new OracleParameter("doc_number", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
      docNumber.Value = number;

      var pubYear = new OracleParameter("pub_year", OracleDbType.Int64, System.Data.ParameterDirection.Input);
      pubYear.Value = publicationYear.HasValue ? publicationYear.Value : (object)null;

      using (var connection = context.Database.GetDbConnection() as OracleConnection) {
        connection.Open();
        // TODO - probar si es posible retirar el namespace WEBL de la siguiente instrucción
        using (var command = new OracleCommand("SEARCH_COUNT", connection)) {
          command.CommandType = System.Data.CommandType.StoredProcedure;
          command.BindByName = true;
          command.Parameters.Add(res);
          command.Parameters.Add(words);
          command.Parameters.Add(entity);
          command.Parameters.Add(documentType);
          command.Parameters.Add(docNumber);
          command.Parameters.Add(pubYear);
          command.ExecuteNonQuery();

          if (long.TryParse(res.Value.ToString(), out var result))
            return result;
          return null;
        }
      }
    }

    public Document Get(long id) {
      return context.Documents
        .Include(d => d.File)
        .First(d => d.DocumentId == id);
    }

    public IQueryable<Document> GetAll() => throw new NotImplementedException();

    public Document Create(Document entity) => throw new NotImplementedException();

    public Document Update(Document entity) => throw new NotImplementedException();

    public void Delete(long id) => throw new NotImplementedException();
  }
}