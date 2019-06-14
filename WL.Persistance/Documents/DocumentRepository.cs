using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using WL.Application.Common;
using WL.Application.Documents;
using WL.Application.Documents.Queries;
using WL.Application.Interfaces.Persistance;
using WL.Domain;
using static WL.Persistance.ExceptionsToValidations.ExceptionsToValidations;
using static WL.Persistance.Helpers.DbHelpers;

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
          var oldDocument = context.Documents.Include(d => d.File).Where(d => d.DocumentTypeId == document.DocumentTypeId
            && d.EntityId == document.EntityId
            && d.Number == document.Number
            && d.PublicationYear == d.PublicationYear
          ).FirstOrDefault();

          if (oldDocument != null) { // exist
            if (oldDocument.File == null) { // Has no file
              file.Document = oldDocument;
            } else { // exist with file
              throw new FormFieldError(FormFieldError.uniqueConstraint);
            }
          } else {
            context.Documents.Add(document);
            context.SaveChanges();
          }

          context.Files.Add(file);

          context.SaveChanges();

          context.Database.CommitTransaction();
        } catch (Exception ex) {
          Console.WriteLine(ex);
          transaction.Rollback();
          throw WrapOracleException(ex);
        }
      }

      return file;
    }

    public IQueryable<AnnotatedDocument> SearchToAnnotate(
      SearchDocumentsMessage msg,
      string token
      ) {
      var pagePrm = new OracleParameter(
        "page", OracleDbType.Int64, System.Data.ParameterDirection.Input) {
        Value = msg.Page.HasValue ? msg.Page.Value : (object)null
      };

      var pageSizePrm = new OracleParameter(
        "page_size", OracleDbType.Int64, System.Data.ParameterDirection.Input);
      pageSizePrm.Value = msg.PageSize.HasValue ? msg.PageSize.Value : (object)null;

      msg.WordsToSearch = msg.WordsToSearch ?? "";
      var wordsToSearchOracleFormatForMultipleWords = Regex.Replace(msg.WordsToSearch, @"\s+", " ");
      wordsToSearchOracleFormatForMultipleWords = wordsToSearchOracleFormatForMultipleWords.Trim();
      wordsToSearchOracleFormatForMultipleWords = Regex.Replace(wordsToSearchOracleFormatForMultipleWords, @"\s+", ",");

      var wordsToSearchPrm = new OracleParameter(
        "words_to_search", OracleDbType.Varchar2, System.Data.ParameterDirection.Input) {
        Value = wordsToSearchOracleFormatForMultipleWords
      };

      var entity = context.Credentials
        .Where(c => c.Token == token)
        .Select(c => c.User.Entity);

      OracleParameter entityIdPrm;
      var userEntity = entity.FirstOrDefault();

      if (userEntity == null) {
        entityIdPrm = new OracleParameter(
          "entity", OracleDbType.Int64, System.Data.ParameterDirection.Input) {
          Value = msg.EntityId.HasValue ? msg.EntityId.Value : (object)null
        };
      } else {
        entityIdPrm = new OracleParameter(
          "entity", OracleDbType.Int64, System.Data.ParameterDirection.Input) {
          Value = userEntity.Id
        };
      }

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
        .FromSql("SELECT * FROM TABLE(search_dt(:page, :page_size, :words_to_search, :entity, :document_type, :doc_number, :pub_year, :order_by, :descend))",
          pagePrm,
          pageSizePrm,
          wordsToSearchPrm,
          entityIdPrm,
          documentTypeIdPrm,
          numberPrm,
          publicationYearPrm,
          orderByPrm,
          descendPrm
          ).Select(d =>
            new AnnotatedDocument {
              Id = d.Id,
              DocumentTypeId = d.DocumentTypeId,
              EntityId = d.EntityId,
              Number = d.Number,
              PublicationDate = d.PublicationDate,
              PublicationYear = d.PublicationYear,
              IsAnnotated = context.Annotations.Where(a => a.To.Id == d.Id).Any(),
              FileId = d.File.DocumentId,
              FileName = d.File.Name,
              Issue = d.File.Issue
            }
          );
    }

    public IQueryable<AnnotatedDocument> Search(
      SearchDocumentsMessage msg
      ) {
      var pagePrm = new OracleParameter(
        "page", OracleDbType.Int64, System.Data.ParameterDirection.Input) {
        Value = msg.Page.HasValue ? msg.Page.Value : (object)null
      };

      var pageSizePrm = new OracleParameter(
        "page_size", OracleDbType.Int64, System.Data.ParameterDirection.Input);
      pageSizePrm.Value = msg.PageSize.HasValue ? msg.PageSize.Value : (object)null;

      msg.WordsToSearch = msg.WordsToSearch ?? "";
      var wordsToSearchOracleFormatForMultipleWords = Regex.Replace(msg.WordsToSearch, @"\s+", " ");
      wordsToSearchOracleFormatForMultipleWords = wordsToSearchOracleFormatForMultipleWords.Trim();
      wordsToSearchOracleFormatForMultipleWords = Regex.Replace(wordsToSearchOracleFormatForMultipleWords, @"\s+", ",");

      var wordsToSearchPrm = new OracleParameter(
        "words_to_search", OracleDbType.Varchar2, System.Data.ParameterDirection.Input) {
        Value = wordsToSearchOracleFormatForMultipleWords
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
        .FromSql("SELECT * FROM TABLE(search_dt(:page, :page_size, :words_to_search, :entity, :document_type, :doc_number, :pub_year, :order_by, :descend))",
          pagePrm,
          pageSizePrm,
          wordsToSearchPrm,
          entityIdPrm,
          documentTypeIdPrm,
          numberPrm,
          publicationYearPrm,
          orderByPrm,
          descendPrm
          ).Select(d =>
            new AnnotatedDocument {
              Id = d.Id,
              DocumentTypeId = d.DocumentTypeId,
              EntityId = d.EntityId,
              Number = d.Number,
              PublicationDate = d.PublicationDate,
              PublicationYear = d.PublicationYear,
              IsAnnotated = context.Annotations.Where(a => a.To.Id == d.Id).Any(),
              FileId = d.File.DocumentId,
              FileName = d.File.Name,
              Issue = d.File.Issue
            }
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

      wordsToSearch = wordsToSearch ?? "";
      var wordsToSearchOracleFormatForMultipleWords = Regex.Replace(wordsToSearch, @"\s+", " ");
      wordsToSearchOracleFormatForMultipleWords = wordsToSearchOracleFormatForMultipleWords.Trim();
      wordsToSearchOracleFormatForMultipleWords = Regex.Replace(wordsToSearchOracleFormatForMultipleWords, @"\s+", ",");

      var words = new OracleParameter("words_to_search", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
      words.Value = wordsToSearchOracleFormatForMultipleWords;

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

    public long? SearchCountToAnnotate(
      string wordsToSearch,
      long? entityId,
      long? documentTypeId,
      string number,
      long? publicationYear,
      string token) {
      var res = new OracleParameter {
        Direction = System.Data.ParameterDirection.ReturnValue,
        OracleDbType = OracleDbType.Int64
      };

      wordsToSearch = wordsToSearch ?? "";
      var wordsToSearchOracleFormatForMultipleWords = Regex.Replace(wordsToSearch, @"\s+", " ");
      wordsToSearchOracleFormatForMultipleWords = wordsToSearchOracleFormatForMultipleWords.Trim();
      wordsToSearchOracleFormatForMultipleWords = Regex.Replace(wordsToSearchOracleFormatForMultipleWords, @"\s+", ",");

      var words = new OracleParameter("words_to_search", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
      words.Value = wordsToSearchOracleFormatForMultipleWords;

      var entity = context.Credentials
        .Where(c => c.Token == token)
        .Select(c => c.User.Entity);

      OracleParameter entityIdPrm;
      var userEntity = entity.FirstOrDefault();

      if (userEntity == null) {
        entityIdPrm = new OracleParameter(
          "entity", OracleDbType.Int64, System.Data.ParameterDirection.Input) {
          Value = entityId.HasValue ? entityId.Value : (object)null
        };
      } else {
        entityIdPrm = new OracleParameter(
          "entity", OracleDbType.Int64, System.Data.ParameterDirection.Input) {
          Value = userEntity.Id
        };
      }

      var documentType = new OracleParameter("document_type", OracleDbType.Int64, System.Data.ParameterDirection.Input);
      documentType.Value = documentTypeId.HasValue ? documentTypeId.Value : (object)null;

      var docNumber = new OracleParameter("doc_number", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
      docNumber.Value = number;

      var pubYear = new OracleParameter("pub_year", OracleDbType.Int64, System.Data.ParameterDirection.Input);
      pubYear.Value = publicationYear.HasValue ? publicationYear.Value : (object)null;

      using (var connection = context.Database.GetDbConnection() as OracleConnection) {
        connection.Open();
        using (var command = new OracleCommand("SEARCH_COUNT", connection)) {
          command.CommandType = System.Data.CommandType.StoredProcedure;
          command.BindByName = true;
          command.Parameters.Add(res);
          command.Parameters.Add(words);
          command.Parameters.Add(entityIdPrm);
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
      try {
        return context.Documents
          .Include(d => d.File)
          .FirstOrDefault(d => d.Id == id);
      } catch (Exception e) {
        throw WrapOracleException(e);
      }
    }

    public PagedResult<Document> GetPageOfDocumentsWithoutFile(DocumentsWithoutFilePageMessage msg, string token) {
      var page = msg.Page ?? 1;
      var pageSize = msg.PageSize ?? 10;
      var desc = msg.Descend ?? false;
      var skip = (int)((page - 1) * pageSize);

      var entity = context.Credentials
        .Where(c => c.Token == token)
        .Select(c => c.User.Entity);

      var documentsWithoutFile = context.Documents
        .Include(x => x.File)
        .Where(x => x.File == null);

      documentsWithoutFile = msg.DocumentTypeId.HasValue
        ? (msg.DocumentTypeId.Value > 0
          ? documentsWithoutFile.Where(d => d.DocumentTypeId == msg.DocumentTypeId.Value)
          : documentsWithoutFile)
        : documentsWithoutFile;

      var entityDomain = entity.FirstOrDefault();
      if (entityDomain == null) {
        documentsWithoutFile = msg.EntityId.HasValue
          ? (msg.EntityId.Value > 0
            ? documentsWithoutFile.Where(d => d.EntityId == msg.EntityId.Value)
            : documentsWithoutFile)
          : documentsWithoutFile;
      } else {
        documentsWithoutFile = documentsWithoutFile.Where(d => d.EntityId == entityDomain.Id);
      }

      documentsWithoutFile = msg.Number != null
        ? documentsWithoutFile.Where(d => d.Number == msg.Number)
        : documentsWithoutFile;

      documentsWithoutFile = msg.PublicationYear.HasValue
        ? (msg.PublicationYear.Value > 0
          ? documentsWithoutFile.Where(d => d.PublicationYear == msg.PublicationYear.Value)
          : documentsWithoutFile)
        : documentsWithoutFile;

      IQueryable<Document> pagedQuery;

      var keySelector = GetOrderProperty(msg.OrderBy);
      if (desc) {
        pagedQuery = documentsWithoutFile.OrderByDescending(keySelector);
      } else {
        pagedQuery = documentsWithoutFile.OrderBy(keySelector);
      }

      return new PagedResult<Document> {
        Count = documentsWithoutFile.Count(),
        Page = pagedQuery.Skip(skip).Take((int)pageSize).AsQueryable()
      };
    }

    public void Delete(long id) {
      var document = context.Documents.FirstOrDefault(d => d.Id == id);
      if (document != null) {
        context.Documents.Remove(document);
        context.SaveChanges();
      }
    }

    private Expression<Func<Document, object>> GetOrderProperty(string orderBy) {
      switch (orderBy) {
        case "ENTIDAD":
          return (Document d) => d.Entity.Name;

        case "TIPO_DOCUMENTO":
          return (Document d) => d.DocumentType.Name;

        case "NUMERO":
          return (Document d) => d.Number;

        case "ANIO_PUBLICACION":
          return (Document d) => d.PublicationYear;

        default:
          return (Document d) => d.DocumentType.Name;
      }
    }

    public void DeleteFile(File file) {
      context.Files.Remove(file);
      context.SaveChanges();
    }

    public Document UpdateDocumentFile(long documentId, string fileName, string issue) {
      try {
        var oldFile = context.Files.FirstOrDefault(f => f.DocumentId == documentId);
        if (oldFile != null) {
          context.Files.Remove(oldFile);
          context.SaveChanges();
        }
        var file = new File { DocumentId = documentId, Name = fileName, Issue = issue };
        context.Files.Add(file);
        context.SaveChanges();
        return NullVerifier(() => context.Documents
          .Include(d => d.File)
          .FirstOrDefault(d => d.Id == documentId));
      } catch (Exception e) {
        throw WrapOracleException(e);
      }
    }

    public bool IsAnnotated(long documentId) {
      return context.Annotations
        .Where(a => a.To.Id == documentId)
        .Any();
    }

    public File GetFileIfExist(long documentId) {
      return context.Files.FirstOrDefault(f => f.DocumentId == documentId);
    }

    public Document GetIncludingRelationsById(long id) {
         Document document = context.Documents       
            .FirstOrDefault(x => x.Id == id);
         DocumentType documentType = context.DocumentTypes
            .FirstOrDefault(x => x.Id == document.DocumentTypeId);
         document.DocumentType = documentType;
         return document;
    }

    public IQueryable<Document> GetAll() => throw new NotImplementedException();

    public Document Create(Document entity) => throw new NotImplementedException();

    public Document Update(Document entity) => throw new NotImplementedException();
  }
}