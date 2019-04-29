﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WL.Application.Annotations.Commands;
using WL.Application.Common;
using WL.Application.Interfaces.Persistance;
using WL.Domain;
using static WL.Persistance.ExceptionsToValidations.ExceptionsToValidations;
using static WL.Persistance.Helpers.DbHelpers;

namespace WL.Persistance.Annotations {

  public class AnnotationRepository : IAnnotationRepository {
    readonly WLDbContext context;

    public AnnotationRepository(WLDbContext context) {
      this.context = context;
    }

    public Annotation Create(CreateAnnotationCommand cmd) {
      try {
        using (var transaction = context.Database.BeginTransaction()) {
          var from = NullVerifier(() => context.Documents
            .Include(d => d.File)
            .FirstOrDefault(d => d.Id == cmd.FromDocumentId));

          if (from.File == null)
            throw new FormFieldError(FormFieldError.notFound, "file");

          var to = context.Documents.FirstOrDefault(d =>
            d.DocumentTypeId == cmd.ToDocumentTypeId
            && d.EntityId == cmd.ToEntityId
            && d.Number == cmd.ToNumber
            && d.PublicationYear == cmd.ToPublicationYear
            );

          var annotation = new Annotation {
            AnnotationTypeId = cmd.AnnotationTypeId,
            Description = cmd.Description,
            FromDocumentId = from.Id
          };

          if (to == null) {
            to = new Document {
              DocumentTypeId = cmd.ToDocumentTypeId,
              EntityId = cmd.ToEntityId,
              Number = cmd.ToNumber,
              PublicationYear = cmd.ToPublicationYear
            };
            context.Documents.Add(to);
            context.SaveChanges();
          }

          annotation.ToDocumentId = to.Id;

          context.Annotations.Add(annotation);
          context.SaveChanges();
          context.Database.CommitTransaction();

          return annotation;
        }
      } catch (Exception e) {
        context.Database.RollbackTransaction();
        throw WrapOracleException(e);
      }
    }

    public Annotation Get(long id) => throw new NotImplementedException();

    public IQueryable<Annotation> GetAll() => throw new NotImplementedException();

    public Annotation Create(Annotation entity) => throw new NotImplementedException();

    public Annotation Update(Annotation entity) => throw new NotImplementedException();

    public void Delete(long id) {
      try {
        context.Database.BeginTransaction();
        var original = NullVerifier(() => context.Annotations
          .Include(a => a.To)
          .ThenInclude(d => d.File)
          .FirstOrDefault(a => a.Id == id));

        var documentDestiny = original.To;
        context.Annotations.Remove(original);
        context.SaveChanges();

        if (HasNoFile(documentDestiny) && HasNoAnnotationsInWhichIsDestiny(documentDestiny)) {
          context.Documents.Remove(documentDestiny);
          context.SaveChanges();
        }
        context.Database.CommitTransaction();
      } catch (Exception e) {
        context.Database.RollbackTransaction();
        throw WrapOracleException(e);
      }
    }

    private bool HasNoFile(Document document)
      => !HasFile(document);

    private bool HasFile(Document documentDestiny)
      => context.Files.Find(documentDestiny.Id) != null;

    private bool HasNoAnnotationsInWhichIsDestiny(Document documentDestiny)
      => GetRemmainingAnnotationsCount(documentDestiny) == 0;

    private int GetRemmainingAnnotationsCount(Document documentDestiny)
      => context.Annotations
        .AsNoTracking()
        .Where(a => a.ToDocumentId == documentDestiny.Id)
        .Count();

    public IQueryable<Annotation> GetDocumentAnnotations(long documentId) {
      return context.Annotations
        .Include(a => a.To)
        .Include(a => a.From)
        .Where(a => a.FromDocumentId == documentId || a.ToDocumentId == documentId)
        .AsNoTracking();
    }
  }
}