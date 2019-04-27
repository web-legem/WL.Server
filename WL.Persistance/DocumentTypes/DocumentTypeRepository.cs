﻿using System;
using System.Linq;
using WL.Application.Interfaces.Persistance;
using WL.Domain;
using static WL.Persistance.ExceptionsToValidations.ExceptionsToValidations;
using static WL.Persistance.Helpers.DbHelpers;

namespace WL.Persistance.DocumentTypes {

   public class DocumentTypeRepository : IDocumentTypeRepository {
      readonly WLDbContext context;

      public DocumentTypeRepository(WLDbContext context) {
         this.context = context;
      }

      public DocumentType Get(long id) {
         try {
            return NullVerifier(() => context.DocumentTypes.Find(id));
         }
         catch (Exception e) {
            throw WrapOracleException(e);
         }
      }

      public IQueryable<DocumentType> GetAll() {
         try {
            return context.DocumentTypes;
         }
         catch (Exception e) {
            throw WrapOracleException(e);
         }
      }

      public DocumentType Create(DocumentType documentType) {
         try {
            context.DocumentTypes.Add(documentType);
            context.SaveChanges();
            return documentType;
         }
         catch (Exception e) {
            throw WrapOracleException(e);
         }
      }

      public DocumentType Update(DocumentType updated) {
         try {
            var original = Get(updated.Id);
            original.Name = updated.Name;
            context.SaveChanges();
            return original;
         }
         catch (Exception e) {
            throw WrapOracleException(e);
         }
      }

      public void Delete(long id) {
         try {
            var original = Get(id);
            context.DocumentTypes.Remove(original);
            context.SaveChanges();
         }
         catch (Exception e) {
            throw WrapOracleException(e);
         }
      }
   }
}