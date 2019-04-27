using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using WL.Application.Interfaces.Persistance;
using WL.Domain.User;

using static WL.Persistance.ExceptionsToValidations.ExceptionsToValidations;
using static WL.Persistance.Helpers.DbHelpers;

namespace WL.Persistance.Roles {

   public class RoleRepository : IRoleRepository {
      readonly WLDbContext context;

      public RoleRepository(WLDbContext context) {
         this.context = context;
      }

      public Role Get(string token) {
         try {
            var credential = NullVerifier(() => context.Credentials
                .Include(c => c.User)
                .First(c => c.Token == token));
            return NullVerifier(() => context.Roles.First(r => credential.User.RoleId == r.Id));
         }
         catch (Exception e) {
            throw WrapOracleException(e);
         }
      }

      public Role Get(long id) {
         try {
            return NullVerifier(() => context.Roles.Find(id));
         }
         catch (Exception e) {
            throw WrapOracleException(e);
         }
      }

      public IQueryable<Role> GetAll() {
         try {
         }
         catch (Exception e) {
            throw WrapOracleException(e);
         }
         return context.Roles;
      }

      public Role Create(Role entity) {
         try {
            context.Roles.Add(entity);
            context.SaveChanges();
            return entity;
         }
         catch (Exception e) {
            throw WrapOracleException(e);
         }
      }

      public Role Update(Role entity) {
         try {
            var original = Get(entity.Id);
            original.Name = entity.Name;
            original.CreateDocuments = entity.CreateDocuments;
            original.DeleteDocuments = entity.DeleteDocuments;
            original.ConfigSystem = entity.ConfigSystem;
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
            context.Roles.Remove(original);
            context.SaveChanges();
         }
         catch (Exception e) {
            throw WrapOracleException(e);
         }
      }
   }
}