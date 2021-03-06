﻿using System.Linq;
using WL.Application.Common;
using WL.Application.Documents;
using WL.Application.Documents.Queries;
using WL.Domain;

namespace WL.Application.Interfaces.Persistance {

  public interface IDocumentRepository : IRepository<Document> {

    File CreateClassifiedDocument(Document document, string Issue, string fileName);

    IQueryable<AnnotatedDocument> Search(SearchDocumentsMessage msg);

    long? SearchCount(
      string wordsToSearch,
      long? entityId,
      long? documentTypeid,
      string number,
      long? publicationYear
    );

    IQueryable<AnnotatedDocument> SearchToAnnotate(SearchDocumentsMessage msg, string token);

    long? SearchCountToAnnotate(
      string wordsToSearch,
      long? entityId,
      long? documentTypeid,
      string number,
      long? publicationYear,
      string token
    );

    Document GetIncludingRelationsById(long id);

    PagedResult<Document> GetPageOfDocumentsWithoutFile(DocumentsWithoutFilePageMessage msg, string token);

    void DeleteFile(File file);

    File GetFileIfExist(long documentId);

    Document UpdateDocumentFile(long documentId, string fileName, string issue);
  }
}